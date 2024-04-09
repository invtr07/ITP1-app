using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MoneyMate
{	
	public partial class HelpPage : ContentPage
	{	
		public HelpPage ()
		{
			InitializeComponent ();
		}

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
			

        }

        void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new ModalPages.BookingModal());

        }
    }
}

