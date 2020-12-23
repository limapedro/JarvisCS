using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JARVIS
{
    /// <summary>
    /// Classe que cuida de algumas coisas da inicialização
    /// </summary>
    public static class InitializingProgram
    {
        public static void Start()
        {
            DateTime hour = DateTime.Now;

            if (hour.Hour > 5 && hour.Hour <= 10)
            {
                Speaker.SpeakRand("olá", "oi", "oi, bom dia", "olá, como você está?");
            }
            else if (hour.Hour > 10 && hour.Hour <= 13)
            {
                Speaker.SpeakRand("olá", "oi", "oi, bom dia", "olá, como você está?");
            }
            else if (hour.Hour > 12 && hour.Hour <= 18)
            {
                Speaker.SpeakRand("olá", "oi", "oi, bom dia", "olá, como você está?");
            }
            else if (hour.Hour > 18 && hour.Hour < 24)
            {
                Speaker.SpeakRand("olá", "oi", "oi, bom dia", "olá, como você está?");
            }
            else
            {
                Speaker.SpeakRand("olá", "oi", "oi, bom dia", "olá, como você está?");
            }
        }
    }
}
