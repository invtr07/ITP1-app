using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MoneyMate.ViewModels
{
    public class DoughnutModel
    {
        public string Category { get; set; }
        public double Amount { get; set; }
    }

    public class SpendingCategory : BindableObject
    {
        public ObservableCollection<DoughnutModel> ExpenseDataCollection { get; set; }

        public SpendingCategory()
        {
            ExpenseDataCollection = new ObservableCollection<DoughnutModel>
            {
                // Mock data for line chart. This will be replaced with database data in the future.
                new DoughnutModel { Category = "Food", Amount = 790 },
                new DoughnutModel { Category = "Utilities", Amount = 450 },
                new DoughnutModel { Category = "Transport", Amount = 300 },
                new DoughnutModel { Category = "Others", Amount = 250 }
            };
        }
    }
}

