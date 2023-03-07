using System;
using System.Reflection;
using System.Threading.Tasks;
using ATMApp.DAL.Models;
using Microsoft.Data.SqlClient;

namespace ATMApp.DAL.DbQueries
{
    public class UpdateQuery
    {
        private readonly ATMDBContext _dbContext;
        public UpdateQuery(ATMDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task UpdateUserAmountAsync(decimal balance, string accountNumber)
        {
            string query = @"UPDATE CustomerAccount SET Balance = @Balance WHERE AccountNumber = @AccountNumber";
            SqlConnection sqlConnection = await _dbContext.OpenConnection();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@Balance", balance);
            sqlCommand.Parameters.AddWithValue("@AccountNumber", accountNumber);
            string message = await sqlCommand.ExecuteNonQueryAsync() > 0 ? "successful" : "not successful";

        }
    }
}

