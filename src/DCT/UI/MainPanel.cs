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
        // stopafter notes -
        // must call ResetStopAfterCounter() or ResetStopAfterTime() before running
        // end with StopAfterFinish()

        private const int STOPAFTERTIME_OFFSET = 3;    // in seconds

        internal int StopAfterCounter { get; set; }

        internal void ResetStopAfterCounter()
        {
            StopAfterCounter = 0;
            StopAfterRunning = true;
        }

        internal bool StopAfterCounterFinished
        {
            get { return StopAfterCounter >= StopAfterVal; }
        }

        internal void ResetStopAfterTime()
        {
            StopAfterTime = DateTime.Now;
            StopAfterRunning = true;
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

        internal bool StopAfterRunning { get; private set; }

        internal void StopAfterFinish()
        {
            StopAfterRunning = false;
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

        internal bool StopAfterEnabled
        {
            set
            {
                cmbStopAfter.Enabled = false;
            }
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

                InitStopAfter();
            }
            mUI.Settings.StopAfterVal = (int)numStopAfter.Value;
        }

        internal void InitStopAfter()
        {
            if (mUI.Settings.StopAfter)
            {
                // intialize stopafter options
                switch (mUI.Settings.StopAfterMode)
                {
                    case UserEditable.StopAfterType.Minutes:
                        ResetStopAfterTime();
                        break;
                    case UserEditable.StopAfterType.Runs:
                        ResetStopAfterCounter();
                        break;
                }

                // if there is no countdown set, then we set an internal countdown of 0
                if (!RunCountdown)
                {
                    UseCountdownTimer = true;
                    CountdownValue = 0;
                }
            }
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
