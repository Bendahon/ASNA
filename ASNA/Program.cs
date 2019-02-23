using IWshRuntimeLibrary;
using System;
using System.IO;
using System.Windows.Forms;

namespace ASNA
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            CheckInstaller();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        const string RenciSSHFileName = @"\Renci.SshNet.dll";
        const string WinSCPFName = @"\WinSCPnet.dll";
        #region NameAndNumbering
        public static string PNAme()
        {
            return "ASNA";
        }
        public static string PVersion()
        {
            return "1.1.3";
        }
        public static string PNameAndVersion()
        {
            return PNAme() + " " + PVersion();
        }
        #endregion
        #region Program Folders
        public static string PIDMAIN()
        {
            return @"C:\ASNA\";
        }
        public static string PIDbin()
        {
            return PIDMAIN() + @"bin\";
        }
        public static string PIDConfig()
        {
            return PIDMAIN() + @"config\";
        }
        public static string PIDsites()
        {
            return PIDMAIN() + @"site\";
        }
        public static string PIDdoc()
        {
            return PIDMAIN() + @"doc\";
        }
        #endregion
        public static void CheckInstaller()
        {
            bool ShouldIExitAfterwards = false;
            if (!Directory.Exists(PIDMAIN()))
            {
                ShouldIExitAfterwards = true;
                if (!InstallingASNA())
                {
                    Environment.Exit(1);
                }
            }
            string THISEXEVersion = PVersion();
            string ExistingVersion = GetExistingVersionNumber();
            if (THISEXEVersion != ExistingVersion && CheckingAllFilesExist() && GettingEXERunningDirectory() != PIDbin())
            {
                DialogResult dr = MessageBox.Show($"Current installed version {ExistingVersion}\nNew Version {THISEXEVersion}\nOverwrite?", PNAme(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    // Check if the new folders exist
                    // if they dont we just make them
                    try
                    {
                        System.IO.File.Copy(GettingEXERunningDirectory(), PIDbin() + "ASNA.exe", true);
                        System.IO.File.Copy(GettingRenciFileName(), PIDbin() + RenciSSHFileName, true);
                    }
                    catch
                    {
                        MessageBox.Show("Failed to copy an important part of ASNA, Can't continue", PNAme());
                        Environment.Exit(1);
                        RemovingVersionControl();
                        return;
                    }

                    try
                    {
                        System.IO.File.Copy(GettingRunningDirectory() + @"\doc\Changelog.rtf", PIDdoc() + @"Changelog.rtf", true);
                        System.IO.File.Copy(GettingRunningDirectory() + @"\doc\Credits.rtf", PIDdoc() + @"Credits.rtf", true);
                        System.IO.File.Copy(GettingRunningDirectory() + @"\doc\UserGuide.rtf", PIDdoc() + @"UserGuide.rtf", true);
                    }
                    catch
                    {
                        MessageBox.Show("Failed to copy over the documents, Program will work but no documentation", PNAme());
                    }
                    ReplacingInstallFolders();
                    CreatingDesktopShortcut("ASNA", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), PIDbin() + "ASNA.exe");
                    ShouldIExitAfterwards = true;
                    WritingVersionNumber();
                }
            }

            // Check the other install folders
            ReplacingInstallFolders();

            if (ShouldIExitAfterwards)
            {
                MessageBox.Show("ASNA installed, start from the Desktop", PNAme());
                Environment.Exit(0);
            }
        }

        private static void RemovingVersionControl()
        {
            StreamWriter sw = new StreamWriter(PIDdoc() + "version");
            sw.Write("INVALID");
            sw.Close();
        }

        private static bool InstallingASNA()
        {
            MessageBox.Show("Missing MAIN folder", PNAme());
            DialogResult dr = MessageBox.Show("Install ASNA?", PNAme(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (!CheckingAllFilesExist())
                {
                    return false;
                }
                Directory.CreateDirectory(PIDMAIN());
                Directory.CreateDirectory(PIDbin());
                // Copying Renci.SSH library to bin
                if (string.IsNullOrWhiteSpace(GettingRenciFileName()))
                {
                    MessageBox.Show("Missing Renci.SshNet.dll, can't continue", PNAme());
                    return false;
                }
                else
                {
                    System.IO.File.Copy(GettingRenciFileName(), PIDbin() + RenciSSHFileName);
                }

                if (string.IsNullOrWhiteSpace(GettingWinSCPFileName()))
                {
                    MessageBox.Show("Missing WinSCPnet.dll, can't continue", PNAme());
                    return false;
                }
                else
                {
                    System.IO.File.Copy(GettingWinSCPFileName(), PIDbin() + WinSCPFName);
                }

                // If the ASNA.exe doesn't exist here, we aint carrying on
                if (string.IsNullOrWhiteSpace(GettingEXERunningDirectory()))
                {
                    MessageBox.Show("Missing ASNA.exe, can't continue", PNAme());
                    return false;
                }
                else
                {
                    // if it does exist, copy it to the install directory, overwriting existing files
                    System.IO.File.Copy(GettingEXERunningDirectory(), PIDbin() + "ASNA.exe", true);
                    if(!System.IO.File.Exists(PIDbin() + "ASNA.exe"))
                    {
                        return false;
                    }
                    CreatingDesktopShortcut("ASNA", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), PIDbin() + "ASNA.exe");
                    return true;
                }
            }
            else
            {
                Environment.Exit(1);
            }
            return false;
        }
        private static void CreateDefaultSiteFile(bool DoiDoIT=false)
        {
            if (DoiDoIT)
            {
                StreamWriter sw = new StreamWriter(PIDdoc() + "DefaultSite");
                sw.Write("<?xml version=\"1.0\" standalone=\"yes\"?>\n");
                sw.Write("<dataset>\n");
                sw.Write("\t<StatusCmd>\n");
                sw.Write("\t\t<Status>echo test</Status>\n");
                sw.Write("\t</StatusCmd>\n");
                sw.Write("\t<ConfigCmd>\n");
                sw.Write("\t\t<Command>cat /etc/groups</Command>\n");
                sw.Write("\t\t<OutputFile>/groups.txt</OutputFile>\n");
                sw.Write("\t</ConfigCmd>\n");
                sw.Write("\t<SFTPCommand>\n");
                sw.Write("\t\t<DownloadDirectory>/home</DownloadDirectory>\n");
                sw.Write("\t\t<OutputDirectory>/tes</OutputDirectory>\n");
                sw.Write("\t</SFTPCommand>\n");
                sw.Write("</dataset>");
                sw.Close();
            }
            else
            {
                if(!System.IO.File.Exists(PIDdoc() + "DefaultSite"))
                {
                    StreamWriter sw = new StreamWriter(PIDdoc() + "DefaultSite");
                    sw.Write("<?xml version=\"1.0\" standalone=\"yes\"?>\n");
                    sw.Write("<dataset>\n");
                    sw.Write("\t<StatusCmd>\n");
                    sw.Write("\t\t<Status>echo test</Status>\n");
                    sw.Write("\t</StatusCmd>\n");
                    sw.Write("\t<ConfigCmd>\n");
                    sw.Write("\t\t<Command>cat /etc/groups</Command>\n");
                    sw.Write("\t\t<OutputFile>/groups.txt</OutputFile>\n");
                    sw.Write("\t</ConfigCmd>\n");
                    sw.Write("\t<SFTPCommand>\n");
                    sw.Write("\t\t<DownloadDirectory>/home</DownloadDirectory>\n");
                    sw.Write("\t\t<OutputDirectory>/tes</OutputDirectory>\n");
                    sw.Write("\t</SFTPCommand>\n");
                    sw.Write("</dataset>");
                    sw.Close();
                }
            }

        }
        private static bool CheckingAllFilesExist()
        {
            if (!System.IO.File.Exists(GettingRunningDirectory() + "ASNA.exe"))
            {
                return false;
            }
            if (!System.IO.File.Exists(GettingRunningDirectory() + "Renci.SshNet.dll"))
            {
                return false;
            }
            if (!System.IO.File.Exists(GettingRunningDirectory() + @"\doc\Changelog.rtf"))
            {
                return false;
            }
            if (!System.IO.File.Exists(GettingRunningDirectory() + @"\doc\Credits.rtf"))
            {
                return false;
            }
            if (!System.IO.File.Exists(GettingRunningDirectory() + @"\doc\UserGuide.rtf"))
            {
                return false;
            }
            return true;
        }
        private static string GettingRenciFileName()
        {
            string RenciFileName = GettingRunningDirectory() + RenciSSHFileName;
            if (System.IO.File.Exists(RenciFileName))
            {
                return RenciFileName;
            }
            else
            {
                return "";
            }
        }
        private static string GettingWinSCPFileName()
        {
            string WinSCPName = GettingRunningDirectory() + WinSCPFName;
            if (System.IO.File.Exists(WinSCPName))
            {
                return WinSCPName;
            }
            else
            {
                return "";
            }
        }

        public static string GettingRunningDirectory()
        {
            return AppContext.BaseDirectory;
        }

        private static string GettingEXERunningDirectory()
        {
            string ASNAFile = GettingRunningDirectory() + @"\ASNA.exe";
            if (System.IO.File.Exists(ASNAFile))
            {
                return ASNAFile;
            }
            else
            {
                return "";
            }
        }
        private static void CreatingDesktopShortcut(string shortcutName, string shortcutPath, string targetFileLocation)
        {
            string shortcutLocation = Path.Combine(shortcutPath, shortcutName + ".lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.Description = "Alistair SSH Network Automator";
            // Where is the EXE Buddy
            shortcut.TargetPath = targetFileLocation;  
            shortcut.Save();
        }
        private static void ReplacingInstallFolders()
        {
            if (!Directory.Exists(PIDConfig()))
            {
                Directory.CreateDirectory(PIDConfig());
            }
            if (!Directory.Exists(PIDdoc()))
            {
                Directory.CreateDirectory(PIDdoc());
            }
            CreateDefaultSiteFile(true);
            CheckIfDocsCanBeCopied();
            if (!Directory.Exists(PIDsites()))
            {
                Directory.CreateDirectory(PIDsites());
            }
        }
        private static void CheckIfDocsCanBeCopied()
        {
            if(GettingRunningDirectory() != PIDbin())
            {
                string Changelog = GettingRunningDirectory() + @"doc\Changelog.rtf";
                string Credits = GettingRunningDirectory() + @"doc\Credits.rtf";
                string Userguide = GettingRunningDirectory() + @"doc\UserGuide.rtf";
                if (System.IO.File.Exists(Changelog))
                {
                    System.IO.File.Copy(Changelog, PIDdoc() + @"Changelog.rtf", true);
                }
                if (System.IO.File.Exists(Credits))
                {
                    System.IO.File.Copy(Credits, PIDdoc() + @"Credits.rtf", true);
                }
                if (System.IO.File.Exists(Userguide))
                {
                    System.IO.File.Copy(Userguide, PIDdoc() + @"UserGuide.rtf", true);
                }

            }
        }
        private static string GetExistingVersionNumber()
        {
            string VersionFileName = PIDdoc() + "version";
            if (System.IO.File.Exists(VersionFileName))
            {
                StreamReader sr = new StreamReader(VersionFileName);
                string version = sr.ReadToEnd();
                sr.Close();
                return version;
            }
            else
            {
                WritingVersionNumber();
                return PVersion();
            }
        }
        private static void WritingVersionNumber()
        {
            if (!Directory.Exists(PIDdoc()))
            {
                Directory.CreateDirectory(PIDdoc());
            }
            string VersionFileName = PIDdoc() + "version";
            StreamWriter sw = new StreamWriter(VersionFileName);
            sw.Write(PVersion());
            sw.Close();
        }
    }
}
