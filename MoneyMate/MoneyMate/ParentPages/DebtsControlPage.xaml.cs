using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using MoneyMate.DatabaseAccess;
using MoneyMate.ParentPages;
using Syncfusion.XForms.Buttons;
using SelectionChangedEventArgs = Syncfusion.XForms.Buttons.SelectionChangedEventArgs;

namespace MoneyMate
{	
	public partial class DebtsControlPage : ContentPage
	{	
		public ViewModels.NetCashFlow LineChartModel { get; private set; }
		public ViewModels.AllTransactions TransactionsList { get; private set; }
		
		public enum TimeGrouping
		{
			Weekly,
			Monthly
		}
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
	        int	productID1 = 105;
	        int productID2 = 106;

	        try
	        {
		        var newTransactions = await dbService.LoadTransactions(productID1, productID2, 70);
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
        
        private async void LoadCurrentBalance()
        {
	        decimal balance1;
	        decimal balance2;
		        
	        DatabaseControl dbService = new DatabaseControl();
	        try
	        {
		        balance1 = await dbService.LoadCreditBalance1();
		        balance2 = await dbService.LoadCreditBalance2();
		        CreditAcc1Label.Text = $"£{balance1}";
		        CreditAcc2Label.Text = $"£{balance2}";
	        }
	        catch(Exception ex)
	        {
		        await DisplayAlert("Error", $"{ex.Message}", "OK");
	        }
        }

        private async Task LoadNetCashFlowData(TimeGrouping timeGrouping)
        {
	        DatabaseControl dbService = new DatabaseControl();
	        int productID1 = 105;
	        int productID2 = 106;

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
	        catch (Exception ex)
	        {
		        await DisplayAlert("Error", $"{ex.Message}", "OK");
	        }
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            LoadCurrentBalance();
            await LoadDataAsync();
            await LoadNetCashFlowData(TimeGrouping.Weekly);
        }

        void SeeAllTapped(System.Object sender, System.EventArgs e)
        {
	        Navigation.PushAsync(new AllDebtsTransactions());
        }

		private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var selectedSegment = (sender as SfSegmentedControl).SelectedIndex;
			await LoadNetCashFlowData(selectedSegment == 0 ? TimeGrouping.Weekly : TimeGrouping.Monthly);
		}
		
	}
}

