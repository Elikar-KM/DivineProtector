using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Net;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace DivineProtector
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }
        string time = DateTime.Now.ToString("h:mm:ss");

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                RegistryKey reg_key;
                reg_key = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\services\\usbvideo", true);
                reg_key.SetValue("Start", 4);
                listBox1.Items.Add(string.Format("{0} | {1}", time, "Webcam disabled"));
            }
            else
            {
                RegistryKey reg_key;
                reg_key = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\services\\usbvideo", true);
                reg_key.SetValue("Start", 3);
                listBox1.Items.Add(string.Format("{0} | {1}", time, "Webcam enabled"));
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                System.Net.WebClient wc = new System.Net.WebClient();
                string url = "https://api.proxyscrape.com?request=getproxies&proxytype=http&timeout=5000&country=US";
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                StreamWriter writer = new StreamWriter("proxy.txt", false);
                writer.Write(responseFromServer);
                writer.Close();
                string MYPROXY = File.ReadLines("proxy.txt").First();

                RegistryKey reg_key;
                reg_key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
                reg_key.SetValue("ProxyEnable", 1);
                reg_key.SetValue("ProxyServer", MYPROXY);
                listBox1.Items.Add(string.Format("{0} | {1}", time, "Proxy enabled"));
            }
            else
            {
                RegistryKey reg_key;
                reg_key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
                reg_key.SetValue("ProxyEnable", 0);
                listBox1.Items.Add(string.Format("{0} | {1}", time, "Proxy disabled"));
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in drives)
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        string itself = System.AppDomain.CurrentDomain.FriendlyName;
                        System.IO.File.Copy(itself, drive.Name + itself, true);
                        System.IO.File.SetAttributes(itself, FileAttributes.Normal);
                        listBox1.Items.Add(string.Format("{0} | {1}", time, "File copied"));
                    }
                }
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                RegistryKey reg_key;
                reg_key = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\lfsvc\\Service\\Configuration", true);
                reg_key.SetValue("Status", 0);
                listBox1.Items.Add(string.Format("{0} | {1}", time, "Location disabled"));
            }
            else
            {
                RegistryKey reg_key;
                reg_key = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\lfsvc\\Service\\Configuration", true);
                reg_key.SetValue("Status", 1);
                listBox1.Items.Add(string.Format("{0} | {1}", time, "Location enabled"));
            }
        }

            private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox10.Checked)
            {
                RegistryKey key;
                key = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\SystemRestore");
                key.SetValue("SystemRestorePointCreationFrequency", 0);
                key.Close();
                listBox1.Items.Add(string.Format("{0} | {1}", time, "System restore point created"));
            }
        }

    private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked)
            {
                OpenFileDialog choofdlog = new OpenFileDialog();
                choofdlog.Filter = "All Files (*.*)|*.*";
                choofdlog.FilterIndex = 1;
                choofdlog.Multiselect = true;

                if (choofdlog.ShowDialog() == DialogResult.OK)
                {
                    string damnfile = System.IO.Path.GetDirectoryName(choofdlog.FileName);
                    ProcessStartInfo startInfo = new ProcessStartInfo(@"C:\Program Files\Windows Defender\MpCmdRun.exe");
                    startInfo.WindowStyle = ProcessWindowStyle.Maximized;
                    startInfo.Arguments = "-Scan -ScanType 3 -File " + damnfile;
                    Process p = new Process();
                    p.StartInfo = startInfo;
                    p.Start();
                }
            }

        }
        
        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo("netstat");
                startInfo.WindowStyle = ProcessWindowStyle.Maximized;
                startInfo.Arguments = "-a";
                Process p = new Process();
                p.StartInfo = startInfo;
                p.Start();
            }
 
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                var dir = System.IO.Path.GetTempPath();
                foreach (var file in Directory.GetFiles(dir.ToString()))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception ex)
                    {
                        
                    }
                    
                }
                listBox1.Items.Add(string.Format("{0} | {1}", time, "Temp cleaned"));
            }

        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox11.Checked)
            {
                Microsoft.Win32.RegistryKey key;
                key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\CredUI");
                key.SetValue("DisablePasswordReveal", 1);
                key.Close();
                listBox1.Items.Add(string.Format("{0} | {1}", time, "Password reveal disabled"));
            }
            else
            {
                RegistryKey reg_key;
                reg_key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\CredUI", true);
                reg_key.SetValue("DisablePasswordReveal", 0);
                listBox1.Items.Add(string.Format("{0} | {1}", time, "Password reveal enabled"));
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                RegistryKey reg_key;
                reg_key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\CapabilityAccessManager\\ConsentStore\\microphone", true);
                reg_key.SetValue("Value", "Deny");
                listBox1.Items.Add(string.Format("{0} | {1}", time, "Microphone disabled"));
            }
            else
            {
                RegistryKey reg_key;
                reg_key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\CapabilityAccessManager\\ConsentStore\\microphone", true);
                reg_key.SetValue("Value", "Allow");
                listBox1.Items.Add(string.Format("{0} | {1}", time, "Microphone enabled"));
            }

        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox12.Checked)
            {
                RegistryKey reg_key;
                reg_key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows Defender", true);
                reg_key.SetValue("DisableAntiSpyware", 0);
                listBox1.Items.Add(string.Format("{0} | {1}", time, "Spyware disabled"));
            }
            else
            {
                RegistryKey reg_key;
                reg_key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows Defender", true);
                reg_key.SetValue("DisableAntiSpyware", 1);
                listBox1.Items.Add(string.Format("{0} | {1}", time, "Spyware enabled"));
            }
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked)
            {
                IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();

                IPEndPoint[] endPoints = ipProperties.GetActiveTcpListeners();
                TcpConnectionInformation[] tcpConnections = ipProperties.GetActiveTcpConnections();

                foreach (TcpConnectionInformation info in tcpConnections)
                {
                    listBox1.Items.Add("Local : " + info.LocalEndPoint.Address.ToString()
                    + ":" + info.LocalEndPoint.Port.ToString()
                    + " \nRemote : " + info.RemoteEndPoint.Address.ToString()
                    + ":" + info.RemoteEndPoint.Port.ToString()
                    + "\nState : " + info.State.ToString() + "\n\n");
                }
            }
           

        }


        private void button1_Click(object sender, EventArgs e)
        {
            string inout;
            if (button3.Text == "In")
            {
                inout = "in";
            }
            else
            {
                inout = "out";
            }
            string openport = textBox1.Text;
            Process.Start("netsh.exe", "advfirewall firewall add rule name=OpenPort dir=" + inout + " action=allow protocol=TCP localport=" + openport);
            listBox1.Items.Add(string.Format("{0} | {1}", time, "Port " + openport + " opened"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string closeport = textBox1.Text;
            string inout;
            if (button3.Text=="In")
            {
                inout = "in";
            }
            else
            {
                inout = "out";
            }
            Process.Start("netsh.exe", "advfirewall firewall add rule name=OpenPort protocol=TCP dir=" + inout + " remoteport=" + closeport + " action = block");
            listBox1.Items.Add(string.Format("{0} | {1}", time, "Port " + closeport + " blocked"));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text=="In")
            {
                button3.Text = "Out";
            }
            else
            {
                button3.Text = "In";
            }

        }
    }
}
