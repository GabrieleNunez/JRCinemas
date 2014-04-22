using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MonoTorrent.Client;
using MonoTorrent.Common;

namespace JRCinemaControls
{
    public partial class CinemaMovie : UserControl
    {
        delegate void SimpleHandler();
        public delegate void PlayRequestedHandler(string file);
        private TorrentManager torrent = null;
        private Timer timer;
        private SimpleHandler handler;
        private SimpleHandler stateHandler;
        public event PlayRequestedHandler PlayRequested;

        private const int KILOBYTE = 1024; // 1024 bytes in a kilobyte
        private const int MEGABYTE = 1048576; //1048576 bytes 
        private const decimal MINUTE = 60.00m; // 60 seconds in an hour
        private const decimal HOUR = 3600.00m; // 3600 seconds in an hour
        private const decimal DAY = 86400.00m; // 86400 seconds in a day

        public TorrentManager StandardTorrent
        {
            get { return torrent; }
            set
            {
                if (torrent != null && torrent.State != TorrentState.Seeding)
                    torrent.Stop();
                torrent = value;
            }
        }
        
        public string MoviePictureUrl
        {
            get { return moviePictureBox.ImageLocation; }
            set
            {
                moviePictureBox.ImageLocation = value;
            }
        }
        public string MovieDescription
        {
            get { return descriptionLabel.Text; }
            set { descriptionLabel.Text = value; }
        }
        public bool WantsHighQuality { get { return highQualityRadioButton.Checked; } }
        public bool WantsStandardQuality { get { return standardQualityRadioButton.Checked; } }
        public object MovieObject { get; set; }
        public CinemaMovie()
        {
            InitializeComponent();
        }
    }
}
