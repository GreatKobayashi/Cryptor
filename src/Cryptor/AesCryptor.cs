using System.Security.Cryptography;
using System.Text;

namespace Cryptor
{
    public class AesCryptor : ICryptor
    {
        private byte[] _aesKey;

        public AesCryptor(string aesKey)
        {
            var temp = Encoding.UTF8.GetBytes(aesKey);
            if (temp.Length != Shared.Aes256KeyLength)
            {
                throw new Exception("Incorrect key format.");
            }
            _aesKey = temp;
        }

        public async Task<string> EncryptAsync(string plainMessage)
        {
            using (var aes = Aes.Create())
            {
                var encryptor = aes.CreateEncryptor(_aesKey, aes.IV);
                using (var ms = new MemoryStream())
                await using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    await ms.WriteAsync(aes.IV);
                    await using (var sw = new StreamWriter(cs))
                    {
                        await sw.WriteAsync(plainMessage);
                    }
                    var bytes = ms.ToArray();
                    return Convert.ToBase64String(bytes);
                }
            }
        }

        public async Task<string> DencryptAsync(string cipher)
        {
            var cipherByte = Convert.FromBase64String(cipher);
            var iv = cipherByte[..16];

            using (var aes = Aes.Create())
            {
                var decryptor = aes.CreateDecryptor(_aesKey, iv);
                using (var ms = new MemoryStream())
                {
                    await using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                    await using (var bw = new BinaryWriter(cs))
                    {
                        bw.Write(cipherByte, iv.Length, cipherByte.Length - iv.Length);
                    }
                    var bytes = ms.ToArray();
                    return Encoding.Default.GetString(bytes);
                }
            }
        }
    }
}
