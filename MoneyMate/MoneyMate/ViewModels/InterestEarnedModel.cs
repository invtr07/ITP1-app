using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MoneyMate.ViewModels
{
    public class TransactionTotal
    {
        public decimal TotalAmount { get; set; }
    }
    
    public class InterestEarnedModel
    {
        private ObservableCollection<TransactionTotal> _transactionTotals;
        public ObservableCollection<TransactionTotal> TransactionTotals
        {
            get => _transactionTotals;
            set
            {
                _transactionTotals = value;
                OnPropertyChanged();
            }
        }

        public InterestEarnedModel()
        {
            TransactionTotals = new ObservableCollection<TransactionTotal>();
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}