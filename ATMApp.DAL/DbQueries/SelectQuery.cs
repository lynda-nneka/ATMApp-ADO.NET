using System;
using ATMApp.DAL.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ATMApp.DAL.DbQueries
{
    public class SelectQuery
    {

        ATMDBContext _dbContext = new ATMDBContext();
        public SelectQuery(ATMDBContext dbContext)
        {
            _dbContext = dbContext; 
        }

        public async Task<IEnumerable<Account>> SelectCustomerAsync(string accountNumber, string queryString)
        {
            IList<Account> accounts = new List<Account>();
            try
            {
                using SqlConnection sqlConn = await _dbContext.OpenConnection();

                 using SqlCommand command = new SqlCommand(queryString, sqlConn);

                command.Parameters.AddWithValue("@AccountNumber", accountNumber);


                using SqlDataReader dataReader = await command.ExecuteReaderAsync();

                while (await dataReader.ReadAsync())
                {
                    accounts.Add(new Account
                    {
                        UserID = (int)dataReader["UserID"],
                        FullName = dataReader["FullName"].ToString(),
                        AccountNumber = dataReader["AccountNumber"].ToString(),
                        CardNumber = dataReader["CardNumber"].ToString(),
                        Pin = dataReader["Pin"].ToString(),
                        Balance = (decimal)dataReader["Balance"],
                        CreatedTime = (DateTime)dataReader["CreationTimeStamp"]

                    });
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            
            return accounts;

            
        }

        public async Task<IEnumerable<Account>> SelectCustomerAsync(string cardNumber, string pin, string queryString)
        {
            IList<Account> accounts = new List<Account>();
            try
            {
                SqlConnection sqlConn = await _dbContext.OpenConnection();

                using SqlCommand command = new SqlCommand(queryString, sqlConn);

                command.Parameters.AddWithValue("@CardNumber", cardNumber);
                command.Parameters.AddWithValue("@Pin", pin);


                using SqlDataReader dataReader = await command.ExecuteReaderAsync();

                while (await dataReader.ReadAsync())
                {
                    accounts.Add(new Account
                    {
                        UserID = (int)dataReader["UserID"],
                        FullName = dataReader["FullName"].ToString(),
                        AccountNumber = dataReader["AccountNumber"].ToString(),
                        CardNumber = dataReader["CardNumber"].ToString(),
                        Pin = dataReader["Pin"].ToString(),
                        Balance = (decimal)dataReader["Balance"],
                        CreatedTime = (DateTime)dataReader["CreationTimeStamp"]

                    });
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            
            return accounts;


        }
    }
}

