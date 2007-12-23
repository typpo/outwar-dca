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
            if (HttpSocket.IP.Contains("24.128."))
            {
                MessageBox.Show("How's your day at work been so far, Mr. Rampid man?");
                Globals.Terminate = true;
                Application.Exit();
                return;
            }

            this.Text = "You are using v[" + Version.Full + "] of Typpo's DC Tool - www.typpo.us";

            Run();

            txtMain.SelectionStart = 0;
            txtMain.SelectionLength = 0;
            lnkGo.Enabled = true;
        }

        private void Run()
        {
            SetStatus("Loading open message...");
            string src =
                HttpSocket.DefaultInstance.Get("http://typpo.dyndns.org:7012/dct/open.txt")
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
                    MessageBox.Show(
                        "Could not read startup instructions from server, try again.\n\nIf this error persists (and you can get to www.typpo.us), please close or adjust any firewall/router/antivirus/antispyware that is blocking this program's connection to the internet.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Globals.Terminate = true;
                    Application.Exit();
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

            SetStatus("Building latest DC maps from host site...");
            ThreadEngine.DefaultInstance.Do(Pathfinder.BuildMap);

            SetStatus("Ready...");
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