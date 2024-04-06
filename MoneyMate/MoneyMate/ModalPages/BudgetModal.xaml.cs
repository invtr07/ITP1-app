using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MoneyMate.ModalPages
{	
	public partial class BudgetModal : ContentPage
	{	
		public BudgetModal ()
		{
			InitializeComponent ();
		}

        void SubmitButton_Clicked(System.Object sender, System.EventArgs e)
        {
			Navigation.PopModalAsync();

        }
 

        void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
        }
    }
}

