using System;
using Renci.SshNet;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Xml.Linq;
using WinSCP;

namespace ASNA
{
    public partial class usrcntrlRun : UserControl
    {
        public usrcntrlRun()
        {
            InitializeComponent();
            try
            {
                ReloadUserControl();
            }
            catch
            {

            }
        }
        SshClient ssh;
        bool SkipICMPScan = true;

        public void ReloadUserControl()
        {
            cmboSavedSite.Items.Clear();
            cmboSavedSite.Items.Add("None");
            cmboSavedSite.SelectedIndex = 0;
            foreach (string s in Directory.GetFiles(Program.PIDConfig()))
            {
                cmboSavedSite.Items.Add(s.Replace(Program.PIDConfig(), ""));
            }

            lstboxSites.Items.Clear();
            string[] filePaths = Directory.GetFiles(Program.PIDsites());
            foreach (string file in filePaths)
            {
                string opfile = file.Replace(Program.PIDsites(), "");
                if (opfile.StartsWith("."))
                {

                }
                else
                {
                lstboxSites.Items.Add(opfile);
                }
            }
        }

        private void lstboxSites_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmboSaveLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            string OutputLocation = "";
            switch (cmboSaveLocation.Text)
            {
                case "Documents":
                    OutputLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    break;
                case "Desktop":
                    OutputLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    break;
                case "Custom":
                    OutputLocation = SelectACustomOutputFolder();
                    break;
                default:
                    cmboSaveLocation.Text = "Desktop";
                    OutputLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    break;
            }
            txtActualSaveLocation.Text = OutputLocation + @"\";
        }
        private string SelectACustomOutputFolder()
        {
            string OutputDir;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                OutputDir = fbd.SelectedPath;
                txtActualSaveLocation.Text = OutputDir;
                return OutputDir;
            }
            else
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }
        }
        #region SSH Shit
        private void btnBackitup_Click(object sender, EventArgs e)
        {

            string IPAddress = txtIPAddress.Text;
            // Check if any boxes are empty
            if (!PreFlightChecks())
            {
                return;
            }
            if (SkipICMPScan)
            {
                WriteToLogBox("Skipping ICMP Scan");
            }
            else
            {
                WriteToLogBox($"Testing ICMP to: {IPAddress}");
                // check if the IP address returns ICMP scan
                if (!CheckValidHost(IPAddress))
                {
                    WriteToLogBox("Failed to return ping from hostname");
                    return;
                }
            }
            // now to connect
            // assumed lstbox null errors are picked up in preflightchecks
            string ConfigurationFile = Program.PIDsites() + lstboxSites.Text;
            string StatusFile = Program.PIDsites() + "." + lstboxSites.Text;

            XDocument DOC = XDocument.Load(ConfigurationFile);

            List<string> ConfigCommand = LoadConfigDLFromXML(DOC);
            List<string> ConfigCommandDir = LoadConfigOPFromXML(DOC);

            WriteToLogBox("Checking if inputs are valid");
            // do error checking on the status commands
            if (!DoSomeErrorChecking1(ConfigCommand, ConfigCommandDir))
            {
                return;
            }

            // check if the folder exists before doing shit
            string monthdaystuff = DateTime.Now.ToString("yyyy-MM-dd");
            string OutputFolderName = $"{txtActualSaveLocation.Text}{txtServerName.Text} {monthdaystuff}";

            if (Directory.Exists(OutputFolderName))
            {
                WriteToLogBox("Output directory already exists, Can't continue");
                return;
            }
            // connect to client
            WriteToLogBox("Attempting connection of SSH");
            try
            {
                ssh = new SshClient(txtIPAddress.Text,Convert.ToInt32(txtPort.Text), txtUsername.Text, txtPassword.Text);
                ssh.Connect();
                WriteToLogBox("Connected to host!");
            }
            catch (Exception ex)
            {
                WriteToLogBox(ex.Message);
                return;
            }

            // creating output directory
            // Checked for existance before 
            Directory.CreateDirectory(OutputFolderName);

            GoOverTheStatusCommands(StatusFile, OutputFolderName, monthdaystuff);
            RunAllConfigCommands(ConfigCommand, ConfigCommandDir, OutputFolderName, monthdaystuff);
            DestroyTheConnection();

            // Now to do the SFTP commands
            List<string> SFTPCommand = LoadSFTPDLFromXML(DOC);
            List<string> SFTPOutputDirectory = LoadSFTPOPFromXML(DOC);

            SessionOptions sessionOptions = new SessionOptions
            {
                Protocol = Protocol.Sftp,
                HostName = txtIPAddress.Text,
                UserName = txtUsername.Text,
                Password = txtPassword.Text,
                PortNumber = Convert.ToInt32(txtPort.Text),
                GiveUpSecurityAndAcceptAnySshHostKey = true
            };

            WriteToLogBox("Setting up SFTP!");
            SFTPDownload(sessionOptions, OutputFolderName, SFTPCommand, SFTPOutputDirectory);
            WriteToLogBox("Completed!");

        }
        private bool PreFlightChecks()
        {
            if (txtActualSaveLocation.Text == "")
            {
                WriteToLogBox("Save location is blank");
                return false;
            }
            if(txtIPAddress.Text == "")
            {
                WriteToLogBox("IP Address cant be blank");
                return false;
            }
            if(txtPassword.Text == "")
            {
                WriteToLogBox("Password cant be blank");
                return false;
            }
            if(txtServerName.Text == "")
            {
                WriteToLogBox("Server needs a name");
                return false;
            }
            if (txtUsername.Text == "")
            {
                WriteToLogBox("Username cant be blank");
                return false;
            }
            if (lstboxSites.SelectedItem == null)
            {
                WriteToLogBox("Please select a site config file");
                return false;
            }
            return true;
        }
        private bool CheckValidHost(string ip)
        {
            Ping ping = new Ping();
            PingReply reply;
            try
            {
                reply = ping.Send(ip, 3000);
            }
            catch
            {
                WriteToLogBox("The ping failed, sad panda");
                return false;
            }

            string Message = reply.Status.ToString();

            switch (Message)
            {
                case "Success":
                    WriteToLogBox(Message);
                    ping.Dispose();
                    return true;
                default:
                    WriteToLogBox(reply.Status.ToString());
                    WriteToLogBox("No reply from host, sad panda");
                    ping.Dispose();
                    return false;
            }
        }
        private List<string> LoadConfigDLFromXML(XDocument xdoc)
        {
            List<string> status = new List<string>();
            foreach (XElement el in xdoc.Descendants("Command"))
            {
                string st = el.ToString().Replace("<Command>", "").Replace("</Command>", "");
                status.Add(st.Trim());
            }
            return status;
        }
        private List<string> LoadConfigOPFromXML(XDocument xdoc)
        {
            List<string> status = new List<string>();
            foreach (XElement el in xdoc.Descendants("OutputFile"))
            {
                string st = el.ToString().Replace("<OutputFile>", "").Replace("</OutputFile>", "");
                status.Add(st.Trim());
            }
            return status;
        }
        private bool DoSomeErrorChecking1(List<string> ConfigCommand, List<string> ConfigCommandDir)
        {
            if (ConfigCommand.Count == 0 && ConfigCommandDir.Count == 0)
            {
                WriteToLogBox("No config command to be done here, im out");
                return false;
            }
            if (ConfigCommand.Count != ConfigCommandDir.Count)
            {
                WriteToLogBox("These dont add up, either you fucked with the XML or i fucked up my saving process");
                return false;
            }
            else
            {
                WriteToLogBox("Valid stuff");
                return true;
            }
        }
        private void GoOverTheStatusCommands(string StatusFile, string OutputFolderName, string dateRN)
        {

            if (!File.Exists(StatusFile))
            {
                WriteToLogBox("Status file doesn't exist, skipping");
                return;
            }

            StreamReader sr = new StreamReader(StatusFile);
            string StatusInformation = sr.ReadToEnd().Trim();
            string[] Statusinfo = StatusInformation.Split('\n');
            if(Statusinfo.Length <= 0)
            {
                return;
            }

            string StatusFileName = OutputFolderName + $@"/{txtServerName.Text} {dateRN} Status.txt";

            StreamWriter sw = new StreamWriter(StatusFileName);
            foreach (string st in Statusinfo)
            {
                //MessageBox.Show(st);
                if(st == "" || st == null)
                {
                    continue;
                }

                if (st.StartsWith("ASNAcommand"))
                {
                    sw.Write(st.Replace("ASNAcommand ", "") + "\n");
                    WriteToLogBox($"Generic command: {st}");
                    continue;
                }
                else if (st.StartsWith("ASNAnewline"))
                {
                    sw.Write("\n");
                    WriteToLogBox($"Generic command: {st}");
                    continue;
                }
                WriteToLogBox($"Running command {st.Trim()}");
                SshCommand output = ssh.RunCommand(st.Trim());
                string FinalResult = output.Result;

                if(FinalResult == "" || FinalResult == null)
                {
                    WriteToLogBox("No result");
                }
                else
                {
                    WriteToLogBox(FinalResult.Trim());
                    sw.Write(FinalResult);
                }
            }
            sw.Close();
        }
        private void RunAllConfigCommands(List<string> ConfigCommand, List<string> ConfigCommandDir, string OutputFolderName, string dateRN)
        {
            for (int x = 0; x < ConfigCommand.Count; x++)
            {
                string ConfigCommand1 = ConfigCommand[x].Trim();
                string ConfigDir1 = ConfigCommandDir[x].Trim();
                WriteToLogBox($"Running command {ConfigCommand1}");
                SshCommand output = ssh.RunCommand(ConfigCommand1);
                string FinalResult = output.Result;

                if (FinalResult == "")
                {
                    WriteToLogBox("No command returned or you dont have access to this file");
                    continue;
                }
                else
                {
                    //Extract the file name
                    string OPFileName = new DirectoryInfo(ConfigDir1).Name;
                    string DirectoryStructure = $@"{OutputFolderName}\{ConfigDir1}".Replace(OPFileName, "");
                    string FinalOutputName = $@"{DirectoryStructure}\{txtServerName.Text} {dateRN} {OPFileName}";

                    if (!Directory.Exists(DirectoryStructure))
                    {
                        Directory.CreateDirectory(DirectoryStructure);
                    }

                    StreamWriter sw = new StreamWriter(FinalOutputName);
                    sw.Write(FinalResult);
                    sw.Close();
                }
                WriteToLogBox("Success!");
            }
        }
        private void DestroyTheConnection()
        {
            WriteToLogBox("Destroying connecting to host");
            ssh.Disconnect();
            ssh.Dispose();
            GC.Collect();
        }
        private List<string> LoadSFTPDLFromXML(XDocument xdoc)
        {
            List<string> status = new List<string>();
            foreach (XElement el in xdoc.Descendants("DownloadDirectory"))
            {
                string st = el.ToString().Replace("<DownloadDirectory>", "").Replace("</DownloadDirectory>", "");
                status.Add(st.Trim());
            }
            return status;
        }
        private List<string> LoadSFTPOPFromXML(XDocument xdoc)
        {
            List<string> status = new List<string>();
            foreach (XElement el in xdoc.Descendants("OutputDirectory"))
            {
                string st = el.ToString().Replace("<OutputDirectory>", "").Replace("</OutputDirectory>", "");
                status.Add(st.Trim());
            }
            return status;
        }

        private void GoOverTheSFTPCommands(List<string> SFTPDir, List<string> SFTPOutputDir, string OutputFolderName, string dateRN)
        {
            for (int x = 0; x < SFTPDir.Count; x++)
            {
                string DownloadThisDirectory = SFTPDir[x];
                string SaveToHere = $@"{OutputFolderName}{SFTPOutputDir[x]}";

               
                ////Extract the file name
                //string OPFileName = new DirectoryInfo(ConfigDir1).Name;
                //string DirectoryStructure = $@"{OutputFolderName}\{ConfigDir1}".Replace(OPFileName, "");
                //string FinalOutputName = $@"{DirectoryStructure}\{txtServerName.Text} {dateRN} {OPFileName}";

                //if (!Directory.Exists(DirectoryStructure))
                //{
                //    Directory.CreateDirectory(DirectoryStructure);
                //}

                //StreamWriter sw = new StreamWriter(FinalOutputName);
                //sw.Write(FinalResult);
                //sw.Close();

            }
        }
        WinSCP.Session session;
        private void SFTPDownload(SessionOptions so, string OPFolderName, List<string> IPDir, List<string> OPdir)
        {
            try
            {

                session = new WinSCP.Session();
                session.Open(so);
                for(int i = 0; i < IPDir.Count; i++)
                {
                    string localPath = $"{OPFolderName}{OPdir[i]}";
                    Directory.CreateDirectory(localPath);
                    string remotePath = IPDir[i];
                    // Enumerate files and directories to download
                    IEnumerable<RemoteFileInfo> fileInfos = session.EnumerateRemoteFiles(remotePath, null, EnumerationOptions.EnumerateDirectories | EnumerationOptions.AllDirectories);

                    foreach (RemoteFileInfo fileInfo in fileInfos)
                    {
                        string localFilePath = session.TranslateRemotePathToLocal(fileInfo.FullName, remotePath, localPath);

                        if (fileInfo.IsDirectory)
                        {
                            // Create local subdirectory, if it does not exist yet
                            Directory.CreateDirectory(localFilePath);
                        }
                        else
                        {
                            WriteToLogBox($"Downloading file {fileInfo.FullName}...");
                            // Download file
                            string remoteFilePath = session.EscapeFileMask(fileInfo.FullName);
                            TransferOperationResult transferResult = session.GetFiles(remoteFilePath, localFilePath);
                            if (!transferResult.IsSuccess)
                            {
                                WriteToLogBox($"Error downloading file {fileInfo.FullName}: {transferResult.Failures[0].Message}");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                WriteToLogBox($"Error: {e}");
                return;
            }
            finally
            {
                session.Close();
                session.Dispose();
                GC.Collect();
            }
        }
        #endregion
        #region Logbox info
        private void WriteToLogBox(string inputtext)
        {
            string DateTimeFileName = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");
            string StartMessage = $"{DateTimeFileName} -- ASNA: ";
            string EndMessage = "\n";
            txtLogBox.AppendText($"{StartMessage}{inputtext}{EndMessage}");
            txtLogBox.ScrollToCaret();
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            txtLogBox.Text = "";
        }
        #endregion
        private void cmboSavedSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmboSavedSite.Text == "None")
            {
                txtIPAddress.Text = "";
                txtPassword.Text = "";
                txtServerName.Text = "";
                txtUsername.Text = "";
                txtPort.Text = "22";
                return;
            }
            string FileName = $@"{Program.PIDConfig()}{cmboSavedSite.Text}";
            XmlSerializer xs = new XmlSerializer(typeof(Settings));
            FileStream read = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            Settings se = (Settings)xs.Deserialize(read);
            read.Dispose();
            txtIPAddress.Text = se.HostIP;
            txtPassword.Text = se.Password;
            txtServerName.Text = se.GenericName;
            txtUsername.Text = se.Username;
            txtPort.Text = se.Port;
            SkipICMPScan = se.SkipICMPScan;
        }
    }
}
