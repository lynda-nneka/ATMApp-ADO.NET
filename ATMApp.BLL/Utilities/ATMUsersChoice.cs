using System;
using ATMApp.DAL;
using System.Threading.Tasks;

namespace ATMApp.BLL.Utilities
{
    public class ATMUsersChoice
    {

        private readonly IATMService _atmService;
        public ATMUsersChoice(IATMService atmService)
        {
            _atmService = atmService;
        }




        public async Task GetUserChoice()
        {
            Console.WriteLine("What operation do you want to carry out \n1 Check Balance\n2 Withdraw\n3 Transfer");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        await _atmService.CheckBalance();
                        break;
                    case 2:
                        await _atmService.Withdraw();
                        break;
                    case 3:
                        await _atmService.Transfer();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Enter a valid input");
                await GetUserChoice();
            }
        }


    }
}


