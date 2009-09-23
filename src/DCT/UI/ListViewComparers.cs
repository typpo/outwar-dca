using System;
using System.Collections;
using System.Windows.Forms;

// TODO this is stupid

namespace DCT.UI
{
    internal abstract class ListViewItemComparer : IComparer
    {
        protected int col;
        protected SortOrder order;

        protected ListViewItemComparer()
        {
            col = 0;
            order = SortOrder.Ascending;
        }

        internal ListViewItemComparer(int column, SortOrder order)
        {
            col = column;
            this.order = order;
        }

        public abstract int Compare(object x, object y);
    }

    internal class RoomsViewItemComparer : ListViewItemComparer
    {
        internal RoomsViewItemComparer(int column, SortOrder order)
            : base(column, order)
        {
        }

        public override int Compare(object x, object y)
        {
            int returnVal;
            string xx = ((ListViewItem) x).SubItems[col].Text,
                   yy = ((ListViewItem) y).SubItems[col].Text;

            if (col != 0)
            {
                int xn = int.Parse(xx),
                    yn = int.Parse(yy);

                if (xn < yn)
                    returnVal = -1;
                else if (xn == yn)
                    returnVal = 0;
                else
                    returnVal = 1;
            }
            else
            {
                returnVal = String.Compare(xx,
                                           yy);
            }

            if (order == SortOrder.Descending)
                returnVal *= -1;

            return returnVal;
        }
    }

    internal class MobViewItemComparer : ListViewItemComparer
    {
        internal MobViewItemComparer(int column, SortOrder order)
            : base(column, order)
        {
        }

        public override int Compare(object x, object y)
        {
            int returnVal;
            string xx = ((ListViewItem) x).SubItems[col].Text,
                   yy = ((ListViewItem) y).SubItems[col].Text;

            if (col != 0)
            {
                int xn = int.Parse(xx),
                    yn = int.Parse(yy);

                if (xn < yn)
                    returnVal = -1;
                else if (xn == yn)
                    returnVal = 0;
                else
                    returnVal = 1;
            }
            else
            {
                returnVal = String.Compare(xx,
                                           yy);
            }

            if (order == SortOrder.Descending)
                returnVal *= -1;

            return returnVal;
        }
    }

    internal class SpawnsViewItemComparer : ListViewItemComparer
    {
        internal SpawnsViewItemComparer(int column, SortOrder order)
            : base(column, order)
        {
        }

        public override int Compare(object x, object y)
        {
            int returnVal;
            string xx = ((ListViewItem)x).SubItems[col].Text,
                   yy = ((ListViewItem)y).SubItems[col].Text;

            if (col != 0)
            {
                int xn = int.Parse(xx),
                    yn = int.Parse(yy);

                if (xn < yn)
                    returnVal = -1;
                else if (xn == yn)
                    returnVal = 0;
                else
                    returnVal = 1;
            }
            else
            {
                returnVal = String.Compare(xx,
                                           yy);
            }

            if (order == SortOrder.Descending)
                returnVal *= -1;

            return returnVal;
        }
    }
}
