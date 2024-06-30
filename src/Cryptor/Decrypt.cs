namespace Cryptor
{
    public static class Decrypt
    {
        public static async void Start()
        {
            ICryptor? cryptor = null;

            do
            {
                Console.WriteLine("Decryption type numbers");
                Console.WriteLine("1: AES-256");
                Console.WriteLine("Enter decryption type number.");
                var cryptType = Console.ReadLine();
                try
                {
                    switch (cryptType)
                    {
                        case "1":
                            Console.WriteLine("Please enter AES key. (Should be 256 bit)");
                            var key = Console.ReadLine();
                            if (key == null)
                            {
                                Console.WriteLine("Incorrect AES key.");
                                Console.WriteLine();
                                break;
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

            string? targetFileName;
            var fileNames = Directory.GetFiles(".");
            do
            {
                Console.WriteLine("Please enter target file name.");
                Console.WriteLine("Current directory's file--");
                foreach (var fileName in fileNames)
                {
                    Console.WriteLine(fileName);
                }
                Console.WriteLine("--------------------------");
                Console.WriteLine();
                targetFileName = Console.ReadLine();
            }
            while (targetFileName == null);

            var cipher = File.ReadAllText(targetFileName);
            var plain = await cryptor.DencryptAsync(cipher);

            Console.WriteLine("Success to decryption.");
            Console.WriteLine("Decrypted message---");
            Console.WriteLine(plain);
            Console.WriteLine("--------------------");

            string? plainFileName;
            do
            {
                Console.WriteLine("Enter decrypted file name.");
                plainFileName = Console.ReadLine();
            }
            while (plainFileName == null);

            if (!plainFileName.Contains('.'))
            {
                plainFileName += ".txt";
            }
            File.WriteAllText(plainFileName, plain);

            if (File.Exists(plainFileName))
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
