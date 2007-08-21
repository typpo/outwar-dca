using System.Collections;

namespace DCT.UI.Wizard
{
    class SetupWizard : Util.Wizard.Wizard
    {
        public SetupWizard()
        {
            // TODO make, this.add slides
        }

        public override bool Start()
        {
            if(!base.Start())
            {
                return false;
            }

            // set vars on main ui
            foreach(DictionaryEntry e in mVars)
            {
                
            }
            return true;
        }
    }
}
