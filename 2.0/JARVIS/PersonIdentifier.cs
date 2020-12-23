using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// namespaces do Emgu
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.IO;
using System.Diagnostics;


namespace JARVIS
{
    public partial class PersonIdentifier : Form
    {
        Image<Bgr, Byte> currentFrame;
        Capture capture;
        HaarCascade faces;
        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.5d, 0.5d);
        Image<Gray, byte> result, trainedFace = null;
        Image<Gray, byte> gray = null;
        List<Image<Gray, byte>> trainningImages = new List<Image<Gray, byte>>();
        List<string> Labels = new List<string>();
        List<string> Names = new List<string>();
        int contTrain, numLabels, t;
        string name, names = null;

        public PersonIdentifier()
        {
            InitializeComponent();
        }

        private void PersonIdentifier_Load(object sender, EventArgs e)
        {
            faces = new HaarCascade("haarcascade_frontalface_default.xml");

            try
            {
                string labelsInfo = File.ReadAllText(Application.StartupPath + "/PeopleData/names.txt");

                string[] labels = labelsInfo.Split('%');
                numLabels = Convert.ToInt16(labels[0]);
                contTrain = numLabels;
                string loadFaces;

                for (int tf = 1; tf < numLabels; tf++)
                {
                    loadFaces = "face" + tf + ".bmp";
                    trainningImages.Add(new Image<Gray, byte>(Application.StartupPath + "/PeopleData/" + loadFaces));
                    Labels.Add(labels[tf]);
                }
            }
            catch (Exception ex)
            {
                Speaker.Speak("sem imagens disponíves, não é possível fazer reconhecimento!");
            }
        }

        public void StartRecognition()
        {
            capture = new Capture();
            capture.QueryFrame();

            Application.Idle += new EventHandler(frame);
        }

        public void StopRecognition()
        {
            Application.Idle += frame;
        }

        private void frame(object s, EventArgs e)
        {
            Names.Add("");

            currentFrame = capture.QueryFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

            gray = currentFrame.Convert<Gray, Byte>();

            pictureBox1.Image = currentFrame.ToBitmap();

            MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(faces, 1.2, 10, HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(15, 15));

            foreach(MCvAvgComp f in facesDetected[0])
            {
                t = t + 1;
                result = currentFrame.Copy(f.rect).Convert<Gray, byte>().Resize(100, 100, INTER.CV_INTER_CUBIC);

                currentFrame.Draw(f.rect, new Bgr(Color.Red), 2);


                /*
                if(trainningImages.ToArray().Length!=0)
                {
                    MCvTermCriteria termCrit = new MCvTermCriteria(contTrain, 0.001);

                    EigenObjectRecognizer recognizer = new EigenObjectRecognizer(trainningImages.ToArray(), Labels.ToArray(), 3000, ref termCrit);

                    name = recognizer.Recognize(result);

                    currentFrame.Draw(name, ref font, new Point(f.rect.X - 2, f.rect.Y - 2), new Bgr(Color.DarkGreen));
                }

                Names.Add("");
                label1.Text = "Pessoas na cena: " + facesDetected[0].Length.ToString();

                for (int n = 0; n < facesDetected[0].Length; n++)
                {
                    names += Names[n] + ", ";
                }

                label2.Text = "Nomes " + name;
                
                Names.Clear();*/
            }
            pictureBox1.Image = currentFrame.ToBitmap();
        }

        private void iniciarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartRecognition();
        }

        private void PersonIdentifier_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Idle -= frame;
            capture.Dispose();
        }

        // Método para abrir uma imagem
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                StopRecognition();
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = (Bitmap)Bitmap.FromFile(ofd.FileName);
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                Speaker.Speak("Erro em reconhecimento facial: mensagem de erro " + ex.Message);
            }
        }
    }
}
