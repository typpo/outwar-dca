using System;

namespace DCT.Parsing
{
    internal class Parser
    {
        internal const string ERR_CONST = "ERROR";

        private string mStr;
        internal string String
        {
            get { return mStr; }
            set { mStr = value; }
        }

        internal Parser(string whole)
        {
            mStr = whole;
        }

        internal int Count(string sub)
        {
            return Count(mStr, sub);
        }

        internal static int Count(string whole, string sub)
        {
            int ret = 0;
            while (whole.IndexOf(sub) != -1)
            {
                ret++;
                whole = CutLeading(whole, sub);
            }
            return ret;
        }

        internal void CutLeading(string sub)
        {
            mStr = CutLeading(mStr, sub);
        }

        internal static string CutLeading(string whole, string cutoff)
        {
            try
            {
                return whole.Substring(whole.IndexOf(cutoff) + cutoff.Length);
            }
            catch (ArgumentOutOfRangeException)
            {
                return ERR_CONST;
            }
        }

        internal void CutTrailing(string cutoff)
        {
            mStr = CutTrailing(mStr, cutoff);
        }

        internal static string CutTrailing(string whole, string cutoff)
        {
            try
            {
                return whole.Substring(0, whole.IndexOf(cutoff));
            }
            catch (ArgumentOutOfRangeException)
            {
                return ERR_CONST;
            }
        }

        internal void RemoveRange(string start, string end)
        {
            mStr = RemoveRange(mStr, start, end);
        }

        internal static string RemoveRange(string whole, string start, string end)
        {
            try
            {
                foreach (string t in MultiParse(whole, start, end))
                {
                    whole = whole.Replace(t, "");
                }
                return whole;
            }
            catch (ArgumentOutOfRangeException)
            {
                return ERR_CONST;
            }
        }

        internal string Parse(string start, string end)
        {
            return Parse(mStr, start, end);
        }

        internal static string Parse(string whole, string start, string end)
        {
            try
            {
                return CutTrailing(CutLeading(whole, start), end);
            }
            catch (ArgumentOutOfRangeException)
            {
                return ERR_CONST;
            }
        }

        internal string[] MultiParse(string start, string end)
        {
            return MultiParse(mStr, start, end);
        }

        internal static string[] MultiParse(string whole, string start, string end)
        {
            try
            {
                string[] tokens = whole.Split(new string[] {start}, StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length < 1)
                {
                    return new string[] { };
                }
                string[] ret = new string[tokens.Length - 1];

                for (int i = 1; i < tokens.Length; i++)
                {
                    ret[i - 1] = CutTrailing(tokens[i], end);
                }

                return ret;
            }
            catch (ArgumentOutOfRangeException)
            {
                return new string[] {ERR_CONST};
            }
        }
    }
}