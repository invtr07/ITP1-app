using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Linq;


namespace MoneyMate.ViewModels
{
    public class DoughnutModel
    {
        public string Category { get; set; }
        public decimal Amount { get; set; }
        
        public double Percentage { get; set; } 
    }

    public class SpendingCategory : BindableObject
    {
        
        private ObservableCollection<DoughnutModel> _expenseDataCollection;
        public ObservableCollection<DoughnutModel> ExpenseDataCollection
        {
            get => _expenseDataCollection;
            set
            {
                _expenseDataCollection = value;
                OnPropertyChanged();
                
            }
        }
        
        public SpendingCategory()
        {
            ExpenseDataCollection = new ObservableCollection<DoughnutModel>();
        }
    }
}

