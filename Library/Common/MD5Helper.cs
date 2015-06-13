using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Library.Common
{
    // ReSharper disable InconsistentNaming
    public class MD5Helper
    // ReSharper restore InconsistentNaming
    {
        /// <summary>
        /// 连接两个字节数组
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        public static byte[] JoinBytes(byte[] b1, byte[] b2)
        {
            var b3 = new byte[b1.Length + b2.Length];
            Array.Copy(b1, b3, b1.Length);
            Array.Copy(b2, 0, b3, 16, b2.Length);
            return b3;
        }

        /// <summary>
        /// 将字符串加密为字节数组
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] Md5ToArray(string input)
        {
            return MD5.Create().ComputeHash(Encoding.Default.GetBytes(input));
        }

        /// <summary>
        /// 加密字符串，并转换十六进制表示的字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Md5(string input)
        {
            var buffer = MD5.Create().ComputeHash(Encoding.Default.GetBytes(input));
            var builder = new StringBuilder();
            foreach (byte t in buffer)
            {
                builder.Append(t.ToString("X2"));
            }
            return builder.ToString();
        }

        /// <summary>
        /// 对一个字节数组加密，并转换十六进制表示的字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Md5(byte[] input)
        {
            var buffer = MD5.Create().ComputeHash(input);
            var builder = new StringBuilder();
            foreach (byte t in buffer)
            {
                builder.Append(t.ToString("X2"));
            }
            return builder.ToString();
        }

        /// <summary>
        /// 对密码进行加密
        /// </summary>
        /// <param name="password">QQ密码</param>
        /// <param name="uin">第一次检验用户状态时获取的key</param>
        /// <param name="yzm">验证码</param>
        /// <returns></returns>
        public static string Md5(string password, string uin, string yzm)
        {
            var b1 = Md5ToArray(password);
            var uinBytes = ToBytes(uin);
            var s1 = Md5(JoinBytes(b1, uinBytes));
            return Md5(s1 + yzm);
        }
        /// <summary>
        /// 转换为字节数组表示
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ToBytes(string str)
        {
            var bytes = new byte[8];
            for (var i = 0; i < 8; i++)
            {
                bytes[i] = byte.Parse(str.Substring((i * 4) + 2, 2), NumberStyles.HexNumber);
            }
            return bytes;
        }
    }
}
