using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Linq;


namespace MoneyMate.ViewModels
{
    public class DoughnutModel
    {
        public string Category { get; set; }
        public double Amount { get; set; }
    }

    public class SpendingCategory : BindableObject
    {
        //public ObservableCollection<DoughnutModel> ExpenseDataCollection { get; set; }
        //need to add calculation of the total amount
        private ObservableCollection<DoughnutModel> _expenseDataCollection;
        public ObservableCollection<DoughnutModel> ExpenseDataCollection
        {
            get => _expenseDataCollection;
            set
            {
                _expenseDataCollection = value;
                //OnPropertyChanged(nameof(ExpenseDataCollection));
                //// Calculate total amount whenever the collection changes.
                //OnPropertyChanged(nameof(TotalAmount));
            }
        }
        public double TotalAmount => ExpenseDataCollection?.Sum(item => item.Amount) ?? 0;


        public SpendingCategory()
        {
            ExpenseDataCollection = new ObservableCollection<DoughnutModel>
            {
                // Mock data for line chart. This will be replaced with database data in the future.
                new DoughnutModel { Category = "Food", Amount = 900 },
                new DoughnutModel { Category = "Utilities", Amount = 450 },
                new DoughnutModel { Category = "Transport", Amount = 300 },
                new DoughnutModel { Category = "Others", Amount = 250 },
                new DoughnutModel { Category = "Others", Amount = 250 }

            };
            ExpenseDataCollection.CollectionChanged += (sender, args) => OnPropertyChanged(nameof(TotalAmount));

        }
    }
}

