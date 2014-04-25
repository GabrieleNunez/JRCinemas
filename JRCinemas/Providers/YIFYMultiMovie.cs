using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JRCinemas.Providers
{
    /// <summary>
    /// MultiMovie is a class where it combines different quality versions of ListedMovies into one
    /// Making it easier to find and search for specific movies and all its qualities for ease of use and development
    /// </summary>
    /// //TODO actually finish implementing class and test
    public class YIFYMultiMovie
    {
        public List<YIFYListedMovie> Versions;
        public int VersionCount { get { return Versions.Count; } }

        public YIFYMultiMovie()
        {
            Versions = new List<YIFYListedMovie>(3);
        }
        public static YIFYMultiMovie[] ParseMovies(YIFYListedMovies movies)
        {
            List<YIFYMultiMovie> multiMovies = new List<YIFYMultiMovie>();
            foreach (YIFYListedMovie movie in movies.MovieList)
            {
                YIFYMultiMovie multiMovie = new YIFYMultiMovie();
                multiMovie.Versions.Add(movie);
                foreach (YIFYListedMovie subMovie in movies.MovieList)
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
}
