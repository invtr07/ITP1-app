using MySqlConnector;
using System;
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
        public async Task<ObservableCollection<TransactionsModel>> LoadTransactions(int product1, int product2)
        {
            ObservableCollection<TransactionsModel> transactions = new ObservableCollection<TransactionsModel>();

            string query = $@"
                SELECT transaction_Date, transaction_Time, transaction_Amount, transaction_Category, transaction_Reference
                FROM transaction
                WHERE account_ID IN (
                    SELECT account_ID FROM account WHERE customer_ID = @customerID AND product_ID IN ({product1},{product2} 
                ))
                ORDER BY transaction_Date, transaction_Time DESC 
                LIMIT 70;";

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

                App.dbConnection.Close();
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

                App.dbConnection.Close();
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

                command.Parameters.AddWithValue("@customerID", App.savedID);
                

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        LineChartModel cashFlow = new LineChartModel
                        {
                            Amount = decimal.Parse(reader["Total"].ToString())
                            , TimeFrame = int.Parse(reader[timeFrameColumn].ToString())
                            , Year = int.Parse(reader["Year"].ToString()),
                        };

                        netCashFlow.Add(cashFlow);
                    }
                }

                App.dbConnection.Close();
            }

            return netCashFlow;


        }
        
        
        public async Task<decimal> GetTotalTransactionAmount()
        {
            string query = $@"SELECT SUM(transaction_Amount) AS Total 
                      FROM transaction t 
                      WHERE t.account_ID IN 
                          (SELECT account_ID 
                           FROM account 
                           WHERE customer_ID = {App.savedID} 
                           AND product_ID IN (103, 104))
                      AND t.transaction_Reference = 'Lloyds Bank';";

            using (MySqlCommand cmd = new MySqlCommand(query, App.dbConnection))
            {
                App.dbConnection.Open();
                var result = await cmd.ExecuteScalarAsync();
                App.dbConnection.Close(); // Make sure to close the connection

                return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
            }
        }


    }
}

