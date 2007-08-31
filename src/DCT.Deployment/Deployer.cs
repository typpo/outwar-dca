using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace DCT.Deployment
{
    static class Deployer
    {
        internal static void RunFromUrl(string url, string entrypoint)
        {
            byte[] bin;
            try
            {
                bin = new WebClient().DownloadData(url);
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    "Could not reach host server.  Make sure no router, firewall, antivirus, or antispyware software is blocking the program's connection to the internet:\n\n"
                    + e.Message,
                    "Deployer Startup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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