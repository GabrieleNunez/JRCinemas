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
    /// <summary>
    /// YIFY Provider class. Currently the only provider 
    /// </summary>
    public static class YIFYProvider
    {
        
        public const string UPCOMING_LIST_URL = "http://yify-torrents.com/api/upcoming.json";
        public const string MOVIE_LIST_URL = "http://yify-torrents.com/api/list.json";
        public const string MOVIE_DETAILS_URL = "http://yify-torrents.com/api/movie.json";
     
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
                    }
                }
                return data;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Format our url based on supplied data
        /// </summary>
        /// <param name="url">Our base url</param>
        /// <param name="data">Supplied arguments, [paramName,value]</param>
        /// <returns>Modifed string</returns>
        private static string ParamUrl(string url, string[,] data)
        {
            string result = "";
            bool firstMod = true;
            for (int i = 0; i < data.GetLength(0); i++)
            {
                if (data[i, 1] != null && firstMod)
                {
                    result = string.Format("{0}?{1}={2}", url, Uri.EscapeDataString(data[i, 0]), Uri.EscapeDataString(data[i, 1]));
                    firstMod = false;
                }
                else if (data[i, 1] != null)
                    result = string.Format("{0}&{1}={2}", result, Uri.EscapeDataString(data[i, 0]), Uri.EscapeDataString(data[i, 1]));
            }
            return result;
        }
        /// <summary>
        /// Gets a list of of upcoming movies
        /// </summary>
        /// <returns></returns>
        public static YIFYUpcomingMovie[] GetUpcomingMovies()
        {
            YIFYUpcomingMovie[] upComingMovies = null;
            try
            {
                string jsonData = GetJsonData(UPCOMING_LIST_URL);
                upComingMovies = JsonConvert.DeserializeObject<YIFYUpcomingMovie[]>(jsonData);
                if (upComingMovies == null)
                    throw new NoUpcomingMovieException(null);
            }
            catch (Exception e)
            {
                throw new NoUpcomingMovieException(e);
            }
            return upComingMovies;
        }
        /// <summary>
        /// Gets a list of movies based on keywords
        /// </summary>
        /// <param name="keywords">The keywords to search for</param>
        /// <returns></returns>
        public static YIFYListedMovies GetMovieList(string keywords)
        {
            return GetMovieList(null, null, null, null, keywords, null, null, null);
        }
        /// <summary>
        /// Gets a list of movies based on the given parameters
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="set"></param>
        /// <param name="quality"></param>
        /// <param name="rating"></param>
        /// <param name="keywords"></param>
        /// <param name="genre"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static YIFYListedMovies GetMovieList(string limit, string set, string quality, string rating, string keywords,
            string genre, string sort, string order)
        {
            YIFYListedMovies movies = null;
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
                movies = JsonConvert.DeserializeObject<YIFYListedMovies>(jsonData);
                if (movies == null && movies.MovieCount == 0)
                    throw new NoListedMovieException(null);
            }
            catch (Exception e)
            {
                throw new NoListedMovieException(e);
            }
            return movies;
        }
        /// <summary>
        /// Getst the inner details of a movie
        /// </summary>
        /// <param name="id">The id of the movie</param>
        /// <returns></returns>
        public static YIFYMovie GetMovieDetail(string id)
        {
            YIFYMovie movie = null;
            try
            {
                string[,] parameters = {
                                            {"id",id}
                                       };
                string url = ParamUrl(MOVIE_DETAILS_URL, parameters);
                url = Uri.EscapeUriString(url);
                string jsonData = GetJsonData(url);
                movie = JsonConvert.DeserializeObject<YIFYMovie>(jsonData);
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
    }
}
