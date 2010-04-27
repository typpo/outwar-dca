using System;
using System.ComponentModel;
using System.Windows.Forms;
using DCT.Outwar;
using DCT.Pathfinding;
using DCT.Protocols.Http;
using DCT.Util;

namespace DCT.UI
{
    public partial class AccountsPanel : UserControl
    {
        private readonly AccountsEngine mEngine;
        private readonly CoreUI mUI;
        private bool mEnabled;

        private int[] mSavedChecks; // indices of checked items
        private bool mRestoreChecks;

        internal AccountsPanel(CoreUI ui)
        {
            mUI = ui;
            mEngine = new AccountsEngine();

            InitializeComponent();
        }

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

        internal bool ChangeAllowed
        {
            get { return mEnabled; }
            set
            {
                mEnabled = value;
                txtUsername.Enabled = mEnabled;
                txtPassword.Enabled = mEnabled;
                lnkAccountsCheckAll.Enabled = mEnabled;
                lnkChkServer.Enabled = mEnabled;
                lnkAccountsUncheckAll.Enabled = mEnabled;
                btnLogin.Enabled = mEnabled;
                btnLogout.Enabled = mEnabled;
                btnRefresh.Enabled = mEnabled;
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

        internal AccountsEngine Engine
        {
            get { return mEngine; }
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

        private void lnkChkServer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string input = InputBox.Prompt("Check Server", "Enter server name:");
            if (string.IsNullOrEmpty(input))
                return;
            input = char.ToUpper(input[0]) + input.Substring(1).ToLower();
            int i = Server.NamesList.IndexOf(input);
            if (i < 0)
            {
                MessageBox.Show("Could not match server, check spelling.", "Check Server", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }
            foreach (ListViewItem item in lvAccounts.Groups[i].Items)
                item.Checked = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            mRestoreChecks = false;
            Login();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && btnLogin.Enabled)
                Login();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new ButtonClickHandler(btnRefresh_Click), sender, e);
                return;
            }

            // save checks
            mSavedChecks = new int[lvAccounts.CheckedIndices.Count];

            for(int i=0; i < lvAccounts.CheckedIndices.Count; i++)
            {
                mSavedChecks[i] = lvAccounts.CheckedIndices[i];
            }

            mRestoreChecks = true;
            // do refresh
            Logout();
            Login();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new ButtonClickHandler(btnLogout_Click), sender, e);
                return;
            }

            Logout();
        }

        private void Logout()
        {
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
                mUI.LogPanel.Log(
                    "E: You are either using an incorrect version of the program or the program was unable to reach the map server.  Make sure your antivirus, antispyware, firewall, router, etc. are not blocking the program's connection to the internet.");
                //return;
            }

            txtUsername.Enabled = false;
            txtPassword.Enabled = false;
            btnLogin.Enabled = false;
            btnLogout.Enabled = false;
            btnRefresh.Enabled = false;

            mUI.LogPanel.Log("Logging in...");

            if (!login_normal.IsBusy)
                login_normal.RunWorkerAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n">Number of accounts added</param>
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
                mUI.LogPanel.Log(
                    "E: Outwar rejected your login.  Make sure you're putting in the correct Rampid Gaming Account information.");
            }
            else
            {
                for (int i = mEngine.Count - n; i < mEngine.Count; i++)
                {
                    Account a = mEngine.Accounts[i];
                    var item = new ListViewItem(new[] {a.Name, "Loaded", "-", "0", "0", "0"});
                    lvAccounts.Groups[Server.NameToId(a.Server) - 1].Items.Add(item);
                    lvAccounts.Items.Add(item);
                }
                if(mRestoreChecks)
                {
                    foreach(int i in mSavedChecks)
                    {
                        if (i < lvAccounts.Items.Count)
                            lvAccounts.Items[i].Checked = true;
                    }
                }
                mUI.LogPanel.Log("Loaded " + n + " characters.");
            }

            mUI.Settings.LastUsername = Username;
            mUI.Settings.LastPassword = Password;

            txtUsername.Enabled = true;
            txtPassword.Enabled = true;
            btnLogout.Enabled = true;
            btnRefresh.Enabled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Number of accounts added</returns>
        private int DoLogin()
        {
            return mEngine.Login(Server.IdToString(mUI.Settings.Server), txtUsername.Text, txtPassword.Text);
        }

        private int DoLoginRgSessId(string sessid)
        {
            return mEngine.Login(Server.IdToString(mUI.Settings.Server), sessid);
        }

        internal void ShowRgSessIdDialog()
        {
            string input = InputBox.Prompt("Session ID Input",
                                           "Enter your rg_sess_id instead of logging in.  This will allow you to run multiple DCAs on the same account:");
            if (string.IsNullOrEmpty(input))
                return;

            txtUsername.Enabled = false;
            txtPassword.Enabled = false;
            btnLogin.Enabled = false;
            btnLogout.Enabled = false;

            mUI.LogPanel.Log("Logging in with rg_sess_id...");

            if (!login_rgsessid.IsBusy)
                login_rgsessid.RunWorkerAsync(input);
        }

        private void login_normal_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = DoLogin();
        }

        private void login_normal_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoginCallback((int) e.Result);
        }

        private void login_rgsessid_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = DoLoginRgSessId((string) e.Argument);
        }

        private void login_rgsessid_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoginCallback((int) e.Result);
        }

        #region Nested type: ButtonClickHandler

        private delegate void ButtonClickHandler(object sender, EventArgs e);

        #endregion

        #region Nested type: LoginCallbackHandler

        private delegate void LoginCallbackHandler(int n);

        #endregion
    }
}