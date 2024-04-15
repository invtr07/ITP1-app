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

	        
	        
			MyTabbedPage p = new MyTabbedPage();
			Navigation.PushAsync(p);
        }
        //
        // private void InitializeDatabaseConnection()
        // {
	       //  string connectionString = "server=localhost;user=username;password=password;database=mydatabase";
	       //  conn = new MySqlConnection(connectionString);
	       //  conn.Open();
        // }
        
    }
}

