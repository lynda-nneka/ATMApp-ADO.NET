using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ATMApp.DAL.DbQueries
{
    public class InsertQuery
    {

        private readonly ATMDBContext _dbContext;
        public InsertQuery(ATMDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InsertIntoTransactionAsync(int Sender, int Receiver, string TransactionType, decimal Amount, DateTime TransactionTimeStamp)
        {
            string query = $@"INSERT INTO Transactions(Sender, Receiver, TransactionType, Amount, TransactionTimeStamp)VALUES(@Sender, @Receiver, @TransactionType, @Amount, @TransactionTimeStamp)";

            using SqlConnection sqlConn = await _dbContext.OpenConnection();

            using SqlCommand command = new SqlCommand(query, sqlConn);

            command.Parameters.AddWithValue("@Sender", Sender);
            command.Parameters.AddWithValue("@Receiver", Receiver);
            command.Parameters.AddWithValue("@TransactionType", TransactionType);
            command.Parameters.AddWithValue("@Amount", Amount);
            command.Parameters.AddWithValue("@TransactionTimeStamp", TransactionTimeStamp);

            string message = await command.ExecuteNonQueryAsync() > 0 ? "successfull" : "not successful";

            Console.WriteLine(message);
        }
    }
}

