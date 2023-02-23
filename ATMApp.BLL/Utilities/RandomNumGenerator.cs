using System;
using System.Security.Cryptography;
using System.Text;

namespace ATMApp.BLL.Utilities
{
    public static class RandomNumGenerator
    {
        public static string AccountNumber()
        {
            StringBuilder generatedRandomNumber = new StringBuilder();
            byte[] randomNumber = new byte[1];
            RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

            // Generate 10 random digits
            for (int i = 0; i < 10; i++)
            {
                rngCsp.GetBytes(randomNumber);
                int randomDigit = Math.Abs(randomNumber[0] % 10);
                generatedRandomNumber.Append(randomDigit);
            }
            return generatedRandomNumber.ToString();



        }

        public static decimal UsersBalance()
        {
            Random rnd = new Random();
            decimal randomNumber = rnd.Next(20000, 50001);

            return randomNumber;
        }
    }
}

