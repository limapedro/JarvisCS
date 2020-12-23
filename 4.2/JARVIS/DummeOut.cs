using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARVIS
{
    /// <summary>
    /// Classe de saídas dos comandos do assistemte
    /// </summary>
    public static class DummeOut
    {
        public static IList<string> OutJarvis = new List<string>
        {
            "olá senhor!",
            "sim",
            "em que posso ajudar",
            "sim, senhor!",
            "olá, em que posso ajudar?"
        };

        public static IList<string> OutStartingConversation = new List<string>
        {
            "olá, senhor!",
            "sim, senhor?",
            "em que posso ajudar?",
            "sim?",
            "oi.. senhor",
            "tudo bem, senhor?"
        };

        public static IList<string> OutQuestionForDumme = new List<string>
        {
            "eu sou o jarvis",
            "meu nome é jarvis", 
            "jarvis"
        };

        public static IList<string> OutDoWork = new List<string>
        {
            "bem, isso é bom",
            "que boa notícia",
            "que legal",
            "que ótimo"
        };

        public static IList<string> OutDummeStatus = new List<string>
       {
           "estou bem",
           "estou bem e você?",
           "estou bem, como posso ajudar?",
           "estou bem, como posso ser útil?"
       };
    }
}
