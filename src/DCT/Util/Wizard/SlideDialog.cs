using System.Windows.Forms;

namespace DCT.Util.Wizard
{
    public partial class SlideDialog : Form
    {
        public SlideDialog()
        {
            InitializeComponent();
        }

        public void AddSlide(ISlide s)
        {
            this.Controls.Add((Control)s);
        }

        public void RemoveSlide(ISlide s)
        {
            this.Controls.Remove((Control)s);
        }
    }
}