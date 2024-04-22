using System;
using System.Collections.Generic;
using Syncfusion;
using Xamarin.Forms;
using MoneyMate.ViewModels;
using System.Threading.Tasks;
using MoneyMate.DatabaseAccess;
using Syncfusion.XForms.Buttons;
using SelectionChangedEventArgs = Syncfusion.XForms.Buttons.SelectionChangedEventArgs;

namespace MoneyMate.ParentPages
{	
	public partial class EarningsSpendings : ContentPage
	{
        
        public ViewModels.SpendingCategory DoughNutChartModel { get; set; }
        public ViewModels.NetCashFlow LineChartModel { get; set; }
        public ViewModels.AllTransactions TransactionsList { get; set; }
        
        public enum TimeGrouping
        {
            Weekly,
            Monthly
        }

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
            int productID1 = 101;
            int productID2 = 102;
            
            try
            {
                var newTransactions = await dbService.LoadTransactions(productID1,productID2);
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
        
        private async Task LoadNetCashFlowData(TimeGrouping timeGrouping)
        {
            DatabaseControl dbService = new DatabaseControl();
            int productID1 = 101;
            int productID2 = 102;
    
            try
            {
                // Load data based on the selected timeframe
                var newLineChartData = timeGrouping == TimeGrouping.Weekly
                    ? await dbService.LoadNetCashFlowWeekly(productID1, productID2)
                    : await dbService.LoadNetCashFlowMonthly(productID1, productID2);

                if (LineChartModel.LineChartData != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        LineChartModel.LineChartData.Clear();
                        foreach (var datapoint in newLineChartData)
                        {
                            LineChartModel.LineChartData.Add(datapoint);
                        }
                    });
                }    
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", $"{ex.Message}", "OK");
            }

	        
        }
        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedSegment = (sender as SfSegmentedControl).SelectedIndex;
            await LoadNetCashFlowData(selectedSegment == 0 ? TimeGrouping.Weekly : TimeGrouping.Monthly);
        }
        
        
        
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadTopSpendingCategories();
            await LoadNetCashFlowData(TimeGrouping.Weekly);
            await LoadDataAsync();
        }

        void SeeAllTapped(System.Object sender, System.EventArgs e)
        {
            // Navigation.PushAsync(new AllTransactions(), true);
        }

        
    }
}

