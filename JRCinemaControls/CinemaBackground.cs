using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace JRCinemaControls
{
    public partial class CinemaBackground : UserControl
    {
        private Color primaryColor = Color.Silver;
        private Color secondaryColor = Color.DarkGray;
        private LinearGradientBrush brush = null;

        [Editor("Primary Color",typeof(Color))]
        public Color PrimaryColor
        {
            get { return primaryColor; }
            set
            {
                primaryColor = value;
                MakeBrush();
                this.Refresh();
            }
        }
        [Editor("Secondary Color", typeof(Color))]
        public Color SecondaryColor
        {
            get { return secondaryColor; }
            set
            {
                secondaryColor = value;
                MakeBrush();
                this.Refresh();
            }
        }
        public CinemaBackground()
        {
            InitializeComponent();
            MakeBrush();
        }
        ~CinemaBackground()
        {
            if (brush != null)
            {
                brush.Dispose();
                brush = null;
            }
        }
        private void MakeBrush()
        {
            if (brush != null)
            {
                brush.Dispose();
                brush = new LinearGradientBrush(this.ClientRectangle, primaryColor, secondaryColor, LinearGradientMode.Vertical);
            }
            else
                brush = new LinearGradientBrush(this.ClientRectangle, primaryColor, secondaryColor, LinearGradientMode.Vertical);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            MakeBrush();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Clear(Color.White);
            e.Graphics.FillRectangle(brush, this.ClientRectangle);
        }
    }
}
