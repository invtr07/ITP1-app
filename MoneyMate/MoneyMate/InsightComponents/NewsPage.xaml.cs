using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Linq;

namespace MoneyMate.InsightComponents
{
	public partial class NewsPage : ContentPage
	{
		public ObservableCollection<NewsItems> NewsItems { get; } = new ObservableCollection<NewsItems>();

		public NewsPage()
		{
			InitializeComponent();
			NewsItems = new ObservableCollection<NewsItems>();
			 // GetNews();
			BindingContext = this;
		}

		private async void GetNews()
		{
			var client = new HttpClient();
			var request = new HttpRequestMessage
			{
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://reuters-business-and-financial-news.p.rapidapi.com/articles-by-trends/2024-01-31/0/80"),
                Headers =
				 {
					{ "X-RapidAPI-Key", "572e416709mshc612b0219f7f1c0p1baeaejsn608006f96dfd" },
					 { "X-RapidAPI-Host", "reuters-business-and-financial-news.p.rapidapi.com" },
				 },
            };
			using (var response = await client.SendAsync(request))
			{
				response.EnsureSuccessStatusCode();
				var body = await response.Content.ReadAsStringAsync();
  
				RootObject data = JsonConvert.DeserializeObject<RootObject>(body);
  
				foreach (var item in data.articles)
				{
  
					NewsItems.Add(new NewsItems
					{
						Title = item.articlesName,
						Brief = item.articlesShortDescription,
						//ImageUrl = item.files[0].urlCdn,
						Link = item.urlSupplier
					});
				}
				OnPropertyChanged(nameof(NewsItems));
			}
		}

		void CollectionView1_ItemTapped(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
		{
			if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
			{
				var selectedItem = (NewsItems)e.CurrentSelection[0]; // Cast the selected item as your data model type
				if (selectedItem != null)
				{
					OpenLink($"https://www.reuters.com{selectedItem.Link}");
			
					// Optionally reset selection
					((CollectionView)sender).SelectedItem = null;
				}
			}

		}
		public void OpenLink(string url)
		{
			try
			{
				Launcher.OpenAsync(new Uri(url));
			}
			catch (Exception ex)
			{
				// Handle exceptions
				DisplayAlert("Error", ex.Message, "ok");
			}
		}

		void SearchBar_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(e.NewTextValue))
			{
				CollectionView1.ItemsSource = NewsItems; // Show all items when search text is cleared
			}
			else
			{
				CollectionView1.ItemsSource = NewsItems.Where(item =>
					item.Title.ToLower().Contains(e.NewTextValue.ToLower()));
			}
		}
	}
	public class NewsItems
		{
			public string Title { get; set; }
			public string Brief { get; set; }
			//public string ImageUrl { get; set; }
			public string Link { get; set; }

		}

		public class RootObject
		{
			public List<Article> articles { get; set; }
		}
		public class Article
		{
			public int articlesId { get; set; }
			public string articlesName { get; set; }
			public string articlesShortDescription { get; set; }
			public int minutesToRead { get; set; }
			public string urlSupplier { get; set; }
			public DateInfo dateModified { get; set; }
			public List<ArticleImages> files { get; set; }
		}

		public class DateInfo
		{
			public DateTime date { get; set; }
		}

		public class ArticleImages
		{
			public string urlCdn { get; set; }
		}

        
    }


//contingency api

