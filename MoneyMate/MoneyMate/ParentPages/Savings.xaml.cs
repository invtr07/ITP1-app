using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using MoneyMate.DatabaseAccess;
using Syncfusion.XForms.Buttons;
using SelectionChangedEventArgs = Xamarin.Forms.SelectionChangedEventArgs;


namespace MoneyMate.ParentPages
{	
	public partial class Savings : ContentPage
	{	
		public ViewModels.NetCashFlow LineChartModel { get; private set; }
		public ViewModels.AllTransactions TransactionsList { get; private set; }
		
		public enum TimeGrouping
		{
			Weekly,
			Monthly
		}


		public Savings ()
		{
			InitializeComponent ();
			
			LineChartModel = new ViewModels.NetCashFlow();
			TransactionsList = new ViewModels.AllTransactions();

			BindingContext = this;
		}
        private async Task LoadDataAsync()
        {
            DatabaseControl dbService = new DatabaseControl();
            int productID1 = 103;
            int productID2 = 104;

            try
            {
                var newTransactions = await dbService.LoadTransactions( productID1, productID2);
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
        
        private async Task LoadNetCashFlowData(TimeGrouping timeGrouping)
        {
	        DatabaseControl dbService = new DatabaseControl();
	        int productID1 = 103;
	        int productID2 = 104;
    
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
        
        private async void OnSelectionChanged(object sender, Syncfusion.XForms.Buttons.SelectionChangedEventArgs selectionChangedEventArgs)
        {
	        var selectedSegment = (sender as SfSegmentedControl).SelectedIndex;
	        await LoadNetCashFlowData(selectedSegment == 0 ? TimeGrouping.Weekly : TimeGrouping.Monthly);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadDataAsync();
            await LoadNetCashFlowData(TimeGrouping.Weekly);
        }

        void SeeAllTapped(System.Object sender, System.EventArgs e)
		{
			// Navigation.PushAsync(new AllTransactions(), true);
		}
	}
}

