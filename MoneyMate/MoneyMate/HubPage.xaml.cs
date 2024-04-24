using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MoneyMate
{	
	public partial class HubPage : ContentPage
	{	
		public HubPage ()
		{
			InitializeComponent ();
            
            userName.Text = App.savedName + " " + App.savedSurname;
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            
            Navigation.PushAsync(new MoneyMate.ParentPages.CashflowTabs());
        }

        void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MoneyMate.ParentPages.SavingMoneyPotTabs());

        }

        void Button_Clicked_2(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MoneyMate.DebtsControlPage());
        }

        void Button_Clicked_3(System.Object sender, System.EventArgs e)
        {
            App.savedID = "";
            App.savedName = "";
            App.savedSurname = "";
            App.savedTotalInterest = 0;
            App.income = 0;
            App.expenses = 0;
            App.persCurrStartingBalance = 0;
            App.personalCurrentBalance = 0;
            App.savingsStartingBalance1 = 0;
            App.savingsStartingBalance2 = 0;
            App.creditStartingBalance1 = 0;
            App.creditStartingBalance2 = 0;
            App.dbConnection.Close();
            Navigation.PopAsync();
           
        }

    }
}

