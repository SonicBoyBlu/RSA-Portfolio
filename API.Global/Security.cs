using System.Security.Cryptography;
using System.Text;

namespace Global.Security
{
    public static class Cryptography
    {
        private static string CryptSalt = Settings.MiscKeys.CryptographySalt;
        private static byte[] CryptSaltBytes = Encoding.Unicode.GetBytes(CryptSalt);
        public static string Encrypt(string text)
        {
            return Convert.ToBase64String(
                ProtectedData.Protect(
                    Encoding.Unicode.GetBytes(text), CryptSaltBytes, DataProtectionScope.LocalMachine));
        }

        public static string Decrypt(string text)
        {
            return Encoding.Unicode.GetString(
                ProtectedData.Unprotect(
                     Convert.FromBase64String(text), CryptSaltBytes, DataProtectionScope.LocalMachine));
        }
    }
}
