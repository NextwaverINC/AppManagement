using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Configuration;
using System.IO;

namespace Robot
{
    public class cMain
    {
        #region Default Method
        Dictionary<Int32, DataRow> ProcessTemp = new Dictionary<Int32, DataRow>();
        public static string Connection;
        public static string UserName;
        public static string OfficeSpaceId;
        public static string DatabaseName = "document";
        string ToolsID;
        DataTable dtMain;
        int[] listSelectIndex;
        int currentIndex;

        private float getCPU()
        {
            PerformanceCounter pfmCount = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            float sum = 0;
            int rounds = 0;
            while (rounds < 2)
            {
                sum += pfmCount.NextValue();
                Thread.Sleep(1000);
                ++rounds;
            }
            pfmCount.Close();
            pfmCount.Dispose();
            float cpu = sum / rounds;

            return cpu;
        }
        private float getRam()
        {
            Process[] pro_list = Process.GetProcesses();
            long cpm = 0;
            foreach (System.Diagnostics.Process proc in pro_list)
            {
                cpm += proc.WorkingSet64;
            }
            float kb_work_ram = cpm / 1024;
            float mb_work_ram = kb_work_ram / 1024;
            float gb_work_ram = mb_work_ram / 1024;
            return gb_work_ram;
        }

        public static Dictionary<string, bool> StopList = new Dictionary<string, bool>();
        public bool Run(string iConnection, string iUserName, string iOfficeSpaceId, string iToolsID, DataTable dt, int[] ilistSelectIndex, int icurrentIndex, string Database)
        {
            Connection = iConnection;
            UserName = iUserName;
            OfficeSpaceId = iOfficeSpaceId;
            DatabaseName = Database;

            ToolsID = iToolsID;
            dtMain = dt;
            listSelectIndex = ilistSelectIndex;
            currentIndex = icurrentIndex;

            if (!StopList.ContainsKey(ToolsID))
                StopList.Add(ToolsID, false);
            
            StopList[ToolsID] = false;
            SaveLog("=====================เริ่มกระบวนการทำงาน");          

            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(StartLoop);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunComplete);
            bw.RunWorkerAsync();

            MessageBox.Show("เริ่มการทำงานแล้ว", "ผลการทำงาน", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }
        public bool Stop(string iToolsID)
        {
            ToolsID = iToolsID;
            if (StopList.ContainsKey(ToolsID))
            {
                MessageStop = "ผู้ใช้งาน ยกเลิกการทำงาน";
                StopList[ToolsID] = true;
            }
            return false;
        }
        private void RunComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                SaveLog("=====================สิ้นสุดการทำงานเนื่องจาก :" + MessageStop);
                MessageBox.Show("เสิ้นสุดการทำงานเนื่องจาก :" + MessageStop, "ผลการทำงาน", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch { }
        }
        private Process genProcess(string Arguments, string PathCommandLine)
        {
            Process ProcessItem = new Process();
            ProcessStartInfo ProcessInfo = new ProcessStartInfo(PathCommandLine);
            ProcessInfo.Arguments = Arguments;          
            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.RedirectStandardOutput = true;
            ProcessInfo.RedirectStandardError = true;
            ProcessInfo.UseShellExecute = false;
            ProcessItem.StartInfo = ProcessInfo;
            return ProcessItem;           
        }
        #endregion

        static string MessageStop = "";
        float Max_CPU = float.Parse(ConfigurationSettings.AppSettings["MaxCPU"]);
        float Max_RAM = float.Parse(ConfigurationSettings.AppSettings["MaxRAM"]);
        private void StartLoop(object sender, DoWorkEventArgs e)
        {            
            for (int i = 0; i < dtMain.Rows.Count; i++)
            {
                float CPU = getCPU();
                float RAM = getRam();
                if (CPU < Max_CPU && RAM < Max_RAM)
                {
                    switch (ToolsID)
                    {
                        case "Tools1":
                            BackgroundWorker bw = new BackgroundWorker();
                            bw.DoWork += new DoWorkEventHandler(RunDemo);
                            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunDemoComplete);
                            bw.RunWorkerAsync(dtMain.Rows[i]);
                            break;
                    }
                }
                else
                {
                    MessageStop = "CPU(Usage:" + CPU + "% Max:" + Max_CPU + "%) หรือ RAM(Usage:" + RAM + "GB Max:" + Max_RAM + "GB) ใช้งานเกินกว่ากำหนด";
                    SaveLog(MessageStop);
                    //StopList[ToolsID] = true;
                }

                if (StopList[ToolsID]) break;

                // หลับไป 1 วิเท่ากับ 1000
                Thread.Sleep(10000);
            }

            if (!StopList[ToolsID])
                StartLoop(null, null);
        }

        #region Demo Process
        private void RunDemo(object sender, DoWorkEventArgs e)
        {
            DataRow dr = (DataRow)e.Argument;            
            SaveLog("Start RunDemo");
            RunDemo(dr);
        }
        private void RunDemoComplete(object sender, RunWorkerCompletedEventArgs e)
        { 
            SaveLog("End RunDemo Complete");
        }
        void SaveLog(string Message)
        {
            string Log = File.ReadAllText(getPathLog());
            if (Log == "")
            {
                Log += DateTime.Now.ToString("ddMMyyyy hh:mm:ss") + " " + Message;
            }
            else
            {
                Log += Environment.NewLine + DateTime.Now.ToString("ddMMyyyy hh:mm:ss") + " " + Message;
            }
            File.WriteAllText(getPathLog(), Log);
        }
        string getPathLog()
        {
            string FilePath = Application.StartupPath + "/CommandLine/Log_" + ToolsID + "_" + DateTime.Now.ToString("ddMMyyyy") + ".txt";
            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, "");
            }
            return FilePath;
        }
        void RunDemo(DataRow dr)
        {
            if (!StopList[ToolsID])
            {
                bool bNext = true; // เขียนตรวจสอบเงื่อนไขเอง
                if (bNext)
                {
                    //======================ส่วนที่ต้องดึงจาก DataRow==================
                    String refID = "15";
                    String FBToken = "";
                    String FBPostID = "";
                    String CommentMessage = "Test Post on " + DateTime.Now.ToString("yyyyMMddHHmmss");
                    //==========================================================

                    string Arguments = @"-ref '{0}' -tkn '{1}' -pid '{2}' -msg '{3}' -del";
                    Arguments = String.Format(Arguments, refID, FBToken, FBPostID, CommentMessage);
                    //=========================ที่อยู่ไฟลExportString()ที่ใช้รัน==============================================
                    string PathCommandLine = Application.StartupPath + "/CommandLine/FBComment.exe";
                    //=========================เริ่มกระบวนการทำงาน==========================================
                    Process ProcessItem = genProcess(Arguments, PathCommandLine);

                    ProcessItem.OutputDataReceived += DemoProcess_OutputDataReceived;
                    ProcessItem.Start();
                    ProcessItem.BeginOutputReadLine();

                    ProcessTemp.Add(ProcessItem.Id, dr);
                }
            }
        }
        public void DemoProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Process ProcessItem = (Process)sender;
            string Output = e.Data;
            if (Output != null)
            {
                DataRow dr = ProcessTemp[ProcessItem.Id];
                //ทำการตรวจสอบเงื่อนไขว่าต้องการรันต่อหรือไม่ ถ้ารันต่อให้ bRunAgain เป็น true
                bool bRunAgain = false;
                if (bRunAgain)
                {
                    RunDemo(dr);
                }
                else
                {
                    //จบการทำงานของ รายการนั้น
                    SaveLog("DemoProcess_OutputDataReceived => ผลลัพธExportString()จากการทำงาน : " + Output);
                    //MessageBox.Show("ผลลัพธExportString()จากการทำงาน : " + Output, "ทำงานสำเร็จแล้ว", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        #endregion        
    }
}
