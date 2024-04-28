using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.XForms.Chat;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Xamarin.Essentials;


namespace MoneyMate
{	
	
	public partial class HelpPage : ContentPage
	{	
		private string ApiKey = "sk-proj-ZOZ20i8tHWA3C0aDSHB2T3BlbkFJ42io28sFyxoUsuZy4h4I";
		Author user;
		Author bot;
		ObservableCollection<object> messages;
		public HelpPage ()
		{
			InitializeComponent ();
			
			user = new Author() { Name = $"{App.savedName}", Avatar ="drawable/avatar" };
			bot = new Author() { Name = "Lloyd", Avatar = "drawable/aichat.png" };

			messages = new ObservableCollection<object>();
			chat.Messages = messages;
			chat.CurrentUser = user;
			

			SendGreetingMessage();
			
		}
		async Task<string> GetResponseFromOpenAI(string userMessage)
		{
			using (var client = new HttpClient())
			{
				var content = new StringContent(JsonConvert.SerializeObject(new
				{
					model = "gpt-3.5-turbo", // Use the specified model
					messages = new[]
					{
						new { role = "system", content = "You are personal finance assistant helping with basic financial queries. Always notify user about your potential errors!" },
						new { role = "user", content = userMessage }
					}
				}), Encoding.UTF8, "application/json"); // Apply the Content-Type header here

				var request = new HttpRequestMessage
				{
					Method = HttpMethod.Post,
					RequestUri = new Uri("https://api.openai.com/v1/chat/completions"), // Updated URI
					Headers =
					{
						{ "Authorization", $"Bearer {ApiKey}" }
					},
					Content = content // Set the content of the request
				};

				var response = await client.SendAsync(request);
				response.EnsureSuccessStatusCode();
				var responseBody = await response.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<dynamic>(responseBody);

				// Assuming the response structure is similar to the completion endpoint
				// This may need to be adjusted based on the actual response structure
				// of the Chat API.
				return result.choices[0].message.content.ToString();
			}

		}
		
		void SendGreetingMessage()
		{
			var greetingMessage = "Hello! I'm your personal finance assistant - LloydsAI. However, please keep in mind, I can sometimes make mistakes, so please double-check any important information.";

			
			messages.Add(new TextMessage { Author = bot, Text = greetingMessage, DateTime = DateTime.Now });
		}
		async void Chat_OnSendMessage(object sender, SendMessageEventArgs e)
		{
			var userMessage = e.Message.Text;
			var botResponse = await GetResponseFromOpenAI(userMessage);
			chat.Messages.Add(new TextMessage { Author = bot, Text = botResponse });
		}

		private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
			Launcher.OpenAsync("https://www.citizensadvice.org.uk/about-us/contact-us/contact-us/web-chat-service/");
		}
	}
}

