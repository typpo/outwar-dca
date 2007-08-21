using System;
using System.Windows.Forms;

namespace DCT.Util.Wizard
{
    public partial class GenericSlide : UserControl, ISlide
    {
        public GenericSlide()
        {
            InitializeComponent();
            // To appease VS form designer
            if (ParentForm != null)
            {
                ParentForm.StartPosition = FormStartPosition.CenterScreen;
                ParentForm.FormBorderStyle = FormBorderStyle.Fixed3D;
                ParentForm.Height = this.Height;
                ParentForm.Width = this.Width;

                ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);
            }
        }

        public virtual void OnNext()
        {
            this.Hide();
            ParentForm.DialogResult = DialogResult.OK;
        }

        public void Display()
        {
            this.Show();
            ParentForm.ShowDialog();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            OnNext();
        }

        private void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ParentForm.DialogResult = DialogResult.Cancel;
        }
    }
}