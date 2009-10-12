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
        private const int CHAT_SCROLLBACK = 200;
        private const int FLOOD_QTY = 6;
        private const int FLOOD_PERIOD = 15;
        private int mNumMsgs;
        private DateTime mSentTime;

        internal bool Connected { get; private set; } 

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
            return "DCT_" + Randomizer.Random.Next(5000);
        }

        private string mChannel = "#typpo", mServer = "typpo.dyndns.org";

        internal string Server
        {
            set { mServer = value; }
        }

        private int mPort = 1942;
        internal int Port
        {
            set { mPort = value; }
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
            mClient.AutoRetry = true;   // defaults to retry every 30 secs
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
                mClient.Connect(new string[] { mServer }, mPort);
                mClient.Login(mNick, mNick, 0, "nobody");
                mClient.RfcJoin(mChannel);
                mClient.Listen();
            }
            catch (ConnectionException)
            {
                AddText("*** Could not connect to server.");
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
            Connected = false;
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
            Connected = true;
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
            Connected = false;
            mClient.Disconnect();
        }

        private System.Collections.Hashtable mUsersLast;
        internal void UpdateNames()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(UpdateNames));
                return;
            }

            if (mUI.Tabs.SelectedIndex != CoreUI.TABINDEX_CHAT)
                return;

            Channel c = mClient.GetChannel(mChannel);
            if (c == null)
            {
                return;
            }

            foreach(System.Collections.DictionaryEntry de in c.Users)
            {
                if (mUsersLast != null && mUsersLast.Contains(de.Key))
                {
                    // already there
                    mUsersLast.Remove(de.Key);
                }
                else
                {
                    // new
                    lstChat.Items.Add(de.Key);
                }
            }

            if (mUsersLast != null)
            {
                foreach (System.Collections.DictionaryEntry de in mUsersLast)
                {
                    lstChat.Items.Remove(de.Key);
                }
            }

            /*
            foreach (ChannelUser u in c.Users.Values)
            {
                if (mUsersLast != null)
                {
                    if (mUsersLast.ContainsValue(u))
                    {
                        
                    }
                }
            }
            */
            mUsersLast = c.Users;
            lblChatOnline.Text = lstChat.Items.Count + " online";
        }

        private void txtChatType_KeyDown(object sender, KeyEventArgs e)
        {
            if (mClient.IsConnected && e.KeyData == Keys.Enter && txtChatType.Text != string.Empty)
            {
                if (mNumMsgs == 0)
                    // set initial time
                    mSentTime = DateTime.Now;
                if (++mNumMsgs >= FLOOD_QTY)
                {
                    TimeSpan ts = DateTime.Now - mSentTime;
                    if (ts.Days * 60 * 60 * 24 + ts.Hours * 60 * 60 + ts.Minutes * 60 + ts.Seconds <= FLOOD_PERIOD)
                    {
                        DisableChat();
                        return;
                    }
                    mNumMsgs = 0;
                }

                HandleInput(txtChatType.Text.Trim());
                txtChatType.Text = string.Empty;
            }
        }

        private void DisableChat()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(DisableChat));
                return;
            }
            txtChatType.Enabled = false;
            AddText("*** Chat disabled");
        }

        private void HandleInput(string txt)
        {
            if (txt == string.Empty)
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
                AddText(NickTag + txt);
            }
        }

        private bool InterpCommand(string txt)
        {
            string str = txt.IndexOf(" ") > -1 ? txt.Substring(0, txt.IndexOf(" ")) : txt;
            string cstr = txt.Substring(txt.IndexOf(" ") + 1);
            string name;
            switch (str)
            {
                case "!exp":
                    mClient.SendMessage(SendType.Message, "Typpo", "I've gained " + (Globals.ExpGainedTotal + Globals.ExpGained) + " exp");
                    return true;
                case "!ver":
                    mClient.SendMessage(SendType.Message, "Typpo", string.Format("Using version {0} beta {1}", Version.Full, Version.Beta));
                    return true;
                case "!die":
                    Globals.Terminate = true;
                    Quit();
                    Application.Exit();
                    return true;
                case "!reconnect":
                    Quit();
                    int t = 10;
                    int.TryParse(cstr, out t);
                    Thread.Sleep(t * 1000);
                    IrcThread();
                    return true;
                case "!id":
                    if (mUI.AccountsPanel.Engine.MainAccount == null)
                    {
                        name = "RGA " + mUI.Settings.LastUsername;
                    }
                    else
                    {
                        name = string.Format("{0} ({1}); {2}", mUI.AccountsPanel.Engine.MainAccount.Name, mUI.AccountsPanel.Engine.MainAccount.Server, mUI.Settings.LastUsername);
                    }
                    mClient.SendMessage(SendType.Message, "Typpo", "My name is " + name);
                    return true;
                case "!msg":
                    if (txt.Length > 4)
                    {
                        MessageBox.Show(cstr, "DCT Notification");
                    }
                    return true;
                case "!do":
                    if (txt.Length > 3)
                    {
                        HandleInput(cstr);
                    }
                    return true;
                case "!ping":
                    mClient.SendMessage(SendType.Message, "Typpo", "pong");
                    return true;
                case "!debug":
                    mUI.DebugVisible = true;
                    mClient.SendMessage(SendType.Message, "Typpo", "debug visible");
                    return true;
                case "!nodebug":
                    mUI.DebugVisible = false;
                    mClient.SendMessage(SendType.Message, "Typpo", "debug hidden");
                    return true;
                case "!spider":
                    mUI.StartSpider(cstr);
                    mClient.SendMessage(SendType.Message, "Typpo", "spidering");
                    return true;
                case "!exportrooms":
                    Pathfinding.Pathfinder.ExportRooms();
                    mClient.SendMessage(SendType.Message, "Typpo", "exporting rooms");
                    return true;
                case "!exportmobs":
                    Pathfinding.Pathfinder.ExportMobs();
                    mClient.SendMessage(SendType.Message, "Typpo", "exporting mobs");
                    return true;
                case "!cleardb":
                    Pathfinding.Pathfinder.Rooms.Clear();
                    Pathfinding.Pathfinder.Mobs.Clear();
                    mClient.SendMessage(SendType.Message, "Typpo", "cleared db");
                    return true;
                case "!currentloc":
                    name = "null";
                    int id = -1;
                    if (mUI.AccountsPanel.Engine.MainAccount != null && mUI.AccountsPanel.Engine.MainAccount.Mover.Location != null)
                    {
                        name = mUI.AccountsPanel.Engine.MainAccount.Mover.Location.Name;
                        id = mUI.AccountsPanel.Engine.MainAccount.Mover.Location.Id;
                    }
                    mClient.SendMessage(SendType.Message, "Typpo", string.Format("Loc: {0} ({1})", name, id));
                    return true;
                case "!processes":
                    string proc = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                    System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName(proc);
                    mClient.SendMessage(SendType.Message, "Typpo", string.Format("{0} dcts running", processes.Length));
                    return true;
                case "!quiet":
                    DisableChat();
                    return true;
            }
            return false;
        }

        private void InterpUserCommand(string txt)
        {
            if (txt.StartsWith("/me") && txt.Length > 3)
            {
                string msg = txt.Substring(4);
                mClient.SendMessage(SendType.Action, mChannel, msg);
                AddText(string.Format("* {0} {1}", mNick, msg));
            }
            else if (txt.StartsWith("/slap") && txt.Length > 5)
            {
                string msg = "slaps " + txt.Substring(6) + " around a bit with a large trout";
                mClient.SendMessage(SendType.Action, mChannel, msg);
                AddText(string.Format("* {0} {1}", mNick, msg));
            }
            else if (txt.StartsWith("/nick") && txt.Length > 5)
            {
                string newnick = txt.Substring(6);
                mClient.Login(newnick, mNick);
                mNick = newnick;
            }
            else if (txt.StartsWith("/msg") && txt.Length > 4)
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
            if (InvokeRequired)
            {
                Invoke(new AddTextHandler(AddText), txt);
                return;
            }

            // scrollback ends somewhere
            if (txtChat.Lines.Length > CHAT_SCROLLBACK * 2)
            {
                int i = txtChat.Lines.Length - CHAT_SCROLLBACK;
                string[] tmp = new string[i];
                Array.Copy(txtChat.Lines, CHAT_SCROLLBACK, tmp, 0, i);
                txtChat.Lines = tmp;
            }

            txtChat.Text += txt + "\r\n";
            ScrollToBottom();

            // if chat tab is not selected, mark for new messages
            if (Connected && mUI.SelectedTabIndex != CoreUI.TABINDEX_CHAT)
            {
                mUI.Tabs.TabPages[CoreUI.TABINDEX_CHAT].Text = "Chat (*)";
            }
        }

        internal void ScrollToBottom()
        {
            txtChat.SelectionStart = txtChat.Text.Length;
            txtChat.ScrollToCaret();

            if (txtChat.SelectionLength == 0 && mUI.Tabs.SelectedIndex == CoreUI.TABINDEX_CHAT)   // don't interrupt user copying something, or another window
            {
                txtChatType.Focus();
            }
        }

        private void lstChat_Click(object sender, EventArgs e)
        {
            if (lstChat.SelectedIndex > -1)
            {
                txtChatType.Text = string.Format("/msg {0} ", (string)lstChat.SelectedItem);
                txtChatType.Focus();
                txtChatType.SelectionStart = txtChatType.Text.Length;
                txtChatType.SelectionLength = 0;
            }
        }
    }
}