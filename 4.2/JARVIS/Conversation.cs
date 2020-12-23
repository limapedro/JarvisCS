using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace JARVIS
{
    /// <summary>
    /// Classe que vai falar algo passando uma frase
    /// </summary>
    public class Conversation
    {
        public static void SaySomethingFor(string phrase)// método que vai falar algo
        {
            switch (phrase) // switch, método mais rápido que if-else para muitas comparações
            {
                case "bom dia":
                    DateTime time = DateTime.Now; // pega as horas
                    if(time.Hour >= 5 && time.Hour < 12) // for de manhã
                    {
                    	Speaker.Speak("olá senhor, desejo que você tenha um bom dia!");
                    	break;
                    }
                    else if(time.Hour >= 12 && time.Hour < 18) // se for de tarde
                    {
                    	Speaker.Speak("olá, boa tarde");
                    	break;
                    }
                    else if(time.Hour >= 18 && time.Hour < 24) // se for e noite
                    {
                    	Speaker.Speak("oi, boa noite, senhor");
                    	break;
                    }
                    break;
                 case "boa tarde": // boa tarde
                    Speaker.Speak("boa tarde, senhor");
                    break;
                    
                 case "boa noite":
                    Speaker.Speak("boa noite, tudo bem?");
                    break;
                case "ainda acordado jarvis?":
                    Speaker.Speak("obrigado por perguntar senhor, mas eu não durmo..");
                    break;
                case "alguma ideia jarvis?":
                    Speaker.Speak("não senhor");
                    break;
                case "obrigado jarvis":
                    Speaker.Speak("por nada, senhor");
                    break;
            }
        }
    }
}
