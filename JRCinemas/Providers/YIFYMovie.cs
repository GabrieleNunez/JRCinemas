using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;

// <summary>
/// JSON deserilizable class meant to be used when getting intricate details about a specific movie
/// Contains all the possible information about a movie that YIFY provides to its users
/// </summary>
/// //TODO Remake
namespace JRCinemas.Providers
{
    public class YIFYMovie
    {
        public int MovieID;
        public string MovieUrl;
        public DateTime DateUploaded;
        public long DateUploadedEpoch;
        public string Uploader;
        public int UploaderUID;
        public string UploaderNotes;
        public string Quality;
        public string Resolution;
        public string Framerate;
        public string Language;
        public string Subtitles;
        public string MediumCover;
        public string LargeCover;
        public string LargeScreenshot1;
        public string LargeScreenshot2;
        public string LargeScreenshot3;
        public string MediumScreenshot1;
        public string MediumScreenshot2;
        public string MediumScreenshot3;
        public string ImdbCode;
        public string ImdbLink;
        public string MovieTitle;
        public string MovieTitleClean;
        public string MovieYear;
        public decimal MovieRating;
        public int MovieRuntime;
        public string YoutubeTrailerID;
        public string YoutubeTrailerUrl;
        public string AgeRating;
        public string Genre1;
        public string Genre2;
        public string ShortDescription;
        public string LongDescription;
        public int Downloaded;
        public string TorrentUrl;
        public string TorrentHash;
        public string TorrentMagnetUrl;
        public int TorrentSeeds;
        public int TorrentPeers;
        public string Size;
        public long SizeByte;

        public override string ToString()
        {
            return WebUtility.HtmlDecode(MovieTitle);
        }
    }
}