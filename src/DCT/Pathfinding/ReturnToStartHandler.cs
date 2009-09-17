using System;
using DCT.Outwar;
using DCT.Outwar.World;
using DCT.UI;

namespace DCT.Pathfinding
{
    internal class ReturnToStartHandler
    {
        private const int NULL_ROOM = -1;

        private int mOriginalRoom;
        private Account mAccount;

        internal ReturnToStartHandler(Account a)
        {
            mAccount = a;
            mOriginalRoom = NULL_ROOM;
        }

        internal void SetOriginal()
        {
            mOriginalRoom = mAccount.Mover.Location.Id;
        }

        internal void InvokeReturn()
        {
            if (mOriginalRoom == NULL_ROOM)
            {
                return;
            }

            CoreUI.Instance.LogPanel.Log("Moving back to starting room...");

            Mover.PathfindHandler d = new Mover.PathfindHandler(mAccount.Mover.PathfindTo);
            d.BeginInvoke(mOriginalRoom, new AsyncCallback(PathfindCallback), d);

            mOriginalRoom = NULL_ROOM;
        }

        private void PathfindCallback(IAsyncResult ar)
        {
            Mover.PathfindHandler d = (Mover.PathfindHandler) ar.AsyncState;
            d.EndInvoke(ar);

            CoreUI.Instance.LogPanel.Log("Moved back to starting room");
        }
    }
}