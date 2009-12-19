using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;
using DCT.Parsing;
using DCT.Pathfinding;
using DCT.Protocols.Http;
using DCT.Settings;
using DCT.Threading;
using Version=DCT.Security.Version;

namespace DCT.UI
{
    internal partial class StartDialog : Form
    {
        internal StartDialog()
        {
            InitializeComponent();

            lnkGo.Enabled = false;
        }

        private void frmStart_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("You are using v[{0}{1}] of Typpo's DC Tool - www.typpo.us", Version.Full, Version.Beta);

            Run();

            txtMain.SelectionStart = 0;
            txtMain.SelectionLength = 0;
            lnkGo.Enabled = true;
        }

        private void Run()
        {
            SetStatus("Loading open message...");
            string src =
                HttpSocket.DefaultInstance.Get("http://www.typpo.us/dctopen.txt")
                    .Replace("\n", "\r\n");

            txtMain.Text = src;

            Parser p = new Parser(src);
            CoreUI.Instance.ChatPanel.Channel = p.Parse("<chan>", "</chan>");
            CoreUI.Instance.ChatPanel.Server = p.Parse("<svr>", "</svr>");
            int tmp = 6667;
            if (int.TryParse(p.Parse("<port>", "</port>"), out tmp))
                CoreUI.Instance.ChatPanel.Port = tmp;
            CoreUI.Instance.Changes = p.Parse("Change History:", "End Changes").Replace("\r", "").Trim();

            if (src.Contains("<msg>"))
            {
                MessageBox.Show(p.Parse("<msg>", "</msg>"), "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Focus();
                txtMain.SelectionLength = 0;
            }

            if (Version.Full != p.Parse("<ver>", "</ver>"))
            {
                string url = p.Parse("<url>", "</url>");

                if (url == "ERROR")
                {
                    SetStatus("Could not access server.");
                    txtMain.Text = " Could not access startup server.  If you already had map data saved on your computer, the program should work but you will not receive software or map updates automatically.";
                    MessageBox.Show(
                        "Could not read startup instructions from server.  If map data has already been saved to your computer, the program should work.\n\nIf this error persists (and you can get to www.typpo.us), please close or adjust any firewall/router/antivirus/antispyware that is blocking this program's connection to the internet.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SetStatus("Attempting to build map data...");
                    ThreadEngine.DefaultInstance.DoParameterized(Pathfinder.BuildMap, false);
                    SetStatus("Could not contact server");
                    //Globals.Terminate = true;
                    //Application.Exit();
                    return;
                }

                SetStatus("Downloading new version...");
                try
                {
                    string local = url.Substring(url.LastIndexOf("/") + 1);
                    if (File.Exists(local))
                    {
                        SetStatus("You've already downloaded the new version, use it instead: " + local);
                    }
                    else
                    {
                        new WebClient().DownloadFile(new Uri(url), local);
                    }

                    Process.Start(local);
                    Globals.Terminate = true;
                    Application.Exit();
                    return;
                }
                catch
                {
                    MessageBox.Show("Automatic updating failed.\n\n"
                                    +
                                    "You will be directed to a manual download.  Place the file in "
                                    + Application.StartupPath,
                                    "Updating Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Process.Start(url);
                    Globals.Terminate = true;
                    Application.Exit();
                    return;
                }
            }

            string mapupdate = p.Parse("<map>", "</map>");
            if (mapupdate != "ERROR")
            {
                DateTime last = DateTime.ParseExact(mapupdate, "yyyy-MM-dd HH:mm", null);
                if (last > CoreUI.Instance.Settings.LastMapUpdate)
                {
                    // new maps
                    SetStatus("Building latest DC maps from host site...");
                    ThreadEngine.DefaultInstance.DoParameterized(Pathfinder.BuildMap, true);
                    SetStatus("Ready with latest maps...");
                }
                else
                {
                    ThreadEngine.DefaultInstance.DoParameterized(Pathfinder.BuildMap, false);
                    SetStatus("Ready...");
                }
            }
            else
            {
                ThreadEngine.DefaultInstance.DoParameterized(Pathfinder.BuildMap, false);
                SetStatus("Could not determine new map status");
            }
        }

        private void SetStatus(string txt)
        {
            lblMain.Text = txt;
        }

        private void lnkGo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }
    }
}