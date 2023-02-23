using System;
using ATMApp.DAL.Models;
using ATMApp.DAL;
using ATMApp.DAL.DbQueries;
using ATMApp.BLL.Utilities;
using System.Linq;
using System.Threading.Tasks;

namespace ATMApp
{
    public class UserAuthentication
    {
        ATMUsersChoice usersChoice = new ATMUsersChoice();
        SelectQuery selectQuery = new SelectQuery(new ATMDBContext());
        public static CustomerLoginViewModel user = new CustomerLoginViewModel();
        ATMService loginAtmService = new ATMService(new ATMDBContext());

        public static Account LoggedInUser {get; set;}
        public async Task LoginAsync()
        {
            
            Console.WriteLine("Welcome to Bank of the Rich");

            Console.WriteLine("Enter your card number (10 digits only):");
            string userCardNo = Console.ReadLine();
            Console.WriteLine("Enter your Pin");
            string Userpin = Console.ReadLine();

            if (userCardNo.Count() == 10 && Userpin.Count() == 4)
            {
                //user.CardNo = userCardNo;
                //user.Pin = Userpin;

                string dbQuery = @"USE LynnAtmDB; SELECT * FROM CustomerAccount WHERE CardNumber = @CardNumber AND Pin = @Pin";
                var user = await selectQuery.SelectCustomerAsync(userCardNo, Userpin, dbQuery);

                var userDetails = user.FirstOrDefault(user => user.CardNumber == userCardNo && user.Pin == Userpin);
                
                if(userDetails != null)
                {
                    LoggedInUser = userDetails;
                    Console.WriteLine($"Welcome {userDetails.FullName}");
                    usersChoice.GetUserChoice();

                }
            }
            else
            {
                Console.WriteLine("Card number should not be less than or more than 10 digits\npin should not be more than or less than 4");
                await LoginAsync();
            }
        }

        public async Task EnrollAsync()
        {
            Console.WriteLine("Enter your full name");
            string userName = Console.ReadLine();

            Console.WriteLine("Enter your Card");
            string cardNo = Console.ReadLine();

            Console.WriteLine("Enter your Pin");
            string pin = Console.ReadLine();

            var model = new CustomerViewModel
            {
                Name = userName,
                CardNo = cardNo,
                Pin = pin
            };
            await loginAtmService.EnrollCustomerAsync(model);
        }

    }
}

