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

            if(firstLaunch)
            {
                ini.Write("AndroidVer", "0", "Preferences");
                ini.Write("Chipset", "0", "Preferences");
            } else
            {
                AndroidVer = int.Parse(ini.Read("AndroidVer", "Preferences"));
                chipset = int.Parse(ini.Read("Chipset", "Preferences")).ToString();

                // i'm way too lazy to rewrite this as a switch even tho it would look so much prettier

                if (AndroidVer == 1)
                {
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    radioButton1.Checked = true;
                } else if (AndroidVer == 2)
                {
                    radioButton1.Checked = false;
                    radioButton3.Checked = false;
                    radioButton2.Checked = true;
                } else if (AndroidVer == 3)
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = true;
                } else
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                }

                switch (chipset) {
                    case "6572":
                        mtButton1.Checked = true;
                        mtButton2.Checked = false;
                        mtButton3.Checked = false;
                        break;
                    case "6582":
                        mtButton1.Checked = false;
                        mtButton2.Checked = true;
                        mtButton3.Checked = false;
                        break;
                    case "6592":
                        mtButton1.Checked = false;
                        mtButton2.Checked = false;
                        mtButton3.Checked = true;
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
            if (AndroidVer == 0)
            {
                MessageBox.Show("Please select a valid android version for your ported rom. ");
                Application.Exit();
            }

            if (chipset == "0")
            {
                MessageBox.Show("Please select a valid chipset. ");
                Application.Exit();
            }
            else
            {
                Directory.CreateDirectory(dirPath + "/temp/");
                Directory.CreateDirectory(dirPath + "/temp/BaseROM/");
                Directory.CreateDirectory(dirPath + "/temp/PortROM/");
                Directory.CreateDirectory(dirPath + "/temp/FilesToPort/");

                ZipActivity("extract", baseRomFile.FileName.ToString(), dirPath + "/temp/BaseROM");
                progressBar1.Value = 20;
                progressBar1.Refresh();
                ZipActivity("extract", portRomFile.FileName.ToString(), dirPath + "/temp/PortROM");
                progressBar1.Value = 40;
                progressBar1.Refresh();

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
            }
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

            ZipActivity("repack", dirPath + "/temp/PortROM", dirPath + "/portedROM.zip");
            
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
    }
}
