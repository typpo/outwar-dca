using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DCT.Outwar;

namespace DCT.UI.Questing
{
    public partial class MobTalkControl : UserControl
    {
        private bool loggedIn;
        private bool failed;

        private Account mAccount;
        private long mMobId;

        internal MobTalkControl()
        {
            loggedIn = false;
            failed = false;
            InitializeComponent();
        }

        internal void LoadTalk(Account a, long mobid, string sessid)
        {
            mAccount = a;
            mMobId = mobid;

            wb.Navigate(string.Format("http://{0}.outwar.com/?rg_sess_id={1}&serverid={2}&suid={3}",
                  a.Server, sessid, Server.NameToId(a.Server), a.Id));
        }

        private void wb_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (failed)
                return;

            if (!loggedIn)
            {
                loggedIn = true;
                // successfully passed rg_sessid
                if (!wb.Document.Title.Contains("Outwar.com Account Management"))
                {
                    wb.Document.Body.InnerHtml = "Sorry, talk failed.";
                    failed = true;
                    return;
                }

                // load talk page
                string url = "http://" + mAccount.Server + ".outwar.com/mob_talk.php?id=" + mMobId + "#masterdiv";
                wb.Navigate(url);
            }

            // else, talk page probably loaded
        }
    }
}
