using System;
using DCT.Outwar;
using DCT.Outwar.World;
using DCT.UI;

namespace DCT.Pathfinding
{
    public class ReturnToStartHandler
    {
        private int mOriginalRoom;
        private Account mAccount;

        public ReturnToStartHandler(Account a)
        {
            mAccount = a;
            mOriginalRoom = 0;
        }

        public void SetOriginal()
        {
            mOriginalRoom = mAccount.Mover.Location.Id;
        }

        public void Return()
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