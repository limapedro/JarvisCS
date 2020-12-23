using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis; // classe usada

namespace JARVIS
{
    /// <summary>
    /// Classe que vai fazer falar os textos do assistente
    /// </summary>
    public class Speaker
    {
        // é preciso ser static, porque vamos usar-lo dentro de um método estático

        private static Random rnd = new Random();
        private static SpeechSynthesizer sp = new SpeechSynthesizer(); // instanciando o sintetizador
        private static string user = Environment.UserName;
        public static void Speak(string text) // falar o texto passado
        {
            speaking = true;
            sp.SpeakAsyncCancelAll();
            sp.SpeakAsync(text); // método async
        }

        public static void SpeakSync(string text) // fala na mesma thread
        {
            sp.Speak(text);
        }

        // Controlar o volume do sintetizador
        public static void VolumeUp()
        {
            sp.Volume += 10;
        }

        public static void VolumeDown()
        {
            sp.Volume -= 10;
        }

        public static void SetVolume(int i)
        {
            sp.Volume = i;
        }

        private static bool speaking = false;
        public static void ResumeOrPause()
        {
            if (speaking == false)
            {
                sp.Resume();
                speaking = true;
            }
            else
            {
                sp.Pause();
                speaking = false;
            }
        }

        public static void SpeakOpenningProcess(string proc) // fala a abertura de um processo
        {
            int num = rnd.Next(0, 6); // de 0 a 3

            switch (num) // casos
            {
                case 0:
                    Speak("abrindo o " + proc);
                    break;
                case 1:
                    Speak("certo, abrindo " + proc);
                    break;
                case 2:
                    Speak("como quiser, iniciando " + proc);
                    break;
                case 3:
                    Speak("como quiser, senhor, abrindo " + proc);
                    break;
                case 4:
                    Speak("tudo bem, iniciando o " + proc);
                    break;
                case 5:
                    Speak("tudo bem, " + user + ", abrindo " + proc);
                    break;
            }
        }

        public static void SayWithStatus(string speak)
        {
            int num = rnd.Next(0, 3);
            switch (num)
            {
                case 0:
                    Speak("certo" + speak);
                    break;
                case 1:
                    Speak("como quiser " + speak);
                    break;
                case 2:
                    Speak("como quiser, " + user + ", " + speak);
                    break;
                case 3:
                    Speak("tudo bem, " + speak);
                    break;
            }
        }

        public static void SpeakException(string ex)
        {
            Speak(ex);
        }

        public static void SpeakRand(params string[] texts)
        {
            Speak(texts[rnd.Next(0, texts.Length)]);
        }
        public static void SetVoice(string voice) // setar voz do sintetizador
        {
            sp.SelectVoice(voice); // definir voz
        }
        
        public static void StopSpeak()
        {
        	sp.SpeakAsyncCancelAll();// para de falar
        }
    }
}
