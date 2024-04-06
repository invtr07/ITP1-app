using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MoneyMate.ParentPages
{	
	public partial class Budget : ContentPage
	{	
		public Budget ()
		{
			InitializeComponent ();
		}

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
			Navigation.PushModalAsync(new ModalPages.BudgetModal());
        }
    }
}

