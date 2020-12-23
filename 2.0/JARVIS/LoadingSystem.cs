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
    public partial class LoadingSystem : Form
    {
        public LoadingSystem()
        {
            InitializeComponent();
        }

        private void LoadingSystem_Load(object sender, EventArgs e)
        {
            progressBar1.Maximum = 100;
            timer1.Interval = 20;
            SoundEffects.InitializingSystem(); // mágica do C#
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = 50;
            while (progressBar1.Value < 100)
            {
                progressBar1.Value = progressBar1.Value + 1;
            }
            StopLoading();
            timer1.Enabled = false;
            this.Close();
        }

        public void StopLoading()
        {
            Speaker.StopSpeak();
            SoundEffects.StopPlayer();
        }
    }
}
