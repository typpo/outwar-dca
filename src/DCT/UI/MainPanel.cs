using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DCT.Outwar.World;
using DCT.Util;
using DCT.Settings;

namespace DCT.UI
{
    public partial class MainPanel : UserControl
    {
        internal string StatusText
        {
            get { return lblExpRage.Text; }
            set
            {
                lblExpRage.Text = value;
                lblExpRage.Left = ((pnlAttack.Right - pnlAttack.Left) / 2) - (lblExpRage.Width / 2);
            }
        }

        internal bool AttackingOn
        {
            get { return btnStart.Enabled; }
            set
            {
                btnStart.Enabled = !value;
                btnStop.Enabled = value;
                btnStartTimer.Enabled = !value;
            }
        }

        internal bool UseCountdown
        {
            get { return chkCountdownTimer.Checked; }
            set { chkCountdownTimer.Checked = value; }
        }

        internal bool UseHourTimer
        {
            get { return chkHourTimer.Checked; }
            set { chkHourTimer.Checked = value; }
        }

        internal int CountdownValue
        {
            get { return (int)numCountdown.Value; }
            set { numCountdown.Value = value; }
        }

        internal CountDownTimer CountdownTimer
        {
            get { return mCountdownTimer; }
        }

        private CountDownTimer mCountdownTimer;
        private AttackingType mCountdownType;

        private CoreUI mUI;

        internal MainPanel(CoreUI ui)
        {
            mUI = ui;
            InitializeComponent();
        }

        /// <summary>
        /// Sets option buttons as per AttackMode setting
        /// </summary>
        internal void SyncAttackMode()
        {
            switch (mUI.Settings.AttackMode)
            {
                case 0: optCountdownSingle.Checked = true;
                    break;
                case 1: optCountdownMulti.Checked = true;
                    break;
                case 2: optCountdownMobs.Checked = true;
                    break;
                default: throw new Exception("Your settings are corrupt; no such attack mode.");
            }
        }

        private void optCountdownSingle_CheckedChanged(object sender, EventArgs e)
        {
            if (optCountdownSingle.Checked)
            {
                mUI.Settings.AttackMode = 0;
            }
        }

        private void optCountdownMulti_CheckedChanged(object sender, EventArgs e)
        {
            if (optCountdownMulti.Checked)
            {
                mUI.Settings.AttackMode = 1;
            }
        }

        private void optCountdownMobs_CheckedChanged(object sender, EventArgs e)
        {
            if (optCountdownMobs.Checked)
            {
                mUI.Settings.AttackMode = 2;
            }
        }

        private void btnStartTimer_Click(object sender, EventArgs e)
        {
            if (mUI.AccountsPanel.Accounts.Count < 1)
            {
                MessageBox.Show("You need to login before setting a timer.", "Start Timer", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            else if (!mUI.Settings.UseCountdownTimer && !mUI.Settings.UseHourTimer)
            {
                MessageBox.Show("Choose a timer.", "Start Timer", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            switch (mCountdownType)
            {
                case AttackingType.Single:
                Countdown(AttackingType.Single);
                    break;
                case AttackingType.Multi:
                Countdown(AttackingType.Multi);
                    break;
                case AttackingType.Mobs:
                Countdown(AttackingType.Mobs);
                    break;
            }
        }

        private delegate void CountdownHandler(AttackingType type);

        internal void Countdown(AttackingType type)
        {
            // pass to UI thread
            if (InvokeRequired)
            {
                Invoke(new CountdownHandler(Countdown), type);
                return;
            }

            int countFor;
            if (mUI.Settings.UseCountdownTimer)
            {
                countFor = ((int)numCountdown.Value) * 60;
            }
            else
            {
                countFor = SecondsUntilHour();
            }

            mCountdownTimer = new CountDownTimer(countFor);

            mCountdownTimer.Interval = 1000;
            mCountdownTimer.Tick += new EventHandler(t_Tick);
            mCountdownTimer.Started += new EventHandler(t_Started);
            mCountdownTimer.Stopped += new EventHandler(t_Stopped);
            mCountdownType = type;

            mCountdownTimer.Start();

            if (mUI.Settings.ClearLogs)
                mUI.LogPanel.ClearMost();
        }

        private int SecondsUntilHour()
        {
            return (61 - DateTime.Now.Minute) * 60;
        }

        private void t_Stopped(object sender, EventArgs e)
        {
            if (Globals.AttackMode || !(mUI.Settings.UseCountdownTimer || mUI.Settings.UseHourTimer))
            {
                return;
            }

            mUI.Toggle(false);
            mUI.ToggleAttack(true);

            switch (mCountdownType)
            {
                case AttackingType.Single:
                    mUI.AttackArea();
                    break;
                case AttackingType.Multi:
                    mUI.AttackAreas();
                    break;
                case AttackingType.Mobs:
                    mUI.AttackMobs();
                    break;
            }
        }

        private void t_Started(object sender, EventArgs e)
        {
            if (mUI.ChatPanel.StatusLabel.Text.StartsWith("Not"))
            {
                mUI.LogPanel.Log("E: Not connected to authorization server.");
                Application.Exit();
                return;
            }

            UpdateCountdown();
        }

        private void t_Tick(object sender, EventArgs e)
        {
            UpdateCountdown();
        }

        private void UpdateCountdown()
        {
            int s = mCountdownTimer.CurrentCountdown;
            string s2 = (s % 60).ToString();
            lblTimeLeft.Text = "Time left: " + (s / 60) + ":" + (s2.Length == 1 ? "0" + s2 : s2);

            if (!mUI.Settings.UseCountdownTimer && !mUI.Settings.UseHourTimer)
            {
                mCountdownTimer.Stop();
            }
        }

        // TODO switch to only one global
        private void chkCountdownTimer_CheckedChanged(object sender, EventArgs e)
        {
            bool b = chkCountdownTimer.Checked;
            if (b)
            {
                if (mCountdownTimer != null)
                {
                    mCountdownTimer.CurrentCountdown = ((int)numCountdown.Value) * 60;
                }
                chkHourTimer.Checked = mUI.Settings.UseHourTimer = !b;

            }
            mUI.Settings.UseCountdownTimer = b;
        }

        private void chkHourTimer_CheckedChanged(object sender, EventArgs e)
        {
            bool b = chkHourTimer.Checked;
            if (b)
            {
                if (mCountdownTimer != null)
                {
                    mCountdownTimer.CurrentCountdown = SecondsUntilHour();
                }
                chkCountdownTimer.Checked = mUI.Settings.UseCountdownTimer = !b;

            }
            mUI.Settings.UseHourTimer = b;
        }

        private void numCountdown_ValueChanged(object sender, EventArgs e)
        {
            mUI.Settings.CycleInterval = (int)numCountdown.Value;
            if (mCountdownTimer != null && mUI.Settings.UseCountdownTimer)
            {
                mCountdownTimer.CurrentCountdown = ((int)numCountdown.Value) * 60;
            }
        }

        private void btnAttackStart_Click(object sender, EventArgs e)
        {
            switch (mUI.Settings.AttackMode)
            {
                case 0: mUI.AttackArea(); break;
                case 1: mUI.AttackAreas(); break;
                default: mUI.AttackMobs(); break;
            }
        }

        private void btnAttackStop_Click(object sender, EventArgs e)
        {
            StopAttacking(true);
        }


        private delegate void StopAttackingHandler(bool timeroff);

        internal void StopAttacking(bool timeroff)
        {
            if (InvokeRequired)
            {
                Invoke(new StopAttackingHandler(StopAttacking), timeroff);
                return;
            }

            if (Globals.AttackOn || Globals.AttackMode)
            {
                Globals.AttackOn = false;
                mUI.ToggleAttack(false);

                if (timeroff)
                {
                    chkCountdownTimer.Checked = false;
                    chkHourTimer.Checked = false;
                }
            }
        }
    }
}
