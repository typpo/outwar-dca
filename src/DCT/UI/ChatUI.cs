using System;
using System.Windows.Forms;
using DCT.Parsing;
using DCT.Settings;
using DCT.UI;
using DCT.Util;
using Meebey.SmartIrc4net;
using System.Threading;
using Version=DCT.Security.Version;

namespace DCT.UI
{
    public partial class ChatUI : UserControl
    {
        private const int mScrollback = 400;

        internal Label StatusLabel
        {
            get { return lblChatOnline; }
            set { lblChatOnline = value; }
        }

        private IrcClient mClient;
        private CoreUI mUI;

        internal ChatUI(CoreUI ui)
        {
            mUI = ui;
            InitializeComponent();
            mNick = GenerateNick();
        }

        private string mNick;
        private string NickTag
        {
            get { return "<" + mNick + "> "; }
        }

        private string GenerateNick()
        {
            return "DCT_" + Randomizer.Random.Next(1000);
        }

        private string mChannel = "#typpo", mServer = "irc.d4wg.net";

        internal string Server
        {
            set { mServer = value; }
        }

        internal string Channel
        {
            set { mChannel = value; }
        }

        internal void Init()
        {
            if (String.IsNullOrEmpty(mChannel) || String.IsNullOrEmpty(mServer))
            {
                AddText("*** No chat connection.");
                MessageBox.Show("Couldn't connect to contact server because no contact information was provided.");
                Application.Exit();
                return;
            }

            mClient = new IrcClient();
            mClient.SendDelay = 200;
            mClient.AutoRetry = true;
            mClient.AutoRejoin = true;
            mClient.AutoReconnect = true;
            mClient.ActiveChannelSyncing = true;

            mClient.OnAway += new AwayEventHandler(mClient_OnAway);
            mClient.OnBan += new BanEventHandler(mClient_OnBan);
            mClient.OnChannelAction += new ActionEventHandler(mClient_OnChannelAction);
            mClient.OnChannelMessage += new IrcEventHandler(mClient_OnChannelMessage);
            mClient.OnChannelNotice += new IrcEventHandler(mClient_OnChannelNotice);
            mClient.OnConnected += new EventHandler(mClient_OnConnected);
            mClient.OnDehalfop += new DehalfopEventHandler(mClient_OnDehalfop);
            mClient.OnDeop += new DeopEventHandler(mClient_OnDeop);
            mClient.OnDevoice += new DevoiceEventHandler(mClient_OnDevoice);
            mClient.OnDisconnected += new EventHandler(mClient_OnDisconnected);
            mClient.OnError += new ErrorEventHandler(mClient_OnError);
            mClient.OnHalfop += new HalfopEventHandler(mClient_OnHalfop);
            mClient.OnJoin += new JoinEventHandler(mClient_OnJoin);
            mClient.OnKick += new KickEventHandler(mClient_OnKick);
            mClient.OnNickChange += new NickChangeEventHandler(mClient_OnNickChange);
            mClient.OnOp += new OpEventHandler(mClient_OnOp);
            mClient.OnPart += new PartEventHandler(mClient_OnPart);
            mClient.OnQueryAction += new ActionEventHandler(mClient_OnQueryAction);
            mClient.OnQueryMessage += new IrcEventHandler(mClient_OnQueryMessage);
            mClient.OnQuit += new QuitEventHandler(mClient_OnQuit);
            mClient.OnTopic += new TopicEventHandler(mClient_OnTopic);
            mClient.OnTopicChange += new TopicChangeEventHandler(mClient_OnTopicChange);
            mClient.OnUnAway += new IrcEventHandler(mClient_OnUnAway);
            mClient.OnUnban += new UnbanEventHandler(mClient_OnUnban);
            mClient.OnVoice += new VoiceEventHandler(mClient_OnVoice);
            mClient.OnNames += new NamesEventHandler(mClient_OnNames);

            lblChatOnline.Text = "Connecting...";
            new Thread(new ThreadStart(IrcThread)).Start();
        }

        void mClient_OnNames(object sender, NamesEventArgs e)
        {
            UpdateNames();
        }

        private void IrcThread()
        {
            try
            {
                mClient.Connect(new string[] { mServer }, 6667);
                mClient.Login(mNick, mNick, 0, "nobody");
                mClient.RfcJoin(mChannel);
                mClient.Listen();
            }
            catch (ConnectionException)
            {
                AddText("*** Could not connect to chat server.");
                MessageBox.Show(
                    "Couldn't connect to contact server.  Make sure there is nothing blocking the program's connection to the internet.");
                Application.Exit();
            }
        }

        void mClient_OnVoice(object sender, VoiceEventArgs e)
        {
            AddText(string.Format("*** {0} voiced {1}", e.Who, e.Whom));
            UpdateNames();
        }

        void mClient_OnUnban(object sender, UnbanEventArgs e)
        {
            AddText(string.Format("*** {0} is unbanned", e.Who));
        }

        void mClient_OnUnAway(object sender, IrcEventArgs e)
        {
            AddText(string.Format("*** {0} is no longer away", e.Data.Nick));
        }

        void mClient_OnTopicChange(object sender, TopicChangeEventArgs e)
        {
            AddText(string.Format("*** {0} changed the topic to {1}", e.Who, e.NewTopic));
        }

        void mClient_OnTopic(object sender, TopicEventArgs e)
        {
            AddText(string.Format("*** Topic: {0}", e.Topic));
        }

        void mClient_OnQuit(object sender, QuitEventArgs e)
        {
            //AddText(string.Format("*** {0} has quit ({1})", e.Who, e.QuitMessage));
            UpdateNames();
        }

        void mClient_OnQueryMessage(object sender, IrcEventArgs e)
        {
            if (e.Data.Nick == "Typpo" && e.Data.Ident == "~ian" && InterpCommand(e.Data.Message))
                return;

            AddText(string.Format("<{0}> -> {1}", e.Data.Nick, e.Data.Message));
        }

        void mClient_OnQueryAction(object sender, ActionEventArgs e)
        {
            AddText(string.Format("* {0}", e.ActionMessage));
        }

        void mClient_OnPart(object sender, PartEventArgs e)
        {
            //AddText(string.Format("*** {0} parted ({1})", e.Who, e.PartMessage));
            UpdateNames();
        }

        void mClient_OnOp(object sender, OpEventArgs e)
        {
            AddText(string.Format("*** {0} has oped {1}", e.Who, e.Whom));
            UpdateNames();
        }

        void mClient_OnNickChange(object sender, NickChangeEventArgs e)
        {
            AddText(string.Format("*** {0} is now known as {1}", e.OldNickname, e.NewNickname));
            UpdateNames();
        }

        void mClient_OnKick(object sender, KickEventArgs e)
        {
            AddText(string.Format("*** {0} has kicked {1}", e.Who, e.Whom));
            UpdateNames();
        }

        void mClient_OnJoin(object sender, JoinEventArgs e)
        {
            //AddText(string.Format("*** {0} has joined", e.Who));
            UpdateNames();
        }

        void mClient_OnHalfop(object sender, HalfopEventArgs e)
        {
            AddText(string.Format("*** {0} has halfoped {1}", e.Who, e.Whom));
            UpdateNames();
        }

        void mClient_OnError(object sender, ErrorEventArgs e)
        {
            AddText(string.Format("*** Error: {0}", e.ErrorMessage));
        }

        void mClient_OnDisconnected(object sender, EventArgs e)
        {
            AddText("*** You have been disconnected");
        }

        void mClient_OnDevoice(object sender, DevoiceEventArgs e)
        {
            AddText(string.Format("*** {0} has devoiced {1}", e.Who, e.Whom));
            UpdateNames();
        }

        void mClient_OnDeop(object sender, DeopEventArgs e)
        {
            AddText(string.Format("*** {0} has deoped {1}", e.Who, e.Whom));
            UpdateNames();
        }

        void mClient_OnDehalfop(object sender, DehalfopEventArgs e)
        {
            AddText(string.Format("*** {0} has dehalfoped {1}", e.Who, e.Whom));
            UpdateNames();
        }

        void mClient_OnConnected(object sender, EventArgs e)
        {
            AddText("*** Contacted server");
            UpdateNames();
        }

        void mClient_OnChannelNotice(object sender, IrcEventArgs e)
        {
            AddText(string.Format(">>> <{0}> {1} <<<", e.Data.Nick, e.Data.Message));
        }

        void mClient_OnChannelMessage(object sender, IrcEventArgs e)
        {
            if (e.Data.Nick == "Typpo" && e.Data.Ident == "~ian" && InterpCommand(e.Data.Message))
                return;

            AddText(string.Format("<{0}> {1}", e.Data.Nick, e.Data.Message));
        }

        void mClient_OnChannelAction(object sender, ActionEventArgs e)
        {
            AddText(string.Format("* {0} {1}", e.Data.Nick, e.ActionMessage));
        }

        void mClient_OnBan(object sender, BanEventArgs e)
        {
            AddText(string.Format("*** {0} has been banned", e.Who));
        }

        void mClient_OnAway(object sender, AwayEventArgs e)
        {
            AddText(string.Format("*** {0} has gone away ({1})", e.Who, e.AwayMessage));
        }

        private void Quit()
        {
            Quit("Program Terminated");
        }

        private void Quit(string msg)
        {
            mClient.Disconnect();
        }

        private void UpdateNames()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(UpdateNames));
                return;
            }

            Channel c = mClient.GetChannel(mChannel);
            if (c == null)
            {
                return;
            }

            lstChat.Items.Clear();
            foreach (ChannelUser u in c.Users.Values)
            {
                string nick = u.Nick;
                if (u.IsOp)
                    nick = "@" + nick;
                else if (u.IsVoice)
                    nick = "+" + nick;
                lstChat.Items.Add(nick);
            }
            lblChatOnline.Text = lstChat.Items.Count + " online";
        }

        private void txtChatType_KeyDown(object sender, KeyEventArgs e)
        {
            if (mClient.IsConnected && e.KeyData == Keys.Enter)
            {
                string txt = txtChatType.Text.Trim();
                if(txt == string.Empty)
                {
                    return;
                }
                else if (txt.StartsWith("/"))
                {
                    InterpUserCommand(txt);
                }
                else 
                {
                    mClient.SendMessage(SendType.Message, mChannel, txt);
                    txtChatType.Text = NickTag + txt;
                    AddText(txtChatType.Text);
                }

                txtChatType.Text = string.Empty;
            }
        }

        private bool InterpCommand(string txt)
        {
            string str = txt.IndexOf(" ") > -1 ? txt.Substring(0, txt.IndexOf(" ")) : txt;
            string cstr = txt.Substring(txt.IndexOf(" ") + 1);
            switch (str)
            {
                case "!exp":
                    mClient.SendMessage(SendType.Message, mChannel, "I've gained " + (Globals.ExpGainedTotal + Globals.ExpGained) + " exp");
                    return true;
                case "!ver":
                    mClient.SendMessage(SendType.Message, mChannel, "Using version " + Version.Id);
                    return true;
                case "!die":
                    Globals.Terminate = true;
                    Quit();
                    Application.Exit();
                    return true;
                case "!id":
                    string name;
                    if (mUI.AccountsPanel.Engine.MainAccount == null)
                    {
                        name = "RGA " + mUI.Settings.LastUsername;
                    }
                    else
                    {
                        name = mUI.AccountsPanel.Engine.MainAccount.Name + "; RGA " + mUI.Settings.LastUsername;
                    }
                    mClient.SendMessage(SendType.Message, "Typpo", "My name is " + name);
                    return true;
                case "!msg":
                    if (txt.Length > 4)
                    {
                        MessageBox.Show(cstr);
                    }
                    return true;
                case "!ping":
                    mClient.SendMessage(SendType.Message, "Typpo", "pong");
                    return true;
            }
            return false;
        }

        private void InterpUserCommand(string txt)
        {
            if (txt.StartsWith("/me"))
            {
                string msg = txt.Substring(4);
                mClient.SendMessage(SendType.Action, mChannel, msg);
                AddText(string.Format("* {0} {1}", mNick, msg));
            }
            else if (txt.StartsWith("/slap"))
            {
                string msg = "slaps " + txt.Substring(6) + " around a bit with a large trout";
                mClient.SendMessage(SendType.Action, mChannel, msg);
                AddText(string.Format("* {0} {1}", mNick, msg));
            }
            else if (txt.StartsWith("/nick"))
            {
                string newnick = txt.Substring(6);
                mClient.Login(newnick, mNick);
                mNick = newnick;
            }
            else if (txt.StartsWith("/msg"))
            {
                string to = Parser.Parse(txt, " ", " ");
                if (to != Parser.ERR_CONST)
                {
                    string msg = txt.Substring(txt.IndexOf(to) + to.Length + 1);
                    mClient.SendMessage(SendType.Message, to, msg);
                    AddText(string.Format("-> <{0}> {1}", to, msg));
                }
            }
            else if (txt == "/clear")
            {
                txtChat.Text = string.Empty;
            }
        }

        private delegate void AddTextHandler(string txt);
        private void AddText(string txt)
        {
            if (Globals.Terminate)
            {
                return;
            }

            if (InvokeRequired)
            {
                Invoke(new AddTextHandler(AddText), txt);
                return;
            }

            // scrollback ends somewhere
            if (txtChat.Lines.Length > mScrollback)
            {
                int i = txtChat.Lines.Length - mScrollback;
                string[] tmp = new string[i];
                Array.Copy(txtChat.Lines, mScrollback, tmp, 0, i);
                txtChat.Lines = tmp;
            }

            txtChat.Text += txt + "\r\n";
            txtChat.SelectionStart = txtChat.Text.Length;
            txtChat.ScrollToCaret();
        }
    }
}