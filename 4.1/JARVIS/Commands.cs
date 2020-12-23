using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JARVIS
{
    /// <summary>
    ///  Classe que vai executar os comandos do JARVIS
    /// </summary>
    public class Commands
    {
        private static int windowCounter;
        private static MediaPlayer mediaPlayer = null; // media player
        private static AddNewCommand addNewCommand = null; // janela de comandos
        private static ProcessList processList = null;
        public static YoutubePlayer youtubePlayer = null;
        public static void Execute(string cmd) // método estático
        {
            DateTime time = DateTime.Now;
            switch (cmd)
            {
                case "que horas são": // fala as horas
                    
                    if (time.Hour <= 12 && time.Hour > 5)
                    {
                        Speaker.Speak("são " + time.Hour.ToString() + " horas da manhã e " + time.Minute.ToString() + " minutos");
                    }
                    else if (time.Hour > 12 && time.Hour < 18)
                    {
                        int h = time.Hour - 12;
                        Speaker.Speak("são " + h.ToString() + " horas da tarde e " + time.Minute.ToString() + " minutos");
                    }
                    else if (time.Hour > 18 && time.Hour < 24)
                    {
                        int h = time.Hour - 12;
                        Speaker.Speak("são " + h + " horas da noite e " + time.Minute.ToString() + "minutos");
                    }
                    else
                    {
                        Speaker.Speak("são " + time.Hour.ToString() + " horas " + time.Minute.ToString() + " minutos");
                    }
                    break;
                case "data de hoje":
                    Speaker.Speak(DateTime.Now.ToShortDateString());
                    break;
                case "que dia é hoje":
                    Speaker.Speak(DateTime.Today.ToString("dddd"));
                    break;
                case "em que mês estamos":
                    // vamos usar switch para pegar o nome do mes
                    string month = "";
                    int n = time.Month;
                    switch (n)
                    {
                        case 1:
                            month = "janeiro";
                            break;
                        case 2:
                            month = "fevereiro";
                            break;
                        case 3:
                            month = "março";
                            break;
                        case 4:
                            month = "abril";
                            break;
                        case 5:
                            month = "maio";
                            break;
                        case 6:
                            month = "junho";
                            break;
                        case 7:
                            month = "julho";
                            break;
                        case 8:
                            month = "agosto";
                            break;
                        case 9:
                            month = "setembro";
                            break;
                        case 10:
                            month = "outubro";
                            break;
                        case 11:
                            month = "novembro";
                            break;
                        case 12:
                            month = "dezembro";
                            break;
                    }
                    Speaker.Speak("estamos no mês de " + month);
                    break;
                case "em que ano estamos":
                    Speaker.Speak(DateTime.Today.ToString("yyyy"));
                    break;
                    
                // SpeechSynthesizer
                case "pare de falar":
                	Speaker.StopSpeak(); // para de falar
                	break;
                
                // media player
                case "media player":
                    if (mediaPlayer != null)
                    {
                        mediaPlayer.Show();
                        Speaker.Speak("abrindo media player");
                    }
                    else
                    {
                        mediaPlayer = new MediaPlayer();
                        mediaPlayer.Show();
                        Speaker.Speak("abrindo media player");
                    }
                    break;
                case "selecionar arquivo para o media player":
                    if (mediaPlayer != null)
                    {
                        mediaPlayer.Show();
                        mediaPlayer.OpenFile();
                    }
                    else
                    {
                        Speaker.Speak("media player, não está aberto!"); // diz algo
                    }
                    break;
                case "pausar":
                    if (mediaPlayer != null)
                    {
                        mediaPlayer.Pause();
                    }
                    else
                    {
                        Speaker.Speak("media player, não está aberto!"); // diz algo
                    }
                    break;
                case "continuar":
                    if (mediaPlayer != null)
                    {
                        mediaPlayer.Play();
                    }
                    else
                    {
                        Speaker.Speak("media player, não está aberto!"); // diz algo
                    }
                    break;
                case "parar":
                    if (mediaPlayer != null)
                    {
                        mediaPlayer.Stop();
                    }
                    else
                    {
                        Speaker.Speak("media player, não está aberto!"); // diz algo
                    }
                    break;
                case "fechar media player":
                    if (mediaPlayer != null)
                    {
                        mediaPlayer.Close();
                        mediaPlayer = null;
                        Speaker.Speak("fechando o media player");
                    }
                    break;
                case "abrir diretório para reproduzir":
                    if (mediaPlayer != null)
                    {
                        mediaPlayer.OpenDirectory();
                        mediaPlayer.Show();
                    }
                    else
                    {
                        Speaker.Speak("media player, não está aberto!"); // diz algo
                    }
                    break;
                case "próximo":
                    if (mediaPlayer != null)
                    {
                        mediaPlayer.NextFile();
                    }
                    else
                    {
                        Speaker.Speak("media player, não está aberto!"); // diz algo
                    }
                    break;
                case "anterior":
                    if (mediaPlayer != null)
                    {
                        mediaPlayer.BackFile();
                    }
                    else
                    {
                        Speaker.Speak("media player, não está aberto!"); // diz algo
                    }
                    break;
                case "aumentar volume do media player":
                    if (mediaPlayer != null)
                    {
                        mediaPlayer.VolumeUp();
                    }
                    else
                    {
                        Speaker.Speak("media player, não está aberto!"); // diz algo
                    }
                    break;
                case "diminuir volume do media player":
                    if (mediaPlayer != null)
                    {
                        mediaPlayer.VolumeDown();
                    }
                    else
                    {
                        Speaker.Speak("media player, não está aberto!"); // diz algo
                    }
                    break;
                case "media player sem som":
                    if (mediaPlayer != null)
                    {
                        mediaPlayer.Mute();
                    }
                    else
                    {
                        Speaker.Speak("media player, não está aberto!"); // diz algo
                    }
                    break;
                case "media player com som":
                    if (mediaPlayer != null)
                    {
                        mediaPlayer.UnMute();
                    }
                    else
                    {
                        Speaker.Speak("media player, não está aberto!"); // diz algo
                    }
                    break;
                case "media player em tela cheia":
                    if (mediaPlayer != null)
                    {
                        mediaPlayer.FullScreen();
                    }
                    else
                    {
                        Speaker.Speak("media player, não está aberto!"); // diz algo
                    }
                    break;
                case "que arquivo está tocando":
                    if (mediaPlayer != null)
                    {
                        mediaPlayer.SayFileThatIsPlaying();
                    }
                    else
                    {
                        Speaker.Speak("media player, não está aberto!"); // diz algo
                    }
                    break;

                // Comandos
                case "adicionar novo comando":
                    if (addNewCommand == null)
                    {
                        addNewCommand = new AddNewCommand();
                        addNewCommand.Show();
                    }
                    else
                    {
                        Speaker.Speak("certo, vou abrir");
                        addNewCommand.Show();
                    }
                    break;
                case "detalhes dos processos":
                    ProcessControl.ProcessesRunning(); // chama o método
                    break;

                case "introdução ao assistente jarvis":
                    JARVISHelp.Introduction();
                    break;

                case "lista de processos":
                    if (processList == null)
                    {
                        processList = new ProcessList();
                        processList.Show();
                    }
                    else
                    {
                        processList.Show();
                    }
                    break;
                case "fechar o processo selecionado":
                    if (processList != null)
                    {
                        processList.CloseSelectedProcess();
                    }
                    break;
                // Código Logo
                case "reproduza um vídeo do canal":
                    string[] links = System.IO.File.ReadAllLines("CodigoLogoVideos.txt");
                    Random r = new Random();
                    
                    if (youtubePlayer == null)
                    {
                        youtubePlayer = new YoutubePlayer();
                        youtubePlayer.PlayUrl(links[r.Next(0, links.Length - 1)]);
                        youtubePlayer.Show();
                    }
                    else
                    {
                        youtubePlayer.PlayUrl(links[r.Next(0, links.Length - 1)]);
                        youtubePlayer.Show();
                    }
                    
                    break;


                // informações do sistema
                case "em quanto estar o uso do processador?":
                    Speaker.Speak("o uso do processador estar em " + Math.Round(PCStats.GetCPUUsage(), 2).ToString() + " porcento");
                    break;
                case "quanta memória ram estar sendo usada?":
                    Speaker.Speak("estão sendo usados " + ((int)PCStats.GetTotalMemory() - PCStats.GetFreeMemory()).ToString() + " megas baites de memória ram");
                    break;
                case "quanta mamória ram ainda há livre?":
                    Speaker.Speak("há " + (int)PCStats.GetFreeMemory() + " megas baites de memória ram livres");
                    break;
                case "quanta memória ram há no total?":
                    Speaker.Speak("há " + (int)PCStats.GetTotalMemory() + " megas baites de memória ram no total");
                    break;
                case "estou com sono":
                    Speaker.Speak("o senhor deveria ir dormir, estarei disponível quando o senhor voltar");
                    break;
                case "estou indo dormir":
                    Speaker.SpeakSync("certo, sendo assim também estou indo dormir,... até mais senhor");
                    Environment.Exit(0); // outra forma de fechar o form, mas também tem o objeto.Close()
                    break;


                case "desligar computador":
                    SpecialCommands.ShutdownComputer();
                    break;
                case "reiniciar computador":
                    SpecialCommands.RestartComputer();
                    break;
                case "cancelar desligamento":
                case "cancelar reinicialização":
                    SpecialCommands.CancelShutdown();
                    break;

                case "tocar algo do youtube":
                    if (youtubePlayer == null)
                    {
                        youtubePlayer = new YoutubePlayer();
                        youtubePlayer.Show();

                    }
                    else
                    {
                        Speaker.Speak("youtube app não está disponível");
                    }
                    break;
                case "adicionar link para o youube":
                    if(youtubePlayer!=null)
                    {
                        EnterURL enterUrl = new EnterURL();
                        Speaker.Speak("cole o link do vídeo");
                        enterUrl.ShowDialog();
                        youtubePlayer.PlayUrl(enterUrl.URL);
                    }
                    break;
                case "previsão do tempo":
                    string temp = Weather.GetConditions();
                    Speaker.Speak("temperatura é de " + temp);
                    break;
                // controle de janelas
                case "alterar de janela":
                    windowCounter++;
                    SendKeys.Send("%{TAB " + windowCounter + "}");
                    Speaker.Speak("alterando de janela");
                    break;
                case "fechar janela":
                    SendKeys.Send("%{F4}");
                    Speaker.Speak("fechando janela");
                    break;

                // comandos de teclas
                case "copiar texto selecionado":
                    SendKeys.Send("^{C}");
                    Speaker.Speak("copiado");
                    break;
                case "colar texto selecionado":
                    SendKeys.Send("^{V}");
                    Speaker.Speak("colado");
                    break;
                case "salvar este arquivo":
                    SendKeys.Send("^{S}");
                    Speaker.Speak("escreva um nome para o arquivo e salve-o");
                    break;
                case "selecionar tudo":
                    SendKeys.Send("^{A}");
                    Speaker.Speak("selecionando todo o texto!");
                    break;
                case "nova linha":
                    SendKeys.Send("{ENTER}");
                    Speaker.Speak("indo para nova linha");
                    break;

                // Comandos do programa
                case "exibir lista de comandos":
                    break;
            }
        }
    }
}
