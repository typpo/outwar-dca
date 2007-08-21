using System;
using System.Windows.Forms;

namespace DCT.Util
{
    public partial class InputBox : Form
    {
        private static string mInput;
        private static bool mRelease;

        private static InputBox mInputBox;
        private static InputBox Instance
        {
            get { return mInputBox; }
        }

        static InputBox()
        {
            mInput = string.Empty;
            mInputBox = new InputBox();
        }

        public static string Prompt(string title, string prompt)
        {
            return Prompt(title, prompt, string.Empty);
        }

        public static string Prompt(string title, string prompt, string defaultText)
        {
            mInput = string.Empty;
            mRelease = false;
            Instance.Show(title, prompt, defaultText);

            while (mInput == string.Empty && !mRelease)
            {
                Instance.ShowDialog();
            }
            return mInput;
        }

        public InputBox()
        {
            InitializeComponent();
        }

        private void Show(string title, string prompt, string defaultText)
        {
            this.Text = title;
            txtPrompt.Text = prompt;
            txtInput.Text = defaultText;
            txtInput.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            mInput = txtInput.Text;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mInput = null;
            mRelease = true;
            Close();
        }
    }
}