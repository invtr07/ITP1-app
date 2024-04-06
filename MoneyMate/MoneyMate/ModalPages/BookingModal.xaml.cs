using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MoneyMate.ModalPages
{	
	public partial class BookingModal : ContentPage
	{	
		public BookingModal ()
		{
			InitializeComponent ();
		}

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}

