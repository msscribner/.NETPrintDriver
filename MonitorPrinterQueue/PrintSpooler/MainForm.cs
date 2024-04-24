using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Printing;
using Atom8.Monitors;


namespace PrintSpooler
{
    public partial class MainForm : Form
    {

        PrintQueueMonitor pqm = null;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            PrintServer ps = new PrintServer();
            foreach (PrintQueue pq in ps.GetPrintQueues())
                cmbPrinters.Items.Add(pq.Name);
        }

        void pqm_OnJobStatusChange(object Sender, PrintJobChangeEventArgs e)
        {
            MethodInvoker invoker = () =>
            {
                lbSpoolChanges.Items.Add(e.JobID + " - " + e.JobName + " - " + e.JobStatus);
            };
            if (lbSpoolChanges.InvokeRequired)
            {
                Invoke(invoker);
            }
            else
            {
                invoker();
            }
            
        }

        private void btnMonitor_Click(object sender, EventArgs e)
        {
            if (cmbPrinters.Enabled)
            {
                if (cmbPrinters.Text.Trim() == "") return;
                pqm = new PrintQueueMonitor(cmbPrinters.Text.Trim());
                pqm.OnJobStatusChange += new PrintJobStatusChanged(pqm_OnJobStatusChange);
                cmbPrinters.Enabled = false;
                btnMonitor.Text = "Stop Monitoring.....";
            }
            else
            {
                if (pqm !=null) pqm.Stop();
                pqm = null;
                cmbPrinters.Enabled = true;
                btnMonitor.Text = "Start Monitoring.....";
            }
        }
    }
}
