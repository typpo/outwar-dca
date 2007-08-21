using System;
using DCT.Outwar;
using DCT.Outwar.World;
using DCT.UI;

namespace DCT.Pathfinding
{
    internal class ReturnToStartHandler
    {
        private int mOriginalRoom;
        private Account mAccount;

        internal ReturnToStartHandler(Account a)
        {
            mAccount = a;
            mOriginalRoom = 0;
        }

        internal void SetOriginal()
        {
            mOriginalRoom = mAccount.Mover.Location.Id;
        }

        internal void Return()
        {
            if (mOriginalRoom == 0)
            {
                return;
            }

            CoreUI.Instance.Log("Moving back to starting room...");

            Mover.PathfindHandler d = new Mover.PathfindHandler(mAccount.Mover.PathfindTo);
            d.BeginInvoke(mOriginalRoom, new AsyncCallback(PathfindCallback), d);

            mOriginalRoom = 0;
        }

        private void PathfindCallback(IAsyncResult ar)
        {
            Mover.PathfindHandler d = (Mover.PathfindHandler) ar.AsyncState;
            d.EndInvoke(ar);

            CoreUI.Instance.Log("Moved back to starting room");
        }
    }
}