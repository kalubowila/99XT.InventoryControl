using _99XT.InventoryControl.Utility;
using System;

namespace StringEncryptor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a string to encrypt:");
            string plaintext = Console.ReadLine();
            Console.WriteLine("");

            Console.WriteLine("Your encrypted string is:");
            string encryptedstring = StringCipher.Encrypt(plaintext);
            Console.WriteLine(encryptedstring);
            Console.WriteLine("");

            Console.ReadLine();
        }
    }
}
