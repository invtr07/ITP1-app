using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MoneyMate.ViewModels
{
    public class TransactionsModel
    {
        public string TransactionID { get; set; }
        public string Treference { get; set; }
        public DateTime DT { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
    }

    public class AllTransactions : BindableObject
    {
        public ObservableCollection<TransactionsModel> Transactions { get; set; }

        public AllTransactions()
        {
            Transactions = new ObservableCollection<TransactionsModel>{
                new TransactionsModel{ TransactionID="123", Treference="Lloydsbank", DT= new DateTime(2024, 4, 6, 10, 30, 0),Category="Food" , Amount= 500.00m},
                new TransactionsModel{ TransactionID="123", Treference="Lloydsbank", DT= new DateTime(2024, 4, 6, 10, 30, 0),Category="Monthly Fees", Amount= 500.00m},
                new TransactionsModel{ TransactionID="123", Treference="Lloydsbank", DT= new DateTime(2024, 4, 6, 10, 30, 0), Category="Interest", Amount= 500.00m},
                new TransactionsModel{ TransactionID="123", Treference="Lloydsbank", DT= new DateTime(2024, 4, 6, 10, 30, 0), Category="Income", Amount= 500.00m},
            };

        }
    }
}
