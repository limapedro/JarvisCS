using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace JARVIS
{
    /// <summary>
    /// Form que vai ser útil para pessoas que não vão editar o código, mas sim adicionar comandos
    /// </summary>
    public partial class AddNewCommand : Form
    {
        public AddNewCommand()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                // vamos verificar os tipos de comandos
                string tagCmd = "";
                if (textBox2.Text.EndsWith(".com") || textBox2.Text.EndsWith(".com.br"))// tag é de um site
                {
                    tagCmd = "site#"; // adiciona a tag
                }
                else if (textBox2.Text.EndsWith(".") || textBox2.Text.EndsWith("?") || textBox2.Text.EndsWith("!")) // é uma conversa
                {
                    tagCmd = "chat#";
                }
                else // é um arquivo do usuário ou um programa
                {
                    tagCmd = "run#";
                }
                string cmd = textBox1.Text + "#" + textBox2.Text; // cria uma tag de comando
                File.AppendAllText("comandos.txt", tagCmd + textBox1.Text + "#" + textBox2.Text + "$", Encoding.UTF8); // vamos escrever a tag do comando 
                Speaker.Speak("comando adicionado!"); // diz algo

                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Speaker.Speak("cancelando, os comando adicionados, limpando caixas de texto");
            textBox1.Text = "";
            textBox2.Text = ""; 
        }

        private void AddNewCommand_Load(object sender, EventArgs e)
        {
            Speaker.Speak("bem-vindo, adicione novos comandos!");
        }
    }
}
