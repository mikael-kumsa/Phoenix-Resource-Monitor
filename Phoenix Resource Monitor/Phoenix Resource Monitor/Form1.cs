using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Phoenix_Resource_Monitor
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        static long folderSize(DirectoryInfo folder)
        {
            long totalSizeOfDir = 0;

            // Get all files into the directory
            FileInfo[] allFiles = folder.GetFiles();

            // Loop through every file and get size of it
            foreach (FileInfo file in allFiles)
            {
                totalSizeOfDir += file.Length;
            }

            // Find all subdirectories
            DirectoryInfo[] subFolders = folder.GetDirectories();

            // Loop through every subdirectory and get size of each
            foreach (DirectoryInfo dir in subFolders)
            {
                totalSizeOfDir += folderSize(dir);
            }

            // Return the total size of folder
            return totalSizeOfDir;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            string userName = Environment.UserName;
            float fcpu = pCPU.NextValue();
            float fram = pRam.NextValue();
            metroProgressBarCPU.Value = (int)fcpu;
            metroProgressBarRAM.Value = (int)fram;
            lblCpu.Text = string.Format("{0:0.0}%", fcpu);
            lblRam.Text = string.Format("{0:0.0}%", fram);
            chart1.Series["CPU"].Points.AddY(fcpu);
            chart1.Series["RAM"].Points.AddY(fram);

            DirectoryInfo folder = new DirectoryInfo(@"C:/Users/" + userName + "/AppData/Local/Temp");

            // Calling a folderSize() method
            long totalFolderSize = folderSize(folder);
            long totalFolderSizeMB = (long)(totalFolderSize * 0.000001);

            lblFolderSize.Text = totalFolderSizeMB.ToString() + "MB";
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            timer.Start();
        }

        
    }
}
