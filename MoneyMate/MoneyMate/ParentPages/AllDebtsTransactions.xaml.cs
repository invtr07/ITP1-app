using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyMate.DatabaseAccess;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyMate.ParentPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllDebtsTransactions : ContentPage
    {
        public ViewModels.AllTransactions TransactionsList { get; set; }

        public AllDebtsTransactions()
        {
            InitializeComponent();
            TransactionsList = new ViewModels.AllTransactions();
            BindingContext = this;
        }
        private async Task TransactionsAsync()
        {
            DatabaseControl dbService = new DatabaseControl();
            int productID1 = 105;
            int productID2 = 106;

            try
            {
                var newTransactions = await dbService.LoadTransactions(productID1, productID2, 200);
                if (TransactionsList.Transactions != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        TransactionsList.Transactions.Clear();
                        foreach (var transaction in newTransactions)
                        {
                            TransactionsList.Transactions.Add(transaction);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"{ex.Message}", "OK");
            }
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await TransactionsAsync();
        }

        private void InputView_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                CollectionViewT.ItemsSource = TransactionsList.Transactions; // Show all items when search text is cleared
            }
            else
            {
                CollectionViewT.ItemsSource = TransactionsList.Transactions.Where(item =>
                    item.Treference.ToLower().Contains(e.NewTextValue.ToLower()) ||
                    item.Category.ToLower().Contains(e.NewTextValue.ToLower()) ||
                    item.DT.ToString().ToLower().Contains(e.NewTextValue.ToLower()) || 
                    item.Amount.ToString().ToLower().Contains(e.NewTextValue.ToLower()));
            }
        }
    }
}