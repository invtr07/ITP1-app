using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MoneyMate.ViewModels;
using Syncfusion.DataSource.Extensions;

namespace MoneyMate.DatabaseAccess
{

    
    public class DatabaseControl
    {
        // public MySqlConnection localConnection = App.dbConnection;
        public string connString = "server=****;user=***;password=***;database=***";

        public async Task<ObservableCollection<TransactionsModel>> LoadTransactions(int product1, int product2, int limit)
        {
            ObservableCollection<TransactionsModel> transactions = new ObservableCollection<TransactionsModel>();

            string query = $@"
                SELECT transaction_Date, transaction_Time, transaction_Amount, transaction_Category, transaction_Reference
                FROM transaction
                WHERE account_ID IN (
                    SELECT account_ID FROM account WHERE customer_ID = @customerID AND product_ID IN ({product1},{product2} 
                ))
                ORDER BY transaction_Date, transaction_Time DESC 
                LIMIT {limit};";

            using (var command = new MySqlCommand(query, App.dbConnection))
            {
                App.dbConnection.Open();

                command.Parameters.AddWithValue("@customerID", App.savedID);

                using (var reader = command.ExecuteReader())
                {
                    // if(reader == null) Console.WriteLine("error");
                    while (reader.Read())
                    {
                        var date = reader.GetDateTime(reader.GetOrdinal("transaction_Date"));
                        var time = reader.GetTimeSpan(reader.GetOrdinal("transaction_Time"));
                        var dateTime = new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes
                            , time.Seconds);

                        TransactionsModel transaction = new TransactionsModel
                        {
                            //AccountID = reader["account_ID"].ToString(),
                            DT = dateTime, Amount = decimal.Parse(reader["transaction_Amount"].ToString())
                            , Category = reader["transaction_Category"].ToString()
                            , Treference = reader["transaction_Reference"].ToString()
                        };
                        transactions.Add(transaction);
                    }
                }

                await App.dbConnection.CloseAsync();
            }

            return transactions;
        }


        public async Task<ObservableCollection<DoughnutModel>> LoadTopSpendingCategories()
        {
            ObservableCollection<DoughnutModel> allCategories = new ObservableCollection<DoughnutModel>();
            decimal totalAmount = 0M;

            string query =
                @"SELECT t.transaction_Category, SUM(t.transaction_Amount) AS total FROM transaction t WHERE t.transaction_Date BETWEEN DATE_SUB(NOW(), INTERVAL 1 YEAR) AND NOW() AND t.account_ID IN ( SELECT a.account_ID FROM account a WHERE a.customer_ID = @customerID AND a.product_ID IN (@productID1, @productID2 ) ) AND t.transaction_Amount < 0 GROUP BY t.transaction_Category ORDER BY total ASC;";

            using (var command = new MySqlCommand(query, App.dbConnection))
            {
                App.dbConnection.Open();

                command.Parameters.AddWithValue("@customerID", App.savedID);
                command.Parameters.AddWithValue("@productID1", 101);
                command.Parameters.AddWithValue("@productID2", 102);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DoughnutModel spendingCategory = new DoughnutModel()
                        {
                            Category = reader["transaction_Category"].ToString()
                            , Amount = decimal.Parse(reader["total"].ToString()),

                        };
                        allCategories.Add(spendingCategory);
                    }
                }

                await App.dbConnection.CloseAsync();
            }

            totalAmount = allCategories.Sum(c => c.Amount);

            // Calculate the percentage for each category in allCategories
            allCategories.ForEach(c => c.Percentage = Convert.ToDouble((c.Amount / totalAmount) * 100M));

            // Select top 5 categories since they are already sorted
            var topCategories = allCategories.Take(5).ToList();

            // Calculate the 'Others' category amount and percentage
            decimal othersAmount = allCategories.Skip(5).Sum(c => c.Amount);
            // The percentage for 'Others' is calculated as 100 minus the sum of the top 5 percentages
            double othersPercentage = 100 - topCategories.Sum(c => c.Percentage);

            // Add 'Others' category if there are more than 5 categories
            if (allCategories.Count > 5)
            {
                topCategories.Add(new DoughnutModel
                    { Category = "Others", Amount = othersAmount, Percentage = othersPercentage });
            }

            return new ObservableCollection<DoughnutModel>(topCategories);
        }

        public async Task<ObservableCollection<LineChartModel>> LoadNetCashFlowWeekly(int product1, int product2)
        {
            // Use the weekly query
            string query =
                $@"SELECT SUM(transaction_Amount) AS Total, YEAR(transaction_Date) AS Year, WEEK(transaction_Date) AS Week FROM transaction t WHERE t.account_ID IN ( SELECT account_ID FROM account WHERE customer_ID = @customerID AND product_ID IN ({product1}, {product2}) ) GROUP BY Year, Week ORDER BY Year ASC, Week ASC;";

            return await LoadNetCashFlow(query, "Week");
        }

// Loads monthly data from the database
        public async Task<ObservableCollection<LineChartModel>> LoadNetCashFlowMonthly(int product1, int product2)
        {
            // Use the monthly query
            string query =
                $@"SELECT SUM(transaction_Amount) AS Total, YEAR(transaction_Date) AS Year, MONTH(transaction_Date) AS Month FROM transaction t WHERE t.account_ID IN ( SELECT account_ID FROM account WHERE customer_ID = @customerID AND product_ID IN ({product1}, {product2}) ) GROUP BY Year, Month ORDER BY Year ASC, Month ASC;";

            return await LoadNetCashFlow(query, "Month");
        }

        public async Task<ObservableCollection<LineChartModel>> LoadNetCashFlow(string query, string timeFrameColumn)
        {
            ObservableCollection<LineChartModel> netCashFlow = new ObservableCollection<LineChartModel>();

            using (var command = new MySqlCommand(query, App.dbConnection))
            {
                App.dbConnection.Open();
                decimal amount;

                command.Parameters.AddWithValue("@customerID", App.savedID);

                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        decimal transactionAmount = decimal.Parse(reader["Total"].ToString());

                        LineChartModel cashFlow = new LineChartModel
                        {
                            Amount = transactionAmount
                            , TimeFrame = int.Parse(reader[timeFrameColumn].ToString())
                            , Year = int.Parse(reader["Year"].ToString()),
                        };

                        netCashFlow.Add(cashFlow);
                    }

                }

                await App.dbConnection.CloseAsync();
            }

            return netCashFlow;

        }

        public async Task<decimal> LoadCurrentBalance()
        {
            decimal Balance = 0;

            using (var command =
                   new MySqlCommand(
                       $@"SELECT transaction_Amount FROM transaction WHERE account_ID IN (SELECT account_ID FROM account WHERE customer_ID = @customerID AND product_ID IN (101, 102)) ;"
                       , App.dbConnection))
            {
                App.dbConnection.Open();

                command.Parameters.AddWithValue("@customerID", App.savedID);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Balance += decimal.Parse(reader["transaction_Amount"].ToString());
                    }

                    App.personalCurrentBalance = App.persCurrStartingBalance + Balance;
                }

                await App.dbConnection.CloseAsync();

            }

            return Balance;
        }

        public async Task<decimal> LoadSavingBalance1()
        {

            decimal TotalBalance = 0;

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                using (var command =
                       new MySqlCommand(
                           $@"SELECT SUM(transaction_Amount) AS Total FROM transaction WHERE account_ID IN (SELECT account_ID FROM account WHERE customer_ID = @customerID AND product_ID IN (103)) ;"
                           , conn))
                {
                    // App.dbConnection.Open();

                    command.Parameters.AddWithValue("@customerID", App.savedID);

                    using (var reader = command.ExecuteReader())
                    {
                        if (await reader.ReadAsync())
                        {
                            // Safely attempt to parse the decimal
                            if (reader["Total"] != DBNull.Value &&
                                decimal.TryParse(reader["Total"].ToString(), out decimal Balance))
                            {
                                TotalBalance = App.savingsStartingBalance1 + Balance;
                            }
                            else
                            {
                                TotalBalance = App.savingsStartingBalance1;
                            }
                        }
                    }
                }

                await conn.CloseAsync();

                // await App.dbConnection.CloseAsync();
            }

            return TotalBalance;
        }

        public async Task<decimal> LoadSavingBalance2()
        {
            decimal TotalBalance=0;

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                using (var command =
                       new MySqlCommand(
                           $@"SELECT SUM(transaction_Amount) AS Total FROM transaction WHERE account_ID IN (SELECT account_ID FROM account WHERE customer_ID = @customerID AND product_ID IN (104)) ;"
                           , conn))
                {
                    // App.dbConnection.Open();

                    command.Parameters.AddWithValue("@customerID", App.savedID);

                    using (var reader = command.ExecuteReader())
                    {
                        if(await reader.ReadAsync())
                        {
                            if (reader["Total"] != DBNull.Value &&
                                                        decimal.TryParse(reader["Total"].ToString(), out decimal Balance))
                                                    {
                                                        TotalBalance = App.savingsStartingBalance2 + Balance;
                                                    }
                                                    else
                                                    {
                                                        TotalBalance = App.savingsStartingBalance2;
                                                    }
                        }
                        
                    }

                }

                // await App.dbConnection.CloseAsync();
                await conn.CloseAsync();

            }

            return TotalBalance;
        }
    



        public async Task<decimal> LoadCreditBalance1()
    {

        decimal TotalBalance = 0;

        using (MySqlConnection conn = new MySqlConnection(connString))
        {
            conn.Open();
            using (var command =
                   new MySqlCommand(
                       $@"SELECT SUM(transaction_Amount) AS Total FROM transaction WHERE account_ID IN (SELECT account_ID FROM account WHERE customer_ID = @customerID AND product_ID IN (105)) ;"
                       , conn))
            {
                // App.dbConnection.Open();

                command.Parameters.AddWithValue("@customerID", App.savedID);

                using (var reader = command.ExecuteReader())
                {
                    // Safely attempt to parse the decimal
                    if(await reader.ReadAsync())
                    {
                        if (reader["Total"] != DBNull.Value &&
                                                decimal.TryParse(reader["Total"].ToString(), out decimal Balance))
                                            {
                                                TotalBalance = App.creditStartingBalance1 + Balance;
                                            }
                                            else
                                            {
                                                TotalBalance = App.creditStartingBalance1;
                                            }
                    }
                }
                await conn.CloseAsync();

            }

            return TotalBalance;
            
        }
    }



        public async Task<decimal> LoadCreditBalance2()
        {
          
            decimal TotalBalance = 0;

            using(MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                using (var command =
                       new MySqlCommand(
                           $@"SELECT SUM(transaction_Amount) AS Total FROM transaction WHERE account_ID IN (SELECT account_ID FROM account WHERE customer_ID = @customerID AND product_ID IN (106)) ;"
                           , conn))
                {
                    // App.dbConnection.Open();

                    command.Parameters.AddWithValue("@customerID", App.savedID);

                    using (var reader = command.ExecuteReader())
                    {
                        if(await reader.ReadAsync())
                                            {
                                                if (reader["Total"] != DBNull.Value &&
                                                                        decimal.TryParse(reader["Total"].ToString(), out decimal Balance))
                                                                    {
                                                                        TotalBalance = App.creditStartingBalance2 + Balance;
                                                                    }
                                                                    else
                                                                    {
                                                                        TotalBalance = App.creditStartingBalance2;
                                                                    }
                                            }
                    }
                }
                await conn.CloseAsync();
            }
            return TotalBalance;
        }

        public async Task LoadArrOver()
        {
            App.arrangedOver = new List<App.OverdraftDetails>();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                using (var command =
                       new MySqlCommand(
                           $@"SELECT product_ID, product_Name,daily_InterestRate, interest_FreeOverdraftLimit FROM product WHERE product_ID IN (SELECT a.product_ID FROM account a WHERE a.customer_ID = @customerID AND a.product_ID IN (107)) ;"
                           , conn))
                {
                    command.Parameters.AddWithValue("@customerID", App.savedID);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            App.OverdraftDetails overdraftDetails= new App.OverdraftDetails
                                {
                                    productName  = reader["product_Name"].ToString(),
                                    dailyInterestRate = decimal.Parse(reader["daily_InterestRate"].ToString()),
                                    interestFreeOverdraftLimit = decimal.Parse(reader["interest_FreeOverdraftLimit"].ToString())
                                };
                            
                            App.arrangedOver.Add(overdraftDetails);
                        }
                    }
                }

                await conn.CloseAsync();
            }
        }
        public async Task LoadUnArrOver()
        {
            App.unarrangedOver = new List<App.OverdraftDetails>();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                using (var command =
                       new MySqlCommand(
                           $@"SELECT product_ID, product_Name,daily_InterestRate, interest_FreeOverdraftLimit FROM product WHERE product_ID IN (SELECT a.product_ID FROM account a WHERE a.customer_ID = @customerID AND a.product_ID IN (108)) ;"
                           , conn))
                {
                    command.Parameters.AddWithValue("@customerID", App.savedID);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            App.OverdraftDetails overdraftDetails= new App.OverdraftDetails
                            {
                                productName  = reader["product_Name"].ToString(),
                                dailyInterestRate = decimal.Parse(reader["daily_InterestRate"].ToString()),
                                interestFreeOverdraftLimit = decimal.Parse(reader["interest_FreeOverdraftLimit"].ToString())
                            };
                            
                            App.unarrangedOver.Add(overdraftDetails);
                        }
                    }
                }

                await conn.CloseAsync();
            }
        }
    }
}


