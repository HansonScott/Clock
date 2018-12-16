using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clock
{
    public partial class FormatPicker : Form
    {
        // tt is the AM/PM suffix
        private const string DefaultFormat = "hh:mm:ss tt";

        public string Format
        {
            get { return tbFormat.Text; }
            set { tbFormat.Text = value; }
        }
        
        public FormatPicker()
        {
            InitializeComponent();

            tbFormat.Text = DefaultFormat;
        }

        #region UI Controls
        private void rb12hr_CheckedChanged(object sender, EventArgs e)
        {
            cbAmPm.Enabled = rb12hr.Checked;
            ResetFormat();
        }
        private void rb24hr_CheckedChanged(object sender, EventArgs e)
        {
            cbAmPm.Enabled = !rb24hr.Checked;
            ResetFormat();
        }
        private void cbAmPm_CheckedChanged(object sender, EventArgs e)
        {
            ResetFormat();
        }
        private void cbSeconds_CheckedChanged(object sender, EventArgs e)
        {
            ResetFormat();
        }
        #endregion UI Controls

        private void ResetFormat()
        {
            string result;
            if(rb12hr.Checked)
            {
                result = "hh:mm";
            }
            else
            {
                result = "HH:mm";
            }

            if(cbSeconds.Checked)
            {
                result += ":ss";
            }

            if(rb12hr.Checked && cbAmPm.Checked)
            {
                result += " tt";
            }

            tbFormat.Text = result;
        }
    }
}
