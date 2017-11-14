namespace Seawater_Measurement
{
    partial class MLDataAnalysis
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
            this.label1 = new System.Windows.Forms.Label();
            this.file = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numberOfAvg = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.runButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.resultPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.resultFileName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.sheetAnalysis = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfAvg)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 48);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Raw Data File:";
            // 
            // file
            // 
            this.file.Location = new System.Drawing.Point(141, 48);
            this.file.Name = "file";
            this.file.Size = new System.Drawing.Size(538, 26);
            this.file.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 207);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sheet to Analyze:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 262);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(255, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Number of Data Points to Average:";
            // 
            // numberOfAvg
            // 
            this.numberOfAvg.Location = new System.Drawing.Point(285, 260);
            this.numberOfAvg.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numberOfAvg.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numberOfAvg.Name = "numberOfAvg";
            this.numberOfAvg.Size = new System.Drawing.Size(45, 26);
            this.numberOfAvg.TabIndex = 6;
            this.numberOfAvg.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(685, 48);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 26);
            this.button1.TabIndex = 7;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(141, 324);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(164, 66);
            this.runButton.TabIndex = 8;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(413, 324);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(164, 66);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.HelpRequest += new System.EventHandler(this.folderBrowserDialog1_HelpRequest);
            // 
            // resultPath
            // 
            this.resultPath.Location = new System.Drawing.Point(141, 99);
            this.resultPath.Name = "resultPath";
            this.resultPath.Size = new System.Drawing.Size(538, 26);
            this.resultPath.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 99);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Result File Path:";
            // 
            // resultFileName
            // 
            this.resultFileName.Location = new System.Drawing.Point(141, 149);
            this.resultFileName.Name = "resultFileName";
            this.resultFileName.Size = new System.Drawing.Size(538, 26);
            this.resultFileName.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 149);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "Result Filename:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(685, 99);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 26);
            this.button2.TabIndex = 14;
            this.button2.Text = "Browse";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // sheetAnalysis
            // 
            this.sheetAnalysis.FormattingEnabled = true;
            this.sheetAnalysis.Items.AddRange(new object[] {
            "Q_Unloaded_Sheet",
            "Q_Loaded_Sheet",
            "NA_Q_Sheet",
            "NA_Center_Sheet",
            "SVD_Center_Sheet"});
            this.sheetAnalysis.Location = new System.Drawing.Point(155, 205);
            this.sheetAnalysis.Name = "sheetAnalysis";
            this.sheetAnalysis.Size = new System.Drawing.Size(250, 28);
            this.sheetAnalysis.TabIndex = 15;
            this.sheetAnalysis.Text = "Q_Unloaded_Sheet";
            // 
            // MLDataAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 475);
            this.Controls.Add(this.sheetAnalysis);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.resultFileName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.resultPath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.numberOfAvg);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.file);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MLDataAnalysis";
            this.Text = "MLDataAnalysis";
            this.Load += new System.EventHandler(this.MLDataAnalysis_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numberOfAvg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox file;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numberOfAvg;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox resultPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox resultFileName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox sheetAnalysis;
    }
}