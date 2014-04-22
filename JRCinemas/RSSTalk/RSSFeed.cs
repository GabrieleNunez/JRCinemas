using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
namespace JRCinemas.RSSTalk
{
    /// <summary>
    /// RSSFeed is a class that allows you to parse and get the results of a RSS feed
    /// </summary>
    public class RSSFeed
    {
        private string url;
        private XmlDocument rssDocument;

        public RSSFeed(string URL)
        {
            url = URL;
            rssDocument = new XmlDocument();
            rssDocument.Load(url);
        }
    }
}
