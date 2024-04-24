using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private static ManagementEventWatcher manEWatch;
        public Form1()
        {
            InitializeComponent();

//          System.Management.ManagementScope oMs = new System.Management.ManagementScope(@"\\" + host + @"\root\cimv2");
            ManagementScope oMs = new ManagementScope("\\root\\cimv2");
            oMs.Connect();
            manEWatch = new ManagementEventWatcher(oMs, new EventQuery("SELECT * FROM    __InstanceCreationEvent WITHIN 0.1 WHERE TargetInstance ISA 'Win32_PrintJob'"));

            manEWatch.EventArrived += new EventArrivedEventHandler(mewPrintJobs_EventArrived);
            manEWatch.Start();
        }

        static void mewPrintJobs_EventArrived(object sender, EventArrivedEventArgs e)
        {
            foreach (PropertyData prop in e.NewEvent.Properties)
            {
                string val = prop.Value == null ? "null" : prop.Value.ToString();
            }

            ManagementBaseObject printJob = (ManagementBaseObject)e.NewEvent.Properties["TargetInstance"].Value;
            string v = "";
            foreach (PropertyData propp in printJob.Properties)
            {
                string name = propp.Name;
                string val = propp.Value == null ? "null" : propp.Value.ToString();
                val += "\n";
                v += name + ":" + val;
            }
            System.Windows.Forms.MessageBox.Show(v);
        }


    }
}
