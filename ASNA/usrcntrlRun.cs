using System;
using Renci.SshNet;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Xml.Linq;
using WinSCP;
using System.Data;

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
        WinSCP.Session session;
        StreamWriter LogFileFile;

        #region Extras
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
        #endregion
        #region SSH Shit
        private void btnBackitup_Click(object sender, EventArgs e)
        {
            string DT_folderName = DateTime.Now.ToString("yyyy-MMM");
            string LogDocFolderName = Program.PIDdoc() + @"\log\" + DT_folderName + @"\";
            if (!Directory.Exists(LogDocFolderName))
            {
                Directory.CreateDirectory(LogDocFolderName);
            }

            string DateTimeFileWriter = DateTime.Now.ToString("dd-MMM-yyyy_HH-mm-ss");
            string LogFileNamington = $@"{LogDocFolderName}{DateTimeFileWriter}.txt";

            LogFileFile = new StreamWriter(LogFileNamington);

            WriteToLogBox($"Running Config: {lstboxSites.Text}");
            WriteToLogBox($"Running Setting: {cmboSavedSite.Text}");
            WriteToLogBox($"Save Location: {txtActualSaveLocation.Text}");
            WriteToLogBox($"Server Name: {txtServerName.Text}");
            WriteToLogBox($"Username: {txtUsername.Text}");
            WriteToLogBox($"IP Address: {txtIPAddress.Text}");
            WriteToLogBox($"Port: {txtPort.Text}");
            WriteToLogBox($"ICMP Skipped: {chckEnableICMP.Checked}");
            WriteToLogBox($"Status Skipped: {chckSkipStatus.Checked}");
            WriteToLogBox($"Config Skipped: {chckSkipConfig.Checked}");
            WriteToLogBox($"SFTP Skipped: {chckSkipSFTP.Checked}");

            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            txtLogBox.Text = "";
            if(chckSkipConfig.Checked && chckSkipSFTP.Checked && chckSkipStatus.Checked)
            {
                LogFileFile.Close();
                return;
            }

            string IPAddress = txtIPAddress.Text;
            // Check if any boxes are empty
            if (!PreFlightChecks())
            {
                LogFileFile.Close();
                return;
            }
            if (chckEnableICMP.Checked)
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
                    LogFileFile.Close();
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
                LogFileFile.Close();
                return;
            }

            // check if the folder exists before doing shit
            string monthdaystuff = DateTime.Now.ToString("yyyy-MM-dd");
            string OutputFolderName = $"{txtActualSaveLocation.Text}{txtServerName.Text} {monthdaystuff}";

            if (Directory.Exists(OutputFolderName))
            {
                WriteToLogBox("Output directory already exists, Can't continue");
                LogFileFile.Close();
                return;
            }
            // connect to client
            WriteToLogBox("Attempting connection of SSH");
            try
            {
                ssh = new SshClient(txtIPAddress.Text, Convert.ToInt32(txtPort.Text), txtUsername.Text, txtPassword.Text);
                ssh.Connect();
                WriteToLogBox("Connected to host!");
            }
            catch (Exception ex)
            {
                WriteToLogBox(ex.Message);
                LogFileFile.Close();
                return;
            }

            // creating output directory
            // Checked for existance before 
            Directory.CreateDirectory(OutputFolderName);

            if (chckSkipStatus.Checked)
            {
                WriteToLogBox("Skipping status commands!");
            }
            else
            {
                WriteToLogBox("Generating Status File");
                GoOverTheStatusCommands(StatusFile, OutputFolderName, monthdaystuff);
                WriteToLogBox("Status File Generated");
            }
            if (chckSkipConfig.Checked)
            {
                WriteToLogBox("Skipping config commands!");
            }
            else
            {
                WriteToLogBox("Downloading Config of Log files");
                RunAllConfigCommands(ConfigCommand, ConfigCommandDir, OutputFolderName, monthdaystuff);
                WriteToLogBox("Config and Log Files download complete");
            }
            DestroyTheConnection();

            // Now to do the SFTP commands

            if (chckSkipSFTP.Checked)
            {
                WriteToLogBox("Skipping SFTP due to settings!");
            }
            else
            {
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
                WriteToLogBox("Completed SFTP!");
            }

            watch.Stop();
            long ElapsedSeconds = watch.ElapsedMilliseconds / 1000;
            WriteToLogBox($"it took {ElapsedSeconds} seconds to run");

            LogFileFile.Close();
            LogFileFile.Dispose();
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
                if(string.IsNullOrEmpty(st))
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

                if (string.IsNullOrEmpty(FinalResult))
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
                // Read each item from the lists
                string ConfigCommand1 = ConfigCommand[x].Trim();
                string ConfigDir1 = ConfigCommandDir[x].Trim();
                // Make sure you get rid of that silly ampersand shit
                ConfigCommand1 = DeserialiseXMLFromStr(ConfigCommand1);
                ConfigDir1 = DeserialiseXMLFromStr(ConfigDir1);

                WriteToLogBox($"Running command {ConfigCommand1}");
                SshCommand output = ssh.RunCommand(ConfigCommand1.Trim());
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
        private void SFTPDownload(SessionOptions so, string OPFolderName, List<string> IPDir, List<string> OPdir)
        {
            try
            {
                session = new WinSCP.Session();
                session.Open(so);
                for(int i = 0; i < IPDir.Count; i++)
                {
                    string localPath = $"{OPFolderName}{OPdir[i]}";
                    localPath = DeserialiseXMLFromStr(localPath);
                    Directory.CreateDirectory(localPath);
                    string remotePath = IPDir[i];
                    remotePath = DeserialiseXMLFromStr(remotePath);
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
            }
            finally
            {
                session.Close();
                session.Dispose();
                GC.Collect();
            }
        }
        private string DeserialiseXMLFromStr(string inputf)
        {
            // Replace all occurances of the escape chars in an input string
            string returnstring = inputf;
            string Ampersand_xml = "&amp;";
            string Ampersand = "&";
            returnstring = returnstring.Replace(Ampersand_xml, Ampersand);

            string Less_than_xml = "&lt;";
            string Less_than = "<";
            returnstring = returnstring.Replace(Less_than_xml, Less_than);

            string Greater_than_xml = "&gt;";
            string Greater_than = ">";
            returnstring = returnstring.Replace(Greater_than_xml, Greater_than);

            string Qoutes_xml = "&quot;";
            string Qoutes = "\"";
            returnstring = returnstring.Replace(Qoutes_xml, Qoutes);

            string Apostrophe_xml = "&apos;";
            string Apostrophe = "'";
            returnstring = returnstring.Replace(Apostrophe_xml, Apostrophe);

            return returnstring;
        }
        #endregion
        #region Logbox info
        private void WriteToLogBox(string inputtext)
        {
            string DateTimeLogBoxWriter = DateTime.Now.ToString("dd-MMM-yyyy_HH-mm-ss");
            string StartMessage = $"[{DateTimeLogBoxWriter}] -- ASNA: ";
            string EndMessage = "\n";

            LogFileFile.Write($"{StartMessage}{inputtext}{EndMessage}");
            txtLogBox.AppendText($"{StartMessage}{inputtext}{EndMessage}");
            txtLogBox.ScrollToCaret();
        }
        private void btnClearLog_Click(object sender, EventArgs e)
        {
            txtLogBox.Text = "";
        }
        #endregion
        #region Combobox
        private void cmboSavedSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeComboBox();
        }
        private void ChangeComboBox()
        {
            if (cmboSavedSite.Text == "None")
            {
                txtIPAddress.Text = "";
                txtPassword.Text = "";
                txtServerName.Text = "";
                txtUsername.Text = "";
                txtPort.Text = "22";
                chckEnableICMP.Checked = false;
                chckSkipConfig.Checked = false;
                chckSkipSFTP.Checked = true;
                chckSkipStatus.Checked = false;
                return;
            }
            string FileName = $@"{Program.PIDConfig()}{cmboSavedSite.Text}";
            XmlSerializer xs = new XmlSerializer(typeof(Settings));
            FileStream read = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            Settings se = (Settings)xs.Deserialize(read);
            read.Dispose();
            txtIPAddress.Text = se.HostIP;
            if (se.isPWEncrypted == false)
            {
                txtPassword.Text = se.Password;
            }
            else
            {
                PasswordManager pm = new PasswordManager();
                txtPassword.Text = pm.DecodeString(se.Password);
            }
            txtServerName.Text = se.GenericName;
            txtUsername.Text = se.Username;
            txtPort.Text = se.Port;
            chckSkipConfig.Checked = se.SkipConfig;
            chckSkipSFTP.Checked = se.SkipSFTP;
            chckSkipStatus.Checked = se.SkipStatus;
            chckEnableICMP.Checked = se.SkipICMPScan;
        }
        private void cmboSaveLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreatingCustomSaveLocation();
        }
        private void CreatingCustomSaveLocation()
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
        #endregion
    }
}
