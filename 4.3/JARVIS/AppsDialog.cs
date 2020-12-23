using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace JARVIS
{
    public partial class AppsDialog : Form
    {
        public AppsDialog()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Speaker.SpeakOpenningProcess("google");
            Process.Start("http://www.google.com");
        }

        private void AppsDialog_Load(object sender, EventArgs e)
        {
            Speaker.SpeakOpenningProcess("janela de sites e aplicativos");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Speaker.SpeakOpenningProcess("youtube");
            Process.Start("http://www.youtube.com");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Speaker.SpeakOpenningProcess("facebook");
            Process.Start("http://www.facebook.com");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Speaker.SpeakOpenningProcess("media player classic");
            Process.Start(@"C:\Program Files\MPC-HC\mpc-hc.exe");
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Speaker.SpeakOpenningProcess("vlc media player");
            Process.Start(@"C:\Program Files\VideoLAN\VLC\vlc.exe");
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Speaker.SpeakOpenningProcess("windows media player");
            Process.Start("wmplayer");
        }
    }
}
