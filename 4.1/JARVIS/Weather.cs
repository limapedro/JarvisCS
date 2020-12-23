using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace JARVIS
{
    class Weather
    {
        public static string GetConditions()
        {
            string query = String.Format("http://weather.yahooapis.com/forecastrss?w=456758");
            return null;
        }
    }
}
