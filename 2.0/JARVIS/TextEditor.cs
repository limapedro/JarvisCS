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
    public partial class TextEditor : Form
    {
        public TextEditor()
        {
            InitializeComponent();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseTextEditor();
        }

        public void OpenFile()
        {
            Speaker.Speak("selecione um arquivo de texto, para abrir");
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(ofd.FileName, Encoding.Default);
                richTextBox1.Text = sr.ReadToEnd();
            }
        }

        public void CloseTextEditor()
        {
            Speaker.Speak("saindo do editor de texto");
            this.Close();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }
    }
}
