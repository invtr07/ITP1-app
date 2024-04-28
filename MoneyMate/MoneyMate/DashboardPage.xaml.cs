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

			userName.Text = $"Hello, {App.savedName} !";
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

        private async void LoadOverDraftDetails()
        {
	        DatabaseControl dbService = new DatabaseControl();

	        try
	        {
		        await dbService.LoadArrOver();
		        await dbService.LoadUnArrOver();
		        
		        if (App.arrangedOver.Count != 0)
		        {
			        arrangedOver.Text = $"{App.arrangedOver[0].productName}";
			        arrangInterest.Text = $"{App.arrangedOver[0].dailyInterestRate.ToString("F2")}%";
			        arrangLimit.Text = $"Limit: £{App.arrangedOver[0].interestFreeOverdraftLimit}";
		        }
		        else
		        {
			        label1.IsVisible = false;
			        arrangedOver.IsVisible = false;
			        arrangInterest.IsVisible = false;
			        arrangLimit.IsVisible = false;
		        }
			
		        if (App.unarrangedOver.Count != 0)
		        {
			        unarrangedOver.Text = $"{App.unarrangedOver[0].productName}";
			        unarrangInterest.Text = $"{App.unarrangedOver[0].dailyInterestRate.ToString("F2")}%";
			        unarrangLimit.Text = $"Limit: £{App.unarrangedOver[0].interestFreeOverdraftLimit}";
		        }
		        else
		        {
			        label2.IsVisible = false;
			        unarrangedOver.IsVisible = false;
			        unarrangInterest.IsVisible = false;
			        unarrangLimit.IsVisible = false;
		        }
	        }
	        catch(Exception ex)
	        {
		        DisplayAlert("Error", $"{ex.Message}", "OK");
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
	        LoadOverDraftDetails();
	        await LoadTopSpendingCategories();
	        await LoadNetCashFlowData(TimeGrouping.Weekly);
        }
        
        
	}
}

