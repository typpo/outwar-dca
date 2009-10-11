using DCT.Util;

namespace DCT.Outwar.World
{
    internal enum AttackingType
    {
        [StringValue("multi area")]
        Multi,
        [StringValue("current area")]
        Single,
        [StringValue("mobs")]
        Mobs,
        [StringValue("rooms")]
        Rooms
    }
}