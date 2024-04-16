using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MoneyMate
{	
	public partial class DebtsControlPage : ContentPage
	{	
		public ViewModels.NetCashFlow LineChartModel { get; private set; }
		public ViewModels.AllTransactions TransactionsList { get; private set; }
		public DebtsControlPage ()
		{
			InitializeComponent ();
			
			LineChartModel = new ViewModels.NetCashFlow();
			TransactionsList = new ViewModels.AllTransactions();

			BindingContext = this; 
		}
	}
}

