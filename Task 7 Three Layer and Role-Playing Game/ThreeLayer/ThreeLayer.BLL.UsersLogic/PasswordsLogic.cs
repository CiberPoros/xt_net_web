using System.Security.Cryptography;
using System.Text;

namespace ThreeLayer.BLL.UsersLogic
{
    public static class PasswordsLogic
    {
        public static byte[] GetHashOfPassword(string password)
        {
            byte[] passwordBytes = Encoding.Default.GetBytes(password);
            HashAlgorithm sha = SHA256.Create();
            return sha.ComputeHash(passwordBytes);
        }
    }
}
