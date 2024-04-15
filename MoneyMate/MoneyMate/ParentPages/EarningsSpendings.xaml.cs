using System;
using System.Collections.Generic;
using Syncfusion;
using Xamarin.Forms;

namespace MoneyMate.ParentPages
{	
	public partial class EarningsSpendings : ContentPage
	{
        
        public ViewModels.SpendingCategory DoughNutChartModel { get; private set; }
        public ViewModels.NetCashFlow LineChartModel { get; private set; }
        public ViewModels.AllTransactions TransactionsList { get; private set; }

        public EarningsSpendings ()
		{
			InitializeComponent ();
            
            LineChartModel = new ViewModels.NetCashFlow();
            TransactionsList = new ViewModels.AllTransactions();
            DoughNutChartModel = new ViewModels.SpendingCategory();

            BindingContext = this;

        }

        void SeeAllTapped(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AllTransactions(), true);
        }
    }
}

