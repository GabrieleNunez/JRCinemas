using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MonoTorrent;
using MonoTorrent.Common;
using MonoTorrent.Client;
using System.IO;
using System.Diagnostics;
namespace JRCinemaControls
{
    /// <summary>
    /// A control that wraps around MonoTorrent's TorrentManager control and manages it
    /// </summary>
    public partial class TorrentDownloadControl: UserControl
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

        public TorrentManager Torrent
        {
            get { return torrent; }
            set
            {
                if (torrent != null && torrent.State != TorrentState.Seeding)
                    torrent.Stop();
                torrent = value;
              
                downloadProgressBar.Value = 0;
                if (torrent.Torrent != null)
                {
                    downloadItemLabel.Text = torrent.Torrent.Name;
                    torrent.TorrentStateChanged += torrent_TorrentStateChanged;

                }
                else
                {
                    downloadItemLabel.Text = torrent.ToString();
                    torrent.TorrentStateChanged += torrent_TorrentStateChanged;
                }
            }
        }
        public ProgressBar ProgressBar { get { return downloadProgressBar; } }
        public double DownloadProgress
        {
            get
            {
                return torrent.Progress;
            }
        }
        public string ItemLabel
        {
            get { return downloadItemLabel.Text; }
            set { downloadItemLabel.Text = value; }
        }
      
        public TorrentDownloadControl()
        {
            InitializeComponent();
            stateHandler = stateChanged;
            handler = UpdateProgress;
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += timer_Tick;
        }
        /// <summary>
        /// Watches state changes and alters appearance of control visually to represent state changes
        /// </summary>
        private void stateChanged()
        {
            if (torrent.State == TorrentState.Seeding)
                downloadItemLabel.ForeColor = Color.DarkGreen;
            else if (torrent.State == TorrentState.Downloading)
                downloadItemLabel.ForeColor = Color.DarkBlue;
            else if (torrent.State == TorrentState.Paused)
                downloadItemLabel.ForeColor = Color.DarkOrange;
            else if (torrent.State == TorrentState.Stopped)
                downloadItemLabel.ForeColor = Color.DarkRed;
            else if (torrent.State == TorrentState.Hashing)
                downloadItemLabel.ForeColor = Color.OrangeRed;
            else if (torrent.State == TorrentState.Stopping)
                downloadItemLabel.ForeColor = Color.Black;
            else if (torrent.State == TorrentState.Error)
                downloadItemLabel.ForeColor = Color.Yellow;
            else
                downloadItemLabel.ForeColor = Color.Black;
            statelabel.Text = "State: " + torrent.State.ToString();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                this.Invoke(handler);
            }
            catch { }
        }
        private void torrent_TorrentStateChanged(object sender, TorrentStateChangedEventArgs e)
        {
            try
            {
                this.Invoke(stateHandler);
            }
            catch { }
           
        }
        private void playRequested(string file) { }
        /// <summary>
        /// Starts the TorrentManager and all timers associated with the control
        /// </summary>
        public void Start()
        {
          
            torrent.Start();
            statelabel.Text = "State: " + torrent.State;
            timer.Start();
            
        }
        /// <summary>
        /// Stops the TorrentManager and any timers associated with the control
        /// </summary>
        public void Stop()
        {
            torrent.Stop();
            timer.Stop();
            downloadSpeedLabel.Text = "Download Speed:";
            uploadSpeedLabel.Text = "Upload Speed:";
            peersLabel.Text = "Leeches: 0";
            seedersLabel.Text = "Seeders: 0";
            if (torrent.Complete)
                downloadProgressBar.Value = downloadProgressBar.Maximum;
        }
        /// <summary>
        /// Open the location of the video file
        /// </summary>
        public void OpenLocation()
        {
            Process process = new Process();
            string filePath = "";
            foreach (TorrentFile file in torrent.Torrent.Files)
            {
                string extension = Path.GetExtension(file.FullPath);
                extension = extension.ToLower();
                if (extension == ".mp4" || extension == ".m2ts" || extension == ".avi" ||
                    extension == ".m4v" || extension == ".mkv")
                {
                    filePath = file.FullPath;
                    break;
                }
            }
            process.StartInfo.Arguments = Path.GetDirectoryName(filePath);
            process.StartInfo.FileName = "explorer.exe";
            process.Start();
        }
        /// <summary>
        /// Invokes the play video file event
        /// </summary>
        public void Play()
        {
            string fullPath = "";
            foreach (TorrentFile file in torrent.Torrent.Files)
            {
                string extension = Path.GetExtension(file.FullPath);
                extension = extension.ToLower();
                if (extension == ".mp4" || extension == ".m2ts" || extension == ".avi" ||
                    extension == ".m4v" || extension == ".mkv")
                {
                    fullPath = file.FullPath;
                    break;
                }
            }
            if (string.IsNullOrEmpty(fullPath) == false)
                PlayRequested.Invoke(fullPath);
        }
        /// <summary>
        /// Convert bytes per second to short hand versions
        /// </summary>
        /// <param name="bytes">The amount of bytes to alter</param>
        /// <returns>A string that is a short hand version of the amount of bytes passed</returns>
        private string ShortMemory(long bytes)
        {

            if (bytes > 0)
            {
                if (bytes < KILOBYTE)
                    return string.Format("{0} B/s", bytes);
                else if (bytes < MEGABYTE)
                    return string.Format("{0}KB/s", bytes / KILOBYTE);
                else if (bytes > MEGABYTE)
                    return string.Format("{0}MB/s", (float)((float)bytes / (float)MEGABYTE));
                else
                    return bytes.ToString();
            }
            else
                return "";
        }
        /// <summary>
        /// Converts a int based time to human readable time estimates
        /// </summary>
        /// <param name="time">The value of time</param>
        /// <returns>A short hand version of the time passed</returns>
        private string ToTime(long time)
        {
            if (time < MINUTE)
                return string.Format("{0} seconds", time);
            else if (time < HOUR)
                return string.Format("{0} minutes", Math.Round((time / MINUTE), 2));
            else if (time < DAY)
                return string.Format("{0} hours", Math.Round((time / HOUR), 2));
            else
                return string.Format("{0} days", Math.Round((time / DAY), 2));
        }
        /// <summary>
        /// Updates an eta,download and upload speed, as well as if the torrent is done or not
        /// </summary>
        private void UpdateProgress()
        {
            if (torrent.Torrent != null)
            {
                int progress = (int)(torrent.Progress);
                if (progress > downloadProgressBar.Maximum)
                    progress = downloadProgressBar.Maximum;

                downloadProgressBar.Value = progress;
                downloadSpeedLabel.Text = "Download Speed: " + ShortMemory(torrent.Monitor.DownloadSpeed);
                uploadSpeedLabel.Text = "Upload Speed: " + ShortMemory(torrent.Monitor.UploadSpeed);
                peersLabel.Text = "Leeches: " + torrent.Peers.Leechs;
                seedersLabel.Text = "Seeders: " + torrent.Peers.Seeds.ToString();
              
                if (torrent.Monitor.DataBytesDownloaded > 0)
                {
                    try
                    {
                       
                        long eta = (long)((torrent.Torrent.Size - torrent.Monitor.DataBytesDownloaded) / torrent.Monitor.DownloadSpeed);
                        etaLabel.Text = "Eta: " + ToTime(eta);
                    }
                    catch { }
                }
                else
                    etaLabel.Text = "Unknown...";
                if (torrent.Complete)
                {
                    etaLabel.Text = "Done...";
                    playButton.Enabled = true;
                }
            }
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            string fullPath = "";
            foreach (TorrentFile file in torrent.Torrent.Files)
            {
                string extension = Path.GetExtension(file.FullPath);
                extension = extension.ToLower();
                if (extension == ".mp4" || extension == ".m2ts" || extension == ".avi" ||
                    extension == ".m4v" || extension == ".mkv")
                {
                    fullPath = file.FullPath;
                    break;
                }
            }
            if(string.IsNullOrEmpty(fullPath) == false)
                PlayRequested.Invoke(fullPath);
        }

        
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Stop();
            foreach (TorrentFile file in torrent.Torrent.Files)
            {
                try
                {
                    if (file.BytesDownloaded > 0)
                        File.Delete(file.FullPath);
                }
                catch { }
            }
            try
            {
                File.Delete(torrent.Torrent.TorrentPath);
            }
            catch { }

            this.Cursor = Cursors.Arrow;
            this.Dispose(true);
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenLocation();
        }
    }
}
