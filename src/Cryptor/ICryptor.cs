namespace Cryptor
{
    public interface ICryptor
    {
        public Task<string> EncryptAsync(string plainMessage);
        public Task<string> DencryptAsync(string cipher);
    }
}
