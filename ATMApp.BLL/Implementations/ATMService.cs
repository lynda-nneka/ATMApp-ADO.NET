using System;
using System.Threading.Tasks;
using ATMApp.DAL.Models;
using Microsoft.Data.SqlClient;
using ATMApp.BLL.Utilities;
using ATMApp.DAL.DbQueries;
using System.Linq;
using ATMApp.BLL;
using ATMApp.DAL.Enums;
using ATMApp;

namespace ATMApp.DAL
{
    public class ATMService : IATMService
    {
        private readonly InsertQuery _insertQuery;
        private readonly UpdateQuery _updateQuery;
        private readonly ATMUsersChoice _usersChoice;
        private readonly SelectQuery _selectQuery;
        private readonly ATMDBContext _dBContext;
        public ATMService(ATMDBContext context)
        {
            _dBContext = context;
            _insertQuery = new InsertQuery(_dBContext);
            _updateQuery = new UpdateQuery(_dBContext);
            _usersChoice = new ATMUsersChoice(this);
            _selectQuery = new SelectQuery(_dBContext);
        }

        public async Task CheckBalance()
        {
            string accNum = ValidateUser.AccountNumber();
            string getUserQuery = $"USE LynnAtmDB; SELECT * FROM CustomerAccount WHERE AccountNumber = @AccountNumber";
            var user = await _selectQuery.SelectCustomerAsync(accNum, getUserQuery);
            var userDetails = user.FirstOrDefault(user => user.AccountNumber == accNum);
            if (userDetails != null)
            {
                Console.WriteLine($"{userDetails.FullName} your account balance is {userDetails.Balance}");
            }
        }



        public async Task EnrollCustomerAsync(CustomerViewModel model)
        {
            SqlConnection sqlConn = await _dBContext.OpenConnection();

            string insertQuery =
                $@"USE LynnAtmDB; INSERT INTO CustomerAccount (FullName, AccountNumber, CardNumber, Pin, CreationTimeStamp, Balance) VALUES (@FullName, @AccountNumber, @CardNumber, @Pin, @CreationTimeStamp, @Balance)";

            await using SqlCommand command = new SqlCommand(insertQuery, sqlConn);


            command.Parameters.AddWithValue("@FullName", model.Name);
            command.Parameters.AddWithValue("@AccountNumber", RandomNumGenerator.AccountNumber());
            command.Parameters.AddWithValue("@CardNumber", model.CardNo);
            command.Parameters.AddWithValue("@Pin", model.Pin);
            command.Parameters.AddWithValue("@Balance", RandomNumGenerator.UsersBalance());
            command.Parameters.AddWithValue("@CreationTimeStamp", DateTime.Now);

            bool IsUserCreated = await command.ExecuteNonQueryAsync() > 0;
            string Message = IsUserCreated ? "User Enrolled successfully." : "User errollment failed.";
            Console.WriteLine(Message);
        }

        public void Exit()
        {
            Console.WriteLine("Thank you for banking with us\nplease take your card");
            Environment.Exit(0);
        }




        public async Task Transfer()
        {

            Console.WriteLine("Enter the recipient account number");
            string recipientAcc = Console.ReadLine();


            var recipient = await _selectQuery.SelectCustomerAsync(recipientAcc);


            var Recipient = recipient.FirstOrDefault(user => user.AccountNumber == recipientAcc);


            var sender = await _selectQuery.SelectCustomerAsync(UserAuthentication.LoggedInUser.AccountNumber);

            var Sender = sender.FirstOrDefault(user => user.AccountNumber == UserAuthentication.LoggedInUser.AccountNumber);

            if (Recipient == null)
            {
                Console.WriteLine("Recipient account number does not exist. Enter a valid information");
                await Transfer();

            }

            if (Sender.AccountNumber == Recipient.AccountNumber)
            {
                Console.WriteLine("Can not send money to self");
                await Transfer();
            }

        Amount: Console.WriteLine("Enter the amount you want to transfer ");
            string enteredAmount = Console.ReadLine();
            if (!decimal.TryParse(enteredAmount, out decimal amount))
            {
                Console.WriteLine("invalid amount");
                await Transfer();
            }


            if (amount >= Sender.Balance)
            {
                Console.WriteLine("Insufficient balance");
                goto Amount;
            }

        Transfer: Console.WriteLine($"Do you want to transfer {amount} to {Recipient.FullName}");
            Console.WriteLine("YES/NO");
            string answer = Console.ReadLine() ?? string.Empty;
            if (answer.Trim().ToUpper() == "YES")
            {
                decimal updateRecipientBalance = Recipient.Balance += amount;
                decimal updateSenderBalance = Sender.Balance += amount;

                await _updateQuery.UpdateUserAmountAsync(updateRecipientBalance, recipientAcc);
                await _updateQuery.UpdateUserAmountAsync(updateSenderBalance, UserAuthentication.LoggedInUser.AccountNumber);
                await _insertQuery.InsertIntoTransactionAsync(UserAuthentication.LoggedInUser.UserID, Recipient.UserID, "Transfer", amount, DateTime.Now);
                Console.WriteLine("Transaction succesful");
                Console.WriteLine($"{Sender.FullName} you have transfered {amount} to {Recipient.FullName} on {DateTime.Now}");
            }
            else if (answer.Trim().ToUpper() == "NO")
            {
                Console.WriteLine("you have cancelled the operation");
                await _usersChoice.GetUserChoice();
            }
            else
            {
                Console.WriteLine("invalid input");
                goto Transfer;
            }


        }

        public async Task Withdraw()
        {

        Userpin: Console.WriteLine("Enter your pin");
            string pin = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(pin))
            {
                Console.WriteLine("input was empty.. try again");
                goto Userpin;
            }

            var fetchedUser = await _selectQuery.SelectCustomerAsync(UserAuthentication.LoggedInUser.CardNumber, UserAuthentication.LoggedInUser.Pin);


            var user = fetchedUser.FirstOrDefault(user => user.Pin == pin);
            if (user == null)
            {
                Console.WriteLine("This user with pin entered does not exist try again");
                goto Userpin;
            }

        Amount: Console.WriteLine("Enter amount to withdraw");
            string enteredAmount = Console.ReadLine();
            if (!decimal.TryParse(enteredAmount, out decimal amount))
            {
                Console.WriteLine("invalid input, enter only numbers");
                goto Amount;
            }

            decimal newBalance = UserAuthentication.LoggedInUser.Balance -= amount;
            await _updateQuery.UpdateUserAmountAsync(newBalance, UserAuthentication.LoggedInUser.AccountNumber);

            await _insertQuery.InsertIntoTransactionAsync(UserAuthentication.LoggedInUser.UserID, UserAuthentication.LoggedInUser.UserID, "withdrawal", amount, DateTime.Now);
            Console.WriteLine($"{UserAuthentication.LoggedInUser.FullName} you withdrew {amount} from your account and your new balance is {newBalance}");

        }



    }
}

