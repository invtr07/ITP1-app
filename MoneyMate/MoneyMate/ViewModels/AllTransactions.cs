using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using MoneyMate.DatabaseAccess;

namespace MoneyMate.ViewModels
{
    public class TransactionsModel
    {
        // public string TransactionID { get; set; }
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
            get => _transactions;
            set
            {
                _transactions = value;
                OnPropertyChanged(nameof(Transactions));  // Notify UI of change
            }
        }

        public AllTransactions()
        {
            Transactions = new ObservableCollection<TransactionsModel>();
            LoadTransactionsAsync();  // Load data asynchronously on initialization
        }
        
        private async Task LoadTransactionsAsync()
        {
            DatabaseControl dbControl = new DatabaseControl();
            var transactionList = await dbControl.GetAllTransactionsAsync();
            // Assume GetAllTransactionsAsync is correctly fetching data from the DB

            if (transactionList != null && transactionList.Count > 0)
            {
                 // If the list is not empty, add each transaction to the ObservableCollection
                foreach (var transaction in transactionList)
                {
                    Transactions.Add(transaction);
                }
                // Console.WriteLine(" transactions found.");

            }
            else
            {
                Console.WriteLine("No transactions found.");
            }
        }
    }
}

// public ObservableCollection<TransactionsModel> Transactions { get; set; }
//
// public AllTransactions()
// {
//     Transactions = new ObservableCollection<TransactionsModel>
//     {
//         new TransactionsModel
//         {
//             TransactionID = "123", Treference = "Lloydsbank", DT = new DateTime(2024, 4, 6, 10, 30, 0)
//             , Category = "Food", Amount = 500.00m
//         }
//         , new TransactionsModel
//         {
//             TransactionID = "123", Treference = "Lloydsbank", DT = new DateTime(2024, 4, 6, 10, 30, 0)
//             , Category = "Monthly Fees", Amount = 500.00m
//         }
//         , new TransactionsModel
//         {
//             TransactionID = "123", Treference = "Lloydsbank", DT = new DateTime(2024, 4, 6, 10, 30, 0)
//             , Category = "Interest", Amount = 500.00m
//         }
//         , new TransactionsModel
//         {
//             TransactionID = "123", Treference = "Lloydsbank", DT = new DateTime(2024, 4, 6, 10, 30, 0)
//             , Category = "Income", Amount = 500.00m
//         }
//         ,
//     };
//
// }