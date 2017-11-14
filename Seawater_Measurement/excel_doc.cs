using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Seawater_Measurement
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Drawing;
    using Excel = Microsoft.Office.Interop.Excel;

        class excel_doc
        {
            public Excel.Application app = null;
            public Excel.Application app1 = null;

            public Excel.Workbook workbook = null;
            public Excel.Workbook workbook1 = null;

            public Excel.Worksheet rawData = null;
            public Excel.Worksheet qVal = null;
            public Excel.Worksheet centFreq = null;
            public Excel.Worksheet cavityTemp = null;
            public Excel.Worksheet roomTemp = null;
            public Excel.Worksheet svd_na = null;
            public Excel.Worksheet Q_L = null;
            public Excel.Worksheet Q_UL = null;
            public Excel.Worksheet f_center = null;
            public Excel.Worksheet s21 = null;
            public Excel.Worksheet svd_na_center = null;
            public Excel.Worksheet general_info = null;


            public Excel.Worksheet Analysis = null;


            public Excel.Range workSheet_range = null;


            public excel_doc()
            {
                
            }

        public void createAnalysisDoc(int section_count,int sample_count)
        {
            app1 = new Excel.Application();
            app1.Visible = false;
            workbook1 = app1.Workbooks.Add(1);
            workbook1.Sheets.Add(Count: 1);

            Analysis = (Excel.Worksheet)workbook1.Sheets[1];
            Analysis.Name = "Analysis";
            int j;
            int i;
            string title;
            string sample_title;

            for (j = 1; j <= section_count; j++)
            {
                int level = Convert.ToUInt16((sample_count * 2 + 12) * (j - 1));
                title = string.Format("Stage {0}", j.ToString());
                addData(level + 2, 1, title, "analysis");
                workSheet_range = Analysis.Range[Analysis.Cells[level + 2, 1], Analysis.Cells[level + 2, 9]];
                workSheet_range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                workSheet_range.Merge();
                workSheet_range.Font.Bold = true;

                addData(level + 3, 1, "Manual", "analysis");
                workSheet_range = Analysis.Range[Analysis.Cells[level + 3, 1], Analysis.Cells[level + 3, 9]];
                workSheet_range.Font.Italic = true;
                workSheet_range.Font.Bold = false;
                workSheet_range.Merge();

                addData(level + 4, 2, "Time", "analysis");
                addData(level + 4, 3, "Q Value Loaded (SVD)", "analysis");
                addData(level + 4, 4, "Q Value Unloaded (SVD)", "analysis");
                addData(level + 4, 5, "Q Value (NA)", "analysis");
                addData(level + 4, 6, "Resonant Frequency (NA)", "analysis");
                addData(level + 4, 7, "Resonant Frequency (SVD)", "analysis");
                addData(level + 4, 8, "Cavity Temperature (C)", "analysis");
                addData(level + 4, 9, "Room Temperature (C)", "analysis");
                workSheet_range = Analysis.Range[Analysis.Cells[level + 4, 1], Analysis.Cells[level + 4, 9]];
                workSheet_range.Font.Bold = true;

                for (i = 0; i < sample_count; i++) {
                    sample_title = string.Format("Sample {0}", (i+1).ToString());
                    addData(level + 5 + i, 1, sample_title, "analysis");
                }

                addData(level + 5+sample_count, 1, "Average", "analysis");
                addData(level + 6+sample_count, 1, "Standard Dev.", "analysis");

                addData(level + 8 + sample_count, 1, "Automatic", "analysis");
                workSheet_range = Analysis.Range[Analysis.Cells[level + 8 + sample_count, 1], Analysis.Cells[level + 8 + sample_count, 9]];
                workSheet_range.Font.Italic = true;
                workSheet_range.Font.Bold = false;
                workSheet_range.Merge();


                addData(level + 9 + sample_count, 2, "Time", "analysis");
                addData(level + 9 + sample_count, 3, "Q Value Loaded (SVD)", "analysis");
                addData(level + 9 + sample_count, 4, "Q Value Unloaded (SVD)", "analysis");
                addData(level + 9 + sample_count, 5, "Q Value (NA)", "analysis");
                addData(level + 9 + sample_count, 6, "Resonant Frequency (NA)", "analysis");
                addData(level + 9 + sample_count, 7, "Resonant Frequency (SVD)", "analysis");
                addData(level + 9 + sample_count, 8, "Cavity Temperature (C)", "analysis");
                addData(level + 9 + sample_count, 9, "Room Temperature (C)", "analysis");
                workSheet_range = Analysis.Range[Analysis.Cells[level + 9 + sample_count, 1], Analysis.Cells[level + 9 + sample_count, 9]];
                workSheet_range.Font.Bold = true;

                for (i = 0; i < sample_count; i++)
                {
                    sample_title = string.Format("Sample {0}", (i + 1).ToString());
                    addData(level + 10 + sample_count + i, 1, sample_title, "analysis");
                }

                addData(level + 10 + (2)*sample_count, 1, "Average", "analysis");
                addData(level + 11 + (2)*sample_count, 1, "Standard Dev.", "analysis");
                workSheet_range = Analysis.Range[Analysis.Cells[1, 1], Analysis.Cells[level + 1000 + sample_count, 1]];
                workSheet_range.Font.Bold = 1;

            }


        }


            public void createDoc()
            {
                try
                {
                    app = new Excel.Application();
                    app.Visible = true;
                    workbook = app.Workbooks.Add(1);
                    workbook.Sheets.Add(Count: 12);

                rawData = (Excel.Worksheet)workbook.Sheets[1];
                rawData.Name = "Raw Data";
                qVal = (Excel.Worksheet)workbook.Sheets[2];
                qVal.Name = "Q vs. Time";
                centFreq = (Excel.Worksheet)workbook.Sheets[3];
                centFreq.Name = "Center Freq.";
                cavityTemp = (Excel.Worksheet)workbook.Sheets[4];
                cavityTemp.Name = "Cavity Temp.";
                roomTemp = (Excel.Worksheet)workbook.Sheets[5];
                roomTemp.Name = "Room Temp.";
                Q_L = (Excel.Worksheet)workbook.Sheets[6];
                Q_L.Name = "Loaded Q";
                svd_na = (Excel.Worksheet)workbook.Sheets[7];
                svd_na.Name = "Loaded Q vs NA Q";
                Q_UL = (Excel.Worksheet)workbook.Sheets[8];
                Q_UL.Name = "Unloaded Q";
                f_center = (Excel.Worksheet)workbook.Sheets[9];
                f_center.Name = "SVD Center Frequency";
                s21 = (Excel.Worksheet)workbook.Sheets[10];
                s21.Name = "SVD S21";
                svd_na_center = (Excel.Worksheet)workbook.Sheets[11];
                svd_na_center.Name = "SVD to NA Center Freq";
                general_info = (Excel.Worksheet)workbook.Sheets[12];
                general_info.Name = "General Info";

            }
                catch (Exception e)
                {
                    Console.Write("Error");
                }
                finally
                {
                }
            }


            public void addData(int row, int col, string data,string wrksheet)
            {

                switch (wrksheet)
                {
                    case "raw_data":
                        rawData.Cells[row, col] = data;
                        break;

                    case "q_val":
                        qVal.Cells[row, col] = data;
                        break;

                    case "cent_freq":
                        centFreq.Cells[row, col] = data;
                        break;

                    case "cavT":
                        cavityTemp.Cells[row, col] = data;
                        break;

                    case "roomT":
                        roomTemp.Cells[row, col] = data;
                        break;

                    case "SVD vs NA Diff":
                        svd_na.Cells[row, col] = data;
                        break;

                    case "Q_Loaded":
                        Q_L.Cells[row, col] = data;
                        break;

                    case "Q_Unloaded":
                        Q_UL.Cells[row, col] = data;
                        break;

                    case "f_center":
                        f_center.Cells[row, col] = data;
                        break;

                    case "s21":
                        s21.Cells[row, col] = data;
                    break;


                    case "svd_na_center":
                        svd_na_center.Cells[row, col] = data;
                        break;

                    case "gen_info":
                        general_info.Cells[row, col] = data;
                        break;

                    case "analysis":
                        Analysis.Cells[row, col] = data;
                        break;

                default:
                            Console.WriteLine("Fix spelling of type!!!!");
                            break;

                }
                
            }

        public string RangeAddress(Excel.Range rng)
        {
            return rng.get_AddressLocal(false, false, Excel.XlReferenceStyle.xlA1);
        }
        public string CellAddress(int row, int col)
        {
            return RangeAddress(rawData.Cells[row, col]);
        }




        public void generateChart(int dataCnt)
        {
            //Q CHART
            Excel.Range chartRange;
            Excel.ChartObjects xlCharts = (Excel.ChartObjects)qVal.ChartObjects(Type.Missing);
            Excel.ChartObject myChart = xlCharts.Add(10, 80, 800, 500);
            Excel.Chart chartPage = myChart.Chart;

            string range = string.Format("B{0}", dataCnt-1);
            string range2 = string.Format("C{0}", dataCnt - 1);

            chartRange = qVal.get_Range("A1",range);
            chartPage.SetSourceData(Source: chartRange);
            chartPage.ChartType = Excel.XlChartType.xlXYScatter;
            chartPage.HasTitle = true;
            chartPage.ChartTitle.Text = "Q-Value vs. Time";
            chartPage.HasLegend = false;

            Excel.Axis xaxis = chartPage.Axes(Excel.XlAxisType.xlCategory, Excel.XlAxisGroup.xlPrimary);
            xaxis.HasTitle = true;
            xaxis.TickLabels.NumberFormat = "[h]:mm:ss;@";
            xaxis.AxisTitle.Text = "Time";

            Excel.Axis yaxis = chartPage.Axes(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlPrimary);
            yaxis.HasTitle = true;
            yaxis.AxisTitle.Text = "Q-Value";

            //CENTER FREQ CHART
            Excel.ChartObjects xlCharts1 = (Excel.ChartObjects)centFreq.ChartObjects(Type.Missing);
            Excel.ChartObject myChart1 = xlCharts1.Add(10, 80, 800, 500);
            Excel.Chart chartPage1 = myChart1.Chart;


            chartRange = centFreq.get_Range("A1", range);
            chartPage1.SetSourceData(Source: chartRange);
            chartPage1.ChartType = Excel.XlChartType.xlXYScatter;
            chartPage1.HasTitle = true;
            chartPage1.ChartTitle.Text = "Center Frequency vs. Time";
            chartPage1.HasLegend = false;

            Excel.Axis xaxis1 = chartPage1.Axes(Excel.XlAxisType.xlCategory, Excel.XlAxisGroup.xlPrimary);
            xaxis1.HasTitle = true;
            xaxis1.TickLabels.NumberFormat = "[h]:mm:ss;@";
            xaxis1.AxisTitle.Text = "Time";

            Excel.Axis yaxis1 = chartPage1.Axes(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlPrimary);
            yaxis1.HasTitle = true;
            yaxis1.AxisTitle.Text = "Center Frequency (Hz)";


            //ROOM TEMP CHART
            Excel.ChartObjects xlCharts2 = (Excel.ChartObjects)roomTemp.ChartObjects(Type.Missing);
            Excel.ChartObject myChart2 = xlCharts2.Add(10, 80, 800, 500);
            Excel.Chart chartPage2 = myChart2.Chart;


            chartRange = roomTemp.get_Range("A1", range);
            chartPage2.SetSourceData(Source: chartRange);
            chartPage2.ChartType = Excel.XlChartType.xlXYScatter;
            chartPage2.HasTitle = true;
            chartPage2.ChartTitle.Text = "Room Temp vs. Time";
            chartPage2.HasLegend = false;

            Excel.Axis xaxis2 = chartPage2.Axes(Excel.XlAxisType.xlCategory, Excel.XlAxisGroup.xlPrimary);
            xaxis2.HasTitle = true;
            xaxis2.TickLabels.NumberFormat = "[h]:mm:ss;@";
            xaxis2.AxisTitle.Text = "Time";

            Excel.Axis yaxis2 = chartPage2.Axes(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlPrimary);
            yaxis2.HasTitle = true;
            yaxis2.AxisTitle.Text = "Room Temperature (C)";

            //CAVITY TEMP CHART
            Excel.ChartObjects xlCharts3 = (Excel.ChartObjects)cavityTemp.ChartObjects(Type.Missing);
            Excel.ChartObject myChart3 = xlCharts3.Add(10, 80, 800, 500);
            Excel.Chart chartPage3 = myChart3.Chart;


            chartRange = cavityTemp.get_Range("A1", range);
            chartPage3.SetSourceData(Source: chartRange);
            chartPage3.ChartType = Excel.XlChartType.xlXYScatter;
            chartPage3.HasTitle = true;
            chartPage3.ChartTitle.Text = "Cavity Temp vs. Time";
            chartPage3.HasLegend = false;

            Excel.Axis xaxis3 = chartPage3.Axes(Excel.XlAxisType.xlCategory, Excel.XlAxisGroup.xlPrimary);
            xaxis3.HasTitle = true;
            xaxis3.TickLabels.NumberFormat = "[h]:mm:ss;@";
            xaxis3.AxisTitle.Text = "Time";

            Excel.Axis yaxis3 = chartPage3.Axes(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlPrimary);
            yaxis3.HasTitle = true;
            yaxis3.AxisTitle.Text = "Cavity Temperature (C)";


            //Matlab SVD Q 
            Excel.ChartObjects xlCharts4 = (Excel.ChartObjects)Q_L.ChartObjects(Type.Missing);
            Excel.ChartObject myChart4 = xlCharts4.Add(10, 80, 800, 500);
            Excel.Chart chartPage4 = myChart4.Chart;


            chartRange = Q_L.get_Range("A1", range);
            chartPage4.SetSourceData(Source: chartRange);
            chartPage4.ChartType = Excel.XlChartType.xlXYScatter;
            chartPage4.HasTitle = true;
            chartPage4.ChartTitle.Text = "Q Loaded vs. Time";
            chartPage4.HasLegend = false;

            Excel.Axis xaxis4 = chartPage4.Axes(Excel.XlAxisType.xlCategory, Excel.XlAxisGroup.xlPrimary);
            xaxis4.HasTitle = true;
            xaxis4.TickLabels.NumberFormat = "[h]:mm:ss;@";
            xaxis4.AxisTitle.Text = "Time";

            Excel.Axis yaxis4 = chartPage4.Axes(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlPrimary);
            yaxis4.HasTitle = true;
            yaxis4.AxisTitle.Text = "Q Value";


            //Matlab SVD Q minus Network Analyzer 
            Excel.ChartObjects xlCharts5 = (Excel.ChartObjects)svd_na.ChartObjects(Type.Missing);
            Excel.ChartObject myChart5 = xlCharts5.Add(10, 80, 800, 500);
            Excel.Chart chartPage5 = myChart5.Chart;


            chartRange = svd_na.get_Range("A1", range2);
            chartPage5.SetSourceData(Source: chartRange);
            chartPage5.ChartType = Excel.XlChartType.xlXYScatter;
            chartPage5.HasTitle = true;
            chartPage5.ChartTitle.Text = "SVD Q and NA Q vs. Time";
            chartPage5.HasLegend = true;


            Excel.Series series1 = chartPage5.SeriesCollection(1);
            series1.Name = "NA Q";
            Excel.Series series2 = chartPage5.SeriesCollection(2);
            series2.Name = "SVD Q";



            Excel.Axis xaxis5 = chartPage5.Axes(Excel.XlAxisType.xlCategory, Excel.XlAxisGroup.xlPrimary);
            xaxis5.HasTitle = true;
            xaxis5.TickLabels.NumberFormat = "[h]:mm:ss;@";
            xaxis5.AxisTitle.Text = "Time";

            Excel.Axis yaxis5 = chartPage5.Axes(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlPrimary);
            yaxis5.HasTitle = true;
            yaxis5.AxisTitle.Text = "Q Value";

            //Unloaded Q
            //Matlab SVD Q minus Network Analyzer 
            Excel.ChartObjects xlCharts6 = (Excel.ChartObjects)Q_UL.ChartObjects(Type.Missing);
            Excel.ChartObject myChart6 = xlCharts6.Add(10, 80, 800, 500);
            Excel.Chart chartPage6 = myChart6.Chart;


            chartRange = Q_UL.get_Range("A1", range);
            chartPage6.SetSourceData(Source: chartRange);
            chartPage6.ChartType = Excel.XlChartType.xlXYScatter;
            chartPage6.HasTitle = true;
            chartPage6.ChartTitle.Text = "Unloaded Q vs. Time";
            chartPage6.HasLegend = false;

            Excel.Axis xaxis6 = chartPage6.Axes(Excel.XlAxisType.xlCategory, Excel.XlAxisGroup.xlPrimary);
            xaxis6.HasTitle = true;
            xaxis6.TickLabels.NumberFormat = "[h]:mm:ss;@";
            xaxis6.AxisTitle.Text = "Time";

            Excel.Axis yaxis6 = chartPage6.Axes(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlPrimary);
            yaxis6.HasTitle = true;
            yaxis6.AxisTitle.Text = "Unloaded Q";


            //SVD Center Frequency
            //Matlab SVD Q minus Network Analyzer 
            Excel.ChartObjects xlCharts7 = (Excel.ChartObjects)f_center.ChartObjects(Type.Missing);
            Excel.ChartObject myChart7 = xlCharts7.Add(10, 80, 800, 500);
            Excel.Chart chartPage7 = myChart7.Chart;


            chartRange = f_center.get_Range("A1", range);
            chartPage7.SetSourceData(Source: chartRange);
            chartPage7.ChartType = Excel.XlChartType.xlXYScatter;
            chartPage7.HasTitle = true;
            chartPage7.ChartTitle.Text = "SVD Center Frequency vs. Time";
            chartPage7.HasLegend = false;

            Excel.Axis xaxis7 = chartPage7.Axes(Excel.XlAxisType.xlCategory, Excel.XlAxisGroup.xlPrimary);
            xaxis7.HasTitle = true;
            xaxis7.TickLabels.NumberFormat = "[h]:mm:ss;@";
            xaxis7.AxisTitle.Text = "Time";

            Excel.Axis yaxis7 = chartPage7.Axes(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlPrimary);
            yaxis7.HasTitle = true;
            yaxis7.AxisTitle.Text = "SVD Center Frequency (GHz)";



            //SVD S21
            Excel.ChartObjects xlCharts8 = (Excel.ChartObjects)s21.ChartObjects(Type.Missing);
            Excel.ChartObject myChart8 = xlCharts8.Add(10, 80, 800, 500);
            Excel.Chart chartPage8 = myChart8.Chart;


            chartRange = s21.get_Range("A1", range);
            chartPage8.SetSourceData(Source: chartRange);
            chartPage8.ChartType = Excel.XlChartType.xlXYScatter;
            chartPage8.HasTitle = true;
            chartPage8.ChartTitle.Text = "SVD S21 vs. Time";
            chartPage8.HasLegend = false;

            Excel.Axis xaxis8 = chartPage8.Axes(Excel.XlAxisType.xlCategory, Excel.XlAxisGroup.xlPrimary);
            xaxis8.HasTitle = true;
            xaxis8.TickLabels.NumberFormat = "[h]:mm:ss;@";
            xaxis8.AxisTitle.Text = "Time";

            Excel.Axis yaxis8 = chartPage8.Axes(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlPrimary);
            yaxis8.HasTitle = true;
            yaxis8.AxisTitle.Text = "S21 (W)";


            //SVD vs NA center freq
            Excel.ChartObjects xlCharts9 = (Excel.ChartObjects)svd_na_center.ChartObjects(Type.Missing);
            Excel.ChartObject myChart9 = xlCharts9.Add(10, 80, 800, 500);
            Excel.Chart chartPage9 = myChart9.Chart;


            chartRange = svd_na_center.get_Range("A1", range2);
            chartPage9.SetSourceData(Source: chartRange);
            chartPage9.ChartType = Excel.XlChartType.xlXYScatter;
            chartPage9.HasTitle = true;
            chartPage9.ChartTitle.Text = "SVD and NA Center Frequency vs. Time";
            chartPage9.HasLegend = true;

            Excel.Series series3 = chartPage9.SeriesCollection(1);
            series3.Name = "NA Center Frequency";
            Excel.Series series4 = chartPage9.SeriesCollection(2);
            series4.Name = "SVD Center Frequency";



            Excel.Axis xaxis9 = chartPage9.Axes(Excel.XlAxisType.xlCategory, Excel.XlAxisGroup.xlPrimary);
            xaxis9.HasTitle = true;
            xaxis9.TickLabels.NumberFormat = "[h]:mm:ss;@";
            xaxis9.AxisTitle.Text = "Time";

            Excel.Axis yaxis9 = chartPage9.Axes(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlPrimary);
            yaxis9.HasTitle = true;
            yaxis9.AxisTitle.Text = "Center Frequency (GHz)";



        }

    }

}
