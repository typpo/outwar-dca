using System;
using System.Globalization;
using System.Text;
using DCT.Util;

namespace DCT.Security
{
    internal static class Crypt
    {
        internal static int StackDecrypt(int bCrypt, int iOpCode, int iSalt)
        {
            switch (iOpCode)
            {
                case 0:
                    bCrypt = bCrypt - iSalt;
                    break;
                case 1:
                    bCrypt = bCrypt + iSalt;
                    break;
                case 2:
                    bCrypt = bCrypt ^ iSalt;
                    break;
            }
            bCrypt = bCrypt & 255;
            return bCrypt;
        }

        /**
         * Get ASCII Integer Code
         *
         * @param char ch Character to get ASCII value for
         * @access private
         * @return int
         */

        private static int ord(char ch)
        {
            return Encoding.GetEncoding(1252).GetBytes(ch + "")[0];
        }

        /**
         * Convert Hex to Binary (hex2bin)
         *
         * @param string packtype Rudimentary in this implementation
         * @param string datastring Hex to be packed into Binary
         * @access private
         * @return string
         */

        private static string pack(string datastring)
        {
            int i, j, datalength, packsize;
            byte[] bytes;
            char[] hex;

            datalength = datastring.Length;
            packsize = (datalength / 2) + (datalength % 2);
            bytes = new byte[packsize];
            hex = new char[2];

            for (i = j = 0; i < datalength; i += 2)
            {
                hex[0] = datastring[i];
                if (datalength - i == 1)
                    hex[1] = '0';
                else
                    hex[1] = datastring[i + 1];
                string tmp = new string(hex, 0, 2);
                try
                {
                    bytes[j++] = byte.Parse(tmp, NumberStyles.HexNumber);
                }
                catch
                {
                } /* grin */
            }
            return Encoding.GetEncoding(1252).GetString(bytes);
        }

        /**
         * Convert Binary to Hex (bin2hex)
         *
         * @param string bindata Binary data
         * @access internal
         * @return string
         */

        internal static string BinToHex(string bindata)
        {
            byte[] bytes = Encoding.GetEncoding(1252).GetBytes(bindata);

            StringBuilder hex = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                hex.Append(bytes[i].ToString("x2"));
            }
            return hex.ToString();
        }

        internal static string HexToBin(string hexdata)
        {
            if (hexdata == null)
                throw new ArgumentNullException("hexdata");
            if (hexdata.Length % 2 != 0)
                throw new ArgumentException("hexdata should have even length");

            byte[] bytes = new byte[hexdata.Length / 2];
            for (int i = 0; i < hexdata.Length; i += 2)
                bytes[i / 2] = (byte)(HexValue(hexdata[i]) * 0x10
                                     + HexValue(hexdata[i + 1]));
            return Encoding.GetEncoding(1252).GetString(bytes);
        }

        private static int HexValue(char c)
        {
            int ch = c;
            if (ch >= '0' && ch <= '9')
                return ch - '0';
            if (ch >= 'a' && ch <= 'f')
                return ch - 'a' + 10;
            if (ch >= 'A' && ch <= 'F')
                return ch - 'A' + 10;
            throw new ArgumentException(c + " is not a hexadecimal digit.");
        }

        /**
         * The symmetric encryption function
         *
         * @param string pwd Key to encrypt with (can be binary of hex)
         * @param string data Content to be encrypted
         * @param bool ispwdHex Key passed is in hexadecimal or not
         * @access internal
         * @return string
         */

        internal static string Get(string data, string pwd, bool ispwdHex)
        {
            int a, i, j;
            int tmp, pwd_length, data_length;
            int[] key, box;
            byte[] cipher;

            if (ispwdHex)
            {
                pwd = pack(pwd); // valid input, please!
            }
            pwd_length = pwd.Length;
            data_length = data.Length;
            key = new int[256];
            box = new int[256];
            cipher = new byte[data.Length];

            for (i = 0; i < 256; i++)
            {
                key[i] = ord(pwd[i%pwd_length]);
                //key[i] = pwd[i % pwd_length];
                box[i] = i;
            }
            for (j = i = 0; i < 256; i++)
            {
                j = (j + box[i] + key[i]) % 256;
                tmp = box[i];
                box[i] = box[j];
                box[j] = tmp;
            }
            for (a = j = i = 0; i < data_length; i++)
            {
                a = (a + 1) % 256;
                j = (j + box[a]) % 256;
                tmp = box[a];
                box[a] = box[j];
                box[j] = tmp;
                int k = box[((box[a] + box[j]) % 256)];
                cipher[i] = (byte)(ord(data[i]) ^ k);
                //cipher[i] = (byte)(data[i] ^ k);
            }
            return Encoding.GetEncoding(1252).GetString(cipher);
        }

        /// <summary>
        /// Generates a random string with the given length
        /// </summary>
        /// <param name="size">Size of the string</param>
        /// <param name="lowerCase">If true, generate lowercase string</param>
        /// <returns>Random string</returns>
        internal static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < size; i++)
            {
                char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * Randomizer.Random.NextDouble() + 65)));
                builder.Append(ch);
            }

            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
    }
}