using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MoneyMate.InsightComponents
{	
	public partial class SuggestionsPage : ContentPage
	{
        public Suggestions[] suggestions;

		public SuggestionsPage ()
		{
			InitializeComponent ();

            suggestions = new Suggestions[]
            {
                new Suggestions{ Title = "Create money pot", Brief="Lorem ipsum dolor sit amet, consectetur adipiscing elit."},
                new Suggestions{ Title = "Block spending", Brief="Lorem ipsum dolor sit amet, consectetur adipiscing elit."},
                new Suggestions{ Title = "Create money pot", Brief = "Lorem ipsum dolor sit amet, consectetur adipiscing elit." },
                new Suggestions{ Title = "Block spending", Brief="Lorem ipsum dolor sit amet, consectetur adipiscing elit."}
            };

            Listview1.ItemsSource = suggestions;

		}

        public class Suggestions
        {
            public string Title { get; set; }
            public string Brief { get; set; }

        }

        void Listview1_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
			
        }
    }
}

