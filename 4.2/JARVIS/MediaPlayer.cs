using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; // namespace para E/S de arquivos

namespace JARVIS
{
    public partial class MediaPlayer : Form
    {
        private string[] supportedFiles = { ".mp3", ".mp4", ".wav", ".wmv", ".wma", ".avi" }; // arquivos suportados
        public MediaPlayer()
        {
            InitializeComponent();
        }

        // Form carregado
        private void MediaPlayer_Load(object sender, EventArgs e)
        {

        }

        // abrir um arquivo para execução no menuStrip
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile(); // chama o método
        }

        // Métodos do MediaPlayer
        public void OpenFile() // método que vai abrir uma janela para reproduzir um arquivo
        {
            OpenFileDialog ofd = new OpenFileDialog();// instancia do diálogo
            if (ofd.ShowDialog() == DialogResult.OK) // se o diálogo for okay
            {
                PlayFile(ofd.FileName); // reproduz o arquivo selecionado
            }
        }

        public void OpenDirectory() // selecionar diretório
        {
            Speaker.Speak("certo, selecione um diretório para obter arquivos");
            FolderBrowserDialog fd = new FolderBrowserDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                PlayDirectory(fd.SelectedPath); // chamar método
            }
        }

        // Método para reproduzir arquivo
        public void PlayFile(string file) // reproduz o arquivo
        {
            axWindowsMediaPlayer1.URL = file; // passa o endereço do arquivo pro componente do Windows Media Player
        }

        // Reproduizr lista de arquivos
        public void PlayListFiles(List<string> files)
        {
            var playList = axWindowsMediaPlayer1.playlistCollection.newPlaylist("Lista de arquivos");

            foreach (var file in files) // percorrer arquivos
            {
                playList.appendItem(axWindowsMediaPlayer1.newMedia(file)); // adicionar arquivo na playlist
            }
            axWindowsMediaPlayer1.Ctlcontrols.play(); // iniciar lista de reprodução
            axWindowsMediaPlayer1.currentPlaylist = playList; // setar a playlist
            axWindowsMediaPlayer1.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(state); // Método
            // das ações que ocorrem no media player
            Speaker.Speak("reproduzindo lista de arquivos");

        }
        private void state(object s, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {

        }

        public void PlayDirectory(string dir)
        {
            string[] files = Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories); // obter arquivos, de todos os formatos, e em sub-diretórios
            List<string> filesSupported = new List<string>();
            foreach (var file in files) // percorrer arquivos
            {
                foreach (var format in supportedFiles) // percorrer formatos suportados
                {
                    if (file.EndsWith(format)) // se o arquivo atual tiver extensão suportada
                    {
                        filesSupported.Add(file); // adicionar o arquivo
                    }
                }
            }
            PlayListFiles(filesSupported); // reproduzir lista de arquivos
        }

        // Método play
        public void Play()
        {
            axWindowsMediaPlayer1.Ctlcontrols.play(); // método play
            Speaker.Speak("reproduzindo arquivo!");
        }

        // Método pause
        public void Pause()
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause(); // pausa
            Speaker.Speak("pausando a reprodução");
        }

        public void Stop()
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop(); // parar
            Speaker.Speak("parando");
        }

        // Controle dos loops
        public void NextFile()
        {
            axWindowsMediaPlayer1.Ctlcontrols.next(); // próximo arquivo
            Speaker.Speak("indo para o próximo");
        }

        public void BackFile()
        {
            axWindowsMediaPlayer1.Ctlcontrols.previous(); // anterior
            Speaker.Speak("indo para o anterior");
        }

        public void VolumeUp()
        {
            axWindowsMediaPlayer1.settings.volume += 10;
            Speaker.SayWithStatus("aumentado o volume do media player");
        }

        public void VolumeDown()
        {
            axWindowsMediaPlayer1.settings.volume -= 10;
            Speaker.SayWithStatus("diminuindo o volume do media player");
        }

        public void Mute()
        {
            axWindowsMediaPlayer1.settings.mute = true;
            Speaker.SayWithStatus("deixando o media player mudo");
        }

        public void UnMute()
        {
            axWindowsMediaPlayer1.settings.mute = false;
            Speaker.SayWithStatus("tirando o media player do mudo");
        }

        public void FullScreen()
        {
            try
            {
                axWindowsMediaPlayer1.fullScreen = true;
                Speaker.Speak("media player em tela cheia");
            }
            catch (Exception ex)
            {
                Speaker.Speak(ex.Message);
            }
        }

        public void SayFileThatIsPlaying()
        {
            string file = axWindowsMediaPlayer1.currentMedia.sourceURL;
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                string[] vet = file.Split('\\');
                Speaker.Speak(vet[vet.Length - 1]);
            }
        }

        private void abrirDitetórioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDirectory(); // chamar método
        }
    }
}
