using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace DCT.Deployment
{
    static class Deployer
    {
        public static void Start(string url)
        {
            RunFromServer(url, null);
        }

        public static void Start(string url, string entrypoint)
        {
            RunFromServer(url, entrypoint);
        }

        private static void RunFromServer(string url, string entrypoint)
        {
            WebClient c;
            Stream s = null;
            BinaryReader br = null;
            byte[] bin;
            try
            {
                c = new WebClient();
                s = c.OpenRead(url);
                br = new BinaryReader(s);
                bin = br.ReadBytes(Convert.ToInt32(s.Length));
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    "Could not reach host server.  Make sure no router, firewall, antivirus, or antispyware software is blocking the program's connection to the internet:\n\n"
                    + e.Message,
                    "Deployer Startup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                if (s != null)
                {
                    s.Flush();
                    s.Close();
                }
                if (br != null)
                {
                    br.Close();
                }
            }

            try
            {
                Assembly a = Assembly.Load(bin);

                Form f;
                if (entrypoint == null)
                {
                    if (a.EntryPoint == null)
                    {
                        throw new Exception("Could not find an entry point in " + a.GetName());
                    }
                    f = (Form)a.CreateInstance(a.EntryPoint.ReflectedType.FullName);
                }
                else
                {
                    Type t = a.GetType(entrypoint, true, false);
                    f = (Form) Activator.CreateInstance(t);
                }

                Application.Run(f);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error loading entry point: " + e.Message, "Deployer Startup", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}