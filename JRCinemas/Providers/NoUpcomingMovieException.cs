using System;
namespace JRCinemas.Providers
{
    public class NoUpcomingMovieException : Exception
    {
        public NoUpcomingMovieException(Exception inner) : base("No upcoming movies could be found", inner) { }
    }
}