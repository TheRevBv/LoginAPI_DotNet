using System.Security.Cryptography;
using System.Text;
namespace LoginApi;

public class HashPassword
{
    private static readonly  SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
    public string HashearPassword(string password)
    {
        var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(password));
        var hashString = BitConverter.ToString(hash).Replace("-", "");
        return hashString;
    }
}
