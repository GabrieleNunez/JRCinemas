using System;
using System.Net;
namespace JRCinemas.Providers
{
    /// <summary>
    /// JSON deserilizable class meant to be used when getting Upcoming movies
    /// </summary>
    public class YIFYUpcomingMovie
    {
        public string MovieTitle;
        public string MovieCover;
        public string ImdbCode;
        public string ImdbLink;
        public string Uploader;
        public int UploaderUID;
        public DateTime DateAdded;
        public long DateAddedEpoc;

        public override string ToString()
        {
            return WebUtility.HtmlDecode(MovieTitle);
        }
    }
}