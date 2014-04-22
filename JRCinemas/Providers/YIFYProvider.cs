using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;

namespace JRCinemas.Providers
{
    #region Exceptions
    public class NoUpcomingMovieException : Exception
    {
        public NoUpcomingMovieException(Exception inner) : base("No upcoming movies could be found", inner) { }
    }
    public class NoListedMovieException : Exception
    {
        public NoListedMovieException(Exception inner) : base("Not able to retrieve or list movies", inner) { }
    }
    public class UnableToFindMovieException : Exception
    {
        public UnableToFindMovieException(Exception inner) : base("Unable to find or retrieve movie details", inner) { }
    }
    #endregion
    /// <summary>
    /// YIFY Provider class. Currently the only provider 
    /// </summary>
    public static class YIFYProvider
    {
        #region Constants
        public const string UPCOMING_LIST_URL = "http://yify-torrents.com/api/upcoming.json";
        public const string MOVIE_LIST_URL = "http://yify-torrents.com/api/list.json";
        public const string MOVIE_DETAILS_URL = "http://yify-torrents.com/api/movie.json";
        #endregion
        
        #region YIFY JSON Classes
        /// <summary>
        /// JSON deserilizable class meant to be used when getting Upcoming movies
        /// </summary>
        public class UpcomingMovie
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
        /// <summary>
        /// JSON deserilizable class meant to be used when getting a list of movies
        /// Contains Movie count and then an array of YIFYProvider.ListedMovie objects
        /// </summary>
        public class ListedMovies
        {
            public int MovieCount;
            public ListedMovie[] MovieList;
        }
        /// <summary>
        /// JSON deserilizable class meant to be used in conjunction when getting a list of movies when searching
        /// Contains a basic overview of data about a movie meant for quick glances
        /// </summary>
        public class ListedMovie
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
        
        /// <summary>
        /// JSON deserilizable class meant to be used when getting intricate details about a specific movie
        /// Contains all the possible information about a movie that YIFY provides to its users
        /// </summary>
        public class Movie
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
        /// <summary>
        /// MultiMovie is a class where it combines different quality versions of ListedMovies into one
        /// Making it easier to find and search for specific movies and all its qualities for ease of use and development
        /// </summary>
        public class MultiMovie
        {
            public List<ListedMovie> Versions;
            public int VersionCount { get { return Versions.Count; } }

            public MultiMovie()
            {
                Versions = new List<ListedMovie>(3);
            }
            public static MultiMovie[] ParseMovies(ListedMovies movies)
            {
                List<MultiMovie> multiMovies = new List<MultiMovie>();
                foreach (ListedMovie movie in movies.MovieList)
                {
                    MultiMovie multiMovie = new MultiMovie();
                    multiMovie.Versions.Add(movie);
                    foreach (ListedMovie subMovie in movies.MovieList)
                    {
                        if (string.Equals(movie.MovieTitleClean, subMovie.MovieTitleClean) &&
                            !string.Equals(movie.MovieTitleClean, subMovie.MovieTitleClean))
                        {
                            multiMovie.Versions.Add(subMovie);
                        }
                        else
                            break;
                    }
                    multiMovies.Add(multiMovie);
                }
                return multiMovies.ToArray();
            }
        }
        #endregion
        #region Helper Methods
        /// <summary>
        /// Trys to get JSON formated data from a url string
        /// </summary>
        /// <param name="url">The URL to request JSON data from</param>
        /// <returns>the JSON data associated with the url</returns>
        private static string GetJsonData(string url)
        {
            try
            {
                string data = "";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.AllowAutoRedirect = true;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        data = reader.ReadToEnd();
                        reader.Close();
                    }
                    response.Close();
                }
                return data;
            }
            catch
            {
                return null;
            }
        }
        private static string ParamUrl(string url, string[,] data)
        {
            string result = url;
            bool firstMod = true;
            for (int i = 0; i < data.GetLength(0); i++)
            {
                if (data[i, 1] != null && firstMod)
                {
                    result = result + "?" + Uri.EscapeDataString(data[i, 0]) + "=" + Uri.EscapeDataString(data[i, 1]);
                    firstMod = false;
                }
                else if (data[i, 1] != null)
                    result = result + "&" + Uri.EscapeDataString(data[i, 0]) + "=" + Uri.EscapeDataString(data[i, 1]);
            }
            return result;
        }
        #endregion
        #region API Methods
        public static UpcomingMovie[] GetUpcomingMovies()
        {
            UpcomingMovie[] upComingMovies = null;
            try
            {
                string jsonData = GetJsonData(UPCOMING_LIST_URL);
                upComingMovies = JsonConvert.DeserializeObject<UpcomingMovie[]>(jsonData);
                if (upComingMovies == null)
                    throw new NoUpcomingMovieException(null);
            }
            catch (Exception e)
            {
                throw new NoUpcomingMovieException(e);
            }
            return upComingMovies;
        }

        public static ListedMovies GetMovieList(string keywords)
        {
            return GetMovieList(null, null, null, null, keywords, null, null, null);
        }

        public static ListedMovies GetMovieList(string limit, string set, string quality, string rating, string keywords,
            string genre, string sort, string order)
        {
            ListedMovies movies = null;
            try
            {
                string[,] parameters = {
                                           {"limit",limit},
                                           {"set",set},
                                           {"quality",quality},
                                           {"rating",rating},
                                           {"keywords",keywords},
                                           {"genre",genre},
                                           {"sort",sort},
                                           {"order",order}
                                       };
                
                string url = ParamUrl(MOVIE_LIST_URL, parameters);
                string jsonData = GetJsonData(url);
                movies = JsonConvert.DeserializeObject<ListedMovies>(jsonData);
                if (movies == null && movies.MovieCount == 0)
                    throw new NoListedMovieException(null);
            }
            catch (Exception e)
            {
                throw new NoListedMovieException(e);
            }
            return movies;
        }
        public static Movie GetMovieDetail(string id)
        {
            Movie movie = null;
            try
            {
                string[,] parameters = {
                                            {"id",id}
                                       };
                string url = ParamUrl(MOVIE_DETAILS_URL, parameters);
                url = Uri.EscapeUriString(url);
                string jsonData = GetJsonData(url);
                movie = JsonConvert.DeserializeObject<Movie>(jsonData);
                if (movie == null)
                    throw new UnableToFindMovieException(null);
                movie.MovieTitle = WebUtility.HtmlDecode(movie.MovieTitle);
                movie.MovieTitleClean = WebUtility.HtmlDecode(movie.MovieTitleClean);
                movie.LongDescription = WebUtility.HtmlDecode(movie.LongDescription);
                movie.ShortDescription = WebUtility.HtmlDecode(movie.ShortDescription);
            }
            catch (Exception e)
            {
                throw new UnableToFindMovieException(e);
            }
            return movie;
        }
        #endregion
    }
}
