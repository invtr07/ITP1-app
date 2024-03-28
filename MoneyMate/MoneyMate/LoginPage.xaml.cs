using System;
using System.Collections.Generic;

using Xamarin.Forms;

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
			MyTabbedPage p = new MyTabbedPage();
			Navigation.PushAsync(p);
        }
    }
}

