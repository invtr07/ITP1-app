using System;
using System.Collections.Generic;
using Syncfusion;
using Xamarin.Forms;
using MoneyMate.ViewModels;
using System.Threading.Tasks;
using MoneyMate.DatabaseAccess;

namespace MoneyMate.ParentPages
{	
	public partial class EarningsSpendings : ContentPage
	{
        
        public ViewModels.SpendingCategory DoughNutChartModel { get; set; }
        public ViewModels.NetCashFlow LineChartModel { get; set; }
        public ViewModels.AllTransactions TransactionsList { get; set; }

        public EarningsSpendings ()
		{
			InitializeComponent ();
            
            LineChartModel = new ViewModels.NetCashFlow();
            TransactionsList = new ViewModels.AllTransactions();
            DoughNutChartModel = new ViewModels.SpendingCategory();
            BindingContext = this;
            
        }
        private async Task LoadDataAsync()
        {
            DatabaseControl dbService = new DatabaseControl();

            try
            {
                var newTransactions = await dbService.LoadTransactions();
                if (TransactionsList.Transactions != null)
                {
                    Device.BeginInvokeOnMainThread(() => 
                    {
                        TransactionsList.Transactions.Clear();
                        foreach (var transaction in newTransactions)
                        {
                            TransactionsList.Transactions.Add(transaction);
                        }
                    });
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", $"{ex.Message}", "OK");
            }
            
        }

        private async Task LoadTopSpendingCategories()
        {
            DatabaseControl dbService = new DatabaseControl();
            
            try
            {
                var newCategories = await dbService.LoadTopSpendingCategories();
                if (DoughNutChartModel.ExpenseDataCollection != null)
                {
                    Device.BeginInvokeOnMainThread(() => 
                    {
                        DoughNutChartModel.ExpenseDataCollection.Clear();
                        foreach (var category in newCategories)
                        {
                            DoughNutChartModel.ExpenseDataCollection.Add(category);
                        }
                    });
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", $"{ex.Message}", "OK");
            }
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadTopSpendingCategories();
            await LoadDataAsync();
        }

        void SeeAllTapped(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AllTransactions(), true);
        }
    }
}

