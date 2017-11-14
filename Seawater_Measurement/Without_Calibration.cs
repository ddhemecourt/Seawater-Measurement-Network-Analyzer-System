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
    public partial class Without_Calibration : Form
    {

        //DECLARE VARIABLES

        [DllImport("kernel32.dll")]

        public static extern uint SetThreadExecutionState(uint esFlags);

        public const uint ES_CONTINUOUS = 0x80000000;

        public const uint ES_SYSTEM_REQUIRED = 0x00000001;

        public const uint ES_DISPLAY_REQUIRED = 0x00000002;

        //Declare network analyzer and digital multimeter
        Device NA = new Device(0, 6, 0);
        Device DMM = new Device(0,16,0);
        Device PS = new Device(0, 5, 0);

        //Declare new excel workbook
        excel_doc Workbook;

        int cnt; //Counts measurment cycles
        int prev;

        const int ARRAYSIZE = 90000; //length of raw NA data


        double totalDataCount;

        string inputPower;

        //Variables that control the temperature measurement cycle
        int tempCnt;
        int tempRow;
        int tempDiv;


        //Variables that control the overall measurment cycle
        int dataCnt;
        int dataCol;
        double dataNum;
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

        string data12;

        //Temperature variables
        string temp1;
        string temp2;
        string temp3;

        //pressure variables
        string pressure;

        string start_freq;
        string stop_freq;
        string span;                //freq range of samples


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

        object Q_UL;
        object Q_L;
        object f_center;
        object s21;

        //f(R) variables for temperature calculation
        double fnc_res1;
        double fnc_res2;
        double fnc_res3;

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
        int j;

        string IFBW;
        string numberOfPoints;

        Stopwatch overallTime;


        double T_Upper, T_Lower, T_Accept;


        string fnc_count_text;
        int fnc_count;

        //Timestamps for time subtraction to allow continuous time axes in charts
        double sampleStartTime;
        double sampleEndTime;



        DateTime start;
        DateTime now;

        //MAIN FUNCTION

        public Without_Calibration()
        {
            InitializeComponent();

            SetThreadExecutionState(

               ES_CONTINUOUS | ES_SYSTEM_REQUIRED | ES_DISPLAY_REQUIRED);

            Q_L = null;
            Q_UL = null;

            totalDataCount = 0;

            StopMeas.Enabled = false;
           // matlabDataAnalysis.Enabled = false;


            // The following coefficients are for temperature greater or equal to
            // 0.0 degrees Celsius to +70.0 degrees Celsius
            overallTime = new Stopwatch();
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

            j = 2;
            while (Workbook.rawData.Cells[10,j].Value2 != null)
            {
                string address1 = Workbook.CellAddress(10, j);
                string address2 = Workbook.CellAddress(1608, j);
                string address = string.Format(address1 + ":" + address2);
                Console.WriteLine("Column address = " + address);
                runScript(matlab,address);
                Workbook.addData(j/2, 2, Q_L.ToString(), "Q_Loaded");
                Workbook.addData(j / 2, 3, Q_L.ToString(), "SVD vs NA Diff");
                Workbook.addData(j / 2, 2, Q_UL.ToString(), "Q_Unloaded");
                Workbook.addData(j / 2, 2, f_center.ToString(), "f_center");
                Workbook.addData(j / 2, 3, f_center.ToString(), "svd_na_center");
                Workbook.addData(j / 2, 2, s21.ToString(), "s21");


                matlab.Execute("clear");
                j = j + 2;

            }


                Workbook.generateChart(dataCol / 2);
                Workbook.workbook.Save();

            ExecuteMeasurment.Enabled = true;
            matlabDataAnalysis.Enabled = true;

            Status.Text = "Done";
            Status.Refresh();
            

        }
        

        private void addGeneralInfo()
        {
            Workbook.addData(1, 1, "Experimentor Last Name:", "gen_info");
            Workbook.addData(1, 2, ExpLastName.Text, "gen_info");
            Workbook.addData(3, 1, "Number of Sweep Averages:", "gen_info");
            Workbook.addData(3, 2, AvgFactor.Text, "gen_info");
            Workbook.addData(5, 1, "Number of Data Points:", "gen_info");
            Workbook.addData(5, 2, numberOfPoints, "gen_info");
            Workbook.addData(7, 1, "IF Bandwidth:", "gen_info");
            Workbook.addData(7, 2, IFBW, "gen_info");
            Workbook.addData(9, 1, "Substance:", "gen_info");
            Workbook.addData(9, 2, Substance.Text, "gen_info");
            Workbook.addData(11, 1, "Experiment Date:", "gen_info");
            Workbook.addData(11, 2, ExpDate.Text, "gen_info");
            Workbook.addData(13, 1, "Tube Number:", "gen_info");
            Workbook.addData(13, 2, TubeNumber.Text, "gen_info");
            



            Workbook.general_info.Rows.AutoFit();
            Workbook.general_info.Columns.AutoFit();


        }



        private void ExecuteMeasurment_Click(object sender, EventArgs e)
        {

            if (checkFileExists() == true)
            {
                generateErrorMessage("Filename Already Exists");
                return;
            }


            if(ExpLastName.Text != "" && TubeNumber.Text != "" && Substance.Text != "" && filename.Text != "")
            {
                
                StopMeas.Enabled = true;
                ExecuteMeasurment.Enabled = false;

                Workbook = new excel_doc();
                Workbook.createDoc();
                Workbook.app.ScreenUpdating = false;
                addGeneralInfo();
                Workbook.addData(2, 1, "Time:", "raw_data");
                Workbook.addData(3, 1, "Cavity Temp:", "raw_data");
                Workbook.addData(4, 1, "Room Temp:", "raw_data");
                Workbook.addData(5, 1, "NA Calc Center:", "raw_data");
                Workbook.addData(6, 1, "NA Calc BW:", "raw_data");
                Workbook.addData(7, 1, "NA Calc IL:", "raw_data");
                Workbook.addData(8, 1, "NA Calc Q:", "raw_data");
                Workbook.addData(9, 1, "Data Set:", "raw_data");
                dataCol = 2;

                Q_L = null;
                Q_UL = null;

                string path = string.Format("{0}{1}.xlsx", pathname.Text, filename.Text);
                Workbook.workbook.SaveAs(path);
                start = DateTime.Now;
                NA.Write("NPX?;");
                numberOfPoints = NA.ReadString();
                NA.Write("IFBWX?;");
                IFBW = NA.ReadString();
                Workbook.addData(5, 1, "Number of Data Points:", "gen_info");
                Workbook.addData(5, 2, numberOfPoints, "gen_info");
                Workbook.addData(7, 1, "IF Bandwidth:", "gen_info");
                Workbook.addData(7, 2, IFBW, "gen_info");

                Workbook.addData(15, 1, "Input Power (dB):", "gen_info");
                NA.Write("PWR?;");
                inputPower = NA.ReadString();
                Workbook.addData(15, 2, inputPower, "gen_info");
                inputPowerLabel.Text = inputPower;
                inputPowerLabel.Refresh();



                startMeasurment();
            }

            else
            {
                generateErrorMessage("Fill in all required fields.");
            }



        }

        private bool checkFileExists()
        {
            string path = pathname.Text + filename.Text + ".xlsx";
            Console.WriteLine("Path = " + path);
            if (File.Exists(@path))
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        private void startMeasurment()
        {
            fnc_count = 250;

            NA.Write("SWAVG;");
            NA.Write("RSTAVG;");
            NA.Write("AVG "+AvgFactor.Text+" XX1;");



            NA.Write("IFBWX?;");
            string test = NA.ReadString(ARRAYSIZE);
            Console.WriteLine(test);


            NA.Write("FMA;");
            NA.Write("FMT0;");
            NA.Write("AOF;");
            NA.Write("AON;");


            DMM.Write("*RST;");
   
            cnt = 1;
            tempRow = 2;

            NA.Write("SRT?;*WAI;");
            start_freq = NA.ReadString();
            Workbook.addData(17, 1, "Start Frequency:", "gen_info");
            Workbook.addData(17, 2, start_freq, "gen_info");

            NA.Write("STP?;*WAI;");
            stop_freq = NA.ReadString();
            Workbook.addData(19, 1, "Stop Frequency:", "gen_info");
            Workbook.addData(19, 2, stop_freq, "gen_info");

            NA.Write("SPAN?;*WAI;");
            span = NA.ReadString();
            Workbook.addData(21, 1, "Frequency Span:", "gen_info");
            Workbook.addData(21, 2, span, "gen_info");

            NA.Write("P1P?;*WAI;");
            string avgP1 = NA.ReadString();
            Workbook.addData(23, 1, "Average P1 Power:", "gen_info");
            Workbook.addData(23, 2, avgP1, "gen_info");




            measure();
        }

        private void measure()
        {

            dataCnt = 0;
            prev = 0;
            dataNum = 15;
            TimeNum = dataNum;

            overallTime.Start(); 

            timer1.Interval = 1000;
            timer1.Enabled = true;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dataCnt = dataCnt + 1;

            if(dataCnt == 15)
            {
                dataCnt = 0;
                Status.Text = "Measurement In Progress";
                Status.Refresh();
                Thread.Sleep(1000);
                microwaveDataRetreival();
                dataCol = dataCol + 2;

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
            
           // Thread.Sleep(500);
           // Setup NA for measurment & call a full sweep command 
            NA.IOTimeout = TimeoutValue.T1000s;

            NA.Write("S21;");
            NA.Write("CH2;");
            NA.Write("DSP;");
            NA.Write("MAG;");
            NA.Write("FMT0;");
            NA.Write("FDH2;");
            NA.Write("TRS;");
            NA.Write("WFS;");

            
            //Add to Raw Data file

                Console.WriteLine("HERE");
                Stopwatch time = Stopwatch.StartNew();
                NA.Write("DPR0;OFD;");
                data12 = NA.ReadString(ARRAYSIZE);
                now = DateTime.Now;
                TimeSpan timespan = now.Subtract(start);
                string currentTime = Convert.ToString(timespan);
                Workbook.addData(2, dataCol, currentTime, "raw_data");
                Excel.Range wrksht_rng = Workbook.rawData.Cells[2, dataCol];
                wrksht_rng.NumberFormat = "[h]:mm:ss;@";

                Workbook.addData(dataCol/2, 1, currentTime, "q_val");
                wrksht_rng = Workbook.qVal.Cells[dataCol / 2, 1];
                wrksht_rng.NumberFormat = "[h]:mm:ss;@";

                Workbook.addData(dataCol/2, 1, currentTime, "cent_freq");
                wrksht_rng = Workbook.centFreq.Cells[dataCol / 2, 1];
                wrksht_rng.NumberFormat = "[h]:mm:ss;@";

                Workbook.addData(dataCol/2, 1, currentTime, "cavT");
                wrksht_rng = Workbook.cavityTemp.Cells[dataCol / 2, 1];
                wrksht_rng.NumberFormat = "[h]:mm:ss;@";

                Workbook.addData(dataCol/2, 1, currentTime, "roomT");
                wrksht_rng = Workbook.roomTemp.Cells[dataCol / 2, 1];
                wrksht_rng.NumberFormat = "[h]:mm:ss;@";

                Workbook.addData(dataCol/2, 1, currentTime, "Q_Loaded");
                wrksht_rng = Workbook.Q_L.Cells[dataCol / 2, 1];
                wrksht_rng.NumberFormat = "[h]:mm:ss;@";

                Workbook.addData(dataCol / 2, 1, currentTime, "SVD vs NA Diff");
                wrksht_rng = Workbook.svd_na.Cells[dataCol / 2, 1];
                wrksht_rng.NumberFormat = "[h]:mm:ss;@";

                Workbook.addData(dataCol / 2, 1, currentTime, "Q_Unloaded");
                wrksht_rng = Workbook.Q_UL.Cells[dataCol / 2, 1];
                wrksht_rng.NumberFormat = "[h]:mm:ss;@";

                Workbook.addData(dataCol / 2, 1, currentTime, "f_center");
                wrksht_rng = Workbook.f_center.Cells[dataCol / 2, 1];
                wrksht_rng.NumberFormat = "[h]:mm:ss;@";

                Workbook.addData(dataCol / 2, 1, currentTime, "s21");
                wrksht_rng = Workbook.s21.Cells[dataCol / 2, 1];
                wrksht_rng.NumberFormat = "[h]:mm:ss;@";

                Workbook.addData(dataCol / 2, 1, currentTime, "svd_na_center");
                wrksht_rng = Workbook.svd_na_center.Cells[dataCol / 2, 1];
                wrksht_rng.NumberFormat = "[h]:mm:ss;@";

                time.Stop();
                Console.WriteLine("Time to run = " + time.Elapsed);
                Console.WriteLine(data12);
                NA.Clear();

                
                string[] data = data12.Split(',');
                string[] dataOut = new string[data.Length];  //String array of output data formatted into decimal numbers

               


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
                            Workbook.addData(j + 10, dataCol, Convert.ToString(value), "raw_data");
                        }
                    }
                    catch
                    {
                        continue;
                    }


                }



            

            Status.Text = "Recording Data and Measuring Temperature";


            double magnitude;
            double power;
            double val;

            //To Calculate Q
            NA.Write("FMKR;BWLS 3 DB;SD1;DR1;MSRM;DSQ1;");
            NA.Write("OM2;");
            mark2 = NA.ReadString();

            NA.Write("FLTC?;");             //Get freq
            centerFreq = NA.ReadString(256);
            NA_CenterFreqTXT.Text = centerFreq;

            string[] c = centerFreq.Split('E');
            magnitude = Convert.ToDouble(c[0]);
            power = Convert.ToDouble(c[1]);
            val = magnitude;
            centerFreq = Convert.ToString(val);

            Workbook.addData(5, dataCol, centerFreq, "raw_data");
            Workbook.addData(dataCol/2, 2, centerFreq, "cent_freq");
            Workbook.addData(dataCol / 2, 2, centerFreq, "svd_na_center");
            Console.WriteLine(centerFreq);

            NA.Write("FLTBW?;");            //Get BW
            bandwidth = NA.ReadString();
            NA_bwTXT.Text = bandwidth;
            Workbook.addData(6, dataCol, bandwidth, "raw_data");
            Console.WriteLine(bandwidth);


            NA.Write("FLTL?;");              //Get insertion loss
            ILValue = NA.ReadString();
            NA_ILTXT.Text = ILValue;
            Workbook.addData(7, dataCol, ILValue, "raw_data");

            NA.Write("FLTQ?;");             //Q Factor
            markerQ = NA.ReadString();
            NA_QValTXT.Text = markerQ;

            string[] q = markerQ.Split('E');
            magnitude = Convert.ToDouble(q[0]);
            power = Convert.ToDouble(q[1]);
            val = magnitude * Math.Pow(10, power);
            markerQ = Convert.ToString(val);

            Workbook.addData(8, dataCol, markerQ, "raw_data");
            Workbook.addData(dataCol/2, 2, markerQ, "q_val");
            Workbook.addData(dataCol / 2, 2, markerQ, "SVD vs NA Diff");
            Console.WriteLine(markerQ);

            //Record temp with the sampled data
            Workbook.addData(3, dataCol, T2_TXT.Text, "raw_data");
            Workbook.addData(4, dataCol, T3_TXT.Text, "raw_data");
            Workbook.addData(dataCol/2,2, T2_TXT.Text,"cavT");
            Workbook.addData(dataCol/2, 2, T3_TXT.Text, "roomT");

            totalDataCount++;
            dataCountTotal.Text = Convert.ToString(totalDataCount);

        }

        private void tempMeas()
        {

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
            t2 = Math.Round((t2 - 273.15), 4);
            temp2 = Convert.ToString(t2);
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
   
     
        public void runScript(MLApp.MLApp matlab, string range)
        {
            
            double start = Convert.ToDouble(start_freq);
            Console.WriteLine("Start freq = " + start);
            double stop = Convert.ToDouble(stop_freq);
            Console.WriteLine("Stop freq = " + stop);
            double bw = Convert.ToDouble(span);
            Console.WriteLine("Bandwidth = " + bw);


            Workbook.workbook.Save();
            string path = pathname.Text;
            string excel_file = pathname.Text + filename.Text + ".xlsx";

            // Change to the directory where the MATLAB function is located 
            matlab.Execute(@path);
            matlab.PutWorkspaceData("data_file", "base", excel_file);
            matlab.PutWorkspaceData("start", "base", start);
            matlab.PutWorkspaceData("stop", "base", stop);
            matlab.PutWorkspaceData("span", "base", bw);
            matlab.PutWorkspaceData("range", "base", range);

            //Run the SVD curve fitting method
            Console.WriteLine(matlab.Execute("[Q_L,Q_UL,f_center,s21] = original_expression_SVD(data_file,start,stop,span,range)"));
            Thread.Sleep(3000);


            //Obtain the returned values from the workspace
            Q_L = getQ(matlab)[0];
            Q_UL = getQ(matlab)[1];
            f_center = getQ(matlab)[2];
            s21 = getQ(matlab)[3];

        }


        
        public object[] getQ(MLApp.MLApp matlab)
        {
            object qUL;
            object qL;
            object f_center;
            object s21;

            matlab.GetWorkspaceData("Q_L", "base", out qL);
            matlab.GetWorkspaceData("Q_UL", "base", out qUL);
            matlab.GetWorkspaceData("f_center", "base", out f_center);
            matlab.GetWorkspaceData("s21", "base", out s21);

            object[] result = { qL, qUL,f_center,s21 };
            return result;
        }


        private void matlabDataAnalysis_Click(object sender, EventArgs e)
        {
            string path = pathname.Text;
            string excel_file = pathname.Text + filename.Text + ".xlsx";
            string resultFile = filename.Text + "_Result.xlsx";

            MLDataAnalysis ml = new MLDataAnalysis(excel_file,path,resultFile);
            ml.Show();

        }
    }
}
