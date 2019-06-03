using System.Text;
using System.Security.Cryptography;
using System;

namespace XM.Comm
{
    /// <summary>
    /// MD5加密类
    /// </summary>
    public class Md5
    {
        /// <summary>
        /// MD5加密字符串
        /// </summary>
        public static string GetMD5String(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.Default.GetBytes(str);
            byte[] md5data = md5.ComputeHash(data);
            md5.Clear();

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < md5data.Length - 1; i++)
            {
                builder.Append(md5data[i].ToString("X2"));
            }
            return builder.ToString();
        }

        public static string GetMd5(string str)
        {
            MD5 ms_MD5Object = MD5.Create();
            byte[] data = ms_MD5Object.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public static string GetMd5(object p)
        {
            throw new NotImplementedException();
        }
    }
}
