using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace JRCinemas
{
    /// <summary>
    /// A local collection of movies the user has
    /// </summary>
    public  sealed class MovieCollection
    {
        private Dictionary<string,Movie> movies;
        private List<string> searchDirectories;

        public MovieCollection()
        {
            movies = new Dictionary<string,Movie>();
            searchDirectories = new List<string>();
        }
        public void ScanDirectory(string dir)
        {
            string[] filters = {
                                   "*.mp4",
                                   "*.avi",
                                   "*.m4v",
                                   "*.mkv",
                                   "*.m2ts",
                               };
            List<string> files = new List<string>();
            foreach (string filter in filters)
            {
                foreach (string path in Directory.GetFiles(dir, filter, SearchOption.AllDirectories))
                {
                    Movie movie = new Movie();
                    movie.MovieFilePath = path;
                    movies.Add(path, movie);
                }
            }
        }
    }
    public class Movie
    {
        public string MovieTitle;
        public string MovieCoverPath;
        public string MovieFilePath;
    }
}
