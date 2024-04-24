using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Threading.Tasks;
using MoneyMate.DatabaseAccess;
using MoneyMate.ViewModels;
using Syncfusion.XForms.Buttons;
using SelectionChangedEventArgs = Syncfusion.XForms.Buttons.SelectionChangedEventArgs;
using Syncfusion.XForms.Buttons;

namespace MoneyMate
{
    public partial class DashboardPage : ContentPage
	{
        public ViewModels.NetCashFlow LineChartModel { get; private set; }
        public ViewModels.SpendingCategory DoughNutChartModel { get; private set; }
        public InterestEarnedModel totalInterestEarned;
        
        public enum TimeGrouping
        {
	        Weekly,
	        Monthly
        }
        
        public DashboardPage ()
		{
			InitializeComponent ();
			
			userName.Text = App.savedName;
			PersBalanceLabel.Text = $"£ {App.personalCurrentBalance}";
			// ExpenseLabel.Text = $"£ {App.expenses}";
			IncomeLabel.Text = $"£ {App.income}";
			TotalInterestLabel.Text = $"+ £{App.savedTotalInterest}";
			InterestPaidLabel.Text = $"£{App.paidInterest}";
			

            LineChartModel = new ViewModels.NetCashFlow();
            DoughNutChartModel = new ViewModels.SpendingCategory();
            totalInterestEarned = new ViewModels.InterestEarnedModel();
            
            BindingContext = this;
            
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

        private async void LoadCurrentBalance()
        {
	        decimal balance;
	        DatabaseControl dbService = new DatabaseControl();
			try
	        {
		        balance = await dbService.LoadCurrentBalance();
		        ExpenseLabel.Text = $"${balance}";
		        App.expenses = balance;
		        PersBalanceLabel.Text = $"£ {App.personalCurrentBalance}";
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
	        LoadCurrentBalance();
	        await LoadTopSpendingCategories();
	        await LoadNetCashFlowData(TimeGrouping.Weekly);
        }
        
        
	}
}

