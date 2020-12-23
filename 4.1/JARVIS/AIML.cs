using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AIMLbot; // importando namespace que vai processar os arquivos de AIML
using System.Xml; // vamos processar os arquivos xml's

namespace JARVIS
{
    /// <summary>
    /// Classe que vai processar os arquivos de AIML
    /// </summary>
    public class AIML
    {
        private static string pathFiles = "DataBaseAIML"; // pasta dos arquivos AIML's
        public static string[] GetWordsOrSentences()
        {
            string[] files = Directory.GetFiles(pathFiles, "*.aiml");
            List<string> wordsOrSentences = new List<string>(); // resultado
            foreach(var file in files)
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(file); // carregando o arquivo
                    foreach (XmlElement ele in doc.GetElementsByTagName("pattern"))
                    {
                        string word = ele.InnerText.Replace("*", "");
                        word = word.Trim();
                        wordsOrSentences.Add(word);
                    }
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Erro no arquivo: " + file + "\nErro: " + ex.Message);
                }
                
            }
            return wordsOrSentences.ToArray();
        }

        public static void ConfigAIMLFiles()
        {
            string[] files = Directory.GetFiles(pathFiles, "*.aiml");
            int index = 0;
            foreach (var file in files)
            {
                string[] lines = File.ReadAllLines(file);
                StreamWriter sw = new StreamWriter("aiml\\aiml_" + index + ".aiml");
                foreach (var line in lines)
                {
                    if (line.StartsWith("<pattern>"))
                    {
                        string temp = RemoverAcentos(line);
                        sw.WriteLine(temp);
                    }
                    else
                    {
                        sw.WriteLine(line);
                    }
                }
                sw.Close();
                index++;
            }
        }

        public static string GetOutputChat(string chat)
        {
            Bot myBot = new Bot();
            myBot.loadSettings();
            User user = new User("pedro", myBot);
            myBot.isAcceptingUserInput = false;

            myBot.loadAIMLFromFiles();
            myBot.isAcceptingUserInput = true;

            Request r = new Request(RemoverAcentos(chat), user, myBot);
            Result res = myBot.Chat(r);
            return res.Output;
        }

        public static string RemoverAcentos(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return String.Empty;
            else
            {
                byte[] bytes = System.Text.Encoding.GetEncoding("iso-8859-8").GetBytes(texto);
                return System.Text.Encoding.UTF8.GetString(bytes);
            }
        }
    }
}
