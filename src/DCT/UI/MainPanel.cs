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
        private const int STOPAFTERTIME_OFFSET = 3;    // in seconds

        internal int StopAfterCounter { get; set; }

        internal void ResetStopAfterCounter()
        {
            StopAfterCounter = 0;
        }

        internal bool StopAfterCounterFinished
        {
            get { return StopAfterCounter >= StopAfterVal; }
        }

        internal void ResetStopAfterTime()
        {
            StopAfterTime = DateTime.Now;
        }

        internal DateTime StopAfterTime { get; set; }

        internal bool StopAfterTimeFinished
        {
            get
            {
                TimeSpan ts = DateTime.Now - StopAfterTime.Add(new TimeSpan(0,0,STOPAFTERTIME_OFFSET));
                int minutes = ts.Days * 24 * 60 + ts.Hours * 60 + ts.Minutes;
                return minutes >= StopAfterVal;
            }
        }

        internal bool StopAfter
        {
            get { return chkStopAfter.Enabled; }
            set { chkStopAfter.Checked = value; }
        }

        internal int StopAfterVal
        {
            get { return (int)numStopAfter.Value; }
            set { numStopAfter.Value = value; }
        }

        internal UserEditable.StopAfterType StopAfterMode
        {

            // TODO should be dynamic
            get
            {
                switch (cmbStopAfter.Text)
                {
                    case "runs":
                        return UserEditable.StopAfterType.Runs;
                    case "minutes":
                        return UserEditable.StopAfterType.Minutes;
                }
                mUI.LogPanel.Log("E: No such StopAfterMode, defaulted to Runs");
                return UserEditable.StopAfterType.Runs;
            }
            set
            {
                switch (value)
                {
                    case UserEditable.StopAfterType.Minutes:
                        cmbStopAfter.Text = "minutes";
                        break;
                    case UserEditable.StopAfterType.Runs:
                        cmbStopAfter.Text = "runs";
                        break;
                }
            }
        }

        internal bool RunCountdown
        {
            get { return chkCountdownTimer.Checked || chkHourTimer.Checked; }
        }

        internal bool UseCountdownTimer
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

        internal string StatusText
        {
            get { return lblExpRage.Text; }
            set
            {
                lblExpRage.Text = value;
                lblExpRage.Left = ((pnlAttack.Right - pnlAttack.Left) / 2) - (lblExpRage.Width / 2);
            }
        }

        private CoreUI mUI;

        internal MainPanel(CoreUI ui)
        {
            mUI = ui;
            InitializeComponent();
        }

        // TODO switch to only one global
        private void chkCountdownTimer_CheckedChanged(object sender, EventArgs e)
        {
            bool b = chkCountdownTimer.Checked;
            if (b)
            {
                if (mUI.CountdownTimer != null)
                {
                    mUI.CountdownTimer.CurrentCountdown = CountdownValue * 60;
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
                if (mUI.CountdownTimer != null)
                {
                    mUI.CountdownTimer.CurrentCountdown = mUI.SecondsUntilHour();
                }
                chkCountdownTimer.Checked = mUI.Settings.UseCountdownTimer = !b;

            }
            mUI.Settings.UseHourTimer = b;
        }

        private void numCountdown_ValueChanged(object sender, EventArgs e)
        {
            mUI.Settings.CycleInterval = (int)numCountdown.Value;
            if (mUI.CountdownTimer != null && mUI.Settings.UseCountdownTimer)
            {
                mUI.CountdownTimer.CurrentCountdown = ((int)numCountdown.Value) * 60;
            }
        }

        private void chkStopAfter_CheckedChanged(object sender, EventArgs e)
        {
            mUI.Settings.StopAfter = numStopAfter.Enabled = cmbStopAfter.Enabled = chkStopAfter.Checked;
            if (mUI.Settings.StopAfter)
            {
                mUI.Settings.StopAfterMode = StopAfterMode;
            }
            mUI.Settings.StopAfterVal = (int)numStopAfter.Value;
        }

        private void cmbStopAfter_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbStopAfter.SelectedItem.ToString())
            {
                case "runs":
                    mUI.Settings.StopAfterMode = UserEditable.StopAfterType.Runs;
                    break;
                case "minutes":
                    mUI.Settings.StopAfterMode = UserEditable.StopAfterType.Minutes;
                    break;
            }
        }
    }
}
