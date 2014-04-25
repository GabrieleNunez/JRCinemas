using System;
namespace JRCinemas.Providers
{
    public class UnableToFindMovieException : Exception
    {
        public UnableToFindMovieException(Exception inner) : base("Unable to find or retrieve movie details", inner) { }
    }
}