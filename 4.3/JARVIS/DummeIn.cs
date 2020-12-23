using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARVIS
{
    /// <summary>
    /// Classe que vai ser responsável por conversas em modos diferentes
    /// </summary>
    public static class DummeIn
    {
        public static IList<string> InJarvis = new List<string>
        {
            "jarvis",
            "olá jarvis",
            "oi jarvis",
            "amigo, você está ai?"
        };

        public static IList<string> InStartingConversation = new List<string>
        {
            "olá",
            "tudo bem?",
            "oi",
            "oi, tudo bem"
        };

        public static IList<string> InQuestionForDumme = new List<string>
        {
            "quem é você?",
            "qual é o seu nome?",
            "como é seu nome?"
        };

        public static IList<string> InDoWork = new List<string>
        {
            "vamos trabalhar?",
            "vamos fazer algo?",
            "o que vamos fazer?",
            "quero criar algo novo!"
        };

        public static IList<string> InDummeStatus = new List<string>
        {
            "como você está?",
            "tudo bem?",
            "oi, tudo bem?",
            "tudo bem jarvis?"
        };
    }
}
