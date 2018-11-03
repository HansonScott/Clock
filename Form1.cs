using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clock
{
    public partial class Form1 : Form
    {
        #region Main
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        #endregion

        #region Fields and Properties
        const string ToolStripMenuItem1Name = "Back Color";
        const string ToolStripMenuItem2Name = "Text Color";
        #endregion

        #region Constructor
        public Form1()
        {
            InitializeComponent();

            this.toolStripMenuItem1.Name = ToolStripMenuItem1Name;
            this.toolStripMenuItem2.Name = ToolStripMenuItem2Name;

            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            Text = $"Clock - {version}";
        }
        #endregion

        #region Event Handlers
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label1.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }
        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStrip1.Show(new Point(e.Location.X + Location.X, e.Location.Y + Location.Y));
            }
        }
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch(e.ClickedItem.Name)
            {
                case ToolStripMenuItem1Name:
                    this.label1.BackColor = GetColorFromPicker(this.label1.BackColor);
                    this.BackColor = this.label1.BackColor;
                    break;
                case ToolStripMenuItem2Name:
                    this.label1.ForeColor = GetColorFromPicker(this.label1.ForeColor);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Private Functions
        private Color GetColorFromPicker(Color currentColor)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                return this.colorDialog1.Color;
            }
            else return currentColor;
        }
        #endregion
    }
}
