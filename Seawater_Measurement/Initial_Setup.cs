using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NationalInstruments.NI4882;

namespace Seawater_Measurement
{
    public partial class Initial_Setup : Form
    {

        Device NA;

        public Initial_Setup()
        {
            InitializeComponent();
        }

        private void NA_ID_Click(object sender, EventArgs e)
        {
            try
            {
                NA = new Device(0, 6, 0);
                NA.Write("*IDN?;");
                string ID = NA.ReadString();
                NA_ID_Value.Text = ID;
            }
            catch
            {
                NA = new Device(0, 17, 0);
                NA.Write("*IDN?;");
                string ID = NA.ReadString();
                NA_ID_Value.Text = ID;
            }
        }

        private void Without_Calibration_Click(object sender, EventArgs e)
        {
            Without_Calibration frm = new Without_Calibration();
            frm.Show();
        }

        private void With_Calibration_Click(object sender, EventArgs e)
        {
            Without_Calibration_e5072a frm = new Without_Calibration_e5072a();
            frm.Show();
        }
    }
}
