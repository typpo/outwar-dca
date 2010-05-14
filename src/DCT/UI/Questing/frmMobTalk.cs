using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DCT.UI.Questing
{
    public partial class frmMobTalk : Form
    {
        internal frmMobTalk(Outwar.Account account, long mobid, string sessid)
        {
            InitializeComponent();

            talkpanel.LoadTalk(account, mobid, sessid);
        }
    }
}
