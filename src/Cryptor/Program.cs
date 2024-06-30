using Cryptor;

var exit = false;
do
{
    string? functionType;
    do
    {
        Console.WriteLine("Functions type number");
        Console.WriteLine("0: Exit");
        Console.WriteLine("1: Encryption");
        Console.WriteLine("2: Decryption");
        Console.WriteLine("Enter functions type number.");
        functionType = Console.ReadLine();
    }
    while (functionType == null);

    switch (functionType)
    {
        case "0":
            Console.WriteLine("Exitting...");
            exit = true;
            break;
        case "1":
            Encrypt.Start();
            break;
        case "2":
            Decrypt.Start();
            break;
        default:
            Console.WriteLine("Incorrect function type.");
            break;
    }
}
while (!exit);