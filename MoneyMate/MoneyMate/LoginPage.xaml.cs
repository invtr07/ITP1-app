using System;
using System.Collections.Generic;
using Xamarin.Forms;
using MySqlConnector;

namespace MoneyMate
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var userID = enteredID.Text;
            var pass = enteredPass.Text;

            if (!string.IsNullOrWhiteSpace(userID) && !string.IsNullOrWhiteSpace(pass))
            {
                try
                {
                    App.dbConnection.Open();

                    using (var cmd = new MySqlCommand(
                               "SELECT customer_ID, name, surname, password, monthly_Income FROM customer WHERE customer_ID = @userID"
                               , App.dbConnection))
                    {
                        cmd.Parameters.AddWithValue("@userID", userID);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string dbPassword = reader["password"].ToString();
                                if (dbPassword == pass) // In real applications, compare password hashes
                                {
                                    // User authenticated, save user details
                                    App.savedName = reader["name"].ToString();
                                    App.savedSurname = reader["surname"].ToString();
                                    App.savedID = reader["customer_ID"].ToString();
                                    App.income = reader["monthly_Income"] != DBNull.Value ? Convert.ToDecimal(reader["monthly_Income"]) : 0;

                                    // Navigation to the tabbed page after successful login
                                }
                                else
                                {
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        await DisplayAlert("Login Failed", "Incorrect username or password.", "OK");
                                    });
                                }
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await DisplayAlert("Login Failed", "User ID not found.", "OK");
                                });
                            }
                        }
                        
                        using (MySqlCommand command = new MySqlCommand($@"SELECT SUM(transaction_Amount) AS Total 
                                                                  FROM transaction t 
                                                                  WHERE t.account_ID IN 
                                                                      (SELECT account_ID 
                                                                       FROM account 
                                                                       WHERE customer_ID = {App.savedID} 
                                                                       AND product_ID IN (103, 104))
                                                                  AND t.transaction_Reference = 'Lloyds Bank';",App.dbConnection))
                        {
                            using(var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    App.savedTotalInterest = reader["Total"] != DBNull.Value ? Convert.ToDecimal(reader["Total"]) : 0;
                                }
                            }
                        }
                        
                        
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await Navigation.PushAsync(new MyTabbedPage());
                        });
                        
                    }
                    
                    
                }
                catch (MySqlException ex)
                {
                    // Handle database related exceptions
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await DisplayAlert("Login Failed", $"Database connection error: {ex.Message}", "OK");
                    });
                }
                catch (Exception ex)
                {
                    // Handle other types of exceptions
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await DisplayAlert("Login Failed", $"Unexpected error occurred: {ex.Message}", "OK");
                    });
                }
                finally
                {
                    // Ensure the connection is closed even if an exception occurs
                    if (App.dbConnection != null && App.dbConnection.State == System.Data.ConnectionState.Open)
                    {
                        App.dbConnection.Close();
                    }
                }
            }
            else
            {
                DisplayAlert("Login Failed", "Please enter both user ID and password.", "OK");
            }
        }


    }
}
	

