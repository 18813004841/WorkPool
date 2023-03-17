using System.Security.Cryptography;
using System.Text;

namespace ScriptTools
{
    public class ToolsUtil
    {
        private static MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();

        public static byte[] GetMD5(string source)
        {
            byte[] hash = MD5.ComputeHash(Encoding.UTF8.GetBytes(source));
            return hash;
        }
    }
}