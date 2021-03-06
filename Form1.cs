﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
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
        const string ToolStripMenuItem3Name = "Digital Format";
        const string ToolStripMenuItem4Name = "analogClockToolStripMenuItem";
        const string ToolStripMenuItem5Name = "digitalClockToolStripMenuItem";
        const string ToolStripMenuItem6Name = "toolStripMenuItem4";

        // make this a public property so it can be bound to the text box in the format control
        public string ClockFormat
        {
            get { return ThisPicker.Format;}
        }

        private FormatPicker ThisPicker = null;

        private ClockType CurrentClockType = ClockType.Digital;

        enum ClockType
        {
            Analog = 0,
            Digital = 1,
        }

        #region Analog Clock Variables
        Point CenterPoint;
        Rectangle clockRect;
        int DisplayDiameter;
        int ClockDiameter;
        int ClockRadius;
        double NotchRadius;
        double HourhandRadius;
        double MinutehandRadius;

        float hourNotchThickness;
        float HourHandWidth;
        float minuteThickness;

        float ClockEdgeThickness = 3;
        Color ClockBackColor = Color.Black;
        Color ClockEdgeColor = Color.DarkGray;
        Brush ClockBackBrush;
        Brush ClockEdgeBrush;
        #endregion
        #endregion

        #region Constructor
        public Form1()
        {
            InitializeComponent();

            // manually set the context menu names, so we can use the same values programatically elsewhere
            this.toolStripMenuItem1.Name = ToolStripMenuItem1Name;
            this.toolStripMenuItem2.Name = ToolStripMenuItem2Name;
            this.toolStripMenuItem3.Name = ToolStripMenuItem3Name;

            // get and display the version as part of the title
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            Text = $"Clock - {version}";

            ThisPicker = new FormatPicker();

            ClockBackBrush = new SolidBrush(ClockBackColor);
            ClockEdgeBrush = new SolidBrush(ClockEdgeColor);
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// This is the main functional loop that causes the time to change on the form.  (Edit settings from the form designer)
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateDisplay(CurrentClockType);
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
                case ToolStripMenuItem1Name: // back color
                    // since there is a margin between the label and form, set both
                    this.label1.BackColor = GetColorFromPicker(this.label1.BackColor);
                    this.BackColor = this.label1.BackColor;
                    break;
                case ToolStripMenuItem2Name: // forecolor
                    this.label1.ForeColor = GetColorFromPicker(this.label1.ForeColor);
                    this.ClockEdgeColor = this.label1.ForeColor;
                    this.ClockEdgeBrush = new SolidBrush(ClockEdgeColor);
                    break;
                case ToolStripMenuItem3Name: // format
                    HandleChangeFormat();
                    break;
                case ToolStripMenuItem4Name: // Analog Clock
                    CurrentClockType = ClockType.Analog;
                    break;
                case ToolStripMenuItem5Name: // Digital Clock
                    CurrentClockType = ClockType.Digital;
                    break;
                case ToolStripMenuItem6Name:
                    this.ClockBackColor = GetColorFromPicker(this.ClockBackColor);
                    this.ClockBackBrush = new SolidBrush(ClockBackColor);
                    break;
                default:
                    break;
            }

            // no matter what was chosen, go ahead and refresh to it shows immediately.
            this.Refresh();
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            // update the font size to match the form size
            Font currentFont = this.label1.Font;
            float newSize = GetNewFontSize(this.label1.ClientSize);
            Font newFont = new Font(currentFont.FontFamily, newSize, currentFont.Style);
            this.label1.Font = newFont;

            // this helps the analog clock redraw
            this.Refresh();
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
        /// <summary>
        /// when the user wants to change the format, this function provides the UI
        /// </summary>
        private void HandleChangeFormat()
        {
            ThisPicker.ShowDialog();
        }
        /// <summary>
        /// Updates the display, based on the current clock type
        /// </summary>
        /// <param name="currentClockType"></param>
        private void UpdateDisplay(ClockType currentClockType)
        {
            switch (currentClockType)
            {
                case ClockType.Analog:
                    this.label1.Visible = false;

                    break;
                case ClockType.Digital:
                default:
                    this.label1.Visible = true;
                    this.label1.Text = DateTime.Now.ToString(ClockFormat);
                    break;
            }

            this.Refresh();
        }
        /// <summary>
        /// Overridden this OnPaint so we can manually draw our analog clock
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if(CurrentClockType == ClockType.Analog)
            {
                DrawClock(DateTime.Now, e.Graphics);
            }
        }
        /// <summary>
        /// use the current time to draw the analog clock piece by piece.
        /// </summary>
        /// <param name="stamp">The current time to render</param>
        /// <param name="g">the graphics object on which to draw the clock</param>
        private void DrawClock(DateTime stamp, Graphics g)
        {
            #region Reset Variables
            CenterPoint = new Point(this.DisplayRectangle.Width / 2, this.DisplayRectangle.Height / 2);

            clockRect = new Rectangle();
            DisplayDiameter = Math.Min(this.DisplayRectangle.Width, this.DisplayRectangle.Height);
            ClockDiameter = (int)(DisplayDiameter * 0.9);
            ClockRadius = ClockDiameter / 2;
            NotchRadius = ClockRadius * 0.90;
            HourhandRadius = ClockRadius * 0.5;
            MinutehandRadius = ClockRadius * 0.9;

            hourNotchThickness = (float)(DisplayDiameter * 0.02);
            HourHandWidth = (float)(DisplayDiameter * 0.02);
            minuteThickness = (float)(DisplayDiameter * 0.01);

            clockRect.Height = ClockDiameter;
            clockRect.Width = ClockDiameter;
            clockRect.X = CenterPoint.X - ClockRadius;
            clockRect.Y = CenterPoint.Y - ClockRadius;

            ClockEdgeThickness = (float)(DisplayDiameter * 0.02);
            #endregion

            // draw background circle
            g.FillEllipse(ClockBackBrush, clockRect);
            // draw edge circle
            g.DrawEllipse(new Pen(ClockEdgeBrush, ClockEdgeThickness), clockRect);

            #region draw hour notches
            double degreesOfTime, unitDegreesOfTime;
            double rads;
            double cr, sr;
            double startX, stopX, startY, stopY;

            for (double hourMark = 0.0; hourMark < 12.0; hourMark ++)
            {
                degreesOfTime = hourMark * 30;
                unitDegreesOfTime = 90 - degreesOfTime;
                rads = (unitDegreesOfTime) / 180.0;

                // then, to find the x and y, use cos/sin
                cr = Math.Cos(rads * Math.PI);
                sr = Math.Sin(rads * Math.PI);

                // orient based on unit circle
                if (rads > 0.5 && rads < 1.5)
                    cr = -cr; 
                if (rads > 1)
                    sr = -sr; 

                startX = cr * NotchRadius;
                stopX = cr * ClockRadius;
                startY = sr * NotchRadius;
                stopY = sr * ClockRadius;

                g.DrawLine(new Pen(ClockEdgeBrush, hourNotchThickness),
                            (int)(CenterPoint.X + startX), (int)(CenterPoint.Y - startY),
                            (int)(CenterPoint.X + stopX), (int)(CenterPoint.Y - stopY));

            }
            #endregion

            // draw labels?

            #region draw hour hand
            double h = stamp.Hour;
            // NOTE: include partial hour too
            double m = ((double)stamp.Minute / 60.0);

            // each hour is 30 degrees
            double d = (h + m) * 30;

            // convert to rads
            double dr = d / 180;

            // adjust for unit circle starting at 3 O'clock
            dr = 0.5 - dr;

            // then, to find the x and y, use cos/sin
            double cd = Math.Cos(dr * Math.PI);
            double sd = Math.Sin(dr * Math.PI);

            if (dr > 0.5 && dr < 1.5) { cd = -cd; }
            if (dr > 1) { sd = -sd; }

            double XRightFromCenter = cd * HourhandRadius;
            double YUpFromCenter = sd * HourhandRadius;

            g.DrawLine(new Pen(ClockEdgeBrush, HourHandWidth), 
                        CenterPoint.X, CenterPoint.Y, 
                        (int)(CenterPoint.X + XRightFromCenter), (int)(CenterPoint.Y - YUpFromCenter));
            #endregion

            #region draw minute hand
            // convert minutes to degrees
            double md = stamp.Minute * 6;

            // convert to unit degrees
            double umd = 90 - md;

            // convert degrees to rads
            double mrads = umd / 180;

            // then, to find the x and y, use cos/sin
            double crm = Math.Cos(mrads * Math.PI);
            double srm = Math.Sin(mrads * Math.PI);

            // orient based on unit circle
            if (mrads > 0.5 && mrads < 1.5) { crm = -crm; }
            if (mrads > 1) { srm = -srm; }

            double stopXm = crm * MinutehandRadius;
            double stopYm = srm * MinutehandRadius;

            g.DrawLine(new Pen(ClockEdgeBrush, minuteThickness),
                        (int)(CenterPoint.X), (int)(CenterPoint.Y),
                        (int)(CenterPoint.X + stopXm), (int)(CenterPoint.Y - stopYm));
            #endregion

            // draw center circle
            float centerSize = ClockEdgeThickness * 2;
            g.FillEllipse(ClockEdgeBrush,
                        CenterPoint.X - centerSize / 2,
                        CenterPoint.Y - centerSize / 2,
                        centerSize,
                        centerSize);

        }

        #endregion

        private void Form1_Resize(object sender, EventArgs e)
        {
            // update the font size to match the form size
            Font currentFont = this.label1.Font;
            float newSize = GetNewFontSize(this.label1.ClientSize);
            Font newFont = new Font(currentFont.FontFamily, newSize, currentFont.Style);
            this.label1.Font = newFont;

            // this helps the analog clock redraw
            this.Refresh();
        }
    }
}
