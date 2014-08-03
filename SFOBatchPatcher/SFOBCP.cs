using Log;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SFOBatchCategoryPatcher
{
    public partial class SFOBCP : Form
    {
        // Some vars we will use
        #region vars
        static string target = "nothing";
        static string appPSNPKG = Directory.GetCurrentDirectory() + @"\toolz\psn_package_npdrm.exe";
        static string appUnPKG = Directory.GetCurrentDirectory() + @"\toolz\ungpkg.exe";
        static string pkgc = "package.conf";
        static string npdrmName = "nothing";
        static string[] pkgFolders;
        static string[] pkgFiles;

        static string[] sfoBaseString = { " - HDD Game",
                                          " - Disc Game",
                                          " - PCE (PC Engine)",
                                          " - Neo Geo",
                                          " - App Photo",
                                          " - App Music",
                                          " - App Video",
                                          " - Broadcast Video",
                                          " - App TV",
                                          " - Web TV",
                                          " - Cell BE",
                                          " - Home",
                                          " - Store Frontend",
                                          " - PS2 Game",
                                          " - PS2 PSN",
                                          " - PS1 PSN",
                                          " - PSP Minis",
                                          " - PSP Emulator",
                                          " - PSP",
                                          " - PS2 Data",
                                        };

        static byte[] sfoBaseByte = new byte[40]
        {
            0x48, 0x47, 0x44, 0x47, 0x58, 0x31, 0x58, 0x32, 0x41, 0x50, 0x41, 0x4D, 0x41, 0x56, 0x42, 0x56, 
            0x41, 0x54, 0x57, 0x54, 0x43, 0x42, 0x48, 0x4D, 0x53, 0x46, 0x32, 0x47, 0x32, 0x50, 0x31, 0x50, 
            0x4D, 0x4E, 0x50, 0x45, 0x50, 0x50, 0x32, 0x44, 
        };
        #endregion vars

        // Initialize Instanc
        public SFOBCP()
        {
            InitializeComponent();
        }

        // On Load of Form define standarts
        private void SFOBCP_Load(object sender, EventArgs e)
        {
            SetFolderIco();
            buttonBC.Enabled = false;
            consoleControl.IsLogEnabled = false;
            //consoleControl.pathToLogFile = "log.txt";
            consoleControl.ShowDiagnostics = true;
            consoleControl.Visible = false;
            this.ClientSize = new System.Drawing.Size(501, 120);
            labelPF1.Text = "by cfwprpht (c)2014";
            labelPF2.Text = "Welcome to the Batch SFO Category Patcher !!";
            //File.Create("log.txt").Close();
        }

        // Automatical set the folder ico on Start of app
        private void SetFolderIco()
        {
            string str = Directory.GetCurrentDirectory() + @"\folderico.ico,0";
            string _str = Directory.GetCurrentDirectory() + @"\desktop.ini";
            string[] setFolderIco = { "[.ShellClassInfo]",
                                      "IconResource=" + str,
                                      "[ViewState]",
                                      "Mode=",
                                      "Vid=",
                                      "FolderType=Generic" };

            if (!File.Exists(_str))
            {
                File.Create(_str).Close();
            }

            FileInfo fileInfo = new FileInfo(_str);
            fileInfo.Attributes = FileAttributes.Normal;
            File.WriteAllLines(_str, setFolderIco);
            fileInfo.Attributes = FileAttributes.Hidden | FileAttributes.System | FileAttributes.Archive;
        }

        // Move the orig pkg before we copy the converted pkg
        private void MoveOrig(string st, string pkg)
        {
            consoleControl.WriteOutput("Moving Original PKG File into 'ORIG' Folder...", System.Drawing.Color.Yellow);

            if (!Directory.Exists(st + @"\ORIG"))
            {
                Directory.CreateDirectory(st + @"\ORIG");
            }

            string str = pkg.Replace(st + @"\", "");
            File.Move(pkg, st + @"\ORIG\" + str);
            consoleControl.WriteOutput("done!\n\n", System.Drawing.Color.Yellow);
            progressBarSFOBCP.PerformStep();
        }

        // Move all content off the PKG into our working dir to compile the pkg
        private void MovePKGContent(string st)
        {
            consoleControl.WriteOutput("Moving content of pkg into working dir...", System.Drawing.Color.Yellow);
            pkgFolders = Directory.GetDirectories(st + @"\" + npdrmName + @"\", "*");
            pkgFiles = Directory.GetFiles(st + @"\" + npdrmName + @"\", "*");

            foreach (string pkgFolder in pkgFolders)
            {
                string str = pkgFolder.Replace(@"\" + npdrmName, "");
                string _str = str.Replace(st + @"\", "");
                consoleControl.WriteOutput("Moving Folder " + _str + "...", System.Drawing.Color.Yellow);
                Directory.Move(pkgFolder, str);
                consoleControl.WriteOutput("done!\n", System.Drawing.Color.Yellow);
            }

            foreach (string pkgFile in pkgFiles)
            {
                string str = pkgFile.Replace(@"\" + npdrmName, "");
                string _str = str.Replace(st + @"\", "");
                consoleControl.WriteOutput("Moving File " + _str + "...", System.Drawing.Color.Yellow);
                File.Move(pkgFile, str);
                consoleControl.WriteOutput("done!\n", System.Drawing.Color.Yellow);
            }
            consoleControl.WriteOutput("\n", System.Drawing.Color.Yellow);

            consoleControl.WriteOutput("Moving content of pkg into working dir...done!\n\n", System.Drawing.Color.Yellow);
            progressBarSFOBCP.PerformStep();
        }

        // Clean up the working files and rename converted pkg
        private void Finalize(string st, string pkg)
        {
            string zZ = st + @"\";
            string _zZ = pkg.Replace(st + @"\", "");
            consoleControl.WriteOutput("Cleaning the working dir...\n\n", System.Drawing.Color.Yellow);
            consoleControl.WriteOutput("Renaming pkg...", System.Drawing.Color.Yellow);
            File.Move(zZ + npdrmName + ".pkg", zZ + "_" + target + "_" + _zZ);
            consoleControl.WriteOutput("done!\n", System.Drawing.Color.Yellow);
            consoleControl.WriteOutput("Deleting package.conf...", System.Drawing.Color.Yellow);
            File.Delete(zZ + "package.conf");
            consoleControl.WriteOutput("done!\n", System.Drawing.Color.Yellow);
            consoleControl.WriteOutput("Deleting Directory " + npdrmName + "...", System.Drawing.Color.Yellow);
            Directory.Delete(zZ + npdrmName, true);
            consoleControl.WriteOutput("done!\n", System.Drawing.Color.Yellow);

            foreach (string pkgFolder in pkgFolders)
            {
                string str = pkgFolder.Replace(@"\" + npdrmName, "");
                string _str = str.Replace(st + @"\", "");
                consoleControl.WriteOutput("Deleting Folder " + _str + "...", System.Drawing.Color.Yellow);
                Directory.Delete(str, true);
                consoleControl.WriteOutput("done!\n", System.Drawing.Color.Yellow);
            }

            foreach (string pkgFile in pkgFiles)
            {
                string str = pkgFile.Replace(@"\" + npdrmName, "");
                string _str = str.Replace(st + @"\", "");
                consoleControl.WriteOutput("Deleting File " + _str + "...", System.Drawing.Color.Yellow);
                File.Delete(str);
                consoleControl.WriteOutput("done!\n", System.Drawing.Color.Yellow);
            }
            consoleControl.WriteOutput("\n", System.Drawing.Color.Yellow);

            consoleControl.WriteOutput("Cleaning the working dir...done!\n\n", System.Drawing.Color.Yellow);
            progressBarSFOBCP.PerformStep();
        }

        // Get the NPDRM Name from the Eboot
        private void GetNameNPDRM(string path)
        {
            string[] subDirs = Directory.GetDirectories(path + @"\", "*-*_*-*");

            foreach (string subdir in subDirs)
            {
                npdrmName = subdir.Replace(path + @"\", "");
                consoleControl.WriteOutput("NPDRM Name: " + npdrmName + "...\n\n", System.Drawing.Color.Yellow);
            }
            progressBarSFOBCP.PerformStep();
        }

        // Extract a PS3 NPDRM PKG File
        private void ExtractPKG(string pkg)
        {
            consoleControl.StartProcess(appUnPKG, pkg);
            progressBarSFOBCP.PerformStep();
        }

        // Repack a PS3 NPDRM PKG File. (the string pkg is only used to show the user the actual pkg which will be repacked right now)
        private void RepackPKG(string pkg)
        {
            consoleControl.StartProcess(appPSNPKG, "");
            progressBarSFOBCP.PerformStep();
        }

        // Patch the Category of SFO
        private void PatchSFO(string path)
        {
            consoleControl.WriteOutput("Patching SFO...", System.Drawing.Color.Yellow);

            int endFlag = 0;
            string str = path + @"\" + npdrmName + @"\PARAM.SFO";
            
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] buffer2 = enc.GetBytes(target);
            FileInfo fileInfo = new FileInfo(str);

            using (var v = new FileStream(str, FileMode.Open, FileAccess.ReadWrite))
            {
                byte[] buffer = new byte[fileInfo.Length];
                v.Read(buffer, 0, Convert.ToInt16(fileInfo.Length));

                for (int i = 0; i < 40; i++)
                {
                    for (int j = 0; j < buffer.Length; j++)
                    {
                        if (buffer[j] == sfoBaseByte[i] && buffer[j + 1] == sfoBaseByte[i + 1])
                        {
                            v.Position = j;
                            v.WriteByte(buffer2[0]);
                            v.Position = j + 1;
                            v.WriteByte(buffer2[1]);

                            endFlag = 1;
                            break;
                        }

                        if (j == (buffer.Length - 1))
                        {
                            break;
                        }
                    }

                    if (endFlag == 1)
                    {
                        break;
                    }

                    i++;
                }
                v.Close();
            }
            consoleControl.WriteOutput("done!\n\n", System.Drawing.Color.Yellow);
            progressBarSFOBCP.PerformStep();
        }

        // Write the NPDRM pkg info .conf file
        private void WritePKGInfo(string path)
        {
            GetNameNPDRM(path);

            consoleControl.WriteOutput("Writting file package.conf...\n\n", System.Drawing.Color.Yellow);

            File.Create(pkgc).Close();

            Logger.WriteText(pkgc, "ContentID=" + npdrmName);
            consoleControl.WriteOutput("ContentID=" + npdrmName + "\n", System.Drawing.Color.Yellow);
            Logger.WriteText(pkgc, "Klicensee=0x0000000000000000");
            consoleControl.WriteOutput("Klicensee=0x0000000000000000\n", System.Drawing.Color.Yellow);
            Logger.WriteText(pkgc, "DRMType=Free");
            consoleControl.WriteOutput("DRMType=Free\n", System.Drawing.Color.Yellow);
            Logger.WriteText(pkgc, "ContentType=GameExec");
            consoleControl.WriteOutput("ContentType=GameExec\n", System.Drawing.Color.Yellow);
            Logger.WriteText(pkgc, "PackageVersion=01.00");
            consoleControl.WriteOutput("PackageVersion=01.00\n\n", System.Drawing.Color.Yellow);

            consoleControl.WriteOutput("Writting file package.conf...done!\n\n", System.Drawing.Color.Yellow);
            progressBarSFOBCP.PerformStep();
        }

        private void checkVerbose_CheckedChanged(object sender, EventArgs e)
        {
            if (checkVerbose.Checked)
            {
                this.ClientSize = new System.Drawing.Size(501, 468);
                consoleControl.Visible = true;
                consoleControl.ClearOutput();
                consoleControl.WriteOutput("Showing Verbose Output...\n\nSFO Batch Category Patcher v1.0\nby cfwprophet (c)2014\n\nhttp://www.PlayStationHax.it/forum\n\n", System.Drawing.Color.Yellow);
            }
            else
            {
                this.ClientSize = new System.Drawing.Size(501, 120);
                consoleControl.Visible = false;
            }
        }

        // Checks if a SFO Category is selected
        private void comboSFO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboSFO.Text != "Target")
            {
                target = comboSFO.Text;

                for (int i = 0; i < 20; i++)
                {
                    if (target.Contains(sfoBaseString[i]))
                    {
                        string newone = target.Replace(sfoBaseString[i], "");
                        target = newone;
                    }
                }
                
                labelPF2.Text = "Will all SFO's patch to: " + target;
                buttonBC.Enabled = true;
            }
        }

        // Start the Batch Convertion of SFO Category
        private void buttonBC_Click(object sender, EventArgs e)
        {
            string st = Directory.GetCurrentDirectory();
            
            // Get all PKG files from working Dir
            string[] pkgs = Directory.GetFiles(st, "*.pkg") ;

            // If there are PKG files within the working folder we will start
            if (pkgs.Length != 0)
            {
                progressBarSFOBCP.Minimum = 0;
                progressBarSFOBCP.Maximum = (pkgs.Length * 8);
                progressBarSFOBCP.Value = 0;
                progressBarSFOBCP.Step = 1;

                foreach (string pkg in pkgs)
                {
                    try
                    {
                        string onlyPKG = pkg.Replace(st + @"\", "");
                        consoleControl.WriteOutput("Extracting: " + pkg + "...\n\n", System.Drawing.Color.Yellow);
                        ExtractPKG(onlyPKG);
                    }
                    catch (Exception a)
                    {
                        MessageBox.Show("Can not extract the pkg file!\n" + pkg);
                        MessageBox.Show(a.ToString());

                        for (int i = 0; i < 4; i++)
                        {
                            progressBarSFOBCP.PerformStep();
                        }
                    }
                    finally
                    {
                        try
                        {
                            while (consoleControl.IsProcessRunning)
                            {
                                Application.DoEvents();
                            }

                            consoleControl.WriteOutput("Extracting: " + pkg + "...done!\n\n", System.Drawing.Color.Yellow);
                            WritePKGInfo(st);
                        }
                        catch (Exception a)
                        {
                            MessageBox.Show("Can not write the config file for this pkg!\n" + pkg);
                            MessageBox.Show(a.ToString());

                            for (int i = 0; i < 2; i++)
                            {
                                progressBarSFOBCP.PerformStep();
                            }
                        }
                        finally
                        {
                            try
                            {
                                PatchSFO(st);
                                MoveOrig(st, pkg);
                                MovePKGContent(st);
                            }
                            catch (Exception a)
                            {
                                MessageBox.Show("Can not Patch the SFO for this pkg file!\n" + pkg);
                                MessageBox.Show(a.ToString());

                                progressBarSFOBCP.PerformStep();
                            }
                            finally
                            {
                                try
                                {
                                    consoleControl.WriteOutput("Repacking: " + pkg + "...\n\n", System.Drawing.Color.Yellow);

                                    // we need to wait a bit on the Backgroundworker...
                                    for (int i = 0; i < 50000; i++)
                                    {
                                        Application.DoEvents();
                                    }

                                    RepackPKG(pkg);
                                }
                                catch (Exception a)
                                {
                                    MessageBox.Show("Can not rebuild your pkg!\n" + pkg);
                                    MessageBox.Show(a.ToString());
                                }
                                finally
                                {
                                    try
                                    {
                                        while (consoleControl.IsProcessRunning)
                                        {
                                            Application.DoEvents();
                                        }

                                        consoleControl.WriteOutput("Repacking: " + pkg + "...done!\n\n", System.Drawing.Color.Yellow);
                                        Finalize(st, pkg);
                                    }
                                    catch (Exception a)
                                    {
                                        MessageBox.Show(a.ToString());
                                    }
                                }
                            }
                        }
                    }
                }
                MessageBox.Show("Done!");

                // Flush
                progressBarSFOBCP.Value = 0;
                npdrmName = "nothing";
                target = "nothing";
                pkgFolders = null;
                pkgFiles = null;
            }
            else
            {
                MessageBox.Show("Can not find any PKG file within this folder!");
            }
        }
    }
}
