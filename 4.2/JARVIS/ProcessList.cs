using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics; // namespace usado

namespace JARVIS
{
    public partial class ProcessList : Form
    {
        private int processIndex = 0;
        private Process[] procs;
        private Dictionary<string, int> dictProcess;
        public ProcessList()
        {
            InitializeComponent();
        }

        private void ProcessList_Load(object sender, EventArgs e)
        {
            dictProcess = new Dictionary<string, int>();
            listBox1.Items.Clear(); // limpar
            Speaker.Speak("mostrando os processos em execução");
            procs = Process.GetProcesses();
            int i = 0;
            foreach (var proc in procs)
            {
                if (proc.MainWindowTitle != "")
                {
                    long mem = proc.PagedMemorySize64 / 1024;
                    listBox1.Items.Add(proc.MainWindowTitle + " " + mem.ToString() + " KB");
                    dictProcess.Add(proc.ProcessName, i);
                    i++;
                }
            }
        }

        public void ShowProcesses()
        {
            listBox1.Items.Clear(); // limpar
            Speaker.Speak("mostrando os processos em execução");
            procs = Process.GetProcesses();
            foreach (var proc in procs)
            {
                if (proc.MainWindowTitle != "")
                {
                    long mem = proc.PagedMemorySize64 / 1024;
                    listBox1.Items.Add(proc.MainWindowTitle  + " " + mem.ToString() + " KB");
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            processIndex = listBox1.SelectedIndex;
        }

        public void CloseSelectedProcess()
        {
            try
            {
                Process[] p = Process.GetProcessesByName(procs[processIndex].MainWindowTitle);
                foreach (var a in p)
                {
                    a.Kill();
                }
                Speaker.SayWithStatus("fechando " + procs[processIndex].MainWindowTitle);
            }
            catch (Exception ex)
            {
                Speaker.Speak("erro " + ex.Message);
            }
        }
    }
}
