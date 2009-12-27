using System;
using System.Windows.Forms;

namespace DCT.UI
{
    public partial class AttackPanel : UserControl
    {
        internal int StopBelowRage
        {
            get { return (int)numRageStop.Value; }
            set { numRageStop.Value = value; }
        }

        internal int RageLimit
        {
            get { return (int)numRageLimit.Value; }
            set { numRageLimit.Value = value; }
        }

        internal int ThreadDelay
        {
            get { return (int)numThreadDelay.Value; }
            set { numThreadDelay.Value = value; }
        }

        internal long LevelMin
        {
            get { return (int)numLevelMin.Value; }
            set { numLevelMin.Value = value; }
        }

        internal long LevelMax
        {
            get { return (int)numLevel.Value; }
            set { numLevel.Value = value; }
        }

        internal bool Variance
        {
            get { return chkVariance.Checked; }
            set { chkVariance.Checked = value; }
        }

        internal bool ReturnToStart
        {
            get { return chkReturnToStart.Checked; }
            set { chkReturnToStart.Checked = value; }
        }

        internal MainPanel MainPanel { get; private set; }

        private readonly CoreUI mUI;

        internal AttackPanel(CoreUI ui)
        {
            mUI = ui;
            InitializeComponent();

            MainPanel = new MainPanel(mUI);
            this.pnlMainPanel.Controls.Add(MainPanel);
        }
        private void numLevel_ValueChanged(object sender, EventArgs e)
        {
            int tmp = (int)numLevel.Value;
            mUI.Settings.LvlLimit = tmp;
            if (tmp <= mUI.Settings.LvlLimitMin)
            {
                tmp = (int)(mUI.Settings.LvlLimitMin + 1);
                numLevel.Value = tmp;
            }
        }

        private void numLevelMin_ValueChanged(object sender, EventArgs e)
        {
            int tmp = (int)numLevelMin.Value;
            mUI.Settings.LvlLimitMin = tmp;
            if (tmp >= mUI.Settings.LvlLimit)
            {
                tmp = (int)(mUI.Settings.LvlLimit - 1);
                numLevelMin.Value = tmp;
            }
        }

        private void numRageLimit_ValueChanged(object sender, EventArgs e)
        {
            mUI.Settings.RageLimit = (int)numRageLimit.Value;
        }

        private void numRageStop_ValueChanged(object sender, EventArgs e)
        {
            mUI.Settings.StopBelowRage = (int)numRageStop.Value;
        }

        private void numTimeout_ValueChanged(object sender, EventArgs e)
        {
            mUI.Settings.Timeout = (int)numTimeout.Value;
        }

        private void numThreadDelay_ValueChanged(object sender, EventArgs e)
        {
            mUI.Settings.Delay = (int)numThreadDelay.Value;
        }

        private void chkVariance_CheckedChanged(object sender, EventArgs e)
        {
            mUI.Settings.Variance = chkVariance.Checked;
        }

        private void chkAutoTeleport_CheckedChanged(object sender, EventArgs e)
        {
            mUI.Settings.AutoTeleport = chkAutoTeleport.Checked;
        }

        private void chkReturnToStart_CheckedChanged_1(object sender, EventArgs e)
        {

            mUI.Settings.ReturnToStart = ReturnToStart;
        }
    }
}
