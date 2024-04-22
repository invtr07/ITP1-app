using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using MoneyMate.DatabaseAccess;
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
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"{ex.Message}", "OK");
            }

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadDataAsync();
        }

        void SeeAllTapped(System.Object sender, System.EventArgs e)
		{
			
		}
	}
}

