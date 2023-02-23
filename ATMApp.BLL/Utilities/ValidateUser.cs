using System;
using ATMApp.DAL.Models;
using System.Threading.Tasks;

namespace ATMApp.BLL.Utilities
{
    public class ValidateUser
    {
        public static string AccountNumber()
        {
            Console.WriteLine("Enter your account number");
            string accountNumber = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                Console.WriteLine("Input was empty");
                AccountNumber();
                
            }
            return accountNumber;
        }
    }
}

