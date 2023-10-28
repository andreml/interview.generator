using System.Security.Cryptography;
using System.Text;

namespace interview.generator.domain.Utils
{
    public static class Encryptor
    {
        //
        // Summary:
        //     Criptografa um texto
        //
        // Parameters:
        //   value:
        //     Texto que será Criptogrado
        //
        // Returns:
        //     Retorna uma string criptografada
        public static string Encrypt(string value)
        {
            string password = "bd449573-f048-40b7-bf61-0a2ec0c9f15e";
            byte[] bytes = Encoding.Unicode.GetBytes(value);
            using Aes aes = Aes.Create();

            Rfc2898DeriveBytes rfc2898DeriveBytes = new(password, new byte[13]
            {
                73, 118, 97, 110, 32, 77, 101, 100, 118, 101, 100, 101, 118
            }, 1000, HashAlgorithmName.SHA1);

            aes.Key = rfc2898DeriveBytes.GetBytes(32);
            aes.IV = rfc2898DeriveBytes.GetBytes(16);
            using MemoryStream memoryStream = new();
            using (CryptoStream cryptoStream = new(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cryptoStream.Write(bytes, 0, bytes.Length);
                cryptoStream.Close();
            }

            value = Convert.ToBase64String(memoryStream.ToArray());
            return value;
        }

        //
        // Summary:
        //     Descriptografa um texto
        //
        // Parameters:
        //   value:
        //     Texto que será Descriptografado
        //
        // Returns:
        //     Retorna uma string descriptografada
        public static string Decrypt(string value)
        {
            string password = "bd449573-f048-40b7-bf61-0a2ec0c9f15e";
            byte[] array = Convert.FromBase64String(value);
            using Aes aes = Aes.Create();

            Rfc2898DeriveBytes rfc2898DeriveBytes = new(password, new byte[13]
            {
                73, 118, 97, 110, 32, 77, 101, 100, 118, 101, 100, 101, 118
            }, 1000, HashAlgorithmName.SHA1);

            aes.Key = rfc2898DeriveBytes.GetBytes(32);
            aes.IV = rfc2898DeriveBytes.GetBytes(16);
            using MemoryStream memoryStream = new();
            using (CryptoStream cryptoStream = new(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cryptoStream.Write(array, 0, array.Length);
                cryptoStream.Close();
            }

            value = Encoding.Unicode.GetString(memoryStream.ToArray());
            return value;
        }
    }
}
