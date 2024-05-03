using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using MoneyMate.DatabaseAccess;
using Newtonsoft.Json.Serialization;
using MySqlConnector;
using System.Collections.Generic;


namespace MoneyMate.ViewModels
{
    public class TransactionsModel
    {
        // public string TransactionID { get; set; }
        public string AccountID { get; set; }
        public string Treference { get; set; }
        public DateTime DT { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
    }

    public class AllTransactions : BindableObject
    {
        private ObservableCollection<TransactionsModel> _transactions;
        public ObservableCollection<TransactionsModel> Transactions
        {
            get { return _transactions; }
            set
            {
                _transactions = value;
                OnPropertyChanged();
            }
        }
        
        public AllTransactions()
        {
            Transactions = new ObservableCollection<TransactionsModel>();
        }
        
        
        
    }
}

