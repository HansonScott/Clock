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

            // manually set the context menu names, so we can use the same values programatically elsewhere
            this.toolStripMenuItem1.Name = ToolStripMenuItem1Name;
            this.toolStripMenuItem2.Name = ToolStripMenuItem2Name;

            // get and display the version as part of the title
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            Text = $"Clock - {version}";
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// This is the main functional loop that causes the time to change on the form.  (Edit settings from the form designer)
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            // tt is the AM/PM suffix
            this.label1.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }
        /// <summary>
        /// This is the context menu click (right click) handler to open the context menu
        /// </summary>
        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // decide where we actually want the context menu to show up (at the cursor)
                Point p = new Point(e.Location.X + Location.X, e.Location.Y + Location.Y);

                this.contextMenuStrip1.Show(p);
            }
        }
        /// <summary>
        /// when any of the items are clicked in the context menu.  
        /// Could have separated them out, but with only two options, easier to handle in one function
        /// </summary>
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // manually hide the context menu as soon as the user clicks an item
            this.contextMenuStrip1.Hide();

            // use the visual text, since that's the constant value we set in this class, above
            switch(e.ClickedItem.Name)
            {
                case ToolStripMenuItem1Name:
                    // since there is a margin between the label and form, set both
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
        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            // update the font size to match the form size
            Font currentFont = this.label1.Font;
            float newSize = GetNewFontSize(this.label1.ClientSize);
            Font newFont = new Font(currentFont.FontFamily, newSize, currentFont.Style);
            this.label1.Font = newFont;
        }
        #endregion

        #region Private Functions
        /// <summary>
        /// Get the color they want, from the standard color dialog
        /// </summary>
        /// <param name="currentColor">This is the default to be returned if no new color is chosen</param>
        /// <returns>the chosen color, or the current one by default</returns>
        private Color GetColorFromPicker(Color currentColor)
        {
            // only pull the color if they ok'd it.
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                return this.colorDialog1.Color;
            }
            // or else return the default current color
            else return currentColor;
        }
        /// <summary>
        /// Calculate the proper font size from the available window size.
        /// </summary>
        /// <param name="labelSize">the parent container size we need to match</param>
        /// <returns>the font size to fit</returns>
        private float GetNewFontSize(Size labelSize)
        {
            // future: account for both height and text width

            // for now, a simple formula is close enough.
            return labelSize.Height / 2;
        }
        #endregion
    }
}
