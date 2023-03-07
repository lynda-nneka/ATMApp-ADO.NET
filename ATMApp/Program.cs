using ATMApp;
using ATMApp.DAL;
public class Program
{


    static async Task Main(string[] args)
    {
        var context = new ATMDBContext();
        var atmService = new ATMService(context);
        var userAuth = new UserAuthentication(context, atmService);
    EnterNum: Console.WriteLine("Enter 1 to Login\nEnter 2 to enroll customer");
        string userInput = Console.ReadLine();
        if (int.TryParse(userInput, out int value))
        {
            switch (value)
            {
                case 1:
                    await userAuth.LoginAsync();
                    break;
                case 2:
                    await userAuth.EnrollAsync();
                    break;
                default:
                    Console.WriteLine("Entered value is not in the options. Please try again.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Choose only numbers.");
            goto EnterNum;
        }



        Console.ReadKey();
    }

}



