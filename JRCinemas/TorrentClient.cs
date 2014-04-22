using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoTorrent;
using MonoTorrent.Dht;
using MonoTorrent.Client;
using System.Net;
using System.Windows.Forms;
using System.IO;
using MonoTorrent.Common;
using MonoTorrent.Dht.Listeners;
using MonoTorrent.BEncoding;

namespace JRCinemas
{
    /// <summary>
    /// TorrentClient class that will wrap the functionality of MonoTorrent framework into a easy to use class.
    /// Sealed for no inheritance should only be one of these running around for sake of simplicity. 
    /// </summary>
    public sealed class TorrentClient
    {
        
        private ClientEngine torrentEngine = null;
        private EngineSettings engineSettings = null;
        private TorrentSettings torrentSettings = null;
        private DhtEngine dhtEngine;
        private DhtListener dhtListener;
        private BEncodedDictionary fastResume;
        
        private string saveDirectory;
        private string metaSaveDirectory;
        private string dhtNodeFile;
        private string resumeFile;

        /// <summary>
        /// Gets or Sets a Directory where we are saving all torrents and downloaded data
        /// </summary>
        public string SaveDirectory
        {
            get { return saveDirectory; }
            set { saveDirectory = value; }
        }
        /// <summary>
        /// Gets a  list of Torrents in use by the Torrent Engine
        /// </summary>
        public IList<TorrentManager> Torrents
        {
            get { return torrentEngine.Torrents; }
        }
        /// <summary>
        /// Gets or Sets the engine settings that power the torrent engine
        /// </summary>
        public EngineSettings Settings
        {
            get { return engineSettings; }
            set { engineSettings = value; }
        }


        public int Port { get; set; }
      /// <summary>
      /// Initializes the Torrent client  and its basic core members
      /// </summary>
        public TorrentClient()
        {
            saveDirectory = GlobalSettings.DownloadDirectory;
            metaSaveDirectory = GlobalSettings.DEFAULT_METADATA_DIRECTORY;
            dhtNodeFile = GlobalSettings.DHT_FILE;
            resumeFile = GlobalSettings.RESUME_FILE;
            Port = GlobalSettings.IncomingPort;
            CreateDir(saveDirectory);
            CreateDir(metaSaveDirectory);

            //initialize engine settings
            engineSettings = new EngineSettings();
            engineSettings = new EngineSettings(saveDirectory, Port);
            engineSettings.AllowedEncryption = MonoTorrent.Client.Encryption.EncryptionTypes.All;
            engineSettings.PreferEncryption = GlobalSettings.UseEncryption;
            engineSettings.GlobalMaxConnections = GlobalSettings.GlobalMaxConnections;

            //initialize torrent settings
            torrentSettings = new TorrentSettings();
            torrentSettings.EnablePeerExchange = GlobalSettings.UsePeerExchange;
            torrentSettings.UseDht = GlobalSettings.UseDht;
            torrentSettings.MaxConnections = GlobalSettings.TorrentMaxConnections;
        }
        /// <summary>
        /// Destructor for TorrentClient
        /// Make sure we shutdown and save data if we're destroying the torrent client
        /// </summary>
        ~TorrentClient()
        {
            Shutdown();
        }
        /// <summary>
        /// Checks if the directory exist and if not creates one
        /// Useful for setting up required directories
        /// </summary>
        /// <param name="path">The Path to check for and or create</param>
        private void CreateDir(string path)
        {
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
        }
        /// <summary>
        /// Fully starts the torrent engine and dht engine and listener
        /// Loads in any torrents from the directory and resumes them automaticallhy
        /// </summary>
        public void Initiate()
        {
            torrentEngine = new ClientEngine(engineSettings);
            torrentEngine.ChangeListenEndpoint(new IPEndPoint(IPAddress.Any, Port));

            //Load DHT nodes from a saved file
            //if not there  or any errors then continue
            byte[] nodes = null;
            try
            {
                nodes = File.ReadAllBytes(dhtNodeFile);
            }
            catch { }

            //construct our listener for our dht engine and then create the dht engine
            //register it and then start it based on the nodes we attempted to load
            dhtListener = new DhtListener(new IPEndPoint(IPAddress.Any, Port));
            dhtEngine = new DhtEngine(dhtListener);
            torrentEngine.RegisterDht(dhtEngine);
            torrentEngine.DhtEngine.Start(nodes);

            //same as with the dht nodes try to load but if any errors then continue 
            try
            {
                fastResume = BEncodedValue.Decode<BEncodedDictionary>(File.ReadAllBytes(resumeFile));
            }
            catch
            {
                fastResume = new BEncodedDictionary();
            }
            
            //This is where we go into our save directory and look for any torrents
            //user may of left off on a download and we can possibly resume it
            //also a backend way to force a download from another  provider source to download
            foreach (string file in Directory.GetFiles(saveDirectory))
            {

                if (file.EndsWith(".torrent"))
                {
                    try
                    {
                        Torrent torrent = null;
                        torrent = Torrent.Load(file);
                        TorrentManager manager = new TorrentManager(torrent, saveDirectory, torrentSettings);
                        if (fastResume.ContainsKey(torrent.InfoHash.ToHex()))
                            manager.LoadFastResume(new FastResume((BEncodedDictionary)fastResume[torrent.InfoHash.ToHex()]));
                        torrentEngine.Register(manager);
                    }
                    catch { }

                }
            }

        }
        /// <summary>
        /// Shuts down all torrents and saves all dht and fastresume data
        /// </summary>
        public void Shutdown()
        {
            dhtListener.Stop();
            torrentEngine.DhtEngine.Stop();
            fastResume.Clear();
            for (int i = 0; i < Torrents.Count; i++)
            {
                Torrents[i].Stop();
                fastResume.Add(Torrents[i].Torrent.InfoHash.ToHex(), Torrents[i].SaveFastResume().Encode());
            }
            File.WriteAllBytes(dhtNodeFile, torrentEngine.DhtEngine.SaveNodes());
            File.WriteAllBytes(resumeFile, fastResume.Encode());
        }
        /// <summary>
        /// Adds a magnent torrent to the Client.
        /// Seems to be bugged in MonoTorrent stays stuck in metadata mode.
        /// Set to obsolete mode and will cause Errors
        /// </summary>
        /// <param name="magnet">magnent url to use</param>
        /// <returns>The TorrentManager associated with the MagnentUrl</returns>
        [Obsolete("Magnent torrents seem to be bugged in MonoTorrent do not use. Use Regular AddTorrent instead",true)]
        public TorrentManager AddMagnetUrl(string magnet)
        {
            MagnetLink link = new MagnetLink(magnet);
            
            string metaStorage = metaSaveDirectory + "\\";
            Random random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < 20; i++)
                metaStorage += random.Next(0, 9).ToString();
            metaStorage += ".meta";
            CreateDir(metaStorage);
            
            TorrentManager manager = new TorrentManager(link, saveDirectory, torrentSettings, metaStorage); 
            torrentEngine.Register(manager);
            return manager;
        }
        /// <summary>
        /// Adds a regular torrent from a path, through either the local filepath or web
        /// </summary>
        /// <param name="path">a local or URI style path</param>
        /// <param name="isUrl">if path is a url then will download file first otherwise will load locally</param>
        /// <returns>The TorrentManager associated with the Torrent file</returns>
        public TorrentManager AddTorrent(string path,bool isUrl)
        {
            Torrent torrent;
            if (isUrl)
            {
                torrent = Torrent.Load(new Uri(path),saveDirectory + "\\" + Path.GetFileName(path) );
                TorrentManager manager = new TorrentManager(torrent, saveDirectory, torrentSettings);
                torrentEngine.Register(manager);
                return manager;
            }
            else
            {
                torrent = Torrent.Load(path);
                TorrentManager manager = new TorrentManager(torrent, saveDirectory, torrentSettings);
                torrentEngine.Register(manager);
                return manager;
            }   
        }
        /// <summary>
        /// Removes and unregister the torrent from the engine
        /// </summary>
        /// <param name="index">The index of the torrent to remove</param>
        public void RemoveTorrent(int index)
        {
            torrentEngine.Unregister(torrentEngine.Torrents[index]);
        }
        /// <summary>
        /// Starts all the TorrentManagers in the engine
        /// </summary>
        public void StartAll()
        {
            torrentEngine.StartAll();
        }
        /// <summary>
        /// Starts an individual TorrentManager in the engine
        /// </summary>
        /// <param name="index">The index of the TorrentManager to start</param>
        public void Start(int index)
        {
            torrentEngine.Torrents[index].Start();
        }
        /// <summary>
        /// Stops all the Torrents in the engine
        /// </summary>
        public void StopAll()
        {
            torrentEngine.StopAll();
        }
        /// <summary>
        /// Stops a specific TorrentManager in the engine
        /// </summary>
        /// <param name="index">the index of the TorrentManager to stop</param>
        public void Stop(int index)
        {
            torrentEngine.Torrents[index].Stop();
        }
    }
}
