using System;
using System.Text;
using System.Security.Cryptography;

namespace System
{
    public static class StringExtentions
    {
        public static string ToMD5(this string str)
        {
            if (!String.IsNullOrEmpty(str))
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                return BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(str))).Replace("-","");
            }
            else
            {
                return str;
            }
        }
    }
}
