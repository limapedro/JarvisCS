using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARVIS
{
    /// <summary>
    /// Classe que vai mostrar detalhes do sistema
    /// </summary>
    public class SystemInfo
    {
        // nome do usuário
        public static void GetUserName()
        {
            Speaker.Speak("Nome do usuário " + Environment.UserName.ToString());
        }
        public static string GetUserNameString()
        {
            return "Nome do usuário: " + Environment.UserName.ToString();
        }

        public static void GetOSVersion()
        {
            System.OperatingSystem infoOS = System.Environment.OSVersion;
            Speaker.Speak("versão do sistema operacional " + infoOS.ToString());
        }

        public static string GetOSVersionString()
        {
            System.OperatingSystem infoOS = System.Environment.OSVersion;
            return "versão do sistema operacional: " + infoOS.ToString();
        }

        public static void GetMachineName()
        {
            
            Speaker.Speak("Nome do computador " + Environment.MachineName.ToString());
        }

        public static string GetMachineNameString()
        {
            return "Nome do computador: " + Environment.MachineName.ToString();
        }

        public static void OSArch()
        {
            bool b = Environment.Is64BitOperatingSystem;
            if (b == true)
            {
                Speaker.Speak("o sistema é de 64 bits");
            }
            else
            {
                Speaker.Speak("o sistema é de 32 bits");
            }
        }

        public static string GetOSArchString()
        {
            bool b = Environment.Is64BitOperatingSystem;
            if (b == true)
            {
                return "O sistema é de 64 bits";
            }
            else
            {
                return "O sistema é de 32 bits";
            }
        }
    }
}
