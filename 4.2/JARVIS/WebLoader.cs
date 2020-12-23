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
    public partial class WebLoader : Form
    {
        public WebLoader()
        {
            InitializeComponent();
        }

        private void WebLoader_Load(object sender, EventArgs e)
        {

        }

        public void Start(string url)
        {
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.Navigate(url);
        }

        private void WebLoader_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1.webLoader = null;
        }
    }
}
