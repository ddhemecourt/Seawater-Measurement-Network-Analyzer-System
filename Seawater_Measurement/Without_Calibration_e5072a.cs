using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System
    .Runtime.InteropServices;
using System.Windows.Forms;
using NationalInstruments.NI4882;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace Seawater_Measurement
{
    public partial class Without_Calibration_e5072a : Form
    {

        //DECLARE VARIABLES

        [DllImport("kernel32.dll")]

        public static extern uint SetThreadExecutionState(uint esFlags);

        public const uint ES_CONTINUOUS = 0x80000000;

        public const uint ES_SYSTEM_REQUIRED = 0x00000001;

        public const uint ES_DISPLAY_REQUIRED = 0x00000002;


        //Declare network analyzer and digital multimeter
        Device NA = new Device(0, 17, 0);
        Device DMM = new Device(0,16,0);
        Device PS = new Device(0, 5, 0);

        //Declare new excel workbook
        excel_doc Workbook;

        int cnt; //Counts measurment cycles

        const int ARRAYSIZE = 90000; //length of raw NA data

        //Varuables for NA data recording For loop
        int array1;
        int array2;
        string intData1;
        string intData2;
        double int1;
        double int2;

        //Variables that control the temperature measurement cycle
        int tempCnt;
        int tempRow;
        int tempDiv;


        //Variables that control the overall measurment cycle
        int dataCnt;
        int dataCol;
        double dataNum;
        int prev;
        double TimeNum;
        double TimeDiv;

        //Strings to store data from NA
        string centerFreq;          //frequency of resonant curve peak
        string bandwidth;           //3db Bandwidth of resonant curve peak
        string markerQ;             //Q value for resonant curve peak
        string ILValue;             //insertion loss at the resonant curve peak
        string mark2;               //appears to be used in delay procedure
        string numOfPoints;         //number of samples in resonant curve
        string OverallCenterFreq;           //lowest freq of samples
        string span;                //freq range of samples
        string data1;
        string data2;
        string data12;
        string start_freq;
        string stop_freq;
        object Q_L;
        object Q_NA;
        object f_res;
        object f_NA;


        //Temperature variables
        string temp1;
        string temp2;
        string temp3;


        //pressure variables
        string pressure;


        //Resisitance variables
        string res1;
        string res2;
        string res3;
        double meas_res1;
        double meas_res2;
        double meas_res3;

        //Voltage variables for pressure
        string volt1;
        double meas_volt1;

        DateTime now;
        DateTime start;

        //f(R) variables for temperature calculation
        double fnc_res1;
        double fnc_res2;
        double fnc_res3;


        //Variables for experiment start time
        long StartTime;
        string startTimeString;
        string startDateString;

        //Steinhart-Hart thermistor coefficients
        double A1, B1, C1, D1;
        double A2, B2, C2, D2;
        double A3, B3, C3, D3;
        double A11, B11, C11, D11;
        double A22, B22, C22, D22;
        double A33, B33, C33, D33;


        //Intermediate variables for temperature calculations
        double T1, T1_L, T1_U;
        double T2, T2_L, T2_U;
        double T3, T3_L, T3_U;
        double F1, F2, F3;
        double funcD;
        int j;


        string numberOfPoints;



        //Parameters for numerical Steinhart-Hart inversion
        string T_Upper_Text;

        private void MatlabDataAnalysis_Click(object sender, EventArgs e)
        {
            string path = pathname.Text;
            string excel_file = pathname.Text + filename.Text + ".xlsx";
            string resultFile = filename.Text + "_Result.xlsx";

            MLDataAnalysis ml = new MLDataAnalysis(excel_file, path, resultFile);
            ml.Show();
        }

        string T_Lower_Text;



        string T_accept_Text;

        double T_Upper, T_Lower, T_Accept;
        string fnc_count_text;
        int fnc_count;

        //Timestamps for time subtraction to allow continuous time axes in charts
        double sampleStartTime;
        double sampleEndTime;

        //internal excel_doc Workbook { get => Workbook1; set => Workbook1 = value; }
        //internal excel_doc Workbook1 { get => workbook; set => workbook = value; }
        //internal excel_doc Workbook2 { get => workbook; set => workbook = value; }



        //MAIN FUNCTION


        public Without_Calibration_e5072a()
        {
            InitializeComponent();

            //  SetThreadExecutionState(

            //    ES_CONTINUOUS | ES_SYSTEM_REQUIRED | ES_DISPLAY_REQUIRED);

            ExpDay.Text = DateTime.Now.ToString();

            DMM.Write("func 'RES';");

            dataCol = 2;
            //   NA.IOTimeout = 10000;
            // Timeout.Infinite();
            // The following coefficients are for temperature greater or equal to
            // 0.0 degrees Celsius to +70.0 degrees Celsius

            //Coefficients for Thermistor 1
            A1 = -5.26707200038027;
            B1 = 4581.58653607423;
            C1 = -27079333.4349911;
            D1 = 333322464108.355;

            //Coefficients for Thermistor 2
            //A2 = -0.684733265;
            //B2 = 2037.96368;
            //C2 = 119583683;
            //D2 = -3436270130000;
            A2 = -0.876691731;
            B2 = 2127.96669;
            C2 = 115904092;
            D2 = -3350113390000;

            //Coefficients for Thermistor 3
            A3 = -5.19939086186174;
            B3 = 4539.48650452823;
            C3 = -24104094.47361;
            D3 = 253243348852.034;

            //The following coefficients are for temperature less than
            // 0.0 degrees Celsius

            //Coefficients for Thermistor 1
            A11 = -4.92892587992022;
            B11 = 4392.22005685727;
            C11 = -15896236.7881741;
            D11 = 38930303174.3173;

            //Coefficients for Thermistor 2
            //A22 = -0.684733265;
            //B22 = 2037.96368;
            //C22 = 119583683;
            //D22 = -3436270130000;
            A22 = -0.876691731;
            B22 = 2127.96669;
            C22 = 115904092;
            D22 = -3350113390000;
            //Coefficients for Thermistor 3
            A33 = -4.95555668301354;
            B33 = 4402.9360100307;
            C33 = -16040062.3204484;
            D33 = 40959763510.1769;
          


        }


        public double calc_F1(double T1)
        {
            double F1 = A1 + (B1 / T1) + (C1 / (Math.Pow(T1, 3))) + (D1 / (Math.Pow(T1, 5)));
            return (F1);
        }

        public double calc_F2(double T2)
        {
            double F2 = A2 + (B2 / T2) + (C2 / (Math.Pow(T2, 3))) + (D2 / (Math.Pow(T2, 5)));
            return (F2);
        }

        public double calc_F3(double T3)
        {
            double F3 = A3 + (B3 / T3) + (C3 / (Math.Pow(T3, 3))) + (D3 / (Math.Pow(T3, 5)));
            return (F3);
        }

        public double calc_F11(double T1)
        {
            double F11 = A11 + (B11 / T1) + (C11 / (Math.Pow(T1, 3))) + (D11 / (Math.Pow(T1, 5)));
            return (F11);
        }

        public double calc_F22(double T2)
        {
            double F22 = A22 + (B22 / T2) + (C22 / (Math.Pow(T2, 3))) + (D22 / (Math.Pow(T2, 5)));
            return (F22);
        }

        public double calc_F33(double T3)
        {
            double F33 = A33 + (B33 / T3) + (C33 / (Math.Pow(T3, 3))) + (D33 / (Math.Pow(T3, 5)));
            return (F33);
        }


        public double SOLVE_1(double funcD,double T_Upper,double T_Lower,double T_Accept)
        {
            //Inputs the upper and lower temp bounds DMM
            //Determines the initial test value as the average of these two values
            T1 = (T_Upper + T_Lower) / 2;
            T1_L = T_Lower;
            T1_U = T_Upper;

            //compares the values calculated in the function F_1(T3) to the actual value of the Ln(R) for the resistance measured
            for (j = 1; j <= fnc_count; j++)
            {
                if (funcD > Math.Log(32747))
                {
                    F1 = calc_F11(T1);
                }
                else
                {
                    F1 = calc_F1(T1);
                }


                //Readjust the value of the test temperature based on whether it is higher or lower than the actual value

                if(F1 == funcD)
                {
                    break;
                }

                else if (F1 < funcD)
                {
                    T1_U = T1;
                    T1 = (T1 + T1_L) / 2; 
                }

                else if (F1 > funcD)
                {
                    T1_L = T1;
                    T1 = (T1_U + T1) / 2;
                }

                //if the tolerance has been met exit the for loop
                if (Math.Abs(F1 - funcD) < T_Accept)
                {
                    break;
                }

            }

            if(j == fnc_count)
            {
                generateErrorMessage("DMM Temp exceeded maximum iteration bound.");
                return (999.999);
            }

            else
            {
                return (T1);
            }


        }



        public double SOLVE_2(double funcD, double T_Upper, double T_Lower, double T_Accept)
        {
            T2 = (T_Upper + T_Lower) / 2;
            T2_L = T_Lower;
            T2_U = T_Upper;

            for (j = 1; j <= fnc_count; j++)
            {
                if (funcD > Math.Log(32124))
                {
                    F2 = calc_F22(T2);
                }
                else
                {
                    F2 = calc_F2(T2);
                }


                //Readjust the value of the test temperature based on whether it is higher or lower than the actual value

                if (F2 == funcD)
                {
                    break;
                }

                else if (F2 < funcD)
                {
                    T2_U = T2;
                    T2 = (T2 + T2_L) / 2;
                }

                else if (F2 > funcD)
                {
                    T2_L = T2;
                    T2 = (T2_U + T2) / 2;
                }

                //if the tolerance has been met exit the for loop
                if (Math.Abs(F2 - funcD) < T_Accept)
                {
                    break;
                }

            }

            if (j == fnc_count)
            {
                generateErrorMessage("DMM Temp exceeded maximum iteration bound.");
                return (999.999);
            }

            else
            {
                return (T2);
            }


        }


        public double SOLVE_3(double funcD, double T_Upper, double T_Lower, double T_Accept)
        {
            T3 = (T_Upper + T_Lower) / 2;
            T3_L = T_Lower;
            T3_U = T_Upper;

            for (j = 1; j <= fnc_count; j++)
            {
                if (funcD > Math.Log(32973))
                {
                    F3 = calc_F11(T3);
                }
                else
                {
                    F3 = calc_F3(T3);
                }


                //Readjust the value of the test temperature based on whether it is higher or lower than the actual value

                if (F3 == funcD)
                {
                    break;
                }

                else if (F3 < funcD)
                {
                    T3_U = T3;
                    T3 = (T3 + T3_L) / 2;
                }

                else if (F3 > funcD)
                {
                    T3_L = T3;
                    T3 = (T3_U + T3) / 2;
                }

                //if the tolerance has been met exit the for loop
                if (Math.Abs(F3 - funcD) < T_Accept)
                {
                    break;
                }

            }

            if (j == fnc_count)
            {
                generateErrorMessage("DMM Temp exceeded maximum iteration bound.");
                return (999.999);
            }

            else
            {
                return (T3);
            }


        }



        private void StopMeas_Click(object sender, EventArgs e)
        {
            StopMeas.Enabled = false;
            timer1.Enabled = false;
            Workbook.app.Visible = true;
            dataCnt = 0;
            tempCnt = 0;


            if (filename.Text == null)
            {
                generateErrorMessage("Specify a filename.");
            }


            var activationContext = Type.GetTypeFromProgID("matlab.application.single");
            var matlab = (MLApp.MLApp)Activator.CreateInstance(activationContext);
            Status.Text = "Running Matlab SVD. Do Not Modify Excel File";
            Status.Refresh();

            runScript(matlab);

            Status.Text = "Done";
            Status.Refresh();

            Workbook.generateChart(dataCol / 2);
            Workbook.workbook.Save();


            /*     while (Workbook.rawData.Cells[10, j].Value2 != null)
                 {
                     string address1 = Workbook.CellAddress(10, j);
                     string address2 = Workbook.CellAddress(1608, j);
                     string address = string.Format(address1 + ":" + address2);
                     Console.WriteLine("Column address = " + address);

                     Workbook.addData(j / 2, 2, Q_L.ToString(), "Q_Loaded");
                     Workbook.addData(j / 2, 3, Q_L.ToString(), "SVD vs NA Diff");
                     Workbook.addData(j / 2, 2, Q_UL.ToString(), "Q_Unloaded");
                     Workbook.addData(j / 2, 2, f_center.ToString(), "f_center");
                     Workbook.addData(j / 2, 3, f_center.ToString(), "svd_na_center");
                     Workbook.addData(j / 2, 2, s21.ToString(), "s21");


                     matlab.Execute("clear");
                     j = j + 2;

                 }*/
        }



        private void ExecuteMeasurment_Click(object sender, EventArgs e)
        {
            /* if (checkFileExists() == true)
             {
                 generateErrorMessage("Filename Already Exists");
                 return;
             }
             */

            if (ExpLastName.Text != "" && Substance.Text != "" && TubeNumber.Text != "")
            {

                StopMeas.Enabled = true;
                ExecuteMeasurment.Enabled = false;


                Workbook = new excel_doc();
                Workbook.createDoc();
                // Workbook.app.ScreenUpdating = false;
                Workbook.addData(3, 1, "Cavity Temp:", "raw_data");
                Workbook.addData(4, 1, "Room Temp:", "raw_data");
                Workbook.addData(5, 1, "NA Calc Center:", "raw_data");
                Workbook.addData(6, 1, "NA Calc BW:", "raw_data");
                Workbook.addData(7, 1, "NA Calc IL:", "raw_data");
                Workbook.addData(8, 1, "NA Calc Q:", "raw_data");
                Workbook.addData(9, 1, "Frequencies", "raw_data");
                Workbook.addData(9, 2, "S21", "raw_data");
                dataCol = 2;

                Q_L = null;
               // Q_UL = null;

                string todayDate = DateTime.Today.ToString("dd-MM-yyyy");

                filename.Text = todayDate + "_" + Substance.Text + "_" + tempSet.Text + "C" + filename_notes.Text;
                filename.Refresh();
                checkFileExists();

                string path = string.Format("{0}{1}.xlsx", pathname.Text, filename.Text);
                Workbook.workbook.SaveAs(path);

                addGeneralInfo();

                start = DateTime.Now;
                startMeasurment();
            }

            else
            {
                generateErrorMessage("Fill in all required fields.");
            }



        }

        private void addGeneralInfo()
        {
            Workbook.addData(1, 1, "Experimentor Last Name:", "gen_info");
            Workbook.addData(1, 2, ExpLastName.Text, "gen_info");
            Workbook.addData(3, 1, "Number of Sweep Averages:", "gen_info");
            Workbook.addData(3, 2, AvgFactor.Text, "gen_info");


            NA.Write(":SENS:SWE:POIN?;*WAI;");
            numPoints.Text = NA.ReadString();
            numPoints.Refresh();
            Console.WriteLine(numPoints.Text);
            Workbook.addData(5, 1, "Number of Data Points:", "gen_info");
            Workbook.addData(5, 2, numPoints.Text, "gen_info");

            NA.Write(":SENS:BWID:RES?;*WAI;");
            IF_BW.Text = NA.ReadString();
            IF_BW.Refresh();
            Console.WriteLine(IF_BW.Text);
            Workbook.addData(7, 1, "IF Bandwidth:", "gen_info");
            Workbook.addData(7, 2, IF_BW.Text, "gen_info");

            NA.Write(":SOUR:POW:LEV:IMM:AMPL?;*WAI;");
            InputPow.Text = NA.ReadString();
            InputPow.Refresh();
            Console.WriteLine(InputPow.Text);
            Workbook.addData(15, 1, "Input Power (dB):", "gen_info");
            Workbook.addData(15, 2, InputPow.Text, "gen_info");

            Workbook.addData(9, 1, "Substance:", "gen_info");
            Workbook.addData(9, 2, Substance.Text, "gen_info");
            ExpDay.Text = DateTime.Now.ToString();
            ExpDay.Refresh();
            Workbook.addData(11, 1, "Experiment Start:", "gen_info");
            Workbook.addData(11, 2, ExpDay.Text, "gen_info");
            Workbook.addData(13, 1, "Tube Number:", "gen_info");
            Workbook.addData(13, 2, TubeNumber.Text, "gen_info");

            NA.Write(":SENS:FREQ:STAR?;*WAI;");
            start_freq = NA.ReadString();
            Workbook.addData(17, 1, "Start Frequency:", "gen_info");
            Workbook.addData(17, 2, start_freq, "gen_info");

            NA.Write(":SENS:FREQ:STOP?;*WAI;");
            stop_freq = NA.ReadString();
            Workbook.addData(19, 1, "Stop Frequency:", "gen_info");
            Workbook.addData(19, 2, stop_freq, "gen_info");

            NA.Write(":SENS:FREQ:DATA?;*WAI;");
            string freqs = NA.ReadString(ARRAYSIZE);
            string[] freq_array = freqs.Split(',');

            for (j = 0; j < freq_array.Length; j++)
            {
                string[] dpoint = freq_array[j].Split('E');
                try
                {
                    double mag = Convert.ToDouble(dpoint[0]);
                    double exp = Convert.ToDouble(dpoint[1]);
                    double value = mag * Math.Pow(10, exp);

                    if (value == 0)
                    {
                        continue;
                    }
                    else
                    {
                        Workbook.addData(j + 10, 1, Convert.ToString(value), "raw_data");
                    }
                }

                catch
                {
                    continue;
                }

            }

            Workbook.general_info.Rows.AutoFit();
            Workbook.general_info.Columns.AutoFit();


        }

        private bool checkFileExists()
        {
            string path = pathname.Text + filename.Text;
            Console.WriteLine("Path = " + path);
            if (Directory.Exists(@path))
            {

                pathname.Text = pathname.Text + filename.Text + "\\";
                pathname.Refresh();

                string[] files = Directory.GetFiles(path);
                int num = -1;

                foreach (string file in files)
                {
                    string fileNameOnly = Path.GetFileNameWithoutExtension(file);

                    if (fileNameOnly.Contains('$'))
                        continue;

                    int lastUnderscore = fileNameOnly.LastIndexOf("_");
                    int fileNum = Convert.ToInt16(fileNameOnly.Substring(lastUnderscore+1));
                    Console.WriteLine(fileNum);


                    if(fileNum >= num)
                        num = fileNum + 1;    
                }

                filename.Text = filename.Text + "_" + Convert.ToString(num);

                return true;
            }
            else
            {
                System.IO.Directory.CreateDirectory(path);
                pathname.Text = pathname.Text + filename.Text + "\\";
                pathname.Refresh();
                filename.Text = filename.Text + "_0";
                return false;
            }

        }


        private void startMeasurment()
        {
            fnc_count = 250;



            //Averaging

            string AverageFactor = ":SENS:AVER:COUN " + AvgFactor.Text + ";";
            NA.Write(AverageFactor);
            /*
            string IFBandw = ":SENS:BAND?;*WAI;";
            NA.Write(IFBandw);
            IFBandwidth.Text = NA.ReadString();
            */
            NA.Write(":SENS:AVER:CLE;");
            NA.Write("SENS:AVER ON;");


            //   NA.Write("FMA;FMT0;AON;");
            DMM.Write("*RST;");
    
            //DMM Initialization
            //DMM.Write("SYST:BEEP 0");
           // DMM.Write("*rst");
            //DMM.Write("FORM:DATA ASCII");


            //DMM.Write("INST:SEL P6V;");
            //DMM.Write("VOLT:TRIG 3;");
            //DMM.Write("INST:SEL P25V;");
            //DMM.Write("VOLT:TRIG 3;");
            //DMM.Write("OUTP ON;");
            //DMM.Write("INIT;");
            //DMM.Write("*TRG;");

            cnt = 1;
            tempRow = 2;

            //string points = string.Format(":SENS:SWE:POIN {0};", numPoints.Text);
            //NA.Write(points);
            // microwaveSetupData();


            measure();
        }

        private void measure()
        {

            dataCnt = 0;
            prev = 0;

            timer1.Interval = 1000;
            timer1.Enabled = true;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dataCnt = dataCnt + 1;
            //Console.WriteLine("Data Count= "+dataCnt);
            

            if(dataCnt == 15)
            {
              //Unclear about the lines below???? Pressure is calculated in tempmeas()
              //  pressure_txt.Text = Convert.ToString(prev + 1);
              //  prev = Convert.ToInt32(pressure_txt.Text);
                dataCnt = 0;
                microwaveDataRetreival();
                TimeDiv = 0;
                dataCol = dataCol + 2;
                NumDataPoints.Text = Convert.ToString(Convert.ToInt32(NumDataPoints.Text) + 1);
                NumDataPoints.Refresh();
            }

            tempCnt = tempCnt + 1;
            tempDiv = tempCnt / 10;

            if (tempDiv == 1)
            {
                //call the temp measurement
                tempMeas();
                //write the temperature values to the temperature data sheet
                tempCnt = 0;
                tempDiv = 0;
                tempRow = tempRow + 1;

            }

        }


        private void microwaveDataRetreival()
        {
            //Console.WriteLine("HERE");
            NA.IOTimeout = TimeoutValue.T1000s;
            Console.WriteLine("HERE");
            NA.Write(":SENS:SWE:TYPE LIN;*WAI;");
            NA.Write(":CALC:PAR:DEF S21;*WAI;");
            NA.Write(":ABOR;");
            NA.Write(":TRIG:SOUR EXT;*WAI;");

            System.Threading.Thread.Sleep(1000);

            Stopwatch time = Stopwatch.StartNew();
            NA.Write(":TRIG:SING;*WAI;");

            //NA.Write(initiateSweep);


            
            NA.Write(":CALC:DATA:FDAT?;*WAI;");
            data12 = NA.ReadString(ARRAYSIZE);


            now = DateTime.Now;
            TimeSpan timespan = now.Subtract(start);
            string currentTime = Convert.ToString(timespan);
            Workbook.addData(2, dataCol, currentTime, "raw_data");
            Excel.Range wrksht_rng = Workbook.rawData.Cells[2, dataCol];
            wrksht_rng.NumberFormat = "[h]:mm:ss;@";

            Workbook.addData(dataCol/2, 1, currentTime, "q_val");
            wrksht_rng = Workbook.qVal.Cells[dataCol/2, 1];
            wrksht_rng.NumberFormat = "[h]:mm:ss;@";

            Workbook.addData(dataCol/2, 1, currentTime, "cent_freq");
            wrksht_rng = Workbook.centFreq.Cells[dataCol/2, 1];
            wrksht_rng.NumberFormat = "[h]:mm:ss;@";

            Workbook.addData(dataCol/2, 1, currentTime, "cavT");
            wrksht_rng = Workbook.cavityTemp.Cells[dataCol/2, 1];
            wrksht_rng.NumberFormat = "[h]:mm:ss;@";

            Workbook.addData(dataCol/2, 1, currentTime, "roomT");
            wrksht_rng = Workbook.roomTemp.Cells[dataCol/2, 1];
            wrksht_rng.NumberFormat = "[h]:mm:ss;@";

            Workbook.addData(dataCol/2, 1, currentTime, "Q_Loaded");
            wrksht_rng = Workbook.Q_L.Cells[dataCol/2, 1];
            wrksht_rng.NumberFormat = "[h]:mm:ss;@";

            Workbook.addData(dataCol/2, 1, currentTime, "SVD vs NA Diff");
            wrksht_rng = Workbook.svd_na.Cells[dataCol/2, 1];
            wrksht_rng.NumberFormat = "[h]:mm:ss;@";

            Workbook.addData(dataCol/2, 1, currentTime, "Q_Unloaded");
            wrksht_rng = Workbook.Q_UL.Cells[dataCol/2, 1];
            wrksht_rng.NumberFormat = "[h]:mm:ss;@";

            Workbook.addData(dataCol/2, 1, currentTime, "f_center");
            wrksht_rng = Workbook.f_center.Cells[dataCol/2, 1];
            wrksht_rng.NumberFormat = "[h]:mm:ss;@";

            Workbook.addData(dataCol/2, 1, currentTime, "s21");
            wrksht_rng = Workbook.s21.Cells[dataCol/2, 1];
            wrksht_rng.NumberFormat = "[h]:mm:ss;@";

            Workbook.addData(dataCol/2, 1, currentTime, "svd_na_center");
            wrksht_rng = Workbook.svd_na_center.Cells[dataCol/2, 1];
            wrksht_rng.NumberFormat = "[h]:mm:ss;@";

            time.Stop();
            Console.WriteLine("Time to run = " + time.Elapsed);
            Console.WriteLine(data12);
            NA.Clear();

           

            string[] data = data12.Split(',');
            string[] dataOut = new string[data.Length];  //String array of output data formatted into decimal numbers

                
            Console.WriteLine("numpoints = " + (dataOut.Length-1)/2);


            for (j = 0; j<data.Length; j++)
            {
                string[] dpoint = data[j].Split('E');
                try
                {
                    double mag = Convert.ToDouble(dpoint[0]);
                    double exp = Convert.ToDouble(dpoint[1]);
                    double value = mag * Math.Pow(10, exp);

                    if (value == 0)
                    {
                       continue;
                    }
                    else
                    {
                       Workbook.addData(j/2 + 10, dataCol, Convert.ToString(value), "raw_data");
                    }
                }

                catch
                {
                    continue;
                }

            }

            //Calculate BW, Cent Freq, Loss and Q
            NA.Write(":CALC:SEL:MARK:BWID ON;*WAI;"); //Turn on bandwidth marker display
          //  NA.Write(":CALC:SEL:MARK:REF ON;*WAI;");  //Turn reference marker on
          //  NA.Write(":CALC:SEL:MARK:ACT ON;*WAI;");
            NA.Write(":CALC:SEL:MARK:STAT 1;*WAI;");
            NA.Write(":CALC:SEL:MARK:BWID:THR -3;"); //Set -3db as the Bandwidth threshold
                                                          //  NA.Write(":CALC:PAR:SEL;*WAI;");

       //     NA.Write(":CALC:SEL:MARK:BWID:THR?;WAI*;");
       //     string test = NA.ReadString();
       //     Console.WriteLine(test);

            NA.Write(":CALC:SEL:MARK:BWID:DATA?;");// Ask for the data, which gets returned as {BW},{Cent Freq.},{Q},{Loss}
            string returnedBW = NA.ReadString(ARRAYSIZE);
            try
            {
                double magnitude;
                double power;
                double val;
                Console.WriteLine("HERERERE");
                //string returnedBW = NA.ReadString(ARRAYSIZE);
                Console.WriteLine(returnedBW);

                string[] outpData = returnedBW.Split(',');


                bandwidth = outpData[0];
                string[] b = bandwidth.Split('E');
                magnitude = Convert.ToDouble(b[0]);
                power = Convert.ToDouble(b[1]);
                val = magnitude * Math.Pow(10, power);
                bandwidth = Convert.ToString(val);
                NA_bwTXT.Text = bandwidth;

                centerFreq = outpData[1];
                string[] c = centerFreq.Split('E');
                magnitude = Convert.ToDouble(c[0]);
                power = Convert.ToDouble(c[1]);
                val = magnitude * Math.Pow(10, power);
                centerFreq = Convert.ToString(val);
                NA_CenterFreqTXT.Text = centerFreq;

                markerQ = outpData[2];
                string[] q = markerQ.Split('E');
                magnitude = Convert.ToDouble(q[0]);
                power = Convert.ToDouble(q[1]);
                val = magnitude * Math.Pow(10, power);
                markerQ = Convert.ToString(val);
                NA_QValTXT.Text = markerQ;

                ILValue = outpData[3];
                string[] il = ILValue.Split('E');
                magnitude = Convert.ToDouble(il[0]);
                power = Convert.ToDouble(il[1]);
                val = magnitude * Math.Pow(10, power);
                ILValue = Convert.ToString(val);
                NA_ILTXT.Text = ILValue;


            }
            catch
            {
                Console.WriteLine("returning");
                return;
            }

            NA.Write(":TRIG:SOUR INT;*WAI;");

            //add q,bw,il,and cent freq to excel doc
            Workbook.addData(8, dataCol, markerQ, "raw_data");
            Workbook.addData(dataCol/2, 2, markerQ, "q_val");
            Workbook.addData(5, dataCol, centerFreq, "raw_data");
            Workbook.addData(dataCol/2, 2, centerFreq, "cent_freq");
            Workbook.addData(6, dataCol, bandwidth, "raw_data");
            Workbook.addData(7, dataCol, ILValue, "raw_data");

            //Record temp with the sampled data
            Workbook.addData(3, dataCol, T2_TXT.Text, "raw_data");
            Workbook.addData(4, dataCol, T3_TXT.Text, "raw_data");
            Workbook.addData(dataCol/2, 2, T2_TXT.Text, "cavT");
            Workbook.addData(dataCol/2, 2, T3_TXT.Text, "roomT");

        }

        private void tempMeas()
        {
            //DMM.Write("*RST;");
            DMM.Write(":FUNC 'RES';*WAI;"); //Temp as a function of thermistor value

            Char[] delimeters = { 'E', 'V','O' }; //To parse through the returned DMM values

            //Thermistor 001 Measurement
            //DMM.Write("rout:clos (@101);:read?");
            //res1 = DMM.ReadString();
            //Console.WriteLine(res1);
            //******Uncomment lines below if thermistor 1 is fixed
            //string[] test_res1 = res1.Split(delimeters);
            //meas_res1 = Math.Round((Convert.ToDouble(test_res1[0])) * (Math.Pow(10, Convert.ToDouble(test_res1[1]))), 8);
            //res1 = Convert.ToString(meas_res1);

            //Thermistor 002 Measurement
            DMM.Write("rout:clos (@102);:read?");
            res2 = DMM.ReadString();
            //Console.WriteLine(res2);
            //Console.WriteLine("res2 = " + res2);
            string[] test_res2 = res2.Split(delimeters);
            meas_res2 = Math.Round((Convert.ToDouble(test_res2[0])) * (Math.Pow(10, Convert.ToDouble(test_res2[1]))), 8);
            res2 = Convert.ToString(meas_res2);


            //Thermistor 003 Measurement
            DMM.Write("rout:clos (@103);:read?");
            res3 = DMM.ReadString();
            //Console.WriteLine(res3);
            string[] test_res3 = res3.Split(delimeters);
            meas_res3 = Math.Round((Convert.ToDouble(test_res3[0])) * (Math.Pow(10, Convert.ToDouble(test_res3[1]))), 8);
            res3 = Convert.ToString(meas_res3);


            //pressure measurement
            DMM.Write("func 'VOLT'"); ;
            DMM.Write("rout:clos (@111);:read?");
            volt1 = DMM.ReadString();
            string[] test_volt = volt1.Split(delimeters);
            meas_volt1 = Math.Round((Convert.ToDouble(test_volt[0])) * (Math.Pow(10, Convert.ToDouble(test_volt[1]))), 8);
            volt1 = Convert.ToString(meas_volt1);


            fnc_res1 = 10; //first thermistor is broken replace with line below if fixed
                           // fnc_res1 = Math.Log(meas_res1);
            fnc_res2 = Math.Log(meas_res2);
            //fnc_res2 = 10;
            fnc_res3 = Math.Log(meas_res3);

            //calculate temp values for the three thermistors
            //double t1 = SOLVE_1(fnc_res1,323.15,263.15,0.00000005);
            //t1 = Math.Round((t1-273.15),4);
            //temp1 = Convert.ToString(t1);
            //T1_TXT.Text = temp1;

            double t2 = SOLVE_2(fnc_res2, 323.15, 263.15, 0.00000005);
            Console.WriteLine("T2 =" + t2);
            t2 = Math.Round((t2 - 273.15), 4);
            Console.WriteLine("T2 =" + t2);
            temp2 = Convert.ToString(t2);
            Console.WriteLine("T2 =" + temp2);
            T2_TXT.Text = temp2;

            double t3 = SOLVE_3(fnc_res3, 323.15, 263.15, 0.00000005);
            t3 = Math.Round((t3 - 273.15), 4);
            temp3 = Convert.ToString(t3);
            T3_TXT.Text = temp3;

            pressure = Convert.ToString((Math.Round(meas_volt1*13.314-52.138,4)));
            pressure_txt.Text = pressure;
        }

        public void generateErrorMessage(string msg)
        {
            string caption = "Error";
            MessageBoxButtons button = MessageBoxButtons.OK;
            var alert = MessageBox.Show(msg,caption,button);

            if(alert == DialogResult.OK)
            {
                return;
            }            
        }

        public void runScript(MLApp.MLApp matlab)
        {

            double start = Convert.ToDouble(start_freq);
            Console.WriteLine("Start freq = " + start);
            double stop = Convert.ToDouble(stop_freq);
            Console.WriteLine("Stop freq = " + stop);


            string address1 = Workbook.CellAddress(10, 2);
            string address2 = Workbook.CellAddress(1610, dataCol);
            string rawDataRange = string.Format(address1 + ":" + address2);  //raw data sheet range


            address1 = Workbook.CellAddress(8, 2);
            address2 = Workbook.CellAddress(8, dataCol);
            string qRange = string.Format(address1 + ":" + address2);  //Q and center freq sheet range


            address1 = Workbook.CellAddress(8, 2);
            address2 = Workbook.CellAddress(8, dataCol);
            string centFreqRange = string.Format(address1 + ":" + address2);


            Workbook.workbook.Save();
            string path = "C:\\Usersadmin\\Desktop\\Seawater_Files\\";
            string excel_file = string.Format("{0}{1}.xlsx", pathname.Text, filename.Text);
            Console.WriteLine("XXXXXXX");
            Console.WriteLine(excel_file);


            // Change to the directory where the MATLAB function is located 
            matlab.Execute(@path);
            matlab.PutWorkspaceData("data_file", "base", excel_file);
            matlab.PutWorkspaceData("start", "base", start);
            matlab.PutWorkspaceData("stop", "base", stop);
            matlab.PutWorkspaceData("range1", "base", rawDataRange);
            matlab.PutWorkspaceData("range2", "base", qRange);
            matlab.PutWorkspaceData("range3", "base", centFreqRange);



            //Run the SVD curve fitting method
            Console.WriteLine(matlab.Execute("[Q_L, Q_NA, f_res, f_NA] = Seawater_SVD_New(data_file, start, stop, range1, range2, range3)"));
            Thread.Sleep(3000);


            //Obtain the returned values from the workspace
            Q_L = getQ(matlab)[0];
            Q_NA = getQ(matlab)[1];
            f_res = getQ(matlab)[2];
            f_NA = getQ(matlab)[3];

            double[,] qLArray = (double[,])Q_L;
           // double[,] qNAArray = (double[,])Q_L;
            double[,] fResArray = (double[,])f_res;
           // double[,] qLArray = (double[,])Q_L;

            int i = 1;
            
            foreach(double item in qLArray)
            {
                Workbook.addData(i, 2, item.ToString(), "Q_Loaded");
                i++;
            }

            i = 1;
            foreach (double item in fResArray)
            {
                Workbook.addData(i, 2, item.ToString(), "f_center");
                i++;
            }


        }



        public object[] getQ(MLApp.MLApp matlab)
        {
            object qL;
            object qNA;
            object fRes;
            object fNA;

            var test = matlab.GetVariable("Q_L", "base");
            Console.WriteLine("GET VARIABLE TEST");
            Console.WriteLine();

            matlab.GetWorkspaceData("Q_L", "base", out qL);
            matlab.GetWorkspaceData("Q_NA", "base", out qNA);
            matlab.GetWorkspaceData("f_res", "base", out fRes);
            matlab.GetWorkspaceData("f_NA", "base", out fNA);

            object[] result = { qL, qNA, fRes, fNA };
            return result;
        }



    }
}
