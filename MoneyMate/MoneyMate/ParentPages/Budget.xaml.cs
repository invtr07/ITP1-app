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
		}

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
	        var savingPot = new StackLayout
	        {
		        Orientation = StackOrientation.Horizontal,
		        Padding = 5,
		        Children =
		        {
			        new Label { Text = "Gambling limit", VerticalOptions = LayoutOptions.Center },
			        new ProgressBar { Progress = 0.3, HorizontalOptions = LayoutOptions.FillAndExpand, ProgressColor = Color.Green},
			        new Label { Text = "Limit: £300", VerticalOptions = LayoutOptions.Center }
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
	        tapGesture.Tapped += (s, args) => 
	        {
		        // Handle the tap event
		        FrameTapped(s, args);
		        
	        };
	        savingPotFrame.GestureRecognizers.Add(tapGesture);
	        
	        BudgetLayout.Children.Add(savingPotFrame);
        }

        private void FrameTapped(System.Object sender, EventArgs e)
        {
	        var frame = (Frame)sender;
	        SfPopupLayout popup = new SfPopupLayout()
	        {
		        BindingContext = this.BindingContext
	        };

	        popup.PopupView.HeaderTitle = "Your budget limit";

	        popup.PopupView.ContentTemplate = new DataTemplate(() =>
	        {

		        var l1 = new StackLayout
		        {
			        Padding = 15, 
			        Children =
			        {
				        new Button
				        {
					        Text = "Edit", BackgroundColor = Color.LightGreen, Command = new Command(() =>
					        {
						        DisplayAlert("Confirmation", "Money has been added", "OK");
						        popup.Dismiss(); // Close the popup
					        })
				        }
				        , new Button
				        {
					        Text = "Delete", BackgroundColor = Color.LightCoral, TextColor = Color.White, Command =
						        new Command(() =>
						        {
							        BudgetLayout.Children.Remove(frame);
							        popup.Dismiss(); // Close the popup
						        })
				        }
			        }
		        };

		        return l1;
	        });

	        popup.PopupView.FooterTemplate = new DataTemplate(() =>
	        {
		        var l2 = new StackLayout
		        {
			        Padding = 15, Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.End
			        , Children =
			        {
				        new Button
				        {
					        Text = "Close", BackgroundColor = Color.LightGray, Command = new Command(() =>
					        {
						        popup.Dismiss(); // Close the popup
					        })
				        }
			        }
		        };
		        return l2;
	        });

	        popup.Show();
        }
	}
}

