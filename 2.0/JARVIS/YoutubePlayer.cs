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
    public partial class YoutubePlayer : Form
    {
        public YoutubePlayer()
        {
            InitializeComponent();
        }

        private void YoutubePlayer_Load(object sender, EventArgs e)
        {

        }

        public void PlayUrl(string url)
        {
            try
            {
                url = url.Replace("watch?v=", "v/");
                axShockwaveFlash1.Movie = url;
                axShockwaveFlash1.Play();
                Speaker.Speak("reproduzindo vídeo do youtube!");
            }
            catch (Exception ex)
            {
                Speaker.Speak("ocorreu um erro! desculpe!");
            }
            
        }

        public void SearchVideo(string search)
        {

        }

        private void axShockwaveFlash1_Enter(object sender, EventArgs e)
        {

        }
    }
}
