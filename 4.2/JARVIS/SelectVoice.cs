using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JARVIS
{
    /// <summary>
    /// Form para alterar a voz do sintetizador
    /// </summary>
    public partial class SelectVoice : Form
    {
        private string[] voices; // vozes declarando string array
        public SelectVoice()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e) // cancelando e saindo do form
        {
            Speaker.Speak("cancelando"); // dz algo
            this.Close(); // fechar este form
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            // vamos tratar possíveis erros
            try
            {
                Speaker.SetVoice(voices[comboBox1.SelectedIndex]); // setamos a voz selecionada na comboBox
                Speaker.Speak("feito, voz já foi alterada");
            }
            catch (Exception ex)
            {
                Speaker.Speak("erro " + ex.Message);
            }
        }

        private void SelectVoice_Load(object sender, EventArgs e) // form carregado
        {
            voices = GetVoices.GetVoicesFromCulture("pt-BR"); // instanciar o array de vozes
            Speaker.Speak("certo.. altere a voz do sintetizador");
            foreach (var voice in voices) // percorrer vozes
            {
                comboBox1.Items.Add(voice); // adicionar voz
            }
            comboBox1.SelectedIndex = 0; // mostrar a primeria voz na comboBox
        }
    }
}
