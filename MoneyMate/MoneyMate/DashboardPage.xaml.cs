using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MoneyMate
{
    public partial class DashboardPage : ContentPage
	{
        public ViewModels.NetCashFlow LineChartModel { get; private set; }
        public ViewModels.SpendingCategory DoughNutChartModel { get; private set; }

        public DashboardPage ()
		{
			InitializeComponent ();

            LineChartModel = new ViewModels.NetCashFlow();
            DoughNutChartModel = new ViewModels.SpendingCategory();

            this.BindingContext = this;
        }
	}
}

