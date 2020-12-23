/*  Projeto JARVIS - Código Logo
 *  Link do Canal: https://www.youtube.com/channel/UCwZFL945LUQcF9OpEWSfbeg
 * 
 * 
 *
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks; // tasking
using System.Windows.Forms;
using Microsoft.Speech.Recognition; // namespace usado pra o reconhecimento de voz, Microsoft Speech Server 11 SDK pt-BR
using System.Threading; // namespace para threading
using System.IO; // namespace para E/S de arquivos
using System.Diagnostics; // diágnosticos
using System.Web;

namespace JARVIS
{
    public partial class Form1 : Form
    {
        // Forms
        public static WebLoader webLoader = null;
        private static ProcessList processList = null;
        private static AppsDialog appsDialog = null;
        private LoadingSystem loadingSystem = null;
        private SelectVoice selectVoice = null;
        private MotionDetection motionDetection = null;
        private PersonIdentifier personId = null;

        private SpeechRecognitionEngine sre; // declarando o reconhecedor

        // Aqui vão ficar alguns objetos básicos
        private string[] voices = GetVoices.GetVoicesFromCulture("pt-BR"); // pegar vozes em português

        private Dictionary<string, string> dictCmdSites = new Dictionary<string, string>(); // dicionário dos comandos

        private bool speechRecognitionActived = false; // booleana do reconhecimento

        private bool comboBoxVisible = true;
        private Random rnd = new Random(); // rand

        private List<string> commandsForQA;

        private List<string> askRules = new List<string>();

        private UserSetings frmUserSettings = new UserSetings(); // form arquivo do usuário.

        private UserProfile userProfile;

        // método carregar arquivo do usuário
        public void LoadUserSettings()
        {
            try // tentar
            {
                if (!File.Exists("user.pfl"))
                {
                    this.frmUserSettings.ShowDialog();
                }
                else
                {
                    Speaker.SetVoice(userProfile.Voice); // setar voz.
                }
            }
            catch (Exception ex)
            {

            }
        }

        // Método pra setar a voz
        private void SetVoice() // percorrer vozes e tentar setar a primeira
        {
            int counter = 0; // contador
            if (voices.Length == 0)
                MessageBox.Show("Desculpe, nenhuma voz em português SAPI  foi encontrada, tente instalar uma e tente novamente!");
            while (counter < voices.Length) // enquanto contador for menor que o array de vozes
            {
                try
                {
                    Speaker.SetVoice(voices[counter]); // tentar setar a voz pelo índice
                    break; // ocorreu bem, então vamos sair do loop
                }
                catch (Exception ex) // deu mal
                {
                    MessageBox.Show(ex.Message); // vamos mostrar o erro pelo menos né!
                    counter++; // incrementa o índice da voz
                }
            }
        }

        private string[] SplitGrammar(string[] arr)
        {
            List<string> g = new List<string>();
            for (int i = 0; i < arr.Length; i++)
            {
                string[] words = arr[i].Split(' '); // dividir string
                for (int j = 0; j < words.Length; j++)
                {
                    if (!g.Contains(words[j]))
                    {
                        g.Add(words[j]);
                    }
                }
            }
            return g.ToArray();
        }

        private void LoadSpeechRecognition() // fazer o que é preciso para o reconhecimento de voz
        {
            sre = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("pt-BR")); // instanciando  o reconhecedor passando a cultura da engine
            sre.SetInputToDefaultAudioDevice(); // definindo o microfone como entrada de aúdio

            commandsForQA = new List<string>();
            commandsForQA.Add("o que é");
            commandsForQA.Add("qual é");
            commandsForQA.Add("defina");
            commandsForQA.Add("definição de");

            // vamos processar o AIML aqui
            Choices cAIML = new Choices(AIML.GetWordsOrSentences()); // obtendo frases e palavras

            // Vamos ler o arquivo dos comandos
            string[] cmds = File.ReadAllText("comandos.txt", Encoding.UTF8).Split('$'); // lendo ele e dividindo em linhas

            Choices cControls = new Choices();
            cControls.Add("detecção de movimento");

            // Alarme
            Choices cAlarm = new Choices();
            for (int i = 1; i <= 12; i++)
                cAlarm.Add(i.ToString());

            // Criação das Gramáticas, Choices
            Choices cChats = new Choices(); // palavras ou frases de conversa
            cChats.Add("bom dia");
            cChats.Add("boa tarde");
            cChats.Add("boa noite");
            cChats.Add("jarvis você está ai?");
            cChats.Add("ainda acordado jarvis?");
            cChats.Add("alguma ideia jarvis?");
            cChats.Add("obrigado jarvis");


            Choices cDummes = new Choices(); // conversa mais desenrolada
            cDummes.Add(DummeIn.InStartingConversation.ToArray());
            cDummes.Add(DummeIn.InQuestionForDumme.ToArray());
            cDummes.Add(DummeIn.InDoWork.ToArray());
            cDummes.Add(DummeIn.InDummeStatus.ToArray());
            cDummes.Add(DummeIn.InJarvis.ToArray());

            Choices cCommands = new Choices(); // palavras ou frases que são comandos
            
            // informações de hora e data
            cCommands.Add("que horas são");
            cCommands.Add("que dia é hoje");
            cCommands.Add("data de hoje");
            cCommands.Add("em que mês estamos");
            cCommands.Add("em que ano estamos");
            cCommands.Add("minimizar a janela principal");
            cCommands.Add("mostrar janela principal");


            // Comandos do programa
            cCommands.Add("exibir lista de comandos");

            // status do usuário
            cCommands.Add("estou com sono");
            cCommands.Add("estou indo dormir");

            // sair do JARVIS
            cCommands.Add("até mais jarvis");
            
            // configurar o sintetizador
            cCommands.Add("pare de falar");
            
            // media player
            cCommands.Add("media player");
            cCommands.Add("selecionar arquivo para o media player");
            cCommands.Add("pausar");
            cCommands.Add("continuar");
            cCommands.Add("parar");
            cCommands.Add("fechar media player");
            cCommands.Add("abrir diretório para reproduzir");
            cCommands.Add("próximo");
            cCommands.Add("anterior");
            cCommands.Add("aumentar volume do media player");
            cCommands.Add("diminuir volume do media player");
            cCommands.Add("media player sem som");
            cCommands.Add("media player com som");
            cCommands.Add("media player em tela cheia");
            cCommands.Add("que arquivo está tocando");

            // informações do sistema
            cCommands.Add("em quanto estar o uso do processador?");
            cCommands.Add("quanta memória ram estar sendo usada?");
            cCommands.Add("quanta mamória ram ainda há livre?");
            cCommands.Add("quanta memória ram há no total?");

            // Comandos, adicionar
            cCommands.Add("adicionar novo comando");
            // processos
            cCommands.Add("detalhes dos processos");
            // processList
            cCommands.Add("lista de processos");
            cCommands.Add("fechar o processo selecionado");
            // jarvis
            cCommands.Add("introdução ao assistente jarvis");

            cCommands.Add("desligar computador");
            cCommands.Add("reiniciar computador");
            cCommands.Add("cancelar desligamento");
            cCommands.Add("cancelar reinicialização");

            // Youtube
            cCommands.Add("tocar algo do youtube");
            cCommands.Add("adicionar link para o youube");
            cCommands.Add("previsão do tempo");

            // controle de janelas
            cCommands.Add("alterar de janela");
            cCommands.Add("fechar janela");

            // comandos de teclas
            cCommands.Add("copiar texto selecionado");
            cCommands.Add("colar texto selecionado");
            cCommands.Add("salvar este arquivo");
            cCommands.Add("selecionar tudo");
            cCommands.Add("nova linha");

            // Comandos do canal Código Logo
            cCommands.Add("reproduza um vídeo do canal");


            //Choices cNumbers = new Choices(File.ReadAllLines("n.txt")); // números

            Choices cProcess = new Choices(); // lista de comandos
            cProcess.Add("bloco de notas");
            cProcess.Add("windows media player");
            cProcess.Add("prompt de comando");
            cProcess.Add("gerenciador de tarefas");
            cProcess.Add("minhas pastas");
            cProcess.Add("calculadora");
            cProcess.Add("mapa de caracteres");
            cProcess.Add("limpeza de disco");
            cProcess.Add("gerenciamento de cores");
            cProcess.Add("serviços de componente");
            cProcess.Add("gerenciamento de computador");
            cProcess.Add("definir programas padrão");
            cProcess.Add("painel de controle");
            cProcess.Add("otimizador de texto");
            cProcess.Add("calibragem de cores");
            cProcess.Add("desfragmentador de disco");
            cProcess.Add("adicionar um novo dispositivo");
            cProcess.Add("gerenciador de dispositivos");
            cProcess.Add("discagem telefônica");
            cProcess.Add("gerenciamento de disco");

            Choices cCustomSites = new Choices(); // lista de comandos do usuário

            Choices webSearch = new Choices();
            StreamReader srWords = new StreamReader("words.txt", Encoding.UTF8);
            while (srWords.Peek() >= 0)
            {
                try
                {
                    webSearch.Add(srWords.ReadLine());
                }
                catch { }
            }
            srWords.Close();

            // vamos processar o comandos.txt
            for (int i = 0; i < cmds.Length; i++)
            {
                try
                {
                    if (cmds[i].StartsWith("site#"))
                    {
                        cmds[i] = cmds[i].Replace("site#", "");
                        string[] temp = cmds[i].Split('#');
                        cCustomSites.Add(temp[0]); // adicionamos a palavra na gramática
                        dictCmdSites.Add(temp[0], temp[1]);

                    }
                }
                catch { }
                
            }

            // Gramática do alarme
            GrammarBuilder gbAlarm = new GrammarBuilder();
            gbAlarm.Append(new Choices("defina alarme", "alarme às", "despertador às"));
            gbAlarm.Append(cAlarm);
            gbAlarm.Append(new Choices("horas da manhã", "horas da tarde", "horas da noite"));

            // GrammarsBuilders
            GrammarBuilder gbChats = new GrammarBuilder(); // vamos criar um grammaBuilder para as conversas
            gbChats.Append(cChats); // já foi feito

            GrammarBuilder gbDumme = new GrammarBuilder(); // conversa solta
            gbDumme.Append(cDummes); 

            GrammarBuilder gbCommands = new GrammarBuilder(); //para a lista de comandos
            gbCommands.Append(cCommands); // feito

            GrammarBuilder gbControls = new GrammarBuilder();
            gbControls.Append(cControls);
            
            /*
            GrammarBuilder gbCalculations = new GrammarBuilder(); // gramática que vai fazer cálculos
            gbCalculations.Append("quanto é"); // primeira parte
            gbCalculations.Append(cNumbers); // números
            gbCalculations.Append(new Choices("mais", "menos", "dividido por", "vezes", "porcento de")); 
            gbCalculations.Append(cNumbers); // números novamente
            // essa gramática pode fazer por exemplo "quanto é 65 vezes 42", "quanto é 14 porcento de 500" 
            */

            GrammarBuilder gbProcess = new GrammarBuilder();
            gbProcess.Append(new Choices("abrir", "fechar")); // comando
            gbProcess.Append(cProcess); // adicionar lista de processos

            GrammarBuilder gbCustomSites = new GrammarBuilder(); // sites e páginas
            gbCustomSites.Append(new Choices("abrir", "iniciar", "carregar", "ir para o")); // parametros
            gbCustomSites.Append(cCustomSites);

            GrammarBuilder gbWebSearch = new GrammarBuilder();
            gbWebSearch.Append(new Choices("pesquisar", "buscar", "procurar", "buscar por", "pesquisar vídeo de", "imagem de", 
                "imagens de", "pesquisar imagem de", "pesquisar por" ));
            gbWebSearch.Append(webSearch);
            gbWebSearch.Append(new Choices("no youtube", "no google"));


            GrammarBuilder gbQA = new GrammarBuilder(); // gramática para responder perguntas
            gbQA.Append(new Choices(commandsForQA.ToArray()));
            gbQA.Append(cAIML);

            // Grammars

            Grammar gQA = new Grammar(gbQA);
            gQA.Name = "QA";

            Grammar gChats = new Grammar(gbChats); // gramática das conversas
            gChats.Name = "Chats"; // damos um nome para a gramática, pois vamos usa isso mais adiante

            Grammar gDumme = new Grammar(gbDumme);
            gDumme.Name = "Dumme"; // nome

            Grammar gCommands = new Grammar(gbCommands); // gramática dos comandos
            gCommands.Name = "Commands"; // nome 

            Grammar gCustomSites = new Grammar(gbCustomSites); // gramáticas dos sites
            gCustomSites.Name = "Sites";


            Grammar gAIML = new Grammar(new GrammarBuilder(cAIML));
            gAIML.Name = "AIML";
            
            /*
            Grammar gCalculations = new Grammar(gbCalculations);
            gCalculations.Name = "Calculations"; */

            Grammar gProcess = new Grammar(gbProcess);
            gProcess.Name = "Process";
            // Agora vamos carregar as gramáticas

            Grammar gWebSearch = new Grammar(gbWebSearch);
            gWebSearch.Name = "Search";


            Grammar gControls = new Grammar(gbControls);
            gControls.Name = "Control";

            // podemos fazer de várias maneiras, por enquanto vou fazer o seguinte
            // Lista de gramáticas 
            List<Grammar> grammars = new List<Grammar>();
            grammars.Add(gQA);
            grammars.Add(gChats);
            grammars.Add(gDumme);
            grammars.Add(gCommands); // comandos
            grammars.Add(gAIML);
            grammars.Add(gWebSearch);
            grammars.Add(gControls);
            //grammars.Add(gCalculations);
            grammars.Add(gProcess);
            grammars.Add(gCustomSites);

            ParallelOptions op = new ParallelOptions() { MaxDegreeOfParallelism = 4 };
            Parallel.For(0, grammars.Count, op, i => // loop paralelo
                {
                    sre.LoadGrammar(grammars[i]); // carregar gramática
                });

            speechRecognitionActived = true; // reconhecimento de voz ativo!
            sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(reconhecido); // evento do reconhecimento
            sre.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(audioElevou); // quando o aúdio é elevadosre
            sre.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(rejeitado); // quando o reconhecimento de voz falhou
            sre.SpeechDetected += new EventHandler<SpeechDetectedEventArgs>(vozDetectada); // alguma voz foi detectada
            sre.LoadGrammarCompleted += new EventHandler<LoadGrammarCompletedEventArgs>(loaded); // gramática carregada
            sre.RecognizeAsync(RecognizeMode.Multiple); // iniciar o reconhecimento async e múltiplo
        }

        public Form1()
        {
            InitializeComponent();
        }


        // Método chamado quando o Form for carregado
        private void Form1_Load(object sender, EventArgs e)
        {
            AIML.ConfigAIMLFiles();

            Thread.CurrentThread.Priority = ThreadPriority.Highest; // setamos o nível de prioridade da thread atual
            // isso manda o Sistema ajudar essa bagaça
            askRules.Add("o que é");
            askRules.Add("quem é");
            askRules.Add("quem foi");
            askRules.Add("defina");
            askRules.Add("o que são");
            askRules.Add("fale sobre");
            askRules.Add("definição de");
            askRules.Add("definição da");
            askRules.Add("busca");
            askRules.Add("artigo sobre");

            // trackBar
            trackBar1.Maximum = 20;
            trackBar1.Value = 10;
            Speaker.SetVolume(100);

            // detalhes do computador
            //lblInfo.Text += SystemInfo.GetUserNameString() + "\n";
            //lblInfo.Text += SystemInfo.GetOSArchString() + "\n";

            // Aqui vamos cuidar dos elementos visuais
            progressBar1.Maximum = 100; // valor máximo da progressBar
            pictureBox1.Visible = false; // não vai mostrar a pictureBox1, a que vai ser usada para mostrar o status
            progressBarCPUUsage.Maximum = 100;

            SetVoice(); // chamar o método para setar a voz

            LoadSpeechRecognition(); // chamar o método que vai fazer o reconhecimento

            LoadUserSettings(); // carregar info do usuário

            // Saudação
            InitializingProgram.Start();

            //loadingSystem = new LoadingSystem();
            //loadingSystem.ShowDialog(); // mostrat diálogo
            //Speaker.Speak("olá senhor, em que posso ajudar?"); // falar algo

            timer2.Tick += timer2_Tick; // timer do evento das progressBar's 1,5 segundos de intervalo
        }

        // Método do evento do reconhecimento
        private void reconhecido(object s, SpeechRecognizedEventArgs e) // passamos a classe EventArgs SpeechRecognized
        {
            string speech = e.Result.Text; // criamos uma variável que contêm a palavra ou frase reconhecida
            double confidence = e.Result.Confidence; // criamos uma variável para a confiança
            if (confidence > 0.4)// pegar o resultado da confiança, se for maior que 35% faz algo
            {
                label1.Text = "Reconhecido:\n" + speech; // mostrar o que foi reconhecido

                
                switch (e.Result.Grammar.Name) // vamos usar o nome da gramática para executar as ações
                {
                    case "Chats": // caso for uma conversa
                        Conversation.SaySomethingFor(speech); // vamos usar a classe que faz algo sobre
                        break;

                    case "Dumme":
                        if (DummeIn.InStartingConversation.Any(x => x == speech))
                        {
                            int randIndex = rnd.Next(0, DummeOut.OutStartingConversation.Count);
                            Speaker.Speak(DummeOut.OutStartingConversation[randIndex]);
                        }
                        else if (DummeIn.InQuestionForDumme.Any(x => x == speech))
                        {
                            int randIndex = rnd.Next(0, DummeOut.OutQuestionForDumme.Count);
                            Speaker.Speak(DummeOut.OutQuestionForDumme[randIndex]);
                        }
                        else if(DummeIn.InDoWork.Any(x => x == speech))
                        {
                            int randIndex = rnd.Next(0, DummeOut.OutDoWork.Count);
                            Speaker.Speak(DummeOut.OutDoWork[randIndex]);
                        }
                        else if(DummeIn.InDummeStatus.Any(x => x == speech))
                        {
                            int randIndex = rnd.Next(0, DummeOut.OutDummeStatus.Count);
                            Speaker.Speak(DummeOut.OutDummeStatus[randIndex]);
                        }
                        else if (DummeIn.InJarvis.Any(x => x == speech))
                        {
                            int randIndex = rnd.Next(0, DummeOut.OutJarvis.Count);
                            Speaker.Speak(DummeOut.OutJarvis[randIndex]);
                        }
                        break;
                    case "Commands":
                        if (speech == "até mais jarvis")
                        {
                            ExitNow(); // chama oo método
                        }
                        else if (speech == "minimizar a janela principal")
                        {
                            MinimizeWindow(); // minimizar
                        }
                        else if (speech == "mostrar janela principal")
                        {
                            BackWindowToNormal(); // mostrar janela principal
                        }
                        else
                        {
                            Commands.Execute(speech);
                        }
                        break;
                    case "Jokes":
                        break;
                    case "Calculations":
                        Calculations.DoCalculation(speech);
                        break;
                    case "Process":
                        ProcessControl.OpenOrClose(speech);
                        break;
                    case "Sites": // caso sites
                        string[] replaces = { "abrir", "iniciar", "carregar", "ir para o" };
                        string site = "";
                        for (int i = 0; i < replaces.Length; i++)
                        {
                            if (speech.StartsWith(replaces[i])) // verifica se string começa com um destes, se sim remove ele.
                            {
                                site = speech.Replace(replaces[i], "");
                            }
                        }
                        site = site.Trim();
                        foreach (KeyValuePair<string, string> entry in dictCmdSites)
                        {
                            if (entry.Key== site)
                            {
                                Speaker.SpeakOpenningProcess(site);
                                Process.Start(entry.Value);
                            }
                        }
                        break;
                    case "Control":
                        motionDetection = new MotionDetection();
                        motionDetection.Show();
                        break;

                    case "Search":
                        string[] parts = speech.Split(' ');
                        string text = "";
                        for(int k = 0; k < parts.Length; k++)
                        {
                            if (System.Text.RegularExpressions.Regex.IsMatch(parts[k], "[A-Z]"))
                            {
                                text += parts[k] + " ";
                            }
                        }
                        if (speech.ToLower().EndsWith("youtube"))
                        {
                            if(speech.Contains(" "))
                            speech = speech.Replace(" ", "+");
                            Speaker.SpeakRand("certo pesquisando " + text + " no youtube", "tudo bem", "okei, buscando por " + text);
                            loadPage();
                            webLoader.Start("https://www.youtube.com/results?search_query=" + text);
                        }
                        else if (speech.ToLower().EndsWith("google"))
                        {
                            if (speech.Contains(" "))
                                speech = speech.Replace(" ", "+");
                            Speaker.SpeakRand("certo pesquisando " + text + " no google", "tudo bem", "okei, buscando por " + text, 
                                "okei, buscando por " + text + " no google");
                            loadPage();
                            webLoader.Start("http://images.google.com/images?q=" + text + "&start=900&filter=1");
                        }
                        break;

                    case "QA":
                        foreach (var t in commandsForQA)
                        {
                            if (speech.StartsWith(t))
                                speech = speech.Replace(t, "");
                        }
                        Speaker.Speak(AIML.GetOutputChat(speech));
                        break;
                    default: // caso padrão
                        Speaker.Speak(AIML.GetOutputChat(speech)); // pegar resposta
                        break;
                }
            }
        }

        public void loadPage()
        {
            if (webLoader == null)
            {
                webLoader = new WebLoader();
                webLoader.Show();
            }
            else
            {

            }
        }

        // Método do aúdio elevado
        private void audioElevou(object s, AudioLevelUpdatedEventArgs e) // passamos a classe
        {
            progressBar1.Value = e.AudioLevel; // setando o valor da progressBar igual ao valor do aúdio em percento

            if (speechRecognitionActived == false)
            {
                Speaker.SpeakRand("fale um comando", "Você já pode me pedir para fazer algo!",
                    "estou aqui!");
                speechRecognitionActived = true;
            }
        }

        // Método do erro no reconhecimento
        private void rejeitado(object s, SpeechRecognitionRejectedEventArgs e) // passamos a classe
        {
            pictureBox1.Image = (Bitmap)Bitmap.FromFile("Pictures\\fall.jpg"); // imagem do erro, carregamos ela
        }

        // Método que será chamado quando a voz for detectada
        private void vozDetectada(object s, SpeechDetectedEventArgs e)
        {
            pictureBox1.Visible = true; // mostrar a pictureBox1
            pictureBox1.Image = (Bitmap)Bitmap.FromFile("Pictures\\ok.jpg"); // carreganda a imagem
        }

        // Gramática carregada
        int grammars = 0;
        private void loaded(object s, LoadGrammarCompletedEventArgs e)
        {
            grammars++;
            //Speaker.Speak("já foram carregadas " + grammars + " gramáticas.");
        }

        // quando clicar na pictureBox1
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (speechRecognitionActived == true) // se o reconhecimento de voz estiver ativo
            {
                sre.RecognizeAsyncCancel(); // para o reconhecimento
                speechRecognitionActived = false; // altera o valor da booleana
                Speaker.Speak("reconhecimento de voz desativado"); // diz algo
            }
            else if (speechRecognitionActived == false)
            {
                sre.RecognizeAsync(RecognizeMode.Multiple);
                speechRecognitionActived = true;
                Speaker.Speak("reconhecimento de voz ativado");
            }
        }

        // Atualizar dados do sistema
        private bool showMemoryFree = false;
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (showMemoryFree == false)
            {
                int memFree = (int)PCStats.GetFreeMemory();
                int memUsage = (int)PCStats.GetTotalMemory() - memFree;
                progressBarMemory.Maximum = (int)PCStats.GetTotalMemory();
                progressBarMemory.Value = memUsage;
                label3.Text = "Memória Usada: " + memUsage + " MB";
                showMemoryFree = true;

                double cpuUsage = PCStats.GetCPUUsage();
                label2.Text = "Uso de CPU: " + Math.Round(cpuUsage, 2) + "%";
                progressBarCPUUsage.Value = Convert.ToInt32(cpuUsage);
            }
            else
            {
                int memFree = (int)PCStats.GetFreeMemory();
                label3.Text = "Memória Livre: " + memFree + " MB";
                progressBarMemory.Maximum = (int)PCStats.GetTotalMemory();
                progressBarMemory.Value = memFree;
                showMemoryFree = false;

                double cpuUsage = PCStats.GetCPUUsage();
                label2.Text = "Uso de CPU: " + Math.Round(cpuUsage, 2) + "%";
                progressBarCPUUsage.Value = Convert.ToInt32(cpuUsage);
            }
        }


        // evento das teclas
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Keys k = (Keys)e.KeyChar;

            if(k==Keys.Escape)
            {
                ExitNow(); // cham o método
            }
            else if (e.KeyChar == 'h' || e.KeyChar == 'H')
            {
                JARVISHelp.Introduction(); // introdução
            }
            else if (e.KeyChar == 's' || e.KeyChar == 'S')
            {
                Speaker.StopSpeak(); // parar de falar
            }
            else if (k == Keys.Up)
            {
                Speaker.VolumeUp();
            }
            else if (k == Keys.Down)
            {
                Speaker.VolumeDown();
            }
            else if (e.KeyChar == 'p' || e.KeyChar == 'P')
            {
                Speaker.ResumeOrPause(); // pausa ou resume
            }
            else if (e.KeyChar == 'i' || e.KeyChar == 'I')
            {
                SystemInfo.GetUserName();
                SystemInfo.GetMachineName();
                SystemInfo.GetOSVersion();
                SystemInfo.OSArch();
            }
            else if (e.KeyChar == 't' || e.KeyChar == 'T')
            {
                if (processList == null)
                {
                    processList = new ProcessList();
                    processList.Show();
                    processList.ShowProcesses();
                }
                else
                {
                    processList.ShowProcesses();
                }
            }
            else if (e.KeyChar == 'a' || e.KeyChar == 'A')
            {
                if (appsDialog == null)
                {
                    appsDialog = new AppsDialog();
                    appsDialog.Show();
                }
                else
                {
                    appsDialog.Show();
                }
            }
            else if (k==Keys.D5)
            {
                ChangeSkin();
            }
            else if (k == Keys.D6)
            {
                ChangeBackColor();
            }
            else if (k == Keys.D4) 
            {
                if (selectVoice == null) // e for null
                {
                    selectVoice = new SelectVoice();
                    selectVoice.ShowDialog(); // diálogo
                    selectVoice.Close(); // já foi mostrado, então vamos fechar
                    selectVoice = null; // deixando nulo 
                }
            }
            else if (k == Keys.D1)
            {
                MinimizeWindow();
            }
            else if (k == Keys.D2)
            {
                BackWindowToNormal();
            }
            else if (e.KeyChar == 'm' || e.KeyChar == 'M')
            {
                motionDetection = new MotionDetection();
                motionDetection.Visible = false;
                motionDetection.Show();
            }
            else if (e.KeyChar == 'f' || e.KeyChar == 'F')
            {
                personId = new PersonIdentifier();
                personId.Show();
                personId.StartRecognition();
            }
        }

        // Sair do jarvis 
        private void ExitNow()
        {
            Speaker.SpeakSync("certo, até mais, senhor!"); // diz algo
            this.Close(); // fecha tudo o que é do jarvis
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            /*
            if (showMemoryUsage == false) // se mostrar memória usada for falsa, vasmos mostrar memória livre
            {
                int memUsage = (int)PCStats.GetTotalMemory() - (int)PCStats.GetFreeMemory();
                label3.Text = "Memória Usada: " + memUsage.ToString() + " MB";
                progressBarMemory.Maximum = (int)PCStats.GetTotalMemory();
                progressBarMemory.Value = memUsage;
                showMemoryUsage = true;

                double cpuUsage = PCStats.GetCPUUsage();
                progressBarCPUUsage.Maximum = 100;
                progressBarCPUUsage.Value = (int)cpuUsage; // uso de CPU
                label2.Text = "Uso de CPU: " + Math.Round(cpuUsage, 2) + "%";
            }
            else
            {
                label3.Text = "Memória Livre: " + PCStats.GetFreeMemory().ToString() + " MB";
                progressBarMemory.Maximum = (int)PCStats.GetTotalMemory();
                progressBarMemory.Value = (int)PCStats.GetFreeMemory();
                showMemoryUsage = false;

                double cpuUsage = PCStats.GetCPUUsage();
                progressBarCPUUsage.Maximum = 100;
                progressBarCPUUsage.Value = (int)cpuUsage; // uso de CPU
                label2.Text = "Uso de CPU: " + Math.Round(cpuUsage, 2) + "%";
            }
            */
            double cpuUsage = PCStats.GetCPUUsage();
            label2.Text = "Uso de CPU: " + Math.Round(cpuUsage, 2) + "%";
            progressBarCPUUsage.Value =  Convert.ToInt32(cpuUsage);
            if (showMemoryFree == false)
            {
                int memFree = (int)PCStats.GetFreeMemory();
                int memUsage = (int)PCStats.GetTotalMemory() - memFree;
                progressBarMemory.Maximum = (int)PCStats.GetTotalMemory();
                progressBarMemory.Value = memUsage;
                label3.Text = "Memória Usada: " + memUsage + " MB";
                showMemoryFree = true;
            }
            else
            {
                int memFree = (int)PCStats.GetFreeMemory();
                label3.Text = "Memória Livre: " + memFree + " MB";
                progressBarMemory.Maximum = (int)PCStats.GetTotalMemory();
                progressBarMemory.Value = memFree;
                showMemoryFree = false;
            }
        }

        public void CloseJarvis()
        {
            this.Close();
        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            trackBar1.Maximum = 20;
            Speaker.SetVolume(trackBar1.Value * 5);
        }

        public void ChangeSkin() // vamos usar async para evitar travamentos
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                Speaker.Speak("selecione uma imagem para aplicar");
                ofd.InitialDirectory = Directory.GetCurrentDirectory() + "\\Themes";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.BackgroundImage = (Image)Image.FromFile(ofd.FileName);
                }
            }
            catch (Exception ex)
            {
                Speaker.Speak("erro " + ex.Message);
            }
        }

        public void ChangeBackColor()
        {
            try
            {
                ColorDialog cd = new ColorDialog();
                Speaker.Speak("selecione uma cor para o fundo");
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    this.BackColor = cd.Color;
                }
            }
            catch (Exception ex)
            {
                Speaker.Speak("erro " + ex.Message);
            }
        }

        public void MinimizeWindow() // minimizar janela
        {
            try
            {
                if (this.WindowState == FormWindowState.Normal || this.WindowState == FormWindowState.Maximized) // se a janela estiver normal ou maximizada
                {
                    this.WindowState = FormWindowState.Minimized;
                    Speaker.Speak("minimizando a janela principal");
                }
            }
            catch(Exception ex)
            {
                Speaker.Speak("erro " + ex.Message);
            }
        }

        public void BackWindowToNormal()
        {
            try
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    this.WindowState = FormWindowState.Normal;
                    Speaker.Speak("tudo bem, voltando ao tamanho normal");
                }
            }
            catch (Exception ex)
            {
                Speaker.Speak("erro " + ex.Message);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://goo.gl/2E2gMB");
        }
    }
}
