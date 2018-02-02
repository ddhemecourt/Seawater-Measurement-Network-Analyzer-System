using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;

namespace Seawater_Measurement
{
    public partial class MLDataAnalysis : Form
    {

        int j;
        double range;

        public MLDataAnalysis(string rawDataFile, string dataPath, string resultName)
        {
            InitializeComponent();

            file.Text = rawDataFile;
            file.Refresh();

            resultPath.Text = dataPath;
            resultPath.Refresh();

            resultFileName.Text = resultName;
            resultFileName.Refresh();


        }

        private void button1_Click(object sender, EventArgs e)
        {

            var FD = new System.Windows.Forms.OpenFileDialog();
            if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileToOpen = FD.FileName;
                file.Text = fileToOpen;
                file.Refresh();

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                resultPath.Text = folderBrowserDialog1.SelectedPath;
            }

        }

        private void runButton_Click(object sender, EventArgs e)
        {
            matlabDataAnalysis(file.Text, sheetAnalysis.Text);
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void MLDataAnalysis_Load(object sender, EventArgs e)
        {

        }



        private void matlabDataAnalysis(string file, string sheetToAnalyze)
        {


            excel_doc doc = new excel_doc();



            Excel.Application excel = new Excel.Application();
            Excel.Workbooks books = excel.Workbooks;
            Excel.Workbook wb = books.Open(Filename: file, ReadOnly: false);

            var activationContext = Type.GetTypeFromProgID("matlab.application.single");
            var matlab = (MLApp.MLApp)Activator.CreateInstance(activationContext);

            string path = resultPath.Text;


            j = 1;
            while (wb.Sheets["Loaded Q"].Cells[j, 2].Value2 != null)
            {
                j++;
            }


            Excel.Range rngFirst = wb.Sheets["Loaded Q"].Cells[1, 2];
            string rangeFirst = rngFirst.get_AddressLocal(false, false, Excel.XlReferenceStyle.xlA1);

            Excel.Range rngSecond = wb.Sheets["Loaded Q"].Cells[j, 2];
            string rangeSecond = rngSecond.get_AddressLocal(false, false, Excel.XlReferenceStyle.xlA1);

            string range = rangeFirst + ":" + rangeSecond;
            Console.WriteLine("range = " + range);

            string excel_file = file;
            double num_avg = Convert.ToDouble(numberOfAvg.Value);

            //Bring file path to the location of the MATLAB script
            matlab.Execute(@path);
            matlab.PutWorkspaceData("file", "base", excel_file);
            matlab.PutWorkspaceData("range", "base", range);
            matlab.PutWorkspaceData("Q_Unloaded_Sheet", "base", "Unloaded Q");
            matlab.PutWorkspaceData("Q_Loaded_Sheet", "base", "Loaded Q");
            matlab.PutWorkspaceData("NA_Q_Sheet", "base", "Q vs. Time");
            matlab.PutWorkspaceData("NA_Center_Sheet", "base", "Center Freq.");
            matlab.PutWorkspaceData("SVD_Center_Sheet", "base", "SVD Center Frequency");
            matlab.PutWorkspaceData("num_avg", "base", num_avg);

            string execute = string.Format("[manual_means,manual_stdevs,sections,indices,a] = q_analysis(file,range,{0},num_avg)", sheetAnalysis.Text);

            Console.WriteLine(matlab.Execute("[manual_means,manual_stdevs,sections,indices,a] = q_analysis(file,range,Q_Loaded_Sheet,num_avg)"));

            object sections = getMLData(matlab)[2];
            int sectionNum = Convert.ToInt16(sections);


            //Creat the new excel result document
            doc.createAnalysisDoc(sectionNum, Convert.ToInt16(num_avg));
            doc.workbook1.SaveAs(resultPath.Text + resultFileName.Text);


            string[] worksheets = new string[6] { "Loaded Q", "Unloaded Q", "Q vs. Time", "Center Freq.", "Room Temp.", "Cavity Temp." };

            foreach (string sheet in worksheets)
            {
                Console.WriteLine(sheet);
                Excel.Worksheet worksheet = wb.Sheets[sheet];
                Excel.ChartObjects charObjects = (Excel.ChartObjects)worksheet.ChartObjects(Type.Missing);
                Excel.ChartObject chart = (Excel.ChartObject)charObjects.Item(1);
                Excel.Chart myChart = chart.Chart;
                Excel.Series series = myChart.SeriesCollection(1);
                series.MarkerBackgroundColor = (int)Excel.XlRgbColor.rgbBlue;
                series.MarkerForegroundColor = (int)Excel.XlRgbColor.rgbBlue;
            }





            //Obtain the workspace data returned from the MATLAB Script and fill the new excel file with that data
            object indices = getMLData(matlab)[3];
            IEnumerable enumerable = indices as IEnumerable;

            if (enumerable != null)
            {
                int i = 1;
                j = 1;
                int v = 0;
                int level = 0;
                int sample_count = Convert.ToUInt16(num_avg);

                List<double> qLoadedAvg = new List<double>();
                List<double> qUnloadedAvg = new List<double>();
                List<double> qNAAvg = new List<double>();
                List<double> resFreqNAAvg = new List<double>();
                List<double> resFreqSVDAvg = new List<double>();
                List<double> roomTAvg = new List<double>();
                List<double> cavityTAvg = new List<double>();

                foreach (object element in enumerable)
                {

                    level = Convert.ToUInt16((num_avg * 2 + 12) * (i - 1));

                    int index = Convert.ToUInt16(element);
                    Excel.Worksheet ws = wb.Sheets["Loaded Q"];
                    Console.WriteLine(ws.Cells[element, 1].text);
                    string value = Convert.ToString(ws.Cells[element, 1].text);
                    doc.addData(level + 10 + sample_count + v, 2, value, "analysis");

                    Excel.ChartObjects charObjects = (Excel.ChartObjects)ws.ChartObjects(Type.Missing);
                    Excel.ChartObject chart = (Excel.ChartObject)charObjects.Item(1);
                    Excel.Chart myChart = chart.Chart;
                    Excel.Series series = myChart.SeriesCollection(1);
                    series.Points(element).MarkerForeGroundColor = (int)Excel.XlRgbColor.rgbRed;
                    series.Points(element).MarkerBackGroundColor = (int)Excel.XlRgbColor.rgbRed;

                    value = Convert.ToString(ws.Cells[element, 2].Value2);
                    doc.addData(level + 10 + sample_count + v, 3, value, "analysis");
                    qLoadedAvg.Add(Convert.ToDouble(value));

                    ws = wb.Sheets["Unloaded Q"];
                    value = Convert.ToString(ws.Cells[element, 2].Value2);
                    doc.addData(level + 10 + sample_count + v, 4, value, "analysis");
                    qUnloadedAvg.Add(Convert.ToDouble(value));

                    charObjects = (Excel.ChartObjects)ws.ChartObjects(Type.Missing);
                    chart = (Excel.ChartObject)charObjects.Item(1);
                    myChart = chart.Chart;
                    series = myChart.SeriesCollection(1);
                    series.Points(element).MarkerForeGroundColor = (int)Excel.XlRgbColor.rgbRed;
                    series.Points(element).MarkerBackGroundColor = (int)Excel.XlRgbColor.rgbRed;

                    ws = wb.Sheets["Q vs. Time"];
                    value = Convert.ToString(ws.Cells[element, 2].Value2);
                    doc.addData(level + 10 + sample_count + v, 5, value, "analysis");
                    qNAAvg.Add(Convert.ToDouble(value));

                    charObjects = (Excel.ChartObjects)ws.ChartObjects(Type.Missing);
                    chart = (Excel.ChartObject)charObjects.Item(1);
                    myChart = chart.Chart;
                    series = myChart.SeriesCollection(1);
                    series.Points(element).MarkerForeGroundColor = (int)Excel.XlRgbColor.rgbRed;
                    series.Points(element).MarkerBackGroundColor = (int)Excel.XlRgbColor.rgbRed;


                    ws = wb.Sheets["Center Freq."];
                    value = Convert.ToString(ws.Cells[element, 2].Value2);
                    doc.addData(level + 10 + sample_count + v, 6, value, "analysis");
                    resFreqNAAvg.Add(Convert.ToDouble(value));

                    charObjects = (Excel.ChartObjects)ws.ChartObjects(Type.Missing);
                    chart = (Excel.ChartObject)charObjects.Item(1);
                    myChart = chart.Chart;
                    series = myChart.SeriesCollection(1);
                    series.Points(element).MarkerForeGroundColor = (int)Excel.XlRgbColor.rgbRed;
                    series.Points(element).MarkerBackGroundColor = (int)Excel.XlRgbColor.rgbRed;



                    ws = wb.Sheets["SVD Center Frequency"];
                    value = Convert.ToString(ws.Cells[element, 2].Value2);
                    doc.addData(level + 10 + sample_count + v, 7, value, "analysis");
                    resFreqSVDAvg.Add(Convert.ToDouble(value));

                    charObjects = (Excel.ChartObjects)ws.ChartObjects(Type.Missing);
                    chart = (Excel.ChartObject)charObjects.Item(1);
                    myChart = chart.Chart;
                    series.Points(element).MarkerForeGroundColor = (int)Excel.XlRgbColor.rgbRed;
                    series.Points(element).MarkerBackGroundColor = (int)Excel.XlRgbColor.rgbRed;



                    ws = wb.Sheets["Room Temp."];
                    value = Convert.ToString(ws.Cells[element, 2].Value2);
                    doc.addData(level + 10 + sample_count + v, 8, value, "analysis");
                    roomTAvg.Add(Convert.ToDouble(value));

                    charObjects = (Excel.ChartObjects)ws.ChartObjects(Type.Missing);
                    chart = (Excel.ChartObject)charObjects.Item(1);
                    myChart = chart.Chart;
                    series = myChart.SeriesCollection(1);
                    series.Points(element).MarkerForeGroundColor = (int)Excel.XlRgbColor.rgbRed;
                    series.Points(element).MarkerBackGroundColor = (int)Excel.XlRgbColor.rgbRed;

                    ws = wb.Sheets["Cavity Temp."];
                    value = Convert.ToString(ws.Cells[element, 2].Value2);
                    doc.addData(level + 10 + sample_count + v, 9, value, "analysis");
                    cavityTAvg.Add(Convert.ToDouble(value));

                    charObjects = (Excel.ChartObjects)ws.ChartObjects(Type.Missing);
                    chart = (Excel.ChartObject)charObjects.Item(1);
                    myChart = chart.Chart;
                    series = myChart.SeriesCollection(1);
                    series.Points(element).MarkerForeGroundColor = (int)Excel.XlRgbColor.rgbRed;
                    series.Points(element).MarkerBackGroundColor = (int)Excel.XlRgbColor.rgbRed;


                    v++;
                    j++;

                    if ((j - 1) % (num_avg) == 0)
                    {

                        double avg = qLoadedAvg.Sum() / num_avg;
                        double sd = qLoadedAvg.Select(x => (x - avg) * (x - avg)).Sum();
                        sd = Math.Sqrt(sd / qLoadedAvg.Count);
                        Console.WriteLine("AVG = " + Convert.ToString(avg));
                        doc.addData(level + 10 + (2 * sample_count), 3, Convert.ToString(avg), "analysis");
                        doc.addData(level + 11 + (2 * sample_count), 3, Convert.ToString(sd), "analysis");
                        qLoadedAvg.Clear();

                        avg = qUnloadedAvg.Sum() / num_avg;
                        sd = qUnloadedAvg.Select(x => (x - avg) * (x - avg)).Sum();
                        sd = Math.Sqrt(sd / qUnloadedAvg.Count);
                        doc.addData(level + 10 + (2 * sample_count), 4, Convert.ToString(Convert.ToString(avg)), "analysis");
                        doc.addData(level + 11 + (2 * sample_count), 4, Convert.ToString(Convert.ToString(sd)), "analysis");
                        qUnloadedAvg.Clear();

                        avg = qNAAvg.Sum() / num_avg;
                        sd = qNAAvg.Select(x => (x - avg) * (x - avg)).Sum();
                        sd = Math.Sqrt(sd / qNAAvg.Count);
                        doc.addData(level + 10 + (2 * sample_count), 5, Convert.ToString(Convert.ToString(avg)), "analysis");
                        doc.addData(level + 11 + (2 * sample_count), 5, Convert.ToString(Convert.ToString(sd)), "analysis");
                        qNAAvg.Clear();

                        avg = resFreqNAAvg.Sum() / num_avg;
                        sd = resFreqNAAvg.Select(x => (x - avg) * (x - avg)).Sum();
                        sd = Math.Sqrt(sd / resFreqNAAvg.Count);
                        doc.addData(level + 10 + (2 * sample_count), 6, Convert.ToString(Convert.ToString(avg)), "analysis");
                        doc.addData(level + 11 + (2 * sample_count), 6, Convert.ToString(Convert.ToString(sd)), "analysis");
                        resFreqNAAvg.Clear();

                        avg = resFreqSVDAvg.Sum() / num_avg;
                        sd = resFreqSVDAvg.Select(x => (x - avg) * (x - avg)).Sum();
                        sd = Math.Sqrt(sd / resFreqSVDAvg.Count);
                        doc.addData(level + 10 + (2 * sample_count), 7, Convert.ToString(Convert.ToString(avg)), "analysis");
                        doc.addData(level + 11 + (2 * sample_count), 7, Convert.ToString(Convert.ToString(sd)), "analysis");
                        resFreqSVDAvg.Clear();

                        avg = roomTAvg.Sum() / num_avg;
                        sd = roomTAvg.Select(x => (x - avg) * (x - avg)).Sum();
                        sd = Math.Sqrt(sd / roomTAvg.Count);
                        doc.addData(level + 10 + (2 * sample_count), 8, Convert.ToString(Convert.ToString(avg)), "analysis");
                        doc.addData(level + 11 + (2 * sample_count), 8, Convert.ToString(Convert.ToString(sd)), "analysis");
                        roomTAvg.Clear();


                        avg = cavityTAvg.Sum() / num_avg;
                        sd = cavityTAvg.Select(x => (x - avg) * (x - avg)).Sum();
                        sd = Math.Sqrt(sd / cavityTAvg.Count);
                        doc.addData(level + 10 + (2 * sample_count), 9, Convert.ToString(Convert.ToString(avg)), "analysis");
                        doc.addData(level + 11 + (2 * sample_count), 9, Convert.ToString(Convert.ToString(sd)), "analysis");
                        cavityTAvg.Clear();


                        i++;
                        v = 0;
                    }

                }
            }






            object a = getMLData(matlab)[4];
            enumerable = a as IEnumerable;

            if (enumerable != null)
            {
                int i = 1;
                j = 1;
                int v = 0;
                int level = 0;

                List<double> qLoadedAvg = new List<double>();
                List<double> qUnloadedAvg = new List<double>();
                List<double> qNAAvg = new List<double>();
                List<double> resFreqNAAvg = new List<double>();
                List<double> resFreqSVDAvg = new List<double>();
                List<double> roomTAvg = new List<double>();
                List<double> cavityTAvg = new List<double>();

                foreach (object element in enumerable)
                {


                    int sample_count = Convert.ToUInt16(num_avg);
                    level = Convert.ToUInt16((num_avg * 2 + 12) * (i - 1));

                    int index = Convert.ToUInt16(element);
                    Excel.Worksheet ws = wb.Sheets["Loaded Q"];
                    Console.WriteLine(ws.Cells[element, 1].text);
                    string value = Convert.ToString(ws.Cells[element, 1].text);
                    doc.addData(level + 5 + v, 2, value, "analysis");

                    Excel.ChartObjects charObjects = (Excel.ChartObjects)ws.ChartObjects(Type.Missing);
                    Excel.ChartObject chart = (Excel.ChartObject)charObjects.Item(1);
                    Excel.Chart myChart = chart.Chart;
                    Excel.Series series = myChart.SeriesCollection(1);
                    series.Points(element).MarkerForeGroundColor = (int)Excel.XlRgbColor.rgbGreen;
                    series.Points(element).MarkerBackGroundColor = (int)Excel.XlRgbColor.rgbGreen;


                    value = Convert.ToString(ws.Cells[element, 2].Value2);
                    doc.addData(level + 5 + v, 3, value, "analysis");
                    qLoadedAvg.Add(Convert.ToDouble(value));

                    ws = wb.Sheets["Unloaded Q"];
                    value = Convert.ToString(ws.Cells[element, 2].Value2);
                    doc.addData(level + 5 + v, 4, value, "analysis");
                    qUnloadedAvg.Add(Convert.ToDouble(value));

                    charObjects = (Excel.ChartObjects)ws.ChartObjects(Type.Missing);
                    chart = (Excel.ChartObject)charObjects.Item(1);
                    myChart = chart.Chart;
                    series = myChart.SeriesCollection(1);
                    series.Points(element).MarkerForeGroundColor = (int)Excel.XlRgbColor.rgbGreen;
                    series.Points(element).MarkerBackGroundColor = (int)Excel.XlRgbColor.rgbGreen;


                    ws = wb.Sheets["Q vs. Time"];
                    value = Convert.ToString(ws.Cells[element, 2].Value2);
                    doc.addData(level + 5 + v, 5, value, "analysis");
                    qNAAvg.Add(Convert.ToDouble(value));

                    charObjects = (Excel.ChartObjects)ws.ChartObjects(Type.Missing);
                    chart = (Excel.ChartObject)charObjects.Item(1);
                    myChart = chart.Chart;
                    series = myChart.SeriesCollection(1);
                    series.Points(element).MarkerForeGroundColor = (int)Excel.XlRgbColor.rgbGreen;
                    series.Points(element).MarkerBackGroundColor = (int)Excel.XlRgbColor.rgbGreen;


                    ws = wb.Sheets["Center Freq."];
                    value = Convert.ToString(ws.Cells[element, 2].Value2);
                    doc.addData(level + 5 + v, 6, value, "analysis");
                    resFreqNAAvg.Add(Convert.ToDouble(value));

                    charObjects = (Excel.ChartObjects)ws.ChartObjects(Type.Missing);
                    chart = (Excel.ChartObject)charObjects.Item(1);
                    myChart = chart.Chart;
                    series = myChart.SeriesCollection(1);
                    series.Points(element).MarkerForeGroundColor = (int)Excel.XlRgbColor.rgbGreen;
                    series.Points(element).MarkerBackGroundColor = (int)Excel.XlRgbColor.rgbGreen;


                    ws = wb.Sheets["SVD Center Frequency"];
                    value = Convert.ToString(ws.Cells[element, 2].Value2);
                    doc.addData(level + 5 + v, 7, value, "analysis");
                    resFreqSVDAvg.Add(Convert.ToDouble(value));

                    charObjects = (Excel.ChartObjects)ws.ChartObjects(Type.Missing);
                    chart = (Excel.ChartObject)charObjects.Item(1);
                    myChart = chart.Chart;
                    series = myChart.SeriesCollection(1);
                    series.Points(element).MarkerForeGroundColor = (int)Excel.XlRgbColor.rgbGreen;
                    series.Points(element).MarkerBackGroundColor = (int)Excel.XlRgbColor.rgbGreen;


                    ws = wb.Sheets["Room Temp."];
                    value = Convert.ToString(ws.Cells[element, 2].Value2);
                    doc.addData(level + 5 + v, 8, value, "analysis");
                    roomTAvg.Add(Convert.ToDouble(value));

                    charObjects = (Excel.ChartObjects)ws.ChartObjects(Type.Missing);
                    chart = (Excel.ChartObject)charObjects.Item(1);
                    myChart = chart.Chart;
                    series = myChart.SeriesCollection(1);
                    series.Points(element).MarkerForeGroundColor = (int)Excel.XlRgbColor.rgbGreen;
                    series.Points(element).MarkerBackGroundColor = (int)Excel.XlRgbColor.rgbGreen;


                    ws = wb.Sheets["Cavity Temp."];
                    value = Convert.ToString(ws.Cells[element, 2].Value2);
                    doc.addData(level + 5 + v, 9, value, "analysis");
                    cavityTAvg.Add(Convert.ToDouble(value));

                    charObjects = (Excel.ChartObjects)ws.ChartObjects(Type.Missing);
                    chart = (Excel.ChartObject)charObjects.Item(1);
                    myChart = chart.Chart;
                    series = myChart.SeriesCollection(1);
                    series.Points(element).MarkerForeGroundColor = (int)Excel.XlRgbColor.rgbGreen;
                    series.Points(element).MarkerBackGroundColor = (int)Excel.XlRgbColor.rgbGreen;

                    v++;
                    j++;

                    if ((j - 1) % (num_avg) == 0)
                    {
                        double avg = qLoadedAvg.Sum() / num_avg;
                        double sd = qLoadedAvg.Select(x => (x - avg) * (x - avg)).Sum();
                        sd = Math.Sqrt(sd / qLoadedAvg.Count);
                        Console.WriteLine("AVG = " + Convert.ToString(avg));
                        doc.addData(level + 5 + sample_count, 3, Convert.ToString(avg), "analysis");
                        doc.addData(level + 6 + sample_count, 3, Convert.ToString(sd), "analysis");
                        qLoadedAvg.Clear();

                        avg = qUnloadedAvg.Sum() / num_avg;
                        sd = qUnloadedAvg.Select(x => (x - avg) * (x - avg)).Sum();
                        sd = Math.Sqrt(sd / qUnloadedAvg.Count);
                        doc.addData(level + 5 + sample_count, 4, Convert.ToString(Convert.ToString(avg)), "analysis");
                        doc.addData(level + 6 + sample_count, 4, Convert.ToString(Convert.ToString(sd)), "analysis");
                        qUnloadedAvg.Clear();

                        avg = qNAAvg.Sum() / num_avg;
                        sd = qNAAvg.Select(x => (x - avg) * (x - avg)).Sum();
                        sd = Math.Sqrt(sd / qNAAvg.Count);
                        doc.addData(level + 5 + sample_count, 5, Convert.ToString(Convert.ToString(avg)), "analysis");
                        doc.addData(level + 6 + sample_count, 5, Convert.ToString(Convert.ToString(sd)), "analysis");
                        qNAAvg.Clear();

                        avg = resFreqNAAvg.Sum() / num_avg;
                        sd = resFreqNAAvg.Select(x => (x - avg) * (x - avg)).Sum();
                        sd = Math.Sqrt(sd / resFreqNAAvg.Count);
                        doc.addData(level + 5 + sample_count, 6, Convert.ToString(Convert.ToString(avg)), "analysis");
                        doc.addData(level + 6 + sample_count, 6, Convert.ToString(Convert.ToString(sd)), "analysis");
                        resFreqNAAvg.Clear();

                        avg = resFreqSVDAvg.Sum() / num_avg;
                        sd = resFreqSVDAvg.Select(x => (x - avg) * (x - avg)).Sum();
                        sd = Math.Sqrt(sd / resFreqSVDAvg.Count);
                        doc.addData(level + 5 + sample_count, 7, Convert.ToString(Convert.ToString(avg)), "analysis");
                        doc.addData(level + 6 + sample_count, 7, Convert.ToString(Convert.ToString(sd)), "analysis");
                        resFreqSVDAvg.Clear();

                        avg = roomTAvg.Sum() / num_avg;
                        sd = roomTAvg.Select(x => (x - avg) * (x - avg)).Sum();
                        sd = Math.Sqrt(sd / roomTAvg.Count);
                        doc.addData(level + 5 + sample_count, 8, Convert.ToString(Convert.ToString(avg)), "analysis");
                        doc.addData(level + 6 + sample_count, 8, Convert.ToString(Convert.ToString(sd)), "analysis");
                        roomTAvg.Clear();

                        avg = cavityTAvg.Sum() / num_avg;
                        sd = cavityTAvg.Select(x => (x - avg) * (x - avg)).Sum();
                        sd = Math.Sqrt(sd / cavityTAvg.Count);
                        doc.addData(level + 5 + sample_count, 9, Convert.ToString(Convert.ToString(avg)), "analysis");
                        doc.addData(level + 6 + sample_count, 9, Convert.ToString(Convert.ToString(sd)), "analysis");
                        cavityTAvg.Clear();

                        i++;
                        v = 0;
                    }


                }
            }


            Excel.Range workSheet_range = doc.Analysis.Range[doc.Analysis.Cells[1, 1], doc.Analysis.Cells[10000, 20]];
            workSheet_range.Columns.AutoFit();

            doc.workbook1.Save();
            wb.Save();
            wb.Close();
            excel.Quit();
            Marshal.FinalReleaseComObject(excel);
            Marshal.FinalReleaseComObject(books);
            Marshal.FinalReleaseComObject(wb);





        }

        public object[] getMLData(MLApp.MLApp matlab)
        {
            object manual_means;
            object manual_stdevs;
            object auto_means;
            object auto_stdevs;
            object sections;
            object indices;
            object a;

            matlab.GetWorkspaceData("manual_means", "base", out manual_means);
            matlab.GetWorkspaceData("manual_stdevs", "base", out manual_stdevs);
           // matlab.GetWorkspaceData("auto_means", "base", out auto_means);
           // matlab.GetWorkspaceData("auto_stdevs", "base", out auto_stdevs);
            matlab.GetWorkspaceData("sections", "base", out sections);
            matlab.GetWorkspaceData("indices", "base", out indices);
            matlab.GetWorkspaceData("a", "base", out a);

            object[] result = { manual_means, manual_stdevs, sections, indices, a };
            return result;

        }

        private void resultPath_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
