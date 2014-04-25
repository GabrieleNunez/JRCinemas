using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace JRCinemas.Providers
{
    /// <summary>
    /// JSON deserilizable class meant to be used in conjunction when getting a list of movies when searching
    /// Contains a basic overview of data about a movie meant for quick glances
    /// </summary>
    public class YIFYListedMovie
    {
        public int MovieID;
        public string State;
        public string MovieUrl;
        public string MovieTitle;
        public string MovieTitleClean;
        public string MovieYear;
        public DateTime DateUploaded;
        public long DateUploadedEpoch;
        public string Quality;
        public string CoverImage;
        public string ImdbCode;
        public string ImdbLink;
        public string Size;
        public long SizeByte;
        public decimal MovieRating;
        public string Genre;
        public string Uploader;
        public int UploaderUID;
        public int TorrentSeeds;
        public int Downloaded;
        public int TorrentPeers;
        public string TorrentUrl;
        public string TorrentHash;
        public string TorrentMagnetUrl;

        public override string ToString()
        {
            return WebUtility.HtmlDecode(MovieTitle);
        }
    }
}
