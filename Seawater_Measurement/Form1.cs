using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ivi.Visa.Interop;
using NationalInstruments.NI4882;
using MLApp;
//using NationalInstruments.Common;

namespace Seawater_Measurement
{
    public partial class Form1 : Form
    {
        Device device;


        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            device = new Device(0, 6, 0);
            //board = new Board(0);

            // device.Write("*RST;*WAI;");
            device.Write("CH1;DSP;MAG;");
            // device.Write("S11;");
            // device.Write("LOGM;");


            //string output = device.ReadString();
            //Console.WriteLine(output);


        }

        private void NA_Controls_Click(object sender, EventArgs e)
        {
            Initial_Setup frm = new Initial_Setup();
            frm.Show();
            this.Hide();
        }

    }
}
