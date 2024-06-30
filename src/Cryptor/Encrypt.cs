using System.Text;

namespace Cryptor
{
    public static class Encrypt
    {
        private static readonly string _keyChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static async void Start()
        {
            ICryptor? cryptor = null;

            do
            {
                Console.WriteLine("Encryption type numbers");
                Console.WriteLine("1: AES-256");
                Console.WriteLine("Enter encryption type number.");
                var cryptType = Console.ReadLine();
                try
                {
                    switch (cryptType)
                    {
                        case "1":
                            Console.WriteLine("Please enter AES key. (Should be 256 bit)");
                            Console.WriteLine("Generate random key to enter \"r\".");
                            var key = Console.ReadLine();
                            if (string.IsNullOrEmpty(key))
                            {
                                Console.WriteLine("Incorrect AES key.");
                                Console.WriteLine();
                                break;
                            }
                            else if (key == "r")
                            {
                                var sb = new StringBuilder(Shared.Aes256KeyLength);
                                var r = new Random();

                                for (var i = 0; i < Shared.Aes256KeyLength; i++)
                                {
                                    sb.Append(_keyChars[r.Next(_keyChars.Length)]);
                                }
                                key = sb.ToString();
                                Console.WriteLine("Generated key----");
                                Console.WriteLine(key);
                                Console.WriteLine("-----------------");
                            }
                            cryptor = new AesCryptor(key);
                            break;
                        default:
                            Console.WriteLine("Incorrect encryption number.");
                            Console.WriteLine();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine();
                }
            }
            while (cryptor == null);

            string? plainMessage;
            do
            {
                Console.WriteLine("Please enter plain message.");
                plainMessage = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(plainMessage));

            var cipher = await cryptor.EncryptAsync(plainMessage);

            string? fileName;
            do
            {
                Console.WriteLine("Enter encrypted file name.");
                fileName = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(fileName));

            if (!fileName.Contains('.'))
            {
                fileName += ".aes";
            }
            File.WriteAllText(fileName, cipher);

            if (File.Exists(fileName))
            {
                Console.WriteLine("Success to save file.");
            }
            else
            {
                Console.WriteLine("Something went wrong.");
                Console.WriteLine("Please check input and directory.");
            }
        }
    }
}
