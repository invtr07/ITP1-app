using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoneyMate.ViewModels;

namespace MoneyMate.DatabaseAccess
{
    public class DatabaseControl
    {
        private MySqlConnection _connection;

        public DatabaseControl()
        {
            _connection = App.dbConnection;
        }
        
        public async Task<List<TransactionsModel>> GetAllTransactionsAsync()
        {
            var transactions = new List<TransactionsModel>();

            // Make sure the connection is open before executing the command
            if (_connection.State != System.Data.ConnectionState.Open)
                await _connection.OpenAsync();
            
            string query = $"SELECT transaction_Date, transaction_Time,transaction_Amount, transaction_Category, transaction_Reference FROM transaction WHERE account_ID = (SELECT account_ID FROM account WHERE customer_ID = {App.savedID} AND product_ID = 101) ORDER BY transaction_Date, transaction_Time LIMIT 50;";
                
                
            var command = new MySqlCommand(query, _connection);

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var date = reader.GetDateTime(reader.GetOrdinal("transaction_Date"));
                    var time = reader.GetTimeSpan(reader.GetOrdinal("transaction_Time"));
                    var dateTime = new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds);

                
                    transactions.Add(new TransactionsModel
                    {
                        // TransactionID = reader["transaction_Reference"].ToString(),  // Adjusted to the actual queried column
                        Treference = reader["transaction_Reference"].ToString(),
                        DT = dateTime,  // Combining date and time into one string
                        Category = reader["transaction_Category"].ToString(),
                        Amount = reader.GetDecimal(reader.GetOrdinal("transaction_Amount"))
                    });
                }
            }

            return transactions;
        }
    }
}