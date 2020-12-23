using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis; // namespace necessário para a classe
using System.Globalization;// namespace para lidar com a cultura das vozes

namespace JARVIS
{
    /// <summary>
    /// Pegar vozes instaladas.
    /// </summary>
    class GetVoices
    {
        /// <summary>
        /// retorna string array com as vozes, passando a cultura com argumento
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string[] GetVoicesFromCulture(string culture)
        {
            SpeechSynthesizer sp = new SpeechSynthesizer(); // sintetizador

            List<string> voices = new List<string>(); // lista com as vozes

            foreach(InstalledVoice voice in sp.GetInstalledVoices()) // percorrer vozes instaladas
            {
                VoiceInfo info = voice.VoiceInfo; // pegar informações da voz atual

                if(culture==Convert.ToString(info.Culture)) // se a cultura alvo for igual ao da voz atual
                {
                    voices.Add(info.Name); // adicionar nome da voz na lista 
                }
            }
            return voices.ToArray(); // retorna array com as vozes, aqui a lista é convertida para array
        }
    }
}
