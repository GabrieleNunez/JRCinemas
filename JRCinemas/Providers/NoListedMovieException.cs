using System;
namespace JRCinemas.Providers
{
    public class NoListedMovieException : Exception
    {
        public NoListedMovieException(Exception inner) : base("Not able to retrieve or list movies", inner) { }
    }
}