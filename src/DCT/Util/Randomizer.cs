using System;

namespace DCT.Util
{
    internal class Randomizer
    {
        private static Random mRandom = new Random();
        internal static Random Random
        {
            get { return mRandom; }
        }

        internal static int RandomPosNeg()
        {
            return mRandom.NextDouble() > .5 ? 1 : -1;
        }
    }
}