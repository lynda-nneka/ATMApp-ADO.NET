using System;
using System.Threading.Tasks;
using ATMApp.DAL.Models;
using Microsoft.Data.SqlClient;
using ATMApp.BLL.Utilities;
using ATMApp.DAL.DbQueries;
using System.Linq;
using ATMApp.BLL;


namespace ATMApp.DAL
{
    public class ATMService : IATMService
    {
       readonly SelectQuery selectQuery = new SelectQuery(new ATMDBContext());
        private readonly ATMDBContext _dBContext;
        public ATMService(ATMDBContext context)
        {
            _dBContext = context;
        }

        public async Task CheckBalance()
        {
            string accNum = ValidateUser.AccountNumber();
            string getUserQuery = $"USE LynnAtmDB; SELECT * FROM CustomerAccount WHERE AccountNumber = @AccountNumber";
            var user = await selectQuery.SelectCustomerAsync(accNum, getUserQuery);
            var userDetails = user.FirstOrDefault(user => user.AccountNumber == accNum);
            if(userDetails != null)
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

        
        

        public void Transfer(TransferViewModel model)
        {
            throw new NotImplementedException();
        }

        public void Withdraw(WithdrawViewModel model)
        {
            throw new NotImplementedException();
        }


      
    }
}

