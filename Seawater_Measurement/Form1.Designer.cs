namespace Seawater_Measurement
{
    partial class Form1
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
            this.NA_Controls = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NA_Controls
            // 
            this.NA_Controls.Location = new System.Drawing.Point(64, 49);
            this.NA_Controls.Name = "NA_Controls";
            this.NA_Controls.Size = new System.Drawing.Size(148, 100);
            this.NA_Controls.TabIndex = 0;
            this.NA_Controls.Text = "Network Analyzer: Microwave Controls";
            this.NA_Controls.UseVisualStyleBackColor = true;
            this.NA_Controls.Click += new System.EventHandler(this.NA_Controls_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 205);
            this.Controls.Add(this.NA_Controls);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button NA_Controls;
    }
}

