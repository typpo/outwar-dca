using System;
using System.Windows.Forms;

namespace DCT.UI
{
    internal partial class TrainPanel : UserControl
    {
        private readonly CoreUI mUI;

        internal bool TrainEnabled
        {
            get { return btnTrain.Enabled; }
            set
            {
                btnTrain.Enabled = value;
                chkTrainReturn.Enabled = value;
            }
        }

        internal bool AutoTrain
        {
            get { return chkAutoTrain.Checked; }
            set { chkAutoTrain.Checked = value; }
        }

        internal TrainPanel(CoreUI ui)
        {
            mUI = ui;
            InitializeComponent();
        }

        private void chkAutoTrain_CheckedChanged(object sender, EventArgs e)
        {
            CoreUI.Instance.Settings.AutoTrain = chkAutoTrain.Checked;
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {
            mUI.InvokeTraining(chkTrainReturn.Checked);
        }
    }
}
