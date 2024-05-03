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
						new Label { Text = $"{limit.Period} limit: £{limit.LimitAmount}", FontSize = 15, TextColor = Color.FromHex("b34000"), FontAttributes = FontAttributes.Bold},
						new Label { Text = $"Category: {limit.Category}", FontSize = 15, FontAttributes = FontAttributes.Bold, TextColor = Color.FromHex("b34000")},
					}
				};

				var secondNestedStack = new StackLayout
				{
					HorizontalOptions = LayoutOptions.FillAndExpand,
					VerticalOptions = LayoutOptions.Center,
					Children =
					{
						new ProgressBar{ Progress = 0.2,ProgressColor = Color.FromHex("b34000"), VerticalOptions = LayoutOptions.Center, HeightRequest = 20},
						new StackLayout{Children =
						{
							new Label { Text = $"{limit.LimitProgress}% left to spend", FontSize = 15, HorizontalOptions = LayoutOptions.EndAndExpand, VerticalOptions = LayoutOptions.Center, TextColor = Color.FromHex("b34000")}, 
							new Label { Text = limit.ThirdParty ? "Authorized by entrusted person" : "", FontSize = 13, HorizontalOptions = LayoutOptions.EndAndExpand, VerticalOptions = LayoutOptions.Center}
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
				var tapGestureRecognizer = new TapGestureRecognizer();
				tapGestureRecognizer.Tapped += (s, e) => OnBudgetItemTapped(limit);
				budgetFrame.GestureRecognizers.Add(tapGestureRecognizer);
				BudgetLayout.Children.Add(budgetFrame);
			}
		}
		void OnBudgetItemTapped(App.BudgetDetails budgetItem)
		{
			if (budgetItem.ThirdParty)
			{
				DisplayAlert("Authorization Required", "This budget limit cannot be edited or deleted until authorized by your entrusted person.", "OK");
			}
			else
			{
				ShowEditDeletePopup(budgetItem);
			}
		}
		void ShowEditDeletePopup(App.BudgetDetails budgetItem)
		{
			var popupLayout = new SfPopupLayout();

			var editButton = new Button
			{
				Text = "Edit",
				Command = new Command(() =>
				{
					EditBudget(budgetItem);
					popupLayout.Dismiss();
				})
			};

			var deleteButton = new Button
			{
				Text = "Delete",
				Command = new Command(() =>
				{
					DeleteBudget(budgetItem);
					RefreshBudgetDisplay();
					popupLayout.Dismiss();
				})
			};

			var stackLayout = new StackLayout
			{
				Children = { editButton, deleteButton }
			};
			popupLayout.PopupView.HeightRequest = 150;
			popupLayout.PopupView.WidthRequest = 300;
			popupLayout.PopupView.ShowFooter = false;
			popupLayout.PopupView.ShowHeader = false;
			popupLayout.PopupView.ContentTemplate = new DataTemplate(() => stackLayout);
			popupLayout.Show();
		}

		void EditBudget(App.BudgetDetails budgetItem)
		{
		    var popupLayout = new SfPopupLayout();
		
		    // Creating UI Components for the Popup
		    var amountEntry = new Entry { Text = budgetItem.LimitAmount.ToString(), Keyboard = Keyboard.Numeric, Placeholder = "Enter your limit" };
		    var categoryPicker = new Picker { Title = "Select Category", HorizontalOptions = LayoutOptions.FillAndExpand };
		    var periodPicker = new Picker { Title = "Select Period", HorizontalOptions = LayoutOptions.FillAndExpand };
		    var thirdPartySwitch = new Switch { IsToggled = budgetItem.ThirdParty };
		
		    // Fill Category Picker
		    var categories = new List<string> { "All", "Food", "Gambling", "Leisure", "Shopping", "Transfer" };
		    foreach (var category in categories)
		    {
		        categoryPicker.Items.Add(category);
		    }
		    categoryPicker.SelectedItem = budgetItem.Category;
		
		    // Fill Period Picker
		    var periods = new List<string> { "Daily", "Weekly", "Monthly" };
		    foreach (var period in periods)
		    {
		        periodPicker.Items.Add(period);
		    }
		    periodPicker.SelectedItem = budgetItem.Period;
		
		    // Layout for Popup
		    var stackLayout = new StackLayout
		    {
		        Padding = new Thickness(20),
		        Children = {
		            new Label { Text = "Edit Budget Limit", TextColor = Color.DarkGreen, FontSize = 18, FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center, HorizontalTextAlignment = TextAlignment.Center, Margin = 10 },
		            new Label { Text = "Limit Amount" },
		            amountEntry,
		            new Label { Text = "Category" },
		            categoryPicker,
		            new Label { Text = "Period" },
		            periodPicker,
		            new StackLayout { Orientation = StackOrientation.Horizontal, Children = { new Label { Text = "Enable Third-party Authorization:" }, thirdPartySwitch }},
		            new Button {
		                Text = "Save Changes",
		                BackgroundColor = Color.DarkGreen, TextColor = Color.White,
		                Command = new Command(() => {
		                    // Save logic here
		                    double.TryParse(amountEntry.Text, out double newAmount);
		                    budgetItem.LimitAmount = newAmount;
		                    budgetItem.Category = categoryPicker.SelectedItem.ToString();
		                    budgetItem.Period = periodPicker.SelectedItem.ToString();
		                    budgetItem.ThirdParty = thirdPartySwitch.IsToggled;
		
		                    RefreshBudgetDisplay();  // Refresh the display to show updated data
		                    popupLayout.Dismiss();   // Close the popup
		                })
		            }
		        }
		    };
		
		    popupLayout.PopupView.ContentTemplate = new DataTemplate(() => new ScrollView { Content = stackLayout });
		    popupLayout.PopupView.WidthRequest = 300;
		    popupLayout.PopupView.HeightRequest = 450;
		    popupLayout.PopupView.ShowHeader = false;
		    popupLayout.PopupView.ShowFooter = false;
		    popupLayout.Show();
		}


		void DeleteBudget(App.BudgetDetails budgetItem)
		{
			App.budgetLimits.Remove(budgetItem);
		}

		
		void AddBudget(double limitAmount, string period, bool thirdPartyEnabled, string category)
		{
			
			App.budgetLimits.Add(new App.BudgetDetails()
			{
				LimitAmount = limitAmount, Period = period, ThirdParty = thirdPartyEnabled, Category = category, LimitProgress = 30
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
						intervalPicker, 
						new StackLayout{ Orientation = StackOrientation.Horizontal, Children = { new Label { Text = "Third-party budget authorizer:" }, thirdPartySwitch }}, thirdPartyName, emailThirdParty, new Button
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

