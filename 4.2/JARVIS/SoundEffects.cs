using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace JARVIS
{
    /// <summary>
    /// Classe responsável pelos efeitos sonoros
    /// </summary>
    public class SoundEffects
    {
        private static SoundPlayer player = null;

        public static void InitializingSystem()
        {
            player = new SoundPlayer("Sounds\\jarvis_morning.wav");
            Speaker.Speak("bom dia, senhor são " + DateTime.Now.Hour + " horas e " + DateTime.Now.Minute + " minutos ");

            player.Play();
        }
        public static void StopPlayer()
        {
            player.Stop();
        }
    }
}
