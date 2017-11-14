namespace Seawater_Measurement
{
    partial class Initial_Setup
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
            this.NA_ID = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.NA_ID_Value = new System.Windows.Forms.Label();
            this.Without_Calibration = new System.Windows.Forms.Button();
            this.With_Calibration = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NA_ID
            // 
            this.NA_ID.Location = new System.Drawing.Point(37, 35);
            this.NA_ID.Name = "NA_ID";
            this.NA_ID.Size = new System.Drawing.Size(131, 48);
            this.NA_ID.TabIndex = 0;
            this.NA_ID.Text = "Obtain Network Analyzer ID";
            this.NA_ID.UseVisualStyleBackColor = true;
            this.NA_ID.Click += new System.EventHandler(this.NA_ID_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(197, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "NA ID#:";
            // 
            // NA_ID_Value
            // 
            this.NA_ID_Value.AutoSize = true;
            this.NA_ID_Value.Location = new System.Drawing.Point(260, 53);
            this.NA_ID_Value.Name = "NA_ID_Value";
            this.NA_ID_Value.Size = new System.Drawing.Size(10, 13);
            this.NA_ID_Value.TabIndex = 2;
            this.NA_ID_Value.Text = "-";
            // 
            // Without_Calibration
            // 
            this.Without_Calibration.Location = new System.Drawing.Point(247, 205);
            this.Without_Calibration.Name = "Without_Calibration";
            this.Without_Calibration.Size = new System.Drawing.Size(158, 100);
            this.Without_Calibration.TabIndex = 3;
            this.Without_Calibration.Text = "Anritsu MS4624D Network Analyzer";
            this.Without_Calibration.UseVisualStyleBackColor = true;
            this.Without_Calibration.Click += new System.EventHandler(this.Without_Calibration_Click);
            // 
            // With_Calibration
            // 
            this.With_Calibration.Location = new System.Drawing.Point(37, 205);
            this.With_Calibration.Name = "With_Calibration";
            this.With_Calibration.Size = new System.Drawing.Size(158, 100);
            this.With_Calibration.TabIndex = 4;
            this.With_Calibration.Text = "E5072A Agilent Network Analyzer";
            this.With_Calibration.UseVisualStyleBackColor = true;
            this.With_Calibration.Click += new System.EventHandler(this.With_Calibration_Click);
            // 
            // Initial_Setup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 373);
            this.Controls.Add(this.With_Calibration);
            this.Controls.Add(this.Without_Calibration);
            this.Controls.Add(this.NA_ID_Value);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NA_ID);
            this.Name = "Initial_Setup";
            this.Text = "Initial_Setup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button NA_ID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label NA_ID_Value;
        private System.Windows.Forms.Button Without_Calibration;
        private System.Windows.Forms.Button With_Calibration;
    }
}