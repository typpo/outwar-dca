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
                    mUI.CountdownTimer.CurrentCountdown = ((int)numCountdown.Value) * 60;
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
    }
}
