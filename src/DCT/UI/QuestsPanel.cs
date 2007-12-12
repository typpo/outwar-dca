using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DCT.UI
{
    internal partial class QuestsPanel : UserControl
    {
        internal bool AutoQuest
        {
            get { return optQuestsAuto.Checked; }
            set { optQuestsAuto.Checked = value; }
        }

        internal bool AlertQuest
        {
            get { return optQuestsAlert.Checked; }
            set { optQuestsAlert.Checked = value; }
        }

        internal bool NothingQuest
        {
            get { return optQuestsNothing.Checked; }
            set { optQuestsNothing.Checked = value; }
        }

        private CoreUI mUI;

        internal QuestsPanel(CoreUI ui)
        {
            mUI = ui;
            InitializeComponent();
        }

        private void optQuestsAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (optQuestsAuto.Checked)
            {
                CoreUI.Instance.Settings.AlertQuests = false;
                CoreUI.Instance.Settings.AutoQuest = true;
            }
            else if (optQuestsAlert.Checked)
            {
                CoreUI.Instance.Settings.AlertQuests = true;
                CoreUI.Instance.Settings.AutoQuest = false;
            }
            else
            {
                CoreUI.Instance.Settings.AlertQuests = false;
                CoreUI.Instance.Settings.AutoQuest = false;
            }
        }

        private void optQuestsAlert_CheckedChanged(object sender, EventArgs e)
        {
            optQuestsAuto_CheckedChanged(null, null);
        }

        private void optQuestsNothing_CheckedChanged(object sender, EventArgs e)
        {
            optQuestsAuto_CheckedChanged(null, null);
        }
    }
}
