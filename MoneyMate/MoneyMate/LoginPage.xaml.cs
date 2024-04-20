using System;
using System.Collections.Generic;
using Xamarin.Forms;
using MySqlConnector;
namespace MoneyMate
{	
	public partial class LoginPage : ContentPage
	{	
		public LoginPage ()
		{
			InitializeComponent ();
		}

		void Button_Clicked(System.Object sender, System.EventArgs e)
		{
			var userID = enteredID.Text;
			var pass = enteredPass.Text;

			if (!string.IsNullOrWhiteSpace(userID) && !string.IsNullOrWhiteSpace(pass))
			{
				//Navigation.PushAsync(new MyTabbedPage());

                MySqlCommand cmd = new MySqlCommand($"SELECT customer_ID, name, surname, password FROM customer WHERE customer_ID = {userID}", App.dbConnection);
                MySqlDataReader reader = cmd.ExecuteReader();
                
                    if (reader.Read())
                    {
                        string dbPassword = reader["password"].ToString();
                        if (dbPassword == pass)
                        {
                            App.savedName = reader["name"].ToString();
                            App.savedSurname = reader["surname"].ToString();
                            App.savedID = reader["customer_ID"].ToString();

                            if (App.savedID != null && App.savedName != null && App.savedSurname != null)
                            {
                                Navigation.PushAsync(new MyTabbedPage());
                                reader.Close();
                            }
                            else
                            {
                                DisplayAlert("Login Failed", "Data retrieval failed", "OK");
                            }
                        }
                        else
                        {
                            DisplayAlert("Login Failed", "Incorrect username or password.", "OK");
                        }
                    }
                    else
                    {
                        DisplayAlert("Login Failed", "Connection failed.", "OK");
                    }
                }
            }
   	
		public class LoginData
        {
	        public string userID { get; set; }
	        public string userName { get; set; }
        }
    }
}

