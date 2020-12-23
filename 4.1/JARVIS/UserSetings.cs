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
    public partial class UserSetings : Form
    {
        public UserProfile userProfile;
        private string[] voices;
        public UserSetings()
        {
            InitializeComponent();
        }

        private void UserSetings_Load(object sender, EventArgs e)
        {
            voices = GetVoices.GetVoicesFromCulture("pt-BR");
            foreach (var voice in voices)
                comboBox1.Items.Add(voice);
            comboBox1.SelectedIndex = 0;

            userProfile = new UserProfile(); // arquivo do usuário.
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                if (!string.IsNullOrEmpty(textBox2.Text))
                {
                    userProfile.Name = textBox1.Text;
                    userProfile.Email = textBox2.Text;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox1.Clear();
            this.textBox2.Clear();
            this.comboBox1.Items.Clear();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            userProfile.Voice = voices[comboBox1.SelectedIndex];
        }
    }
}
