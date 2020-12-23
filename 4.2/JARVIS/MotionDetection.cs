using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// AForge Namespaces
using AForge.Video; // vídeo
using AForge.Video.DirectShow; // dispositivos
using AForge.Vision.Motion; // detecção de movimentos

namespace JARVIS
{
    public partial class MotionDetection : Form
    {
        public bool hasDevice = false;
        public FilterInfoCollection videoDevices = null;
        public VideoCaptureDevice source = null;
        public AForge.Vision.Motion.MotionDetector detector = null;
        public float Confidence { get; set; }

        public void getDevices()
        {
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                comboBox1.Items.Clear(); // limpar items

                foreach (FilterInfo fi in videoDevices)
                {
                    comboBox1.Items.Add(fi.Name); // adicionar dispositivo
                }
                comboBox1.SelectedIndex = 0;
                hasDevice = true;
            }
            catch (Exception ex)
            {
                Speaker.Speak("erro " + ex.Message);
                hasDevice = false;
            }
        }

        public MotionDetection()
        {
            InitializeComponent();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MotionDetection_Load(object sender, EventArgs e)
        {
            getDevices();
            detector = new AForge.Vision.Motion.MotionDetector(new TwoFramesDifferenceDetector(), new MotionBorderHighlighting());
            Detect();
            Speaker.Speak("detecção de movimento iniciada!");
        }

        public void Detect()
        {
            if (hasDevice)
            {
                source = new VideoCaptureDevice(videoDevices[comboBox1.SelectedIndex].MonikerString);

                source.NewFrame += new NewFrameEventHandler(frame);

                ClosevideoDevice();

                source.DesiredFrameSize = new System.Drawing.Size(320, 240);

                source.Start();

            }
        }

        private void frame(object s, NewFrameEventArgs e)
        {
            Bitmap img = (Bitmap)e.Frame.Clone();

            Confidence = detector.ProcessFrame(img);

            pictureBox1.Image = img;
        }

        public void ClosevideoDevice()
        {
            if (source != null)
            {
                if (source.IsRunning == true)
                {
                    source.SignalToStop();
                    source = null;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = Confidence.ToString() + " %";
            if (Confidence > 0.01f)
                Speaker.SpeakRand(new string[] { "tem alguém ai?", "quem está ai?", "quem é você?", "alguém foi detectado se movimentando!", "algo estar se movendo!" });
            Confidence = 0.0f;
        }

        private void iniciarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Detect();
        }

        private void MotionDetection_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClosevideoDevice();
        }
    }
}
