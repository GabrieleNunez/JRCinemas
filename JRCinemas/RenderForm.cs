using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JRCinemas
{
    public partial class RenderForm : Form
    {
        private string utorrentPath;
        private TorrentClient torrentClient;
        private bool isMagnet = true;
        private string torrentPath = "";
        public RenderForm()
        {
            InitializeComponent();
            
        }
        ~RenderForm()
        {

        }
        
    }
}
