using System;
using ATMApp.DAL;

namespace ATMApp.BLL.Utilities
{
    public class ATMUsersChoice
    {
        IATMService atmservice = new ATMService(new ATMDBContext());

        public void GetUserChoice()
        {
            Console.WriteLine("What operation do you want to carry out \n1 Check Balance\n2 Withdraw\n3 Transfer");
            if (int.TryParse(Console.ReadLine() ,out int choice))
            {
                switch (choice)
                {
                    case 1:
                        atmservice.CheckBalance();
                        break;
                    //case 2:
                    //    atmservice.Withdraw();
                    //    break;
                    //case 3:
                    //    atmservice.Transfer();
                    //    break;
                }
            }
            else
            {
                Console.WriteLine("Enter a valid input");
                GetUserChoice();
            }
        }
      

    }
}


