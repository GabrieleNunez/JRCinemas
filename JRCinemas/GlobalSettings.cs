using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JRCinemas
{
    public static class GlobalSettings
    {
        private static int incomingPort;
        private static int globalMaxConnections;
        private static int torrentMaxConnections;

        private static bool useInternal;
        private static bool useDht;
        private static bool usePeerExchange;
        private static bool useEncryption;
        private static bool isLoaded;

        private static string externalTorrent;
        private static string externalTorrentArgs;
        private static string downloadDirectory;

        public static int IncomingPort
        {
            get { return incomingPort; }
            set { incomingPort = value; }
        }
        public static int GlobalMaxConnections
        {
            get { return globalMaxConnections; }
            set { globalMaxConnections = value; }
        }
        public static int TorrentMaxConnections
        {
            get { return torrentMaxConnections; }
            set { torrentMaxConnections = value; }
        }
        public static bool UseInternal
        {
            get { return useInternal; }
            set { useInternal = value; }
        }
        public static bool UseDht
        {
            get { return useDht; }
            set { useDht = value; }
        }
        public static bool UsePeerExchange
        {
            get { return usePeerExchange; }
            set { usePeerExchange = value; }
        }
        public static bool UseEncryption
        {
            get { return useEncryption; }
            set { useEncryption = value; }
        }
        public static string ExternalTorrentClient
        {
            get { return externalTorrent; }
            set { externalTorrent = value; }
        }
        public static string ExternalTorrentClientArgs
        {
            get { return externalTorrentArgs; }
            set { externalTorrentArgs = value; }
        }
        public static string DownloadDirectory
        {
            get { return downloadDirectory; }
            set { downloadDirectory = value; }
        }

        private const string FILENAME_DHT = "data.dht";
        private const string FILENAME_RESUME = "data.resume";
        private const string FILENAME_GLOBAL_SETTINGS = "global.set";
        private const string FILENAME_EXT_SETTINGS = ".set";
        private const string DIRECTORY_DOWNLOADS = "Downloads";
        private const string DIRECTORY_METADATA = "Metadata";


        private const string FORMAT_SETTING_DATA = "{0}\t{1}\r\n";
        private const string FORMAT_SETTING_CODEBASE = "CODEBASE";
        private const string FORMAT_SETTING_INCPORT = "INCOMING_PORT";
        private const string FORMAT_SETTING_GMAXCONN = "GLOBAL_MAX_CONNECTIONS";
        private const string FORMAT_SETTING_TMAXCONN = "TORRENT_MAX_CONNECTIONS";
        private const string FORMAT_SETTING_USEINTERNAL = "USE_INTERNAL_TORRENT";
        private const string FORMAT_SETTING_USEDHT = "USE_DHT";
        private const string FORMAT_SETTING_USEPEEREXCHANGE = "USE_PEER_EXCHANGE";
        private const string FORMAT_SETTING_USEENCYPTION = "USE_ENCRYPTION";
        private const string FORMAT_SETTING_EXTTORRENTPATH = "EXTERNAL_TORRENTCLIENT_PATH";
        private const string FORMAT_SETTING_EXTORRENTARGS = "EXTERNAL_TORRENTCLIENT_ARGS";
        private const string FORMAT_SETTING_DOWNLOADDIR = "DOWNLOAD_DIRECTORY";


        public const string CODEBASE = "UTERO";
        public const string TORRENT_FILE_SWAP = "@[TORRENTFILE]";


        public const int DEFAULT_INCOMING_PORT = 45732;
        public const bool DEFAULT_USE_INTERNAL = true;
        public const bool DEFAULT_USE_ENCRYPTION = false;
        public const bool DEFAULT_USE_DHT = true;
        public const bool DEFAULT_USE_PEER_EXCHANGE = true;
        public const int DEFAULT_GLOBAL_MAX_CONNECTIONS = 400;
        public const int DEFAULT_TORRENT_CONNECTIONS = 100;
        public const string DEFAULT_EXTERNALTORRENT = "";
        public const string DEFAULT_EXTERNALTORRENT_ARGS = TORRENT_FILE_SWAP;


        public readonly static string DHT_FILE = Path.Combine(Application.StartupPath, FILENAME_DHT);
        public readonly static string RESUME_FILE = Path.Combine(Application.StartupPath, FILENAME_RESUME);
        public readonly static string SETTINGS_FILE = Path.Combine(Application.StartupPath, FILENAME_GLOBAL_SETTINGS);
        public readonly static string DEFAULT_DOWNLOAD_DIRECTORY = Path.Combine(Application.StartupPath, DIRECTORY_DOWNLOADS);
        public readonly static string DEFAULT_METADATA_DIRECTORY = Path.Combine(Application.StartupPath, DIRECTORY_METADATA);


        static GlobalSettings()
        {
            isLoaded = false;
            Load();
        }
        public static void ResetAllToDefault()
        {
            incomingPort = DEFAULT_INCOMING_PORT;
            globalMaxConnections = DEFAULT_GLOBAL_MAX_CONNECTIONS;
            torrentMaxConnections = DEFAULT_TORRENT_CONNECTIONS;

            useInternal = DEFAULT_USE_INTERNAL;
            useDht = DEFAULT_USE_DHT;
            usePeerExchange = DEFAULT_USE_PEER_EXCHANGE;
            useEncryption = DEFAULT_USE_ENCRYPTION;
            downloadDirectory = DEFAULT_DOWNLOAD_DIRECTORY;
            externalTorrent = DEFAULT_EXTERNALTORRENT;
            externalTorrentArgs = DEFAULT_EXTERNALTORRENT_ARGS;
        }
        public static void Load()
        {
            if (File.Exists(SETTINGS_FILE) && !isLoaded)
            {
                using (StreamReader reader = new StreamReader(SETTINGS_FILE))
                {
                    while (!reader.EndOfStream)
                    {
                        string[] data = reader.ReadLine().Split('\t');
                        if (!string.IsNullOrEmpty(data[0]))
                        {
                            switch (data[0].ToUpper())
                            {
                                case FORMAT_SETTING_INCPORT:
                                    incomingPort = int.Parse(data[1]);
                                    break;
                                case FORMAT_SETTING_GMAXCONN:
                                    globalMaxConnections = int.Parse(data[1]);
                                    break;
                                case FORMAT_SETTING_TMAXCONN:
                                    torrentMaxConnections = int.Parse(data[1]);
                                    break;
                                case FORMAT_SETTING_USEDHT:
                                    useDht = bool.Parse(data[1]);
                                    break;
                                case FORMAT_SETTING_USEENCYPTION:
                                    useEncryption = bool.Parse(data[1]);
                                    break;
                                case FORMAT_SETTING_USEPEEREXCHANGE:
                                    usePeerExchange = bool.Parse(data[1]);
                                    break;
                                case FORMAT_SETTING_EXTORRENTARGS:
                                    externalTorrentArgs = data[1];
                                    break;
                                case FORMAT_SETTING_EXTTORRENTPATH:
                                    externalTorrent = data[1];
                                    break;
                                case FORMAT_SETTING_CODEBASE:
                                    break;
                                case FORMAT_SETTING_DOWNLOADDIR:
                                    downloadDirectory = data[1];
                                    break;
                                case FORMAT_SETTING_USEINTERNAL:
                                    useInternal = bool.Parse(data[1]);
                                    break;
                                default:
                                    MessageBox.Show("Unknown label", data[0]);
                                    break;
                            }
                        }
                    }
                }
            }
            else
            {
                ResetAllToDefault();
                Save();
            }
            isLoaded = true;
        }
        public static void Save()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat(FORMAT_SETTING_DATA, FORMAT_SETTING_CODEBASE, CODEBASE);
            builder.AppendFormat(FORMAT_SETTING_DATA, FORMAT_SETTING_INCPORT, incomingPort);
            builder.AppendFormat(FORMAT_SETTING_DATA, FORMAT_SETTING_GMAXCONN, globalMaxConnections);
            builder.AppendFormat(FORMAT_SETTING_DATA, FORMAT_SETTING_TMAXCONN, torrentMaxConnections);
            builder.AppendFormat(FORMAT_SETTING_DATA, FORMAT_SETTING_USEINTERNAL, useInternal);
            builder.AppendFormat(FORMAT_SETTING_DATA, FORMAT_SETTING_USEDHT, useDht);
            builder.AppendFormat(FORMAT_SETTING_DATA, FORMAT_SETTING_USEPEEREXCHANGE, usePeerExchange);
            builder.AppendFormat(FORMAT_SETTING_DATA, FORMAT_SETTING_USEENCYPTION, useEncryption);
            builder.AppendFormat(FORMAT_SETTING_DATA, FORMAT_SETTING_EXTTORRENTPATH, externalTorrent);
            builder.AppendFormat(FORMAT_SETTING_DATA, FORMAT_SETTING_EXTORRENTARGS, externalTorrentArgs);
            builder.AppendFormat(FORMAT_SETTING_DATA, FORMAT_SETTING_DOWNLOADDIR, downloadDirectory);

            using (StreamWriter writer = new StreamWriter(SETTINGS_FILE))
            {
                writer.WriteLine(builder.ToString());
            }
        }
    }
}