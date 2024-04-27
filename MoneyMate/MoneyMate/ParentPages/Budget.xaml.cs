using System;
using System.Collections.Generic;
using Syncfusion.XForms.PopupLayout;
using Xamarin.Forms;

namespace MoneyMate.ParentPages
{	
	public partial class Budget : ContentPage
	{	
		public Budget ()
		{
			InitializeComponent ();
			App.budgetLimits = new List<App.BudgetDetails>();
		}

		void RefreshBudgetDisplay()
		{
			BudgetLayout.Children.Clear();

			foreach (var limit in App.budgetLimits)
			{
				var mainStack = new StackLayout
				{
					Orientation = StackOrientation.Horizontal,
				};

				var firstNestedStack = new StackLayout
				{
                    HorizontalOptions = LayoutOptions.Start,
					Children =
					{
						new Label { Text = $"{limit.Period} limit: £{limit.LimitAmount}", FontSize = 16, TextColor = Color.Orange, FontAttributes = FontAttributes.Bold},
						new Label { Text = limit.Category, FontSize = 14, FontAttributes = FontAttributes.Bold, TextColor = Color.Orange},
					}
				};

				var secondNestedStack = new StackLayout
				{
					HorizontalOptions = LayoutOptions.FillAndExpand,
					Children =
					{
						new ProgressBar{ Progress = 0.3,ProgressColor = Color.DarkOrange, HeightRequest = 15, WidthRequest = 100 },
						new StackLayout{Children =
						{
							new Label { Text = "70% left to spend", FontSize = 13, HorizontalOptions = LayoutOptions.End}, 
							new Label { Text = limit.ThirdParty ? "Authorized by entrusted person" : "", FontSize = 12 }
						}},
						
					}
				};

				mainStack.Children.Add(firstNestedStack);
				mainStack.Children.Add(secondNestedStack);

				var budgetFrame = new Frame
				{
					Content = mainStack,
					Padding = 15,
					CornerRadius = 10,
					Margin = 10,
					BackgroundColor = Color.White,
				};
				BudgetLayout.Children.Add(budgetFrame);
			}
		}
		void AddBudget(double limitAmount, string period, bool thirdPartyEnabled, string category)
		{
			
			App.budgetLimits.Add(new App.BudgetDetails()
			{
				LimitAmount = limitAmount, Period = period, ThirdParty = thirdPartyEnabled, Category = category
			});

			RefreshBudgetDisplay();
		}
		bool TryAddBudget(string amountText, string period, bool thirdPartyEnabled, string category)
		{
			if (double.TryParse(amountText, out double limitAmount) && !string.IsNullOrWhiteSpace(period))
			{
				AddBudget(limitAmount, period, thirdPartyEnabled, category);
				return true;
			}
		    
			DisplayAlert("Error", "Please enter a valid limit amount and select a period", "OK");
			return false;
		}
		void ShowPopup()
		{
			Entry targetAmountEntry, category;
			Switch thirdPartySwitch;
			Picker intervalPicker = new Picker
			{
				Title = "Limit period", HorizontalOptions = LayoutOptions.FillAndExpand
			};
			intervalPicker.Items.Add("Daily");
			intervalPicker.Items.Add("Weekly");
			intervalPicker.Items.Add("Monthly");

			Picker categoryPicker = new Picker
			{
				Title = "Transaction category"
			};
			categoryPicker.Items.Add("All");
			categoryPicker.Items.Add("Food");
			categoryPicker.Items.Add("Gambling");
			categoryPicker.Items.Add("Leisure");
			categoryPicker.Items.Add("Shopping");
			categoryPicker.Items.Add("Transfer");

			var popupLayout = new SfPopupLayout();

			// Correctly set properties of PopupView by defining the ContentTemplate directly
			popupLayout.PopupView.ContentTemplate = new DataTemplate(() =>
			{
				Entry emailThirdParty = new Entry { Placeholder = "3rd party's email", IsVisible = false };
				Entry thirdPartyName = new Entry { Placeholder = "3rd party's name", IsVisible = false };
				thirdPartySwitch = new Switch { IsToggled = false };

				// Now set up the bindings since the controls have been instantiated
				emailThirdParty.SetBinding(IsVisibleProperty, new Binding("IsToggled", source: thirdPartySwitch));
				thirdPartyName.SetBinding(IsVisibleProperty, new Binding("IsToggled", source: thirdPartySwitch));

				var contentStack = new StackLayout
				{
					Padding = new Thickness(20), Children =
					{
						new Label
						{
							Text = "Enter details for new Budget Limit", TextColor = Color.DarkGreen, FontSize = 18
							, FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center
							, HorizontalTextAlignment = TextAlignment.Center, Margin = 10
						}
						
						, (targetAmountEntry = new Entry { Placeholder = "Enter your limit",Keyboard = Keyboard.Numeric })
						, categoryPicker, 
 intervalPicker, new StackLayout{ Orientation = StackOrientation.Horizontal, Children = { new Label { Text = "Third-party budget authorizer:" }, thirdPartySwitch }}, thirdPartyName, emailThirdParty, new Button
						{
							Text = "Save", BackgroundColor = Color.DarkGreen, TextColor = Color.White, Command =
								new Command(() =>
								{
									if (TryAddBudget(targetAmountEntry.Text, intervalPicker.SelectedItem as string
										    , thirdPartySwitch.IsToggled, categoryPicker.SelectedItem as string))
										popupLayout.Dismiss();
								})
						}
						,

					}
				};
				return new ScrollView
				{
					Content = contentStack
				};
			});
			popupLayout.PopupView.WidthRequest = 300;
			popupLayout.PopupView.HeightRequest = 350;
			popupLayout.PopupView.ShowHeader = false;
			popupLayout.PopupView.ShowFooter = false;
			popupLayout.Show();
		}
		

		void Button_Clicked(System.Object sender, System.EventArgs e)
        {
	        ShowPopup();
        }
		
		protected override void OnAppearing()
		{
			base.OnAppearing();
			RefreshBudgetDisplay();
		}
	}
}

