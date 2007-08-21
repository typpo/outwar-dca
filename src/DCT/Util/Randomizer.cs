using System;

namespace DCT.Util
{
    internal class Randomizer
    {
        private static Random mRandom;
        public static Random Random
        {
            get { return mRandom; }
        }

        static Randomizer()
        {
            mRandom = new Random();
        }

        public static int RandomPosNeg()
        {
            return mRandom.NextDouble() > .5 ? 1 : -1;
        }
    }
}