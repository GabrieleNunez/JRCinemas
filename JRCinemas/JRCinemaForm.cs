using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using JRCinemas.Providers;
using System.Diagnostics;
using System.IO;
using MonoTorrent.Client;
using JRCinemaControls;
namespace JRCinemas
{
    public partial class JRCinemaForm : Form
    {

        private bool keywordSearch = true;
        private TorrentClient torrentClient;
        private bool isMagnet = true;
        private string torrentPath = "";
        
        public JRCinemaForm()
        {
            InitializeComponent();
        }
     
        protected override void OnLoad(EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.Text = "JRCinemas (Beta) " + Application.ProductVersion.ToString();
            internalEngineRadioButton.Checked = GlobalSettings.UseInternal;
            externalEngineRadioButton.Checked = !GlobalSettings.UseInternal;
            incomingPortNumeric.Value = GlobalSettings.IncomingPort;
            globalMaxConnNumeric.Value = GlobalSettings.GlobalMaxConnections;
            perTorrentMaxNumeric.Value = GlobalSettings.TorrentMaxConnections;
            dhtCheckBox.Checked = GlobalSettings.UseDht;
            peerExchangeCheckBox.Checked = GlobalSettings.UsePeerExchange;
            encryptionCheckBox.Checked = GlobalSettings.UseEncryption;
            externalClientPathTextBox.Text = GlobalSettings.ExternalTorrentClient;
            externalClientArgsTextbox.Text = GlobalSettings.ExternalTorrentClientArgs;
            externalSettingsGroupBox.Enabled = !GlobalSettings.UseInternal;
            internalSettingsGroupBox.Enabled = GlobalSettings.UseInternal;

            torrentClient = new TorrentClient();
            torrentClient.Initiate();

            foreach (TorrentManager torrent in torrentClient.Torrents)
                CreateTorrentControl(torrent);

            this.Cursor = Cursors.Arrow;
            base.OnLoad(e);
        }
        private void CreateTorrentControl(TorrentManager torrent)
        {
            TorrentDownloadControl control = new TorrentDownloadControl();
            control.Torrent = torrent;
            downloadFlowlayout.Controls.Add(control);
            control.PlayRequested += PlayRequested;
            control.Start();
        }
        private void PlayRequested(string file)
        {
            Process process = new Process();
            process.StartInfo.Arguments = Path.GetDirectoryName(file);
            process.StartInfo.FileName = "explorer.exe";
            process.Start();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            foreach (Control control in downloadFlowlayout.Controls)
            {
                if(control is TorrentDownloadControl)
                    ((TorrentDownloadControl)control).Stop();
            }
            torrentClient.Shutdown();
            GlobalSettings.Save();
            base.OnFormClosing(e);
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResizeEnd(e);
            jrCinemaTabControl.Width = this.ClientSize.Width;
            jrCinemaTabControl.Height = this.ClientSize.Height;
            
        }
        
        private void ClearResults()
        {
            this.Cursor = Cursors.WaitCursor;
            resultListBox.Items.Clear();
            resultListBox.Refresh();
            this.Cursor = Cursors.Arrow;
        }

        private void Search()
        {
            ClearResults();
            this.Cursor = Cursors.WaitCursor;
            if (string.IsNullOrEmpty(searchTextBox.Text) == false)
            {
                try
                {
                    YIFYListedMovies movies = null;
                    if (keywordSearch)
                        movies = YIFYProvider.GetMovieList(searchTextBox.Text);
                    else
                        movies = YIFYProvider.GetMovieList("50",
                            null, null, null, null, searchTextBox.Text,null, null);
                    if (movies != null && movies.MovieList != null)
                    {
                        if (movies.MovieList.Length > 0)
                        {
                            foreach (YIFYListedMovie movie in movies.MovieList)
                                resultListBox.Items.Add(movie);
                        }
                        else
                            throw new Exception("No Results");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            this.Cursor = Cursors.Arrow;
        }
        private void SeachUpcoming()
        {
            this.Cursor = Cursors.WaitCursor;
            YIFYUpcomingMovie[] movies = YIFYProvider.GetUpcomingMovies();
            //TODO need to finish method SearchUpcoming
            //only gets the movies but does nothing with them
            this.Cursor = Cursors.Arrow;
        }
        private void searchButton_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void resultListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (youtubeLinkLabel.Links.Count > 0)
                {
                    youtubeLinkLabel.Links.RemoveAt(0);
                    youtubeLinkLabel.Links.Clear();
                }
                if (imdbLinkLabel.Links.Count > 0)
                {
                    imdbLinkLabel.Links.RemoveAt(0);
                    imdbLinkLabel.Links.Clear();
                }
                if (((YIFYListedMovie)resultListBox.SelectedItem) != null)
                {
                    int movieId = ((YIFYListedMovie)resultListBox.SelectedItem).MovieID;
                    YIFYMovie movie = YIFYProvider.GetMovieDetail(movieId.ToString());
                    if (movie != null)
                    {
                        searchImageBox.LoadAsync(movie.LargeCover);
                        descriptionTextBox.Text = movie.LongDescription;

                        movieTitleLabel.Text = "Title:" + movie.MovieTitleClean;
                        resolutionLabel.Text = "Resolution:" + movie.Resolution;
                        qualityLabel.Text = "Quality:" + movie.Quality;
                        framerateLabel.Text = "Framerate:" + movie.Framerate;
                        languageLabel.Text = "Language:" + movie.Language;
                        fileSizeLabel.Text = "File Size:" + movie.Size;
                        seedersLabel.Text = "Seeders:" + movie.TorrentSeeds;
                        peersLabel.Text = "Peers:" + movie.TorrentPeers;
                        subtitlesLabel.Text = "Subtitles:" + movie.Subtitles;
                        dateAddedLabel.Text = "Dated Added:" + movie.DateUploaded.ToString();
                        timesDownloadedLabel.Text = "Times Downloaded:" + movie.Downloaded;
                        movieRuntimeLabel.Text = "Movie Runtime:" + movie.MovieRuntime + " minutes";
                        youtubeLinkLabel.Text = "Youtube Link: " + movie.YoutubeTrailerUrl;
                        genreLabel.Text = "Genres: " + movie.Genre1 + "," + movie.Genre2;

                        LinkLabel.Link youtubeLink = new LinkLabel.Link();
                        youtubeLink.Description = movie.YoutubeTrailerUrl;
                        youtubeLinkLabel.Links.Add(youtubeLink);
                        imdbLinkLabel.Text = "IMDB Link: " + movie.ImdbLink;

                        LinkLabel.Link imdbLink = new LinkLabel.Link();
                        imdbLink.Description = movie.ImdbLink;
                        imdbLinkLabel.Links.Add(imdbLink);
                        torrentPath = movie.TorrentUrl;
                        isMagnet = false;
                    }
                }
            }
            catch (UnableToFindMovieException ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Arrow;
        }
        
        private void linkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Process.Start(e.Link.Description);
                e.Link.Visited = true;
            }
        }

        
        private void clearResultsButton_Click(object sender, EventArgs e)
        {
            ClearResults();
        }

        private void downloadButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            TorrentManager manager = null;
            downloadButton.Enabled = false;
            manager = torrentClient.AddTorrent(torrentPath, true);
            CreateTorrentControl(manager);
            jrCinemaTabControl.SelectedIndex = 1;
            downloadButton.Enabled = true;
            this.Cursor = Cursors.Arrow;
        }

        private void searchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search();
            }
        }

        private void keywordSearchRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            keywordSearch = keywordSearchRadioButton.Checked;
        }

        private void genreSearchRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            keywordSearch = keywordSearchRadioButton.Checked;
        }

        private void globalMaxConnNumeric_ValueChanged(object sender, EventArgs e)
        {
            GlobalSettings.GlobalMaxConnections = (int)globalMaxConnNumeric.Value;
        }

        private void perTorrentMaxNumeric_ValueChanged(object sender, EventArgs e)
        {
            GlobalSettings.TorrentMaxConnections = (int)perTorrentMaxNumeric.Value;
        }

        private void dhtCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GlobalSettings.UseDht = dhtCheckBox.Checked;
        }

        private void incomingPortNumeric_ValueChanged(object sender, EventArgs e)
        {
            GlobalSettings.IncomingPort = (int)incomingPortNumeric.Value;
        }

        private void encryptionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GlobalSettings.UseEncryption = encryptionCheckBox.Checked;
        }

        private void peerExchangeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GlobalSettings.UsePeerExchange = peerExchangeCheckBox.Checked;
        }

        private void internalEngineRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            GlobalSettings.UseInternal = internalEngineRadioButton.Checked;
            internalSettingsGroupBox.Enabled = GlobalSettings.UseInternal;
        }

        private void externalEngineRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            GlobalSettings.UseInternal = !externalEngineRadioButton.Checked;
            externalSettingsGroupBox.Enabled = !GlobalSettings.UseInternal;
        }

        private void browseExternalClientButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    GlobalSettings.ExternalTorrentClient = externalClientPathTextBox.Text;
                }
            }
        }

        private void externalClientArgsTextbox_TextChanged(object sender, EventArgs e)
        {
            GlobalSettings.ExternalTorrentClientArgs = externalClientPathTextBox.Text;
        }

        private void latestMoviesButton_Click(object sender, EventArgs e)
        {
            //TODO actually implement this one
        }
    }
}
