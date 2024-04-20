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
            Navigation.PopAsync();



        }

    }
}

