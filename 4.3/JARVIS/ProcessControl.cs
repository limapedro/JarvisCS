/*
 * Criado por SharpDevelop.
 * Usuário: pedro
 * Data: 02/01/2016
 * Hora: 19:27
 * 
 * Para alterar este modelo use Ferramentas | Opções | Codificação | Editar Cabeçalhos Padrão.
 */
using System;
using System.Diagnostics; // namespace usado

namespace JARVIS
{
	/// <summary>
	/// Description of ProcessControl.
	/// </summary>
	public class ProcessControl
	{
		public ProcessControl()
		{
		}
		
		public static void OpenOrClose(string proc)
		{
            if (proc.StartsWith("abrir"))
            {
                proc = proc.Replace("abrir", ""); // remove o comando
                proc = proc.Trim(); // remove espaços m branco
                Speaker.SpeakOpenningProcess(proc);
                switch (proc) // verificando o argumento
                {
                    case "bloco de notas":
                        Process.Start("notepad");
                        break;
                    case "prompt de comando":
                        Process.Start("cmd");
                        break;
                    case "windows media player":
                        Process.Start("wmplayer");
                        break;
                    case "gerenciador de tarefas":
                        Process.Start("taskmgr");
                        break;
                    case "minhas pastas":
                        Process.Start("explorer");
                        break;
                    case "calculadora":
                        Process.Start("calc.exe");
                        break;
                    case "mapa de caracteres":
                        Process.Start("charmap");
                        break;
                    case "limpeza de disco":
                        Process.Start("cleanmgr");
                        break;
                    case "gerenciamento de cores":
                        Process.Start("colorcpl");
                        break;
                    case "serviços de componente":
                        Process.Start("comexp.msc");
                        break;
                    case "gerenciamento de computador":
                        Process.Start("compmgmt.msc");
                        break;
                    case "definir programas padrão":
                        Process.Start("ComputerDefaults");
                        break;
                    case "painel de controle":
                        Process.Start("control.exe");
                        break;
                    case "otimizador de texto":
                        Process.Start("cttune.exe");
                        break;
                    case "calibragem de cores":
                        Process.Start("dccw.exe");
                        break;
                    case "desfragmentador de disco":
                        Process.Start("dfrgui.exe");
                        break;
                    case "adicionar um novo dispositivo":
                        Process.Start("DevicePairingWizard");
                        break;
                    case "gerenciador de dispositivos":
                        Process.Start("devmgmt.msc");
                        break;
                    case "discagem telefônica":
                        Process.Start("dialer.exe");
                        break;
                    case "gerenciamento de disco":
                        Process.Start("diskmgmt.msc");
                        break;
                }
            }
            else if(proc.StartsWith("fechar"))
            {
                proc = proc.Replace("fechar", "");
                proc = proc.Trim();
                switch (proc)
                {
                    case "bloco de notas":
                        CloseProcess("notepad", proc);
                        break;
                    case "windows media player":;
                        CloseProcess("wmplayer", proc);
                        break;
                    case "gerenciador de tarefas":
                        CloseProcess("taskmgr", proc);
                        break;
                    case "calculadora":
                        CloseProcess("calc", proc);
                        break;
                }
            }
		}
        private static void CloseProcess(string cmd, string proc)
		{
            try // vamos usar try/catch
            {
                Process[] p = Process.GetProcessesByName(cmd);
                if (p[0] != null) // se o processo não for nulo
                {
                    Speaker.SayWithStatus("fechando o " + proc);
                    p[0].Kill();
                }
                else // se for nulo
                {
                    Speaker.Speak("desculpe, mas o " + proc + " não estar aberto");
                }
            }
            catch (Exception ex) // jarvis vai nos ajudar no debug
            {
                Speaker.Speak("senhor, ocorreu um erro, desculpe, o erro foi, " + ex.Message);
            }
		}

        /// <summary>
        /// Faz algo sobre a lista de processos
        /// </summary>
        public static void ProcessesRunning()
        {
            try
            {
                Process[] procs = Process.GetProcesses(); // pega todos os processos
                Speaker.Speak("obtendo lista de processos");
                foreach (Process proc in procs) // percorrer os processos
                {
                    if (proc.MainWindowTitle != "") // se a janela tiver título
                    {
                        Speaker.Speak(proc.MainWindowTitle + " está usando " + Convert.ToString(proc.PagedMemorySize64 / 1024 / 1024) + " mega baites");
                    }
                }
            }
            catch (Exception ex)
            {
                Speaker.Speak("ocorreu um erro " + ex.Message); // fala o erro
            }
        }
	}
}
