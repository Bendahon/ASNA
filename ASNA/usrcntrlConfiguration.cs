using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ASNA
{
    public partial class usrcntrlConfiguration : UserControl
    {
        public usrcntrlConfiguration()
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
        bool ChangesToMake = false;
        string onesplitter = "-------------------1-------------------";

        #region Selecting a different Site
        private void lstboxSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeCurrentActiveSite();

        }
        private void ChangeCurrentActiveSite()
        {
            if (string.IsNullOrWhiteSpace(lstboxSites.Text))
            {
                return;
            }
            CheckIfNeedsToBeSaved();

            txtRenameBox.Text = lstboxSites.Text;
            string SelectedItem = Program.PIDsites() + lstboxSites.Text;
            string StatusItem = Program.PIDsites() + "." + lstboxSites.Text;

            if (File.Exists(SelectedItem))
            {
                LoadSelectedConfigFile(SelectedItem);
            }
            else
            {
                ReloadUserControl();
            }
            if (File.Exists(StatusItem))
            {
                LoadSelectedStatusFile(StatusItem);
            }
            else
            {
                txtStatusBox.Text = "";
            }
            ResetColour();
            ColourMeTimbers();
            ChangesToMake = false;
            btnExport.Enabled = true;
            btnSave.Enabled = true;
            btnAddCommand.Enabled = true;
            btnAddNewline.Enabled = true;
            btnRenameOK.Enabled = true;
            btnRemove.Enabled = true;
            txtRenameBox.Enabled = true;
            txtStatusBox.Enabled = true;
        }
        private void LoadSelectedStatusFile(string StatusItem)
        {
            StreamReader sr = new StreamReader(StatusItem);
            txtStatusBox.Text = sr.ReadToEnd();
            sr.Close();
        }
        private void LoadSelectedConfigFile(string SelectedItem)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(SelectedItem);
            dgridSystem.DataSource = ds;
            dgridSFTP.DataSource = ds;
            dgridSystem.DataMember = "ConfigCmd";
            dgridSFTP.DataMember = "SFTPCommand";
        }
        #endregion
        #region Adding a new site
        private void btnAddSite_Click(object sender, EventArgs e)
        {
            AddANewSite();
        }

        private void AddANewSite()
        {
            CheckIfNeedsToBeSaved();
            string TemplateName = Program.PIDdoc() + "DefaultSite";
            //string DateTimeFileName = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");
            string DateTimeFileName = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string DirectoryToCopyTo = Program.PIDsites() + DateTimeFileName;
            if (File.Exists(DirectoryToCopyTo))
            {
                return;
            }
            File.Copy(TemplateName, DirectoryToCopyTo);
            ReloadUserControl();
        }
        #endregion
        #region Data Grid information
        private bool ParseTheSystemDataGrids()
        {
            double count = 1.0;
            foreach (DataGridViewRow row in dgridSystem.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    try
                    {
                        if (cell.Value.ToString().Trim() == "")
                        {
                            int counter = Convert.ToInt32(Math.Floor(count));
                            MessageBox.Show($"Row {counter} in system contains blank data, can't continue", Program.PNAme());
                            return false;
                        }

                    }
                    catch
                    {

                    }
                    count += 0.5;
                }
            }
            return true;
        }
        private bool ParseTheSFTPDataGrids()
        {
            double count = 1.0;
            foreach (DataGridViewRow row in dgridSFTP.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    try
                    {
                        if (cell.Value.ToString().Trim() == "")
                        {
                            int counter = Convert.ToInt32(Math.Floor(count));
                            MessageBox.Show($"Row {counter} in SFTP contains blank data, can't continue", Program.PNAme());
                            return false;
                        }

                    }
                    catch
                    {

                    }
                    count += 0.5;
                }
            }
            return true;
        }
        private bool ParseDataGridsForInvalidInformation()
        {
            bool failure = false;

            if (!ParseTheSystemDataGrids())
            {
                failure = true;
            }
            if (!ParseTheSFTPDataGrids())
            {
                failure = true;
            }
            return failure;
        }
        #endregion
        #region Removing a File
        private void btnRemove_Click(object sender, EventArgs e)
        {
            RemoveTheSelectedFile();
        }
        private void RemoveTheSelectedFile()
        {
            string SelectedItem = Program.PIDsites() + lstboxSites.Text;
            string HiddenSelectedItem = Program.PIDsites() + "." + lstboxSites.Text;
            if (!File.Exists(SelectedItem))
            {
                return;
            }
            DialogResult dr = MessageBox.Show("Are you sure?", Program.PNAme(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    File.Delete(SelectedItem);
                    File.Delete(HiddenSelectedItem);
                }
                catch
                {

                }
                ReloadUserControl();
            }
        }
        #endregion
        #region Renaming
        private void btnRenameOK_Click(object sender, EventArgs e)
        {
            RenameTheCurrentFile();
        }
        private void RenameTheCurrentFile()
        {
            if (string.IsNullOrWhiteSpace(lstboxSites.Text))
            {
                return;
            }
            if (!IsValidFileName(txtRenameBox.Text, true))
            {
                MessageBox.Show("Invalid file name");
                return;
            }
            CheckIfNeedsToBeSaved();
            string OriginalFileName = Program.PIDsites() + lstboxSites.Text;
            string OriginalHiddenFileName = Program.PIDsites() + "." + lstboxSites.Text;
            if (!File.Exists(OriginalFileName))
            {
                return;
            }

            string NewFileName = Program.PIDsites() + txtRenameBox.Text;
            string HiddenFileName = Program.PIDsites() + "." + txtRenameBox.Text;

            if (DoTheFilesExits(NewFileName, HiddenFileName))
            {
                return;
            }

            try
            {
                File.Move(OriginalFileName, NewFileName);
                File.Move(OriginalHiddenFileName, HiddenFileName);
            }
            catch
            {

            }
            ReloadUserControl();
        }
        public bool DoTheFilesExits(string newFileName, string hiddenFileName)
        {
            if (File.Exists(newFileName))
            {
                MessageBox.Show("File already exists, try again", Program.PNAme());
                txtRenameBox.Text = "";
                return true;
            }
            if (File.Exists(hiddenFileName))
            {
                MessageBox.Show("Status file already exists, manual clean me please", Program.PNAme());
                txtRenameBox.Text = "";
                return true;
            }
            return false;
        }
        #endregion
        #region Text Box addition
        private void btnAddNewline_Click(object sender, EventArgs e)
        {
            txtStatusBox.Text = txtStatusBox.Text.Insert(txtStatusBox.SelectionStart, "ASNAnewline \n");
            ResetColour();
            ColourMeTimbers();
        }
        private void btnAddCommand_Click(object sender, EventArgs e)
        {
            txtStatusBox.Text = txtStatusBox.Text.Insert(txtStatusBox.SelectionStart, "ASNAcommand ");
            ResetColour();
            ColourMeTimbers();
        }
        #endregion
        #region SavingFunctions
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveActiveInformation();

        }
        private void SaveActiveInformation()
        {
            if (ParseDataGridsForInvalidInformation())
            {
                return;
            }

            string filePath;
            string StatusFilePath;
            if (lstboxSites.Text == "" || lstboxSites.Text == null)
            {
                filePath = "";
                StatusFilePath = "";
                MessageBox.Show("You need to add a new site first!");
                return;
            }
            else
            {
                filePath = Program.PIDsites() + lstboxSites.Text;
                StatusFilePath = Program.PIDsites() + "." + lstboxSites.Text; ;
            }
            DataSet dsSystem = (DataSet)dgridSystem.DataSource;
            DataSet dsSFTP = (DataSet)dgridSFTP.DataSource;
            dsSystem.WriteXml(filePath);
            dsSFTP.WriteXml(filePath);

            StreamWriter sw = new StreamWriter(StatusFilePath);
            sw.Write(txtStatusBox.Text);
            sw.Close();
            sw.Dispose();
            MessageBox.Show("Saved!", Program.PNAme());
            ReloadUserControl();
            ChangeSaveToANegative();
        }
        public void CheckIfNeedsToBeSaved()
        {
            if (GetCurrentSaveState())
            {
                DialogResult dr = MessageBox.Show("Save changes?", Program.PNAme(), MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    SaveActiveInformation();
                }
                else
                {
                    ChangeSaveToANegative();
                }
            }
        }
        #endregion
        #region Text Changed for saving
        private void dgridSystem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ChangesToMake = true;
        }
        private void dgridSFTP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ChangesToMake = true;
        }
        private bool GetCurrentSaveState()
        {
            return ChangesToMake;
        }
        private void ChangeSaveToANegative()
        {
            ChangesToMake = false;
        }
        private void txtStatusBox_TextChanged_1(object sender, EventArgs e)
        {
            ChangesToMake = true;
        }
        #endregion
        #region Colouring Books
        private void ResetColour()
        {
            txtStatusBox.SelectAll();
            txtStatusBox.SelectionBackColor = System.Drawing.Color.White;
            txtStatusBox.DeselectAll();
        }
        private void ColourMeTimbers()
        {
            List<string> WordList = new List<string>();
            WordList.Add("ASNAnewline");
            WordList.Add("ASNAcommand");

            foreach (string word in WordList)
            {
                Color col = GetColour(word);

                int StartIndex = 0;

                while (StartIndex < txtStatusBox.TextLength)
                {
                    int WordStartIndex = txtStatusBox.Find(word, StartIndex, RichTextBoxFinds.None);

                    if (WordStartIndex != -1)
                    {
                        txtStatusBox.SelectionStart = WordStartIndex;
                        txtStatusBox.SelectionLength = word.Length;
                        txtStatusBox.SelectionBackColor = col;
                    }
                    else
                    {
                        break;
                    }
                    StartIndex = WordStartIndex + word.Length;
                }
            }
        }
        private static Color GetColour(string word)
        {
            switch (word)
            {
                case "ASNAnewline":
                    return Color.LightGreen;
                case "ASNAcommand":
                    return Color.LightBlue;
                default:
                    return Color.Blue;
            }
        }
        #endregion
        #region Exporting
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (ParseDataGridsForInvalidInformation())
            {
                return;
            }

            List<string> StatusCommands = GetStatusCommandsForExport();

            string OutputFile = GetOutputFileName();
            if (string.IsNullOrEmpty(OutputFile))
            {
                MessageBox.Show("Failed to export due to invalid file name!", Program.PNAme());
                return;
            }

            DataTable dtStatus = DynamicToDT(StatusCommands);
            DataSet dsSystem = (DataSet)dgridSystem.DataSource;
            StreamWriter sw = new StreamWriter(OutputFile);

            dtStatus.WriteXml(sw);
            sw.Write(onesplitter);
            dsSystem.WriteXml(sw);
            sw.Close();
            MessageBox.Show("Exported!", Program.PNAme());
        }

        private string GetOutputFileName()
        {
            string OutputFile;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                OutputFile = $@"{fbd.SelectedPath}\{lstboxSites.Text}.ASNA";
            }
            else
            {
                return "";
            }
            fbd.Dispose();
            return OutputFile;
        }

        private List<string> GetStatusCommandsForExport()
        {
            List<string> opls = new List<string>();
            if (txtStatusBox.TextLength > 1)
            {
                foreach (string line in txtStatusBox.Text.Split('\n'))
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }
                    opls.Add(line);
                }
            }
            return opls;
        }

        public static DataTable DynamicToDT(List<string> objects)
        {
            DataTable dt = new DataTable("StatusCMD");
            dt.Columns.Add("Status", typeof(string));
            foreach(string line in objects)
            {
                DataRow dr = dt.NewRow();
                dr["Status"] = line;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        #endregion
        #region Importing File
        private void btnImport_Click(object sender, EventArgs e)
        {
            ImportFile();
        }

        private void ImportFile()
        {
            string filename = GetFileNameForImport();
            if (!File.Exists(filename))
            {
                MessageBox.Show("Failed to find file exists", Program.PNAme());
                return;
            }

            StreamReader sr = new StreamReader(filename);
            string WholeFile = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            if (!WholeFile.Contains(onesplitter))
            {
                MessageBox.Show("Invalid file! :(", Program.PNAme());
                return;
            }
            string[] myfirsttmp = WholeFile.Split(new[] { onesplitter }, StringSplitOptions.None);
            if (myfirsttmp.Length != 2)
            {
                MessageBox.Show("Invalid file! :(", Program.PNAme());
                return;
            }

            string StatusCommand = myfirsttmp[0];
            // now the other two
            string ConfigAndSFTP = myfirsttmp[1];
            // this is now a complete file
            // now to check the file can be imported
            string ImportFileName;
            string HiddenImportFileName;
            ImportFileName = Program.PIDsites() + Path.GetFileName(filename.Replace(".ASNA", ""));
            HiddenImportFileName = Program.PIDsites() + "." + Path.GetFileName(filename.Replace(".ASNA", ""));
            if (File.Exists(ImportFileName))
            {
                MessageBox.Show("File already exists, will give a default name", Program.PNAme());
                string DateTimeFileName = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                ImportFileName = Program.PIDsites() + DateTimeFileName;
                HiddenImportFileName = Program.PIDsites() + "." + DateTimeFileName;
            }

            WriteTheStatusFile(StatusCommand, HiddenImportFileName);
            WriteTheConfigFile(ConfigAndSFTP, ImportFileName);

        }

        private void WriteTheConfigFile(string ConfigAndSFTP, string ImportFileName)
        {
            StreamWriter ws = new StreamWriter(ImportFileName);
            ws.Write(ConfigAndSFTP);
            ws.Close();
            ws.Dispose();
            ReloadUserControl();
        }

        private void WriteTheStatusFile(string StatusCommand, string HiddenImportFileName)
        {
            StreamWriter sw = new StreamWriter(HiddenImportFileName);
            foreach (string line in StatusCommand.Split('\n'))
            {
                if (line.Trim().StartsWith("<Status>"))
                {
                    sw.Write(line.Trim().Replace("<Status>", "").Replace("</Status>", "") + "\n");
                }
            }
            sw.Close();
            sw.Dispose();
        }

        private string GetFileNameForImport()
        {
            OpenFileDialog dlgOpen = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Filter = "ASNA Project Files|*.ASNA",
                FilterIndex = 1
            };
            dlgOpen.ShowDialog();
            return dlgOpen.FileName;
        }
        #endregion
        #region Extras
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ReloadUserControl();
        }
        public bool IsValidFileName(string expression, bool platformIndependent)
        {
            if (expression.StartsWith("."))
            {
                MessageBox.Show("File can't start with a FULLSTOP");
                return false;
            }
            if (expression.StartsWith("None"))
            {
                MessageBox.Show("File can't start with None");
                return false;
            }

            string sPattern = @"^(?!^(PRN|AUX|CLOCK\$|NUL|CON|COM\d|LPT\d|\..*)(\..+)?$)[^\x00-\x1f\\?*:\"";|/]+$";
            if (platformIndependent)
            {
                sPattern = @"^(([a-zA-Z]:|\\)\\)?(((\.)|(\.\.)|([^\\/:\*\?""\|<>\. ](([^\\/:\*\?""\|<>\. ])|([^\\/:\*\?""\|<>]*[^\\/:\*\?""\|<>\. ]))?))\\)*[^\\/:\*\?""\|<>\. ](([^\\/:\*\?""\|<>\. ])|([^\\/:\*\?""\|<>]*[^\\/:\*\?""\|<>\. ]))?$";
            }
            return (Regex.IsMatch(expression, sPattern, RegexOptions.CultureInvariant));
        }
        public void ReloadUserControl()
        {
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
            dgridSystem.DataSource = null;
            dgridSFTP.DataSource = null;
            txtRenameBox.Text = "";
            txtStatusBox.Text = "";
            btnExport.Enabled = false;
            btnSave.Enabled = false;
            btnAddCommand.Enabled = false;
            btnAddNewline.Enabled = false;
            btnRenameOK.Enabled = false;
            btnRemove.Enabled = false;
            txtRenameBox.Enabled = false;
            txtStatusBox.Enabled = false;
            ChangeSaveToANegative();
            //MessageBox.Show(ChangesToMake.ToString());
        }
        #endregion
    }
}
