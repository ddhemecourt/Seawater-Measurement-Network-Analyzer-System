namespace Seawater_Measurement
{
    partial class Without_Calibration_e5072a
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numPoints = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.IFBWtxt = new System.Windows.Forms.ComboBox();
            this.Substance = new System.Windows.Forms.ComboBox();
            this.TubeNumber = new System.Windows.Forms.TextBox();
            this.InputPower = new System.Windows.Forms.TextBox();
            this.ExpDay = new System.Windows.Forms.TextBox();
            this.Temp = new System.Windows.Forms.TextBox();
            this.ExpLastName = new System.Windows.Forms.TextBox();
            this.AvgFactor = new System.Windows.Forms.TextBox();
            this.AvgPeriodCyc = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ExecuteMeasurment = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.counter_txt = new System.Windows.Forms.Label();
            this.pressure_txt = new System.Windows.Forms.Label();
            this.DwnldRawData = new System.Windows.Forms.CheckBox();
            this.RST = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.NA_ILTXT = new System.Windows.Forms.Label();
            this.NA_QValTXT = new System.Windows.Forms.Label();
            this.NA_bwTXT = new System.Windows.Forms.Label();
            this.NA_CenterFreqTXT = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.T3_TXT = new System.Windows.Forms.Label();
            this.T2_TXT = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.StopMeas = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.filename = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.pathname = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numPoints);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.IFBWtxt);
            this.groupBox1.Controls.Add(this.Substance);
            this.groupBox1.Controls.Add(this.TubeNumber);
            this.groupBox1.Controls.Add(this.InputPower);
            this.groupBox1.Controls.Add(this.ExpDay);
            this.groupBox1.Controls.Add(this.Temp);
            this.groupBox1.Controls.Add(this.ExpLastName);
            this.groupBox1.Controls.Add(this.AvgFactor);
            this.groupBox1.Controls.Add(this.AvgPeriodCyc);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(417, 516);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Set By User";
            // 
            // numPoints
            // 
            this.numPoints.FormattingEnabled = true;
            this.numPoints.Items.AddRange(new object[] {
            "1601",
            "801",
            "401"});
            this.numPoints.Location = new System.Drawing.Point(238, 121);
            this.numPoints.Name = "numPoints";
            this.numPoints.Size = new System.Drawing.Size(155, 21);
            this.numPoints.TabIndex = 22;
            this.numPoints.Text = "1601";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(6, 119);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(174, 20);
            this.label21.TabIndex = 21;
            this.label21.Text = "Number of Data Points:";
            // 
            // IFBWtxt
            // 
            this.IFBWtxt.FormattingEnabled = true;
            this.IFBWtxt.Items.AddRange(new object[] {
            "10",
            "30",
            "100",
            "300",
            "1K",
            "10K"});
            this.IFBWtxt.Location = new System.Drawing.Point(238, 180);
            this.IFBWtxt.Name = "IFBWtxt";
            this.IFBWtxt.Size = new System.Drawing.Size(155, 21);
            this.IFBWtxt.TabIndex = 20;
            this.IFBWtxt.Text = "300";
            // 
            // Substance
            // 
            this.Substance.FormattingEnabled = true;
            this.Substance.Items.AddRange(new object[] {
            "Methanol 2002",
            "Methanol 2008",
            "Seawater 10.007",
            "Seawater 30.002",
            "Seawater 34.997",
            "Seawater 38.274",
            "Seawater 9.926 p"});
            this.Substance.Location = new System.Drawing.Point(238, 281);
            this.Substance.Name = "Substance";
            this.Substance.Size = new System.Drawing.Size(155, 21);
            this.Substance.TabIndex = 19;
            // 
            // TubeNumber
            // 
            this.TubeNumber.Location = new System.Drawing.Point(238, 485);
            this.TubeNumber.Name = "TubeNumber";
            this.TubeNumber.Size = new System.Drawing.Size(155, 20);
            this.TubeNumber.TabIndex = 18;
            this.TubeNumber.Text = "1";
            // 
            // InputPower
            // 
            this.InputPower.Location = new System.Drawing.Point(238, 436);
            this.InputPower.Name = "InputPower";
            this.InputPower.Size = new System.Drawing.Size(155, 20);
            this.InputPower.TabIndex = 17;
            this.InputPower.Text = "-5";
            // 
            // ExpDay
            // 
            this.ExpDay.Location = new System.Drawing.Point(238, 380);
            this.ExpDay.Name = "ExpDay";
            this.ExpDay.Size = new System.Drawing.Size(155, 20);
            this.ExpDay.TabIndex = 16;
            this.ExpDay.Text = "1";
            // 
            // Temp
            // 
            this.Temp.Location = new System.Drawing.Point(238, 330);
            this.Temp.Name = "Temp";
            this.Temp.Size = new System.Drawing.Size(155, 20);
            this.Temp.TabIndex = 15;
            this.Temp.Text = "23";
            // 
            // ExpLastName
            // 
            this.ExpLastName.Location = new System.Drawing.Point(238, 232);
            this.ExpLastName.Name = "ExpLastName";
            this.ExpLastName.Size = new System.Drawing.Size(155, 20);
            this.ExpLastName.TabIndex = 13;
            this.ExpLastName.Text = "dd";
            // 
            // AvgFactor
            // 
            this.AvgFactor.Location = new System.Drawing.Point(238, 69);
            this.AvgFactor.Name = "AvgFactor";
            this.AvgFactor.Size = new System.Drawing.Size(155, 20);
            this.AvgFactor.TabIndex = 11;
            this.AvgFactor.Text = "8";
            // 
            // AvgPeriodCyc
            // 
            this.AvgPeriodCyc.Location = new System.Drawing.Point(238, 24);
            this.AvgPeriodCyc.Name = "AvgPeriodCyc";
            this.AvgPeriodCyc.Size = new System.Drawing.Size(155, 20);
            this.AvgPeriodCyc.TabIndex = 10;
            this.AvgPeriodCyc.Text = "70";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 484);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 20);
            this.label9.TabIndex = 9;
            this.label9.Text = "Tube Number:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 435);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(132, 20);
            this.label8.TabIndex = 8;
            this.label8.Text = "Input Power (dB):";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 379);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 20);
            this.label7.TabIndex = 7;
            this.label7.Text = "Experiment Day #:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 329);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 20);
            this.label6.TabIndex = 6;
            this.label6.Text = "Temperature (C):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 283);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "Substance:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 231);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(222, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Experimentor (last name only):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "IF Bandwidth:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Average Factor:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Average Period of Cycle:";
            // 
            // ExecuteMeasurment
            // 
            this.ExecuteMeasurment.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExecuteMeasurment.Location = new System.Drawing.Point(12, 534);
            this.ExecuteMeasurment.Name = "ExecuteMeasurment";
            this.ExecuteMeasurment.Size = new System.Drawing.Size(119, 58);
            this.ExecuteMeasurment.TabIndex = 1;
            this.ExecuteMeasurment.Text = "Execute Measurment";
            this.ExecuteMeasurment.UseVisualStyleBackColor = true;
            this.ExecuteMeasurment.Click += new System.EventHandler(this.ExecuteMeasurment_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(354, 534);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 20);
            this.label10.TabIndex = 2;
            this.label10.Text = "Counter:";
            // 
            // counter_txt
            // 
            this.counter_txt.AutoSize = true;
            this.counter_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.counter_txt.Location = new System.Drawing.Point(380, 563);
            this.counter_txt.Name = "counter_txt";
            this.counter_txt.Size = new System.Drawing.Size(18, 20);
            this.counter_txt.TabIndex = 3;
            this.counter_txt.Text = "0";
            // 
            // pressure_txt
            // 
            this.pressure_txt.AutoSize = true;
            this.pressure_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pressure_txt.Location = new System.Drawing.Point(235, 162);
            this.pressure_txt.Name = "pressure_txt";
            this.pressure_txt.Size = new System.Drawing.Size(14, 20);
            this.pressure_txt.TabIndex = 5;
            this.pressure_txt.Text = "-";
            // 
            // DwnldRawData
            // 
            this.DwnldRawData.AutoSize = true;
            this.DwnldRawData.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DwnldRawData.Location = new System.Drawing.Point(95, 34);
            this.DwnldRawData.Name = "DwnldRawData";
            this.DwnldRawData.Size = new System.Drawing.Size(191, 24);
            this.DwnldRawData.TabIndex = 7;
            this.DwnldRawData.Text = "Download Raw Data";
            this.DwnldRawData.UseVisualStyleBackColor = true;
            // 
            // RST
            // 
            this.RST.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RST.Location = new System.Drawing.Point(269, 534);
            this.RST.Name = "RST";
            this.RST.Size = new System.Drawing.Size(79, 58);
            this.RST.TabIndex = 8;
            this.RST.Text = "System Reset";
            this.RST.UseVisualStyleBackColor = true;
            this.RST.Click += new System.EventHandler(this.RST_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.NA_ILTXT);
            this.groupBox2.Controls.Add(this.NA_QValTXT);
            this.groupBox2.Controls.Add(this.NA_bwTXT);
            this.groupBox2.Controls.Add(this.NA_CenterFreqTXT);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Location = new System.Drawing.Point(435, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(411, 192);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Network Analyzer Output";
            // 
            // NA_ILTXT
            // 
            this.NA_ILTXT.AutoSize = true;
            this.NA_ILTXT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NA_ILTXT.Location = new System.Drawing.Point(235, 142);
            this.NA_ILTXT.Name = "NA_ILTXT";
            this.NA_ILTXT.Size = new System.Drawing.Size(14, 20);
            this.NA_ILTXT.TabIndex = 7;
            this.NA_ILTXT.Text = "-";
            // 
            // NA_QValTXT
            // 
            this.NA_QValTXT.AutoSize = true;
            this.NA_QValTXT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NA_QValTXT.Location = new System.Drawing.Point(235, 103);
            this.NA_QValTXT.Name = "NA_QValTXT";
            this.NA_QValTXT.Size = new System.Drawing.Size(14, 20);
            this.NA_QValTXT.TabIndex = 6;
            this.NA_QValTXT.Text = "-";
            // 
            // NA_bwTXT
            // 
            this.NA_bwTXT.AutoSize = true;
            this.NA_bwTXT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NA_bwTXT.Location = new System.Drawing.Point(235, 65);
            this.NA_bwTXT.Name = "NA_bwTXT";
            this.NA_bwTXT.Size = new System.Drawing.Size(14, 20);
            this.NA_bwTXT.TabIndex = 5;
            this.NA_bwTXT.Text = "-";
            // 
            // NA_CenterFreqTXT
            // 
            this.NA_CenterFreqTXT.AutoSize = true;
            this.NA_CenterFreqTXT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NA_CenterFreqTXT.Location = new System.Drawing.Point(235, 31);
            this.NA_CenterFreqTXT.Name = "NA_CenterFreqTXT";
            this.NA_CenterFreqTXT.Size = new System.Drawing.Size(14, 20);
            this.NA_CenterFreqTXT.TabIndex = 4;
            this.NA_CenterFreqTXT.Text = "-";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(38, 142);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(113, 20);
            this.label15.TabIndex = 3;
            this.label15.Text = "Insertion Loss:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(38, 65);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(88, 20);
            this.label14.TabIndex = 2;
            this.label14.Text = "Bandwidth:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(38, 103);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 20);
            this.label13.TabIndex = 1;
            this.label13.Text = "Q Value:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(38, 31);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(140, 20);
            this.label12.TabIndex = 0;
            this.label12.Text = "Center Frequency:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.T3_TXT);
            this.groupBox3.Controls.Add(this.T2_TXT);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.pressure_txt);
            this.groupBox3.Location = new System.Drawing.Point(435, 210);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(411, 215);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "DMM Output";
            // 
            // T3_TXT
            // 
            this.T3_TXT.AutoSize = true;
            this.T3_TXT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.T3_TXT.Location = new System.Drawing.Point(235, 105);
            this.T3_TXT.Name = "T3_TXT";
            this.T3_TXT.Size = new System.Drawing.Size(14, 20);
            this.T3_TXT.TabIndex = 10;
            this.T3_TXT.Text = "-";
            // 
            // T2_TXT
            // 
            this.T2_TXT.AutoSize = true;
            this.T2_TXT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.T2_TXT.Location = new System.Drawing.Point(235, 46);
            this.T2_TXT.Name = "T2_TXT";
            this.T2_TXT.Size = new System.Drawing.Size(14, 20);
            this.T2_TXT.TabIndex = 9;
            this.T2_TXT.Text = "-";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(38, 160);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(76, 20);
            this.label19.TabIndex = 4;
            this.label19.Text = "Pressure:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(38, 104);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(176, 20);
            this.label18.TabIndex = 3;
            this.label18.Text = "Room Temperature (C):";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(38, 46);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(175, 20);
            this.label17.TabIndex = 2;
            this.label17.Text = "Cavity Temperature (C):";
            // 
            // StopMeas
            // 
            this.StopMeas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StopMeas.Location = new System.Drawing.Point(137, 534);
            this.StopMeas.Name = "StopMeas";
            this.StopMeas.Size = new System.Drawing.Size(126, 58);
            this.StopMeas.TabIndex = 11;
            this.StopMeas.Text = "Stop Measurment";
            this.StopMeas.UseVisualStyleBackColor = true;
            this.StopMeas.Click += new System.EventHandler(this.StopMeas_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.filename);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.pathname);
            this.groupBox4.Controls.Add(this.DwnldRawData);
            this.groupBox4.Location = new System.Drawing.Point(435, 431);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(411, 161);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Saving Options";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(6, 119);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(78, 20);
            this.label20.TabIndex = 11;
            this.label20.Text = "Filename:";
            // 
            // filename
            // 
            this.filename.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filename.Location = new System.Drawing.Point(92, 119);
            this.filename.Name = "filename";
            this.filename.Size = new System.Drawing.Size(313, 26);
            this.filename.TabIndex = 10;
            this.filename.Text = "Test";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(6, 75);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 20);
            this.label11.TabIndex = 9;
            this.label11.Text = "Pathname:";
            // 
            // pathname
            // 
            this.pathname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pathname.Location = new System.Drawing.Point(92, 71);
            this.pathname.Name = "pathname";
            this.pathname.Size = new System.Drawing.Size(313, 26);
            this.pathname.TabIndex = 8;
            this.pathname.Text = "C:\\Users\\admin\\Desktop\\Seawater_Files\\";
            // 
            // Without_Calibration_e5072a
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 604);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.StopMeas);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.RST);
            this.Controls.Add(this.counter_txt);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.ExecuteMeasurment);
            this.Controls.Add(this.groupBox1);
            this.Name = "Without_Calibration_e5072a";
            this.Text = "Without_Calibration";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox Substance;
        private System.Windows.Forms.TextBox TubeNumber;
        private System.Windows.Forms.TextBox InputPower;
        private System.Windows.Forms.TextBox ExpDay;
        private System.Windows.Forms.TextBox Temp;
        private System.Windows.Forms.TextBox ExpLastName;
        private System.Windows.Forms.TextBox AvgFactor;
        private System.Windows.Forms.TextBox AvgPeriodCyc;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ExecuteMeasurment;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label counter_txt;
        private System.Windows.Forms.Label pressure_txt;
        private System.Windows.Forms.CheckBox DwnldRawData;
        private System.Windows.Forms.Button RST;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label NA_ILTXT;
        private System.Windows.Forms.Label NA_QValTXT;
        private System.Windows.Forms.Label NA_bwTXT;
        private System.Windows.Forms.Label NA_CenterFreqTXT;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label T3_TXT;
        private System.Windows.Forms.Label T2_TXT;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox IFBWtxt;
        private System.Windows.Forms.Button StopMeas;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox filename;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox pathname;
        private System.Windows.Forms.ComboBox numPoints;
        private System.Windows.Forms.Label label21;
    }
}