using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using HtmlAgilityPack;
using System.Windows.Forms;

namespace JARVIS
{
    public static class YoutubeSearch
    {
        public static string[] Seach(string search)
        {
            List<string> results = new List<string>();
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Encoding = Encoding.UTF8;

                    string youtube = "https://www.youtube.com/results?search_query=";
                    if (search.Contains(" "))
                        search = search.Replace(" ", "+");

                    youtube += search;


                    string html = wc.DownloadString(youtube);

                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(html);

                    foreach(HtmlNode node in doc.DocumentNode.SelectNodes("\\a[@href]"))
                    {
                        if(node.InnerText.StartsWith("https://www.youtube.com/watch?v="))
                        {
                            results.Add(node.InnerText);
                            
                        }
                        string t = "";
                        results.ForEach(r => t += r + "\n");
                        MessageBox.Show(t);
                    }
                }
            }
            catch(Exception ex)
            {
                Speaker.Speak("erro:" + ex.Message);
            }
            return results.ToArray();
        }
    }
}
