using System;
using System.Collections.Generic;
using System.Globalization;
using Syncfusion.ListView.XForms;
using Syncfusion.SfCalendar.XForms;
using Xamarin.Forms;
using Syncfusion.XForms.PopupLayout;
namespace MoneyMate.ParentPages
{	
	public partial class MoneyPots : ContentPage
	{	
		private static Random random = new Random();
		public MoneyPots ()
		{
			InitializeComponent ();
			App.moneyPots = new List<App.MoneyPotDetails>();
		}

		void RefreshMoneyPotPage()
		{
			SavingPotsStackLayout.Children.Clear();

			foreach (var pot in App.moneyPots)
			{
				double progress = pot.CurrentAmount / pot.TargetAmount;
				var savingPot = new StackLayout
				{
					Orientation = StackOrientation.Horizontal,
					Padding = 5,
					Children =
					{
						new StackLayout{ HorizontalOptions = LayoutOptions.Start,
							Children = {
							new Label { Text = $"{pot.PotName}", VerticalOptions = LayoutOptions.Center, TextColor = Color.DarkGreen, FontAttributes = FontAttributes.Bold}, 
							new Label { Text = $"Progress: {pot.PotProgress.ToString("F2")}%", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, TextColor = Color.DarkGreen,FontAttributes = FontAttributes.Bold},
							new Label
							{
								Text = $"Due: {pot.DueDate.ToString("MM/dd/yyyy")}", TextColor = Color.FromHex("b34000"), FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.Center
							}
						}},
						new StackLayout{
							HorizontalOptions = LayoutOptions.FillAndExpand,
							Children =
						{
							new ProgressBar { Progress = progress, HorizontalOptions = LayoutOptions.FillAndExpand, ProgressColor = Color.Green, HeightRequest = 20},
							new Label { Text = $"Target amount: £ {pot.TargetAmount}", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.End, TextColor = Color.DarkGreen, FontAttributes = FontAttributes.Bold},
							new Label { Text = $"Current amount: £ {pot.CurrentAmount}", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.End, TextColor = Color.DarkGreen, FontAttributes = FontAttributes.Bold},
							
						}}
						
					}
				};
	        
				var savingPotFrame = new Frame
				{
					CornerRadius = 5,
					Margin = 5,
					BackgroundColor = Color.White,
					Content = savingPot  // Nesting the StackLayout within the Frame
				};
				var tapGesture = new TapGestureRecognizer();
				tapGesture.Tapped += (s, e) => ShowOptionsPopup(pot);
				savingPotFrame.GestureRecognizers.Add(tapGesture);
				
				SavingPotsStackLayout.Children.Add(savingPotFrame);
				
			}
		}

		void ShowAddOrWithdrawPopup(App.MoneyPotDetails pot, bool isAdding)
		{
		    var popupLayout = new SfPopupLayout();
		    Entry amountEntry = new Entry { Keyboard = Keyboard.Numeric };
		    string action = isAdding ? "Add" : "Withdraw";
		
		    popupLayout.PopupView.ContentTemplate = new DataTemplate(() =>
		    {
		        var contentStack = new StackLayout
		        {
		            Padding = new Thickness(20),
		            Children =
		            {
		                new Label { Text = $"{action} Money" },
		                amountEntry,
		                new Button
		                {
		                    Text = $"{action}",
		                    Command = new Command(() =>
		                    {
		                        if (double.TryParse(amountEntry.Text, out double amount))
		                        {
		                            if (isAdding)
		                            {
		                                if (pot.CurrentAmount + amount > pot.TargetAmount)
		                                {
		                                    DisplayAlert("Error", "Amount exceeds the target amount of the pot.", "OK");
		                                    return;
		                                }
		                                pot.CurrentAmount += amount;
		                                pot.PotProgress = (pot.CurrentAmount / pot.TargetAmount) * 100;
		                            }
		                            else // Withdrawal
		                            {
		                                if (amount > pot.CurrentAmount)
		                                {
		                                    DisplayAlert("Error", "Cannot withdraw more than the current amount in the pot.", "OK");
		                                    return;
		                                }
		                                pot.CurrentAmount -= amount;
		                                pot.PotProgress = (pot.CurrentAmount / pot.TargetAmount) * 100;
		                            }
		
		                            RefreshMoneyPotPage();
		                            popupLayout.Dismiss();
		                        }
		                        else
		                        {
		                            DisplayAlert("Error", "Please enter a valid numeric amount.", "OK");
		                        }
		                    })
		                }
		            }
		        };
		        return contentStack;
		    });
			popupLayout.PopupView.ShowFooter = false;
			popupLayout.PopupView.ShowHeader = false;
		    popupLayout.Show();
		}
		
		void ShowOptionsPopup(App.MoneyPotDetails pot)
		{
			var popupLayout = new SfPopupLayout();
			popupLayout.PopupView.ContentTemplate = new DataTemplate(() =>
			{
				var contentStack = new StackLayout
				{
					Padding = new Thickness(20),
					Children =
					{
						new Button { Text = "Add Money", Command = new Command(() => { ShowAddOrWithdrawPopup(pot, true); }) },
						new Button { Text = "Withdraw Money", Command = new Command(() => { ShowAddOrWithdrawPopup(pot, false); }) },
						new Button { Text = "Edit", Command = new Command(() => { ShowEditPopup(pot); }) }
					}
				};
				return contentStack;
			});
			popupLayout.PopupView.ShowFooter = false;
			popupLayout.PopupView.ShowHeader = false;
			popupLayout.PopupView.WidthRequest = 300;
			popupLayout.PopupView.HeightRequest = 200;
			popupLayout.Show();
		}

		void AddMoneyPot(double targetAmount, DateTime? dueDate, string moneyPotName)
		{
			App.moneyPots.Add(new App.MoneyPotDetails
			{
				PotName = moneyPotName,
				TargetAmount = targetAmount,
				DueDate = dueDate.Value,
				PotProgress = 0,
			});
			RefreshMoneyPotPage();
		}
		bool TryAddPot(double targetAmout, DateTime? dueDate, string moneyPotName)
		{
			if (targetAmout != 0 && !string.IsNullOrWhiteSpace(moneyPotName) && dueDate.HasValue)
			{
				AddMoneyPot(targetAmout, dueDate, moneyPotName);
				return true;
			}
			{
				DisplayAlert("Error", "Please enter a valid limit amount and select a period", "OK");
				return false;
			}
			
		}
		void ShowPopup()
		{
			Entry targetAmountEntry, potName;
			
			var calendar = new SfCalendar();
			
			var popupLayout = new SfPopupLayout();
			
			popupLayout.PopupView.ContentTemplate = new DataTemplate(() =>
			{
			
				var contentStack = new StackLayout
				{
					Padding = new Thickness(20), Children =
					{
						new Label
						{
							Text = "Enter details for new Money pot", TextColor = Color.DarkGreen, FontSize = 18
							, FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center
							, HorizontalTextAlignment = TextAlignment.Center, Margin = 10
						}, { potName= new Entry { Placeholder = "Enter your pot name" } }
						, (targetAmountEntry = new Entry { Placeholder = "Enter your target amount",Keyboard = Keyboard.Numeric })
						, new Label { Text = "Target due date:" }, calendar
						, new Button
						{
							Text = "Save", BackgroundColor = Color.DarkGreen, TextColor = Color.White, Command =
								new Command(() =>
								{
									if (TryAddPot(Convert.ToDouble(targetAmountEntry.Text), calendar.SelectedDate, potName.Text))
										popupLayout.Dismiss();
								})
						}
					}
				};
				return new ScrollView
				{
					Content = contentStack
				};
			});
			popupLayout.PopupView.WidthRequest = 300;
			popupLayout.PopupView.HeightRequest = 500;
			popupLayout.PopupView.ShowHeader = false;
			popupLayout.PopupView.ShowFooter = false;
			popupLayout.Show();
		}
		void ShowEditPopup(App.MoneyPotDetails pot)
		{
			var potNameEntry = new Entry { Text = pot.PotName };
			var targetAmountEntry = new Entry { Text = pot.TargetAmount.ToString(), Keyboard = Keyboard.Numeric };
			var calendar = new SfCalendar { SelectedDate = pot.DueDate };

			var popupLayout = new SfPopupLayout();

			popupLayout.PopupView.ContentTemplate = new DataTemplate(() =>
			{
				var contentStack = new StackLayout
				{
					Padding = new Thickness(20),
					Children =
					{
						new Label { Text = "Edit Money Pot", FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center },
						new Label { Text = "Pot Name:" },
						potNameEntry,
						new Label { Text = "Target Amount (£):" },
						targetAmountEntry,
						new Label { Text = "Due Date:" },
						calendar,
						new Button { Text = "Save Changes", BackgroundColor = Color.DarkGreen,TextColor = Color.White,Command = new Command(() => {
							double targetAmount;
							if (double.TryParse(targetAmountEntry.Text, out targetAmount))
							{
								pot.PotName = potNameEntry.Text;
								pot.TargetAmount = targetAmount;
								pot.DueDate = calendar.SelectedDate.GetValueOrDefault();
								RefreshMoneyPotPage();
								popupLayout.Dismiss();
							}
						}) },
						new Button { Text = "Delete", BackgroundColor = Color.White, TextColor = Color.Red, Command = new Command(() => {
							if(pot.CurrentAmount > 0)
							{
								DisplayAlert("Error", "Cannot delete a pot with money in it.", "OK");
								return;
							}
							else
							{
								App.moneyPots.Remove(pot);
								RefreshMoneyPotPage();
							}
							popupLayout.Dismiss();
						}) }
					}
				};
				return new ScrollView { Content = contentStack };
			});
			
			popupLayout.PopupView.ShowFooter = false;
			popupLayout.PopupView.ShowHeader = false;
			popupLayout.PopupView.WidthRequest = 300;
			popupLayout.PopupView.HeightRequest = 500;
			popupLayout.Show();
		}
        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
	        ShowPopup();
	        
        }
        protected override void OnAppearing()
        {
	        base.OnAppearing();
	        RefreshMoneyPotPage();
        }
    }
}

