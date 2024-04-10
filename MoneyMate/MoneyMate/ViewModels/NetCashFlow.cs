using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MoneyMate.ViewModels
{
    public class LineChartModel
    {
        public string Date { get; set; }
        public double Amount { get; set; }
    }

    public class NetCashFlow : BindableObject
    {
        public ObservableCollection<LineChartModel> LineChartData { get; set; } // Line chart data collection

        public NetCashFlow()
        {
            
            LineChartData = new ObservableCollection<LineChartModel>
        {
            // Mock data for line chart. This will be replaced with database data in the future.
            new LineChartModel { Date = "Aug", Amount = 500 },
            new LineChartModel { Date = "Sep", Amount = 800 },
            new LineChartModel { Date = "Oct", Amount = 1200 },
        };
        }
    }

}

