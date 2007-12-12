using DCT.Protocols.Http;
using DCT.Parsing;

namespace DCT.Outwar.World
{
    internal class RaidFormMob : Occupier
    {
        private string mFormUrl;

        internal RaidFormMob(string name, string url, string formurl, Room rm) : base(name, url, rm)
        {
            mFormUrl = formurl;
        }

        internal void Form()
        {
            HitButton();
            UI.CoreUI.Instance.LogPanel.Log("Formed raid for " + mName);
        }

        internal void Launch()
        {
            HitButton();
            UI.CoreUI.Instance.LogPanel.Log("Launched raid for " + mName);
        }

        private void HitButton()
        {
            HttpSocket s = mRoom.Mover.Account.Socket;
            string codeid = Parser.Parse(s.Get(mFormUrl), "codeid\" value=\"", "\"");
            UI.CoreUI.Instance.LogPanel.Log("Foudn codeid for " + mName + ": " + codeid);
            s.Post(mFormUrl, "formtime=3&message=&bomb=none&codeid=" + codeid + "&submit=Launch!");
        }
    }
}