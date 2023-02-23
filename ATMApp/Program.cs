using ATMApp;

public class Program
{
    
        public static readonly UserAuthentication UserAuthentication = new();
    static async Task Main(string[] args)
    {
        
       EnterNum: Console.WriteLine("Enter 1 to Login\nEnter 2 to enroll customer");
        string userInput = Console.ReadLine();
        if(int.TryParse(userInput,  out int value))
        {
            switch (value)
            {
                case 1:
                    await UserAuthentication.LoginAsync();
                    break;
                case 2:
                    await UserAuthentication.EnrollAsync();
                    break;
                default: Console.WriteLine("Entered value is not in the options. Please try again.");
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



