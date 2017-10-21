/* 
 Swoopae's MediaTek65x2 Porting Tool
        (C) Swoopae 2017
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;

namespace MediaTek65xxTool
{
    public partial class Main : Form
    {
        IniParse ini = new IniParse("settings.ini");

        List<string> bootimgtool = new List<string>
            { "bootimg.exe", "repack.bat", "unpack.bat" };

        public string dirPath = Directory.GetCurrentDirectory();
        public string chipset = "0";
        public string customChip = "";

        private int AndroidVer;
        private bool firstLaunch = true;

        public Main()
        {
            InitializeComponent();
        }

        private static void ExtractEmbeddedResource(string outputDir, string resourceLocation, List<string> files)
        {
            foreach (string file in files)
            {
                using (System.IO.Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceLocation + @"." + file))
                {
                    using (System.IO.FileStream fileStream = new System.IO.FileStream(System.IO.Path.Combine(outputDir, file), System.IO.FileMode.Create))
                    {
                        for (int i = 0; i < stream.Length; i++)
                        {
                            fileStream.WriteByte((byte)stream.ReadByte());
                        }
                        fileStream.Close();
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(dirPath + "/settings.ini"))
            {
                firstLaunch = false;
            }

            if (firstLaunch)
            {
                ini.Write("AndroidVer", "0", "Preferences");
                ini.Write("Chipset", "0", "Preferences");
            } else
            {
                AndroidVer = int.Parse(ini.Read("AndroidVer", "Preferences"));
                chipset = ini.Read("Chipset", "Preferences");

                // i'm way too lazy to rewrite this as a switch even tho it would look so much prettier

                switch (AndroidVer)
                {
                    case 1:
                        radioButton2.Checked = false;
                        radioButton3.Checked = false;
                        radioButton1.Checked = true;
                        break;
                    case 2:
                        radioButton1.Checked = false;
                        radioButton3.Checked = false;
                        radioButton2.Checked = true;
                        break;
                    case 3:
                        radioButton1.Checked = false;
                        radioButton2.Checked = false;
                        radioButton3.Checked = true;
                        break;
                    default:
                        radioButton1.Checked = false;
                        radioButton2.Checked = false;
                        radioButton3.Checked = false;
                        break;
                }
                switch (chipset) {
                    case "6572":
                        mtButton1.Checked = true;
                        mtButton2.Checked = false;
                        mtButton3.Checked = false;
                        mtButton4.Checked = false;
                        break;
                    case "6582":
                        mtButton1.Checked = false;
                        mtButton2.Checked = true;
                        mtButton3.Checked = false;
                        mtButton4.Checked = false;
                        break;
                    case "6592":
                        mtButton1.Checked = false;
                        mtButton2.Checked = false;
                        mtButton3.Checked = true;
                        mtButton4.Checked = false;
                        break;
                    case "custom":
                        mtButton1.Checked = false;
                        mtButton2.Checked = false;
                        mtButton3.Checked = false;
                        mtButton4.Checked = true;
                        break;
                    default:
                        mtButton1.Checked = false;
                        mtButton2.Checked = false;
                        mtButton2.Checked = false;
                        break;
                }
            }
        }

        private void stockZipButton_Click(object sender, EventArgs e)
        {
            baseRomFile.ShowDialog();
        }

        private void portZipButton_Click(object sender, EventArgs e)
        {
            portRomFile.ShowDialog();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            AndroidVer = 3;
            ini.Write("AndroidVer", AndroidVer.ToString(), "Preferences");
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            AndroidVer = 2;
            ini.Write("AndroidVer", AndroidVer.ToString(), "Preferences");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            AndroidVer = 1;
            ini.Write("AndroidVer", AndroidVer.ToString(), "Preferences");
        }

        void ZipActivity(string activity, string filepath, string newpath)
        {
            if (activity == "extract")
            {
                try {
                    ZipFile.ExtractToDirectory(filepath, newpath);
                } catch (Exception)
                {
                    MessageBox.Show("Unexpected error. Error code: 0x01");
                    Application.Exit();
                }
            } else if (activity == "repack")
            {
                try
                {
                    ZipFile.CreateFromDirectory(filepath, newpath);
                }
                catch (Exception)
                {
                    MessageBox.Show("Unexpected error. Error code: 0x02");
                    Application.Exit();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!File.Exists(dirPath + "/" + customChip + ".ini"))
            {
                MessageBox.Show("Please select a valid custom chipset preset. (Make sure what you typed is equal to what the .ini preset is named, for example you must type PRESET for the file PRESET.ini");
                return;
            }
            if (AndroidVer == 0)
            {
                MessageBox.Show("Please select a valid android version for your ported rom. ");
                return;
            }

            if (chipset != "6572" && chipset != "6582" && chipset != "6592" && chipset != "custom")
            {
                MessageBox.Show("Please select a valid chipset. ");
                return;
            }
            else
            {
                Directory.CreateDirectory(dirPath + "/temp/");
                Directory.CreateDirectory(dirPath + "/temp/BaseROM/");
                Directory.CreateDirectory(dirPath + "/temp/PortROM/");
                Directory.CreateDirectory(dirPath + "/temp/FilesToPort/");

                label2.Text = "Status: Extracting base ROM";

                ZipActivity("extract", baseRomFile.FileName.ToString(), dirPath + "/temp/BaseROM");
                progressBar1.Value = 20;
                progressBar1.Refresh();

                label2.Text = "Status: Extracting port ROM";

                ZipActivity("extract", portRomFile.FileName.ToString(), dirPath + "/temp/PortROM");
                progressBar1.Value = 40;
                progressBar1.Refresh();

                if (chipset != "custom")
                {
                    switch (AndroidVer)
                    {
                        case 1:
                            KitkatPort();
                            break;
                        case 2:
                            GeneralPort();
                            break;
                        case 3:
                            GeneralPort();
                            break;
                    }
                } else
                {
                    if (!File.Exists(dirPath + "/" + customChip + ".ini"))
                    {
                        MessageBox.Show("Please select a valid custom chipset preset. (Make sure what you typed is equal to what the .ini preset is named, for example you must type PRESET for the file PRESET.ini");
                        return;
                    } else
                    {
                        CustomPort();
                    }
                }
            }
        }

        void CustomPort()
        {
            IniParse preset = new IniParse(customChip + ".ini");

            label2.Text = "Status: Checking preset consistency";

            int dirsToMove = 0, filesToMove = 0, btFilesToMove = 0;

            try
            {
                dirsToMove = int.Parse(preset.Read("Count", "Directories"));
                filesToMove = int.Parse(preset.Read("Count", "Files"));
                btFilesToMove = int.Parse(preset.Read("Count", "BootImg"));
            } catch (Exception)
            {
                MessageBox.Show("Unexpected error.Error code: 0x05 (Corrupted preset file)");
                Cleanup(false);
            }
            
            label2.Text = "Status: Replacing drivers";

            while (dirsToMove < 0) {
                try
                {
                    Directory.Delete(dirPath + "/temp/PortROM" + preset.Read("Directory" + dirsToMove.ToString(), "Directories"), true);
                    Directory.Move(dirPath + "/temp/BaseROM" + preset.Read("Directory" + dirsToMove.ToString(), "Directories"), dirPath + "/temp/PortROM" + preset.Read("Directory" + dirsToMove.ToString(), "Directories"));
                } catch (Exception)
                {
                    MessageBox.Show("Unexpected error.Error code: 0x06 (Invalid directory path in preset)");
                    Cleanup(true);
                }
                dirsToMove -= 1;
            }

            while (filesToMove < 0)
            {
                try
                {
                    File.Copy(dirPath + "/temp/BaseROM" + preset.Read("File" + filesToMove.ToString(), "Files"), dirPath + "/temp/PortROM" + preset.Read("File" + filesToMove.ToString(), "Files"), true);
                } catch (Exception)
                {
                    MessageBox.Show("Unexpected error.Error code: 0x07 (Invalid file path in preset)");
                    Cleanup(true);
                }
                filesToMove -= 1;
            }

            Directory.CreateDirectory(dirPath + "/temp/BaseROM/bootimage");
            Directory.CreateDirectory(dirPath + "/temp/PortROM/bootimage");
            ExtractEmbeddedResource(dirPath + "/temp/BaseROM/bootimage", "MediaTek65xxTool", bootimgtool);
            ExtractEmbeddedResource(dirPath + "/temp/PortROM/bootimage", "MediaTek65xxTool", bootimgtool);

            File.Move(dirPath + "/temp/BaseROM/boot.img", dirPath + "/temp/BaseROM/bootimage/boot.img");
            File.Move(dirPath + "/temp/PortROM/boot.img", dirPath + "/temp/PortROM/bootimage/boot.img");
            System.Threading.Thread.Sleep(5000);

            label2.Text = "Status: Modifying boot image";

            Process unpackStock = new Process();

            unpackStock.StartInfo.FileName = dirPath + "/temp/BaseROM/bootimage/unpack.bat";
            unpackStock.StartInfo.Arguments = "";
            unpackStock.StartInfo.WorkingDirectory = dirPath + "/temp/BaseROM/bootimage/";
            unpackStock.Start();
            System.Threading.Thread.Sleep(1000);

            Process unpackPort = new Process();

            unpackPort.StartInfo.FileName = dirPath + "/temp/PortROM/bootimage/unpack.bat";
            unpackPort.StartInfo.Arguments = "";
            unpackPort.StartInfo.WorkingDirectory = dirPath + "/temp/PortROM/bootimage/";
            unpackPort.Start();
            System.Threading.Thread.Sleep(1000);

            progressBar1.Value = 75;
            progressBar1.Refresh();

            System.Threading.Thread.Sleep(10000);

            while (btFilesToMove != 0)
            {
                try
                {
                    File.Copy(dirPath + "/temp/BaseROM/bootimage" + preset.Read("File" + btFilesToMove.ToString(), "BootImg"), dirPath + "/temp/PortROM/bootimage" + preset.Read("File" + btFilesToMove.ToString(), "BootImg"), true);
                }
                catch (Exception)
                {
                    MessageBox.Show("Unexpected error.Error code: 0x08 (Invalid boot image files path in preset)");
                    Cleanup(true);
                }

                btFilesToMove -= 1;
            }

            progressBar1.Value = 85;
            progressBar1.Refresh();

            unpackPort.StartInfo.FileName = dirPath + "/temp/PortROM/bootimage/repack.bat";
            unpackPort.StartInfo.Arguments = "";
            unpackPort.StartInfo.WorkingDirectory = dirPath + "/temp/PortROM/bootimage/";
            unpackPort.Start();

            System.Threading.Thread.Sleep(10000);

            File.Copy(dirPath + "/temp/PortROM/bootimage/boot-new.img", dirPath + "/temp/PortROM/boot.img", true);

            progressBar1.Value = 90;
            progressBar1.Refresh();
            
            Directory.Delete(dirPath + "/temp/PortROM/bootimage/", true);

            label2.Text = "Status: Repacking ported ROM";

            ZipActivity("repack", dirPath + "/temp/PortROM", dirPath + "/portedROM.zip");

            label2.Text = "Status: Cleaning up";

            progressBar1.Value = 95;
            progressBar1.Refresh();

            DirectoryInfo dx = new DirectoryInfo(dirPath + "/temp/");

            foreach (FileInfo file in dx.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in dx.GetDirectories())
            {
                dir.Delete(true);
            }

            progressBar1.Value = 100;
            progressBar1.Refresh();

            Directory.Delete(dirPath + "/temp/");

            label2.Text = "Status: Idle";

            MessageBox.Show("Job done! Please consider re-checking the updater-script and modifying it for your device/Doing some build.prop tweaks before flashing! :)");
        }

        void KitkatPort()
        {
            MessageBox.Show("This feature isn't available just yet. :)");
            return;
            File.Copy(dirPath + "/temp/BaseROM/system/etc/firmware", dirPath + "/temp/PortROM/system/etc/firmware", true);
            File.Copy(dirPath + "/temp/BaseROM/system/etc/wifi", dirPath + "/temp/PortROM/system/etc/wifi", true);
            File.Copy(dirPath + "/temp/BaseROM/system/etc/bluetooth", dirPath + "/temp/PortROM/system/etc/bluetooth", true);
        }
        void GeneralPort()
        {
            label2.Text = "Status: Replacing drivers";

            Directory.Delete(dirPath + "/temp/PortROM/system/etc/firmware/", true);
            Directory.Delete(dirPath + "/temp/PortROM/system/etc/wifi/", true);
            Directory.Delete(dirPath + "/temp/PortROM/system/etc/bluetooth/", true);
            Directory.Move(dirPath + "/temp/BaseROM/system/etc/firmware/", dirPath + "/temp/PortROM/system/etc/firmware/");
            Directory.Move(dirPath + "/temp/BaseROM/system/etc/wifi/", dirPath + "/temp/PortROM/system/etc/wifi/");
            Directory.Move(dirPath + "/temp/BaseROM/system/etc/bluetooth/", dirPath + "/temp/PortROM/system/etc/bluetooth/");

            try
            {
                progressBar1.Value = 50;
                progressBar1.Refresh();
                File.Copy(dirPath + "/temp/BaseROM/system/lib/hw/hwcomposer.mt" + chipset + ".so", dirPath + "/temp/PortROM/system/lib/hw/hwcomposer." + chipset + ".so", true);
                File.Copy(dirPath + "/temp/BaseROM/system/lib/libaudio.primary.default.so", dirPath + "/temp/PortROM/system/lib/libaudio.primary.default.so", true);
                File.Copy(dirPath + "/temp/BaseROM/system/lib/libMali.so", dirPath + "/temp/PortROM/system/lib/libMali.so", true);
                File.Copy(dirPath + "/temp/BaseROM/system/lib/libcamalgo.so", dirPath + "/temp/PortROM/system/lib/libcamalgo.so", true);
                File.Copy(dirPath + "/temp/BaseROM/system/lib/libcamdrv.so", dirPath + "/temp/PortROM/system/lib/libcamdrv.so", true);
                File.Copy(dirPath + "/temp/BaseROM/system/lib/libcameracustom.so", dirPath + "/temp/PortROM/system/lib/libcameracustom.so", true);
                File.Copy(dirPath + "/temp/BaseROM/system/bin/gsm0710muxd", dirPath + "/temp/PortROM/system/bin/gsm0710muxd", true);
                File.Copy(dirPath + "/temp/BaseROM/system/bin/gsm0710muxdmd2", dirPath + "/temp/PortROM/system/bin/gsm0710muxdmd2", true);
                File.Copy(dirPath + "/temp/BaseROM/system/bin/rild", dirPath + "/temp/PortROM/system/bin/rild", true);
                File.Copy(dirPath + "/temp/BaseROM/system/bin/rildmd2", dirPath + "/temp/PortROM/system/bin/rildmd2", true);
                progressBar1.Value = 70;
                progressBar1.Refresh();
            }
            catch (Exception)
            {
                MessageBox.Show("Unexpected error. Error code: 0x03 (at moving libraries from stock to port rom)");

                DirectoryInfo di = new DirectoryInfo(dirPath + "/temp/");

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }

                Application.Exit();
            }

            Directory.CreateDirectory(dirPath + "/temp/BaseROM/bootimage");
            Directory.CreateDirectory(dirPath + "/temp/PortROM/bootimage");
            ExtractEmbeddedResource(dirPath + "/temp/BaseROM/bootimage", "MediaTek65xxTool", bootimgtool);
            ExtractEmbeddedResource(dirPath + "/temp/PortROM/bootimage", "MediaTek65xxTool", bootimgtool);

            File.Move(dirPath + "/temp/BaseROM/boot.img", dirPath + "/temp/BaseROM/bootimage/boot.img");
            File.Move(dirPath + "/temp/PortROM/boot.img", dirPath + "/temp/PortROM/bootimage/boot.img");
            System.Threading.Thread.Sleep(5000);

            // Boot image unpacking-repacking start. 

            label2.Text = "Status: Modifying boot image";

            Process unpackStock = new Process();

            unpackStock.StartInfo.FileName = dirPath + "/temp/BaseROM/bootimage/unpack.bat";
            unpackStock.StartInfo.Arguments = "";
            unpackStock.StartInfo.WorkingDirectory = dirPath + "/temp/BaseROM/bootimage/";
            unpackStock.Start();
            System.Threading.Thread.Sleep(1000);

            Process unpackPort = new Process();

            unpackPort.StartInfo.FileName = dirPath + "/temp/PortROM/bootimage/unpack.bat";
            unpackPort.StartInfo.Arguments = "";
            unpackPort.StartInfo.WorkingDirectory = dirPath + "/temp/PortROM/bootimage/";
            unpackPort.Start();
            System.Threading.Thread.Sleep(1000);

            progressBar1.Value = 75;
            progressBar1.Refresh();

            System.Threading.Thread.Sleep(10000);

            try
            {
                File.Copy(dirPath + "/temp/BaseROM/bootimage/kernel", dirPath + "/temp/PortROM/bootimage/kernel", true);
            }
            catch (Exception)
            {
                MessageBox.Show("Unexpected error.Error code: 0x04 (Boot image unpacking/repacking error)");
                
                DirectoryInfo di = new DirectoryInfo(dirPath + "/temp/");

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }

                Application.Exit();
            }

            progressBar1.Value = 85;
            progressBar1.Refresh();

            unpackPort.StartInfo.FileName = dirPath + "/temp/PortROM/bootimage/repack.bat";
            unpackPort.StartInfo.Arguments = "";
            unpackPort.StartInfo.WorkingDirectory = dirPath + "/temp/PortROM/bootimage/";
            unpackPort.Start();

            System.Threading.Thread.Sleep(10000);

            File.Copy(dirPath + "/temp/PortROM/bootimage/boot-new.img", dirPath + "/temp/PortROM/boot.img", true);

            progressBar1.Value = 90;
            progressBar1.Refresh();

            // Boot image unpacking-repacking done.

            Directory.Delete(dirPath + "/temp/PortROM/bootimage/", true);

            label2.Text = "Status: Repacking ported ROM";

            ZipActivity("repack", dirPath + "/temp/PortROM", dirPath + "/portedROM.zip");

            label2.Text = "Status: Cleaning up";

            progressBar1.Value = 95;
            progressBar1.Refresh();

            DirectoryInfo dx = new DirectoryInfo(dirPath + "/temp/");

            foreach (FileInfo file in dx.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in dx.GetDirectories())
            {
                dir.Delete(true);
            }

            progressBar1.Value = 100;
            progressBar1.Refresh();

            Directory.Delete(dirPath + "/temp/");

            label2.Text = "Status: Idle";

            MessageBox.Show("Job done! Please consider re-checking the updater-script and modifying it for your device/Doing some build.prop tweaks before flashing! :)");
        }

        private void mtButton1_CheckedChanged(object sender, EventArgs e)
        {
            chipset = "6572";
            ini.Write("Chipset", chipset, "Preferences");
        }

        private void mtButton2_CheckedChanged(object sender, EventArgs e)
        {
            chipset = "6582";
            ini.Write("Chipset", chipset, "Preferences");
        }

        private void mtButton3_CheckedChanged(object sender, EventArgs e)
        {
            chipset = "6592";
            ini.Write("Chipset", chipset, "Preferences");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            customChip = textBox1.Text;
        }

        private void mtButton4_CheckedChanged(object sender, EventArgs e)
        {
            chipset = "custom";
            ini.Write("Chipset", chipset, "Preferences");
        }

        void Cleanup(bool exit)
        {
            DirectoryInfo direcx = new DirectoryInfo(dirPath + "/temp/");

            foreach (FileInfo file in direcx.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in direcx.GetDirectories())
            {
                dir.Delete(true);
            }

            Directory.Delete(dirPath + "/temp/");

            if (exit)
                Application.Exit();
        }
    }
}
