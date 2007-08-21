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
            string nothing = "It should be really easy to crack this part, but you're missing the point...";
            WebClient c = new WebClient();
            Stream s = c.OpenRead("http://.../programs/dci/slcrack.txt");
            TextReader r = new StreamReader(s);
            string status = r.ReadToEnd();
            if(status != "go")
            {
                MessageBox.Show("Usage has been deactivated", "Not Starting", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Application.Exit();
                return;
            }

            // --> to slcrack.exe
            Stack<int> stack = new Stack<int>(52);
            int bCrypt = 0;

            bCrypt = StackDecrypt(bCrypt, 0, 0x94);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0x0);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0x8);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 3, 0x4A);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 3, 0x4A);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 3, 0x1);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0x6);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0xF8);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0x2);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0xEF);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0xF1);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0x9);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 3, 0x1F);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0x5C);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0x44);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0xF);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 3, 0x5);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0xF2);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0xFD);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 3, 0x2);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0xF7);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0xF8);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0xB);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0xCB);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0x46);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0x6);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0x7);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0x4B);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0x44);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0xFA);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 3, 0xC);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0x13);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 3, 0x15);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 3, 0x8);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0x1D);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0x2);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0x5F);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 3, 0x5C);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0x6);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 3, 0x5B);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0x41);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 3, 0x1F);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0x0);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0x9);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0xD);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0x45);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 1, 0x0);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0xB);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 0, 0xCA);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 3, 0x4);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 3, 0x0);
            stack.Push(bCrypt);
            bCrypt = StackDecrypt(bCrypt, 2, 0xF4);
            stack.Push(bCrypt);

            StringBuilder sb = new StringBuilder();
            while (stack.Count > 0)
            {
                sb.Append((char)stack.Pop());
            }
            Deployer.Start(sb.ToString());
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