using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JRCinemas.Providers
{
    /// <summary>
    /// JSON deserilizable class meant to be used when getting a list of movies
    /// Contains Movie count and then an array of YIFYProvider.ListedMovie objects
    /// </summary>
    public class YIFYListedMovies
    {
        public int MovieCount;
        public YIFYListedMovie[] MovieList;
    }
}
