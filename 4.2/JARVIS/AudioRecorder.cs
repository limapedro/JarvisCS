using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;

namespace JARVIS
{
    public partial class AudioRecorder : Form
    {
        private WaveIn wavesource = null; // stream que vem do microfone
        private WaveFileWriter writer = null; // stream que vai gravar o arquivo
        public AudioRecorder()
        {
            InitializeComponent();
        }

        private void AudioRecorder_Load(object sender, EventArgs e)
        {
            button1.Enabled = true; // botão iniciar ativado
            button2.Enabled = false; // botão parar desativado
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartRecording();
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StopRecording();
        }

        // método para iniciar a gravação de voz
        public void StartRecording()
        {
            button1.Enabled = false; // desabilitar o botão1
            button2.Enabled = true; // habilitar o botão2

            wavesource = new WaveIn(); // instancia a stream de entrada
            wavesource.WaveFormat = new WaveFormat(44100, 1); // definindo os parametros do aúdio, qualidade e número de canais

            wavesource.DataAvailable += new EventHandler<WaveInEventArgs>(data); // evento da gravação
        }

        public void StopRecording()
        {
            wavesource.StopRecording();
            button1.Enabled = true;
            button2.Enabled = false;
        }

        private void data(object s, WaveInEventArgs e)
        {
            if (writer != null) // se a stream de escrita estiver instanciada
            {
                writer.Write(e.Buffer, 0, e.Buffer.Length); // escrever os bytes
                writer.Flush();
            }
        }
    }
}
