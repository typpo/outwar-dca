using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DCT.Outwar;
using DCT.Pathfinding;
using DCT.Protocols.Http;

namespace DCT.UI
{
    public partial class AccountsPanel : UserControl
    {
        internal ListView.ListViewItemCollection Accounts
        {
            get { return lvAccounts.Items; }
        }

        internal ListViewGroupCollection Groups
        {
            get { return lvAccounts.Groups; }
        }

        internal ListView.CheckedIndexCollection CheckedIndices
        {
            get { return lvAccounts.CheckedIndices; }
        }

        internal ListView.CheckedListViewItemCollection CheckedAccounts
        {
            get { return lvAccounts.CheckedItems; }
        }

        internal ListViewItem FocusedAccount
        {
            get { return lvAccounts.FocusedItem; }
        }

        private bool mEnabled;
        internal bool ChangeAllowed
        {
            get { return mEnabled; }
            set
            {
                mEnabled = value;
                txtUsername.Enabled = mEnabled;
                txtPassword.Enabled = mEnabled;
                lnkAccountsCheckAll.Enabled = mEnabled;
                lnkAccountsInvert.Enabled = mEnabled;
                lnkAccountsUncheckAll.Enabled = mEnabled;
            }
        }

        internal string Username
        {
            get { return txtUsername.Text; }
            set { txtUsername.Text = value; }
        }

        internal string Password
        {
            get { return txtPassword.Text; }
            set { txtPassword.Text = value; }
        }

        private AccountsEngine mEngine;
        internal AccountsEngine Engine
        {
            get { return mEngine; }
        }

        private CoreUI mUI;

        internal AccountsPanel(CoreUI ui)
        {
            mUI = ui;
            mEngine = new AccountsEngine();

            InitializeComponent();
        }

        private void lvAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            mUI.AccountsPanel.SyncMainConnection();
            mUI.UpdateDisplay();
        }

        internal void SyncMainConnection()
        {
            if (CheckedIndices.Count > 0)
            {
                mEngine.SetMain(CheckedIndices[0]);
            }
            else if (FocusedAccount != null)
            {
                mEngine.SetMain(FocusedAccount.Index);
            }
        }

        private void lnkAccountsCheckAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (ListViewItem item in lvAccounts.Items)
            {
                item.Checked = true;
            }
        }

        private void lnkAccountsUncheckAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (ListViewItem item in lvAccounts.Items)
            {
                item.Checked = false;
            }
        }

        private void lnkAccountsInvert_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (ListViewItem item in lvAccounts.Items)
            {
                item.Checked = !item.Checked;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && btnLogin.Enabled)
                Login();
        }

        private delegate void ButtonClickHandler(object sender, EventArgs e);
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new ButtonClickHandler(btnRefresh_Click), sender, e);
                return;
            }

            btnLogout_Click("refresh", null);
            btnLogin_Click(null, null);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new ButtonClickHandler(btnLogout_Click), sender, e);
                return;
            }

            mUI.LogPanel.Log("Logging out...");
            mUI.StopAttacking(false);
            HttpSocket.DefaultInstance.Get("http://outwar.com/index.php?cmd=logout");
            HttpSocket.DefaultInstance.Cookie = null;
            mEngine.Accounts.Clear();
            lvAccounts.Items.Clear();
            btnLogin.Enabled = true;
            btnLogout.Enabled = false;
            btnRefresh.Enabled = false;
        }

        private void Login()
        {
            if (Pathfinder.Rooms.Count == 0)
            {
                mUI.LogPanel.Log("E: You are either using an incorrect version of the program or the program was unable to reach the map server.  Make sure your antivirus, antispyware, firewall, router, etc. are not blocking the program's connection to the internet.");
                //return;
            }

            txtUsername.Enabled = false;
            txtPassword.Enabled = false;
            btnLogin.Enabled = false;
            btnLogout.Enabled = false;

            mUI.LogPanel.Log("Logging in...");

            login_normal.RunWorkerAsync();
        }

        private delegate void LoginCallbackHandler(int n);

        private void LoginCallback(int n)
        {
            if (InvokeRequired)
            {
                Invoke(new LoginCallbackHandler(LoginCallback), n);
                return;
            }

            if (n < 1)
            {
                txtUsername.Text = string.Empty;
                txtPassword.Text = string.Empty;
                btnLogin.Enabled = true;
                btnLogout.Enabled = false;
                btnRefresh.Enabled = false;
                mUI.LogPanel.Log("E: Outwar rejected your login.  Make sure you're putting in the correct Rampid Gaming Account information.");
            }
            else
            {
                for (int i = mEngine.Count - n; i < mEngine.Count; i++)
                {
                    Account a = mEngine.Accounts[i];
                    ListViewItem item = new ListViewItem(new string[] { a.Name, "Loaded", "-", "0", "0", "0" });
                    lvAccounts.Groups[Server.NameToId(a.Server) - 1].Items.Add(item);
                    lvAccounts.Items.Add(item);
                }
                mUI.LogPanel.Log("Loaded " + n + " characters.");
            }

            CoreUI.Instance.Settings.LastUsername = Username;
            CoreUI.Instance.Settings.LastPassword = Password;

            txtUsername.Enabled = true;
            txtPassword.Enabled = true;
            btnLogout.Enabled = true;
            btnRefresh.Enabled = true;
        }

        private delegate int LoginHandler();

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Number of accounts added</returns>
        private int DoLogin()
        {
            int orig = mEngine.Count;
            mEngine.Login(txtUsername.Text, txtPassword.Text);

            return mEngine.Count - orig;
        }

        private int DoLoginRgSessId(string sessid)
        {
            int orig = mEngine.Count;
            mEngine.Login(sessid);

            return mEngine.Count - orig;
        }

        internal void ShowRgSessIdDialog()
        {
            int i = 0;
            string input = DCT.Util.InputBox.Prompt("Session ID Input", "Enter your rg_sess_id instead of logging in.  This will allow you to run multiple DCAs on the same account:");
            if (string.IsNullOrEmpty(input))
                return;

            txtUsername.Enabled = false;
            txtPassword.Enabled = false;
            btnLogin.Enabled = false;
            btnLogout.Enabled = false;

            mUI.LogPanel.Log("Logging in with rg_sess_id...");

            login_rgsessid.RunWorkerAsync(input);
        }

        private void login_normal_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = DoLogin();
        }

        private void login_normal_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoginCallback((int)e.Result);
        }

        private void login_rgsessid_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = DoLoginRgSessId((string)e.Argument);
        }

        private void login_rgsessid_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoginCallback((int)e.Result);
        }
    }
}
