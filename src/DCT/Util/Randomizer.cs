using System;
using System.Text;

namespace DCT.Util
{
    internal static class Randomizer
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

        internal static string RandomString(int len)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < len; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * mRandom.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}