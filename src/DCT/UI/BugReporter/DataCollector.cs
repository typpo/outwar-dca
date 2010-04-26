using System;
using System.Collections.Generic;
using System.Text;

namespace DCT.UI.BugReporter
{
    static class DataCollector
    {
        internal static string GetString()
        {
            StringBuilder sb = new StringBuilder("State summary:\n\n");

            sb.AppendFormat("{0} accounts loaded\n", mUI.AccountsPanel.Engine.Count);
            sb.AppendFormat("{0} accounts selected\n", mUI.AccountsPanel.lvAccounts.SelectedIndices.Count);
            sb.AppendFormat("{0} accounts checked\n\n", mUI.AccountsPanel.lvAccounts.CheckedIndices.Count);

            sb.AppendFormat("{0} rooms loaded\n", mUI.RoomsPanel.Rooms.Count);
            sb.AppendFormat("{0} rooms checked\n\n", mUI.RoomsPanel.CheckedRooms.Count);


            sb.AppendFormat("{0} mobs loaded\n", mUI.MobsPanel.Mobs.Count);
            sb.AppendFormat("{0} mobs checked\n\n", mUI.MobsPanel.CheckedMobs.Count);

            sb.AppendFormat("{0} spawns loaded\n", mUI.SpawnsPanel.Spawns.Count);
            sb.AppendFormat("{0} spawns checked\n\n", mUI.SpawnsPanel.CheckedSpawns.Count);

            sb.Append("Settings serialization:\n\n");
            sb.Append(new ConfigSerializer().StringSerialize(mUI.Settings));
        }
    }
}
