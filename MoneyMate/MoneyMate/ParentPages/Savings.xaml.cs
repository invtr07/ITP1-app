using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MoneyMate.ParentPages
{	
	public partial class Savings : ContentPage
	{	
		public ViewModels.NetCashFlow LineChartModel { get; private set; }
		public ViewModels.AllTransactions TransactionsList { get; private set; }


		public Savings ()
		{
			InitializeComponent ();
			
			LineChartModel = new ViewModels.NetCashFlow();
			TransactionsList = new ViewModels.AllTransactions();

			BindingContext = this;
		}
		
		void SeeAllTapped(System.Object sender, System.EventArgs e)
		{
			// Navigation.PushAsync(new AllTransactions(), true);
		}
	}
}

