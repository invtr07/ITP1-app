using System;
using System.Collections.Generic;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Syncfusion.XForms.PopupLayout;
namespace MoneyMate.ParentPages
{	
	public partial class MoneyPots : ContentPage
	{	
		public MoneyPots ()
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
			        new Label { Text = "Save for iPad", VerticalOptions = LayoutOptions.Center },
			        new ProgressBar { Progress = 0.3, HorizontalOptions = LayoutOptions.FillAndExpand, ProgressColor = Color.Green},
			        new Label { Text = "Target: £500", VerticalOptions = LayoutOptions.Center }
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
	        
	        SavingPotsStackLayout.Children.Add(savingPotFrame);
	        
        }
        private void FrameTapped(System.Object sender, EventArgs e)
        {
                 var frame = (Frame)sender;
                 SfPopupLayout popup = new SfPopupLayout()
                 {
	                 BindingContext = this.BindingContext
                 };
                 
                 popup.PopupView.HeaderTitle = "Your money pot";
                 
                 popup.PopupView.ContentTemplate = new DataTemplate(() =>
                 {
	                 
	                 var layout = new StackLayout
	                 {
		                 Padding = 15,
		                 MinimumHeightRequest = 250,
		                 Children =
		                 {
			                 new Button
			                 {
				                 Text = "Withdraw",
				                 BackgroundColor = Color.LightBlue,
				                 Command = new Command(() => {
					                 DisplayAlert("Confirmation", "Money were withdrawn", "OK");
					                 popup.Dismiss(); // Close the popup
				                 })
			                 },
			                 
			                 new Button
			                 {
				                 Text = "Add money",
				                 BackgroundColor = Color.LightGreen,
				                 Command = new Command(() => {
					                 DisplayAlert("Confirmation", "Money has been added", "OK");
					                 popup.Dismiss(); // Close the popup
				                 })
			                 },
			                 new Button
			                 {
				                 Text = "Delete",
				                 BackgroundColor = Color.Red,
				                 TextColor = Color.White,
				                 Command = new Command(() => {
					                 SavingPotsStackLayout.Children.Remove(frame);
					                 popup.Dismiss(); // Close the popup
				                 })
			                 }
		                 }
	                 };
                 
	                 return layout;
                 });

                 popup.PopupView.FooterTemplate = new DataTemplate(() =>
                 {
	                 var l = new StackLayout
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
	                 return l;
                 });
                 
                 popup.Show();
               
		}
    }
}

