using System.Text;
using System.Security.Cryptography;

namespace BookStore.Utils
{
	public class MD5
	{
        static public string GetHashString(string s)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(s);
            MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();
            byte[] hashByte = CSP.ComputeHash(bytes);
            string hash = "";
            foreach (byte b in hashByte)
            {
                hash += string.Format("{0:x2}", b);
            }
            return hash;
        }
    }
}
