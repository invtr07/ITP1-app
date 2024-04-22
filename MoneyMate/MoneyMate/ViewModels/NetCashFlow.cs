using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MoneyMate.ViewModels
{
    public class LineChartModel
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        
        public int TimeFrame { get; set; } // Represents the week number
        public int Year { get; set; }
    }

    public class NetCashFlow : BindableObject
    {
        private ObservableCollection<LineChartModel> _lineChartData;
        public ObservableCollection<LineChartModel> LineChartData
        {
            get => _lineChartData;
            set
            {
                _lineChartData = value;
                OnPropertyChanged();
                
            }
        } // Line chart data collection

        public NetCashFlow()
        {
            LineChartData = new ObservableCollection<LineChartModel>();
        }
    }

}


