using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MoneyMate.InsightComponents
{	
	public partial class NewsPage : ContentPage
	{
		public NewsItems[] newsItems;

		public NewsPage ()
		{
			InitializeComponent ();

			newsItems = new NewsItems[]
			{
				new NewsItems{ Title= "News 1", Brief= "This is news brief 1"},
                new NewsItems{ Title= "News 2", Brief= "This is news brief 1"},
                new NewsItems{ Title= "News 3", Brief= "This is news brief 1"},
                new NewsItems{ Title= "News 4", Brief= "This is news brief 1"}
            };

			Listview1.ItemsSource = newsItems;
		}

		public class NewsItems
		{
			public string Title { get; set; }
			public string Brief { get; set; }
		}

        void ListView_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
        }
    }
}

