using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JARVIS
{
    public partial class CommandList : Form
    {
        private List<string> comandos = null;

        public CommandList()
        {
            InitializeComponent();
        }

        private void CommandList_Load(object sender, EventArgs e)
        {
            comandos = new List<string>();

            comandos.Add("Que horas são");
            comandos.Add("Que dia é hoje");

            foreach (var comando in comandos)
            {
                listBox1.Items.Add(comando);
            }
        }
    }
}
