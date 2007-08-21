using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Drawing;
using System.Collections;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace prjDCI
{
    public class Captcha
    {
        public const int BACKGROUND = -13487566,
            PROMPT_START_X = 85,
            PROMPT_END_X = 135,
            PROMPT_Y = 16,
            PROMPT_Y_SCAN = 10;

        private static int[] PROMPT_PIXELS_NUMBERS = { 0, 52, 70, 74, 76, 72, 82, 63, 94, 87 };
        private const int PROMPT_PIXELS_PLUS = 45;
        private const int PROMPT_PIXELS_MINUS = 13;

        private static int[] MAIN_PIXEL_NUMBERS = { 197, 144, 173, 175, 179, 195, 200, 149, 216, 205 };
        private const int MAIN_PIXEL_NEGATIVE = 40;

        private Bitmap mImg;
        public Bitmap Img
        {
            get { return mImg; }
            set { mImg = value; }
        }

        private string mUrl;
        public string Url
        {
            get { return mUrl; }
        }

        private int[] mPixels;

        public Captcha(string url)
        {
            mUrl = url;
        }

        public void Load()
        {
            try
            {
                Stream SaveStream = new MemoryStream();
                WebRequest req = WebRequest.Create(mUrl);
                WebResponse resp = req.GetResponse();

                new Bitmap(resp.GetResponseStream()).Save(SaveStream, ImageFormat.Png);
                mImg = new Bitmap(Image.FromStream(SaveStream));
                InitArray();
            }
            catch
            {
                mImg = null;
            }
        }

        public Point Auto()
        {
            Load();

            if (mImg == null)
                return Auto();

            // START OCR Code

            // Determine prompt

            int count = 0;
            Character[] promptChars = new Character[3];

            for (int x = PROMPT_START_X; x < PROMPT_END_X && count != 3; x++)
            {
                if (SameColor(x, PROMPT_Y_SCAN, 1))
                {
                    Character c = new Character(this, x, PROMPT_Y_SCAN);
                    SetPromptValue(c);
                    promptChars[count++] = c;
                }
            }

            int firstNum, secondNum;
            if (promptChars[0] == null || promptChars[1] == null || promptChars[2] == null || !int.TryParse(promptChars[0].Value, out firstNum) || !int.TryParse(promptChars[2].Value, out secondNum))
                return Auto();
            bool add = promptChars[1].Value == "+" ? true : false;
            int answer = add ? firstNum + secondNum : firstNum - secondNum;

            // TODO: maybe remove
            if (answer == 1)
                return Auto();

            // Determine possible answers

            List<Character> chars = new List<Character>();
            for (int x = 0; x < mImg.Width; x++)
            {
                for (int y = PROMPT_Y + 1; y < mImg.Height; y++)
                {
                    if (SameColor(x, y, 1))
                    {
                        Character c = new Character(this, x, y);
                        SetMainValue(c);
                        if (c.NumPixels >= 250)
                            return Auto();

                        chars.Add(c);
                    }
                }
            }

            //chars = SortMainValues(chars);

            Character clickMe = null;
            string strAnswer = answer.ToString();

            for (int tries = 0; tries < 2 && clickMe == null; tries++)
            {
                StringBuilder sb = new StringBuilder();
                foreach (Character c in chars)
                {
                    sb.Append(c.Value);
                }
                string allChars = sb.ToString();

                // If positive single digit
                if (strAnswer.Length == 1)
                {
                    for (int i = 0; i < chars.Count; i++)
                    {
                        Character c = chars[i];
                        // check to make sure it doesn't have a negative in front of it
                        if (c.Value == strAnswer && (i == 0 || chars[i - 1].Value != "-"))
                        {
                            // check if last index
                            if (i != 0 && allChars.LastIndexOf(strAnswer) != i && (chars[i - 1].Value == "1" || chars[i - 1].Value == "7"))
                            {
                                // don't choose this one if it has a 1 in front of it
                                continue;
                            }

                            clickMe = c;
                            break;
                        }
                    }
                }
                // If negative or double digit
                else
                {
                    // negative : double digit
                    int i = allChars.IndexOf(strAnswer);
                    if (i != -1)
                        clickMe = chars[i + 1];
                }

                if (clickMe != null)
                {
                    Point p = clickMe.Point;
                    //frmSecPreview frm = new frmSecPreview(mImg, p);
                    //frm.ShowDialog();
                    return p;
                }


                return Auto();
                // TODO: TEST/(REMOVE)
                /*
                // Change problematic combinations
                for(int i = 0; i < chars.Count; i++)
                {
                    Character c = chars[i];
                    switch (c.Value)
                    {
                        case "0":
                            c.Value = "6";
                            break;
                        case "6":
                            c.Value = "0";
                            break;
                        case "2":
                            c.Value = "3";
                            break;
                        case "3":
                            c.Value = "4";
                            break;
                        case "5":
                            c.Value = "0";
                            break;
                        case "7":
                            c.Value = "1";
                            break;
                        case "9":
                            c.Value = "6";
                            break;
                    }
                }
                 * */
            }

            return Auto();
        }

        private void SetPromptValue(Character c)
        {
            int n = c.NumPixels;

            if (n == PROMPT_PIXELS_PLUS)
                c.Value = "+";
            else if (n == PROMPT_PIXELS_MINUS)
                c.Value = "-";
            else
            {
                for (int i = 0; i < PROMPT_PIXELS_NUMBERS.Length; i++)
                {
                    if (n == PROMPT_PIXELS_NUMBERS[i])
                    {
                        c.Value = i.ToString();
                        return;
                    }
                }
                c.Value = string.Empty;
            }
        }

        private void SetMainValue(Character c)
        {
            int n = c.NumPixels;

            int best = -1;
            int bestDiff = Math.Abs(n - MAIN_PIXEL_NEGATIVE);

            for (int i = 0; i < MAIN_PIXEL_NUMBERS.Length; i++)
            {
                int diff = Math.Abs(n - MAIN_PIXEL_NUMBERS[i]);
                if (diff < bestDiff)
                {
                    best = i;
                    bestDiff = diff;
                }
            }
            c.Value = best == -1 ? "-" : best.ToString();
        }

        public int TestPoint(int x, int y)
        {
            Character c = new Character(this, x, y);
            return c.NumPixels;
        }

        #region ARRAY MANIPULATION

        private void InitArray()
        {
            mPixels = new int[mImg.Width * mImg.Height];

            for (int x = 1; x < mImg.Width; x++)
            {
                for (int y = 1; y < mImg.Height; y++)
                {
                    if (mImg.GetPixel(x, y).ToArgb() == BACKGROUND)
                        SetArgb(x, y, 0);
                    else
                        SetArgb(x, y, 1);    //mImg.GetPixel(x, y)
                }
            }
        }

        private void SetArgb(int x, int y, Color clr)
        {
            mPixels[GetIndex(x, y)] = clr.ToArgb();
        }

        private void SetArgb(int x, int y, int argb)
        {
            mPixels[GetIndex(x, y)] = argb;
        }

        private int GetIndex(int x, int y)
        {
            return ((y * 250) + x);
        }

        private int GetArgb(int x, int y)
        {
            return mPixels[GetIndex(x, y)];
        }

        private bool SameColor(int argb1, int argb2)
        {
            return argb1 == argb2;
        }

        private bool SameColor(int x, int y, int argb)
        {
            return GetArgb(x, y) == argb;
        }

        private bool IsBKG(int x, int y)
        {
            return IsBKG(GetArgb(x, y));
        }

        private bool IsBKG(int argb)
        {
            return argb == BACKGROUND;
        }

        #endregion

        private class Character
        {
            private Captcha mCaptcha;
            private Dictionary<int, List<int>> mPoints;
            public Point Point
            {
                get
                {
                    int bestKey = mY;
                    int largestValueList = mPoints[mY].Count;
                    foreach (int y in mPoints.Keys)
                    {
                        int i = mPoints[y].Count;
                        if (i > largestValueList)
                        {
                            bestKey = y;
                            largestValueList = i;
                        }
                    }
                    List<int> xCoords = mPoints[bestKey];
                    xCoords.Sort();
                    return new Point(xCoords[(xCoords.Count - 1) / 2], bestKey);
                }
            }

            private int mTop, mBottom, mLeft, mRight;

            private int mX;
            public int X
            {
                get { return mX; }
            }

            private int mY;
            public int Y
            {
                get { return mY; }
            }

            private int mNumPixels;
            public int NumPixels
            {
                get
                {
                    if (mNumPixels == 0)
                        SpiderPixels();
                    return mNumPixels;
                }
            }

            private string mVal;
            public string Value
            {
                get { return mVal; }
                set { mVal = value; }
            }

            public Character(Captcha c, int x, int y)
            {
                mCaptcha = c;
                mX = x;
                mY = y;
                mPoints = new Dictionary<int, List<int>>();
            }

            public void SpiderPixels()
            {
                mTop = mY;
                mBottom = mY;
                mLeft = mX;
                mRight = mX;
                mNumPixels = 0;
                mPoints.Clear();
   
                SpiderPixels(mX, mY);
            }

            private void SpiderPixels(int x, int y)
            {
                mNumPixels += 1;

                if (x < mCaptcha.Img.Width - 1 && y < mCaptcha.Img.Height - 1)
                {
                    if (y < mTop)
                        mTop = y;
                    else if (y > mBottom)
                        mBottom = y;
                    if (x < mLeft)
                        mLeft = x;
                    else if (x > mRight)
                        mRight = x;

                    if (!mPoints.ContainsKey(y))
                        mPoints.Add(y, new List<int>());
                    if(!mPoints[y].Contains(x))
                        mPoints[y].Add(x);

                    if (mCaptcha.SameColor(x + 1, y, 1)) //+,~
                    {
                        mCaptcha.SetArgb(x + 1, y, 2);
                        SpiderPixels(x + 1, y);
                    }

                    if (mCaptcha.SameColor(x, y + 1, 1))  //~,+
                    {
                        mCaptcha.SetArgb(x, y + 1, 2);
                        SpiderPixels(x, y + 1);
                    }

                    if (mCaptcha.SameColor(x - 1, y, 1)) //-,~
                    {
                        mCaptcha.SetArgb(x - 1, y, 2);
                        SpiderPixels(x - 1, y);
                    }

                    if (mCaptcha.SameColor(x, y - 1, 1)) //~,-
                    {
                        mCaptcha.SetArgb(x, y - 1, 2);
                        SpiderPixels(x, y - 1);
                    }
                }
            }
        }
    }
}