using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using MoneyMate.DatabaseAccess;

namespace MoneyMate
{
    public partial class DashboardPage : ContentPage
	{
        public ViewModels.NetCashFlow LineChartModel { get; private set; }
        public ViewModels.SpendingCategory DoughNutChartModel { get; private set; }

        public DashboardPage ()
		{
			InitializeComponent ();
			
			userName.Text = App.savedName;

            LineChartModel = new ViewModels.NetCashFlow();
            DoughNutChartModel = new ViewModels.SpendingCategory();

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
        protected override async void OnAppearing()
        {
	        base.OnAppearing();
	        await LoadTopSpendingCategories();
        }
	}
}

