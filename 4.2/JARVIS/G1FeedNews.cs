using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.ServiceModel.Syndication;

namespace JARVIS
{
    /// <summary>
    /// Classe que traz notícias do portal G1
    /// </summary>
    public static class G1FeedNews
    {
        public static G1NewsItem[] GetNews()
        {
            List<G1NewsItem> news = new List<G1NewsItem>();
            XmlDocument doc = new XmlDocument();

            doc.Load("http://g1.globo.com/dynamo/rss2.xml");
            foreach (XmlNode node in doc.GetElementsByTagName("item"))
            {
                G1NewsItem it = new G1NewsItem(node["title"].InnerText, RemoveTags(node["description"].InnerText));
                news.Add(it);
            }
            return news.ToArray();
        }

        private static string RemoveTags(string text)
        {
            return System.Text.RegularExpressions.Regex.Replace(text, "<.*?>", string.Empty);
        }
    }

    public class G1NewsItem
    {
        public string Title { get; set; }
        public string URL { get; set; }
        public string Text { get; set; }

        public G1NewsItem(string title, string text)
        {
            this.Title = title;
   
            this.Text = text;
        }
    }
}
