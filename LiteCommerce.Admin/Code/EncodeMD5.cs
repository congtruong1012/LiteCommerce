using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace LiteCommerce.Admin
{
    public static class EncodeMD5
    {
        public static string GetMD5(string text)
        {

            //MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            //byte[] bHash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));

            //StringBuilder sbHash = new StringBuilder();

            //foreach (byte b in bHash)
            //{

            //    sbHash.Append(String.Format("{0:x2}", b));

            //}

            //return sbHash.ToString();
            try
            {
                MD5 md5 = MD5CryptoServiceProvider.Create();
                byte[] dataMd5 = md5.ComputeHash(Encoding.Default.GetBytes(text));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < dataMd5.Length; i++)
                    sb.AppendFormat("{0:x2}", dataMd5[i]);
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static bool IsMD5(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return false;
            }

            return Regex.IsMatch(input, "^[0-9a-fA-F]{32}$", RegexOptions.Compiled);
        }
    }
}