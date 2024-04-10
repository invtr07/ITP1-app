using System;
using System.Collections.Generic;
using Syncfusion;
using Xamarin.Forms;

namespace MoneyMate.ParentPages
{	
	public partial class EarningsSpendings : ContentPage
	{
        public Transactions[] transactions;
        

        public EarningsSpendings ()
		{
			InitializeComponent ();

            transactions = new Transactions[]{
                new Transactions{ TransactionID="123", Treference="Lloydsbank", DT= new DateTime(2024, 4, 6, 10, 30, 0),Category="A" , Amount= 500.00m},
                new Transactions{ TransactionID="123", Treference="Lloydsbank", DT= new DateTime(2024, 4, 6, 10, 30, 0),Category="B", Amount= 500.00m},
                new Transactions{ TransactionID="123", Treference="Lloydsbank", DT= new DateTime(2024, 4, 6, 10, 30, 0), Category="C", Amount= 500.00m},
                new Transactions{ TransactionID="123", Treference="Lloydsbank", DT= new DateTime(2024, 4, 6, 10, 30, 0), Category="D", Amount= 500.00m},

            

            };
            



            CollectionViewT.ItemsSource = transactions;
            

        }

        void SeeAllTapped(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AllTransactions(), true);
        }

        public class Transactions
        {
            public string TransactionID { get; set; }
            public string Treference { get; set; }
            public DateTime DT { get; set; }
            public string Category { get; set; }
            public decimal Amount { get; set; }
        }


    }
}

