using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DCT.Util.Wizard
{
    public abstract class Wizard
    {
        /// <summary>
        /// User setting variables stored here
        /// </summary>
        protected Hashtable mVars;

        private List<ISlide> mSlides;
        private SlideDialog mDialog;

        public Wizard()
        {
            mVars = new Hashtable();
            mSlides = new List<ISlide>();
            mDialog = new SlideDialog();
        }

        /// <summary>
        /// Shows all slides in slides list
        /// </summary>
        /// <returns>True if run completely, otherwise false</returns>
        public virtual bool Start()
        {
            foreach(ISlide s in mSlides)
            {
                mDialog.AddSlide(s);
                s.Display();
                mDialog.RemoveSlide(s);

                if (mDialog.DialogResult != DialogResult.OK)
                {
                    return false;
                }
            }
            return true;
        }

        public void Add(ISlide p)
        {
            mSlides.Add(p);
        }

        public void Remove(ISlide p)
        {
            mSlides.Remove(p);
        }

        public void Insert(int i, ISlide p)
        {
            mSlides.Insert(i, p);
        }
    }
}