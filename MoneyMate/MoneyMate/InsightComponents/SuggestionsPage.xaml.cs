using System;
using System.Collections.Generic;
using MoneyMate.ParentPages;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MoneyMate.InsightComponents
{	
	public partial class SuggestionsPage : ContentPage
	{
        // public Suggestions[] suggestions;

		public SuggestionsPage ()
		{
			InitializeComponent ();
			
		}

        public class Suggestions
        {
            public string Title { get; set; }
            public string Brief { get; set; }

        }
        
		void OnFrameTapped(object sender, EventArgs e)
        {
	        
	        var popupLayout = new Syncfusion.XForms.PopupLayout.SfPopupLayout();

	        var popupView = new Syncfusion.XForms.PopupLayout.PopupView
	        {
		        ShowHeader = false,
		        ShowFooter = false,
		        ContentTemplate = new DataTemplate( () =>
		        {
			        var content = new StackLayout
			        {
				        Padding = 15,
				        Children =
				        {
					        new Label
					        {
						        Text = "Set up a limit to prevent gambling spending", FontSize = 18, TextColor = Color.DarkGreen, FontAttributes = FontAttributes.Bold,
						        HorizontalOptions = LayoutOptions.CenterAndExpand
						        , VerticalOptions = LayoutOptions.CenterAndExpand
					        },
					        new Label
					        {
						        Text = "If you found that during the last 30 days, a large portion of your spending has been at gambling institutions, then we recommend you to setup a budget limit with third-party authorizor.", HorizontalOptions = LayoutOptions.CenterAndExpand
						        , VerticalOptions = LayoutOptions.CenterAndExpand
					        },
					        new Label
					        {
						        Text = "Third-party authorizor - your entrusted person who cares about your financial well-being. The limit with the third-party authorization cannot be deleted or edited unless verification was received!", HorizontalOptions = LayoutOptions.CenterAndExpand
						        , VerticalOptions = LayoutOptions.CenterAndExpand
					        },
					        new Button()
					        {
						        TextColor = Color.White,
						        Text = "Take action", HorizontalOptions = LayoutOptions.CenterAndExpand, BackgroundColor = Color.DarkGreen,
						        Command = new Command(() =>
						        {
							        Navigation.PushAsync( new CashflowTabs());
							        popupLayout.Dismiss();
						        })
					        }
				        }
			        };

			        var wrapper = new ScrollView
			        {
						Content = content
			        };

			        return wrapper;
		        })
	        };

	        // Set the PopupView of the SfPopupLayout
	        popupLayout.PopupView = popupView;
	        popupLayout.PopupView.HeightRequest = 350;
	        

	        // Show the SfPopupLayout
	        popupLayout.Show();
        }

        private void OnFrameTapped1(object sender, EventArgs e)
        {
	         
	        var popupLayout = new Syncfusion.XForms.PopupLayout.SfPopupLayout();

	        var popupView = new Syncfusion.XForms.PopupLayout.PopupView
	        {
		        ShowHeader = false,
		        ShowFooter = false,
		        ContentTemplate = new DataTemplate( () =>
		        {
			        var content = new StackLayout
			        {
				        Padding = 15,
				        Children =
				        {
					        new Label
					        {
						        Text = "Limit spending in non-essential areas", FontSize = 18, TextColor = Color.DarkGreen, FontAttributes = FontAttributes.Bold,
						        HorizontalOptions = LayoutOptions.CenterAndExpand
						        , VerticalOptions = LayoutOptions.CenterAndExpand
					        },
					        new Label
					        {
                                FontSize = 15,
						        Text = "If you find yourself constantly spending on non-essential things and you see by data that it impacts your long-term financial well-being, then you can create a budget limit for particular category or all categories of spending. Budget limit could be created without a third-party authorization for cases with less serious spending habits", HorizontalOptions = LayoutOptions.CenterAndExpand
						        , VerticalOptions = LayoutOptions.CenterAndExpand
					        },
					     
					        new Button()
					        {
						        TextColor = Color.White,
						        Text = "Create budget", HorizontalOptions = LayoutOptions.CenterAndExpand, BackgroundColor = Color.DarkGreen,
						        Command = new Command(() =>
						        {
							        Navigation.PushAsync( new CashflowTabs());
							        popupLayout.Dismiss();
						        })
					        }
				        }
			        };
			        var wrapper = new ScrollView
			        {
				        Content = content
			        };

			        return wrapper;
		        })
	        };

	        // Set the PopupView of the SfPopupLayout
	        popupLayout.PopupView = popupView;
	        popupLayout.PopupView.HeightRequest = 370;


	        // Show the SfPopupLayout
	        popupLayout.Show();
        }

        private void OnFrameTapped2(object sender, EventArgs e)
        {
	            var popupLayout = new Syncfusion.XForms.PopupLayout.SfPopupLayout();
            
            	        var popupView = new Syncfusion.XForms.PopupLayout.PopupView
            	        {
            		        ShowHeader = false,
            		        ShowFooter = false,
            		        ContentTemplate = new DataTemplate( () =>
            		        {
            			        var content = new StackLayout
            			        {
            				        Padding = 15,
            				        Children =
            				        {
            					        new Label
            					        {
            						        Text = "Set a savings goal for big life events", FontSize = 18, TextColor = Color.DarkGreen, FontAttributes = FontAttributes.Bold,
            						        HorizontalOptions = LayoutOptions.CenterAndExpand
            						        , VerticalOptions = LayoutOptions.CenterAndExpand
            					        },
            					        new Label
            					        {
                                            FontSize = 15,
            						        Text = "Achieving important life goals like arranging a wedding require proactive consistency and effort. For example the average cost of a wedding in the UK is £20,775, so start saving now for your special day and keep motivated by seeing results!", HorizontalOptions = LayoutOptions.CenterAndExpand
            						        , VerticalOptions = LayoutOptions.CenterAndExpand
            					        },
            					        
            					        new Button()
            					        {
            						        TextColor = Color.White,
            						        Text = "Start saving now!", HorizontalOptions = LayoutOptions.CenterAndExpand, BackgroundColor = Color.DarkGreen,
            						        Command = new Command(() =>
            						        {
            							        Navigation.PushAsync( new Savings());
            							        popupLayout.Dismiss();
            						        })
            					        }
            				        }
            			        };
            			        var wrapper = new ScrollView
            			        {
            				        Content = content
            			        };
            
            			        return wrapper;
            		        })
            	        };
            
            	        // Set the PopupView of the SfPopupLayout
            	        popupLayout.PopupView = popupView;
            	        popupLayout.PopupView.HeightRequest = 270;
            
            
            	        // Show the SfPopupLayout
            	        popupLayout.Show();
        }

        private void OnFrameTapped3(object sender, EventArgs e)
        {
	            var popupLayout = new Syncfusion.XForms.PopupLayout.SfPopupLayout();
            
            	        var popupView = new Syncfusion.XForms.PopupLayout.PopupView
            	        {
            		        ShowHeader = false,
            		        ShowFooter = false,
            		        ContentTemplate = new DataTemplate( () =>
            		        {
            			        var content = new StackLayout
            			        {
            				        Padding = 15,
            				        Children =
            				        {
            					        new Label
            					        {
            						        Text = "Open a new savings account for family plan", FontSize = 18, TextColor = Color.DarkGreen, FontAttributes = FontAttributes.Bold,
            						        HorizontalOptions = LayoutOptions.CenterAndExpand
            						        , VerticalOptions = LayoutOptions.CenterAndExpand
            					        },
            					        new Label
            					        {
                                            FontSize = 15,
            						        Text = "Raising a child is a wonderful, joyous and emotional journey take away some of the financial stress and start saving today to focus on those precious moments with your little one.", HorizontalOptions = LayoutOptions.CenterAndExpand
            						        , VerticalOptions = LayoutOptions.CenterAndExpand
            					        },
            					        
            					        new Button()
            					        {
            						        TextColor = Color.White,
            						        Text = "Create money pot!", HorizontalOptions = LayoutOptions.CenterAndExpand, BackgroundColor = Color.DarkGreen,
            						        Command = new Command(() =>
            						        {
            							        Navigation.PushAsync( new SavingMoneyPotTabs());
            							        popupLayout.Dismiss();
            						        })
            					        }
            				        }
            			        };
            			        var wrapper = new ScrollView
            			        {
            				        Content = content
            			        };
            
            			        return wrapper;
            		        })
            	        };
            
            	        // Set the PopupView of the SfPopupLayout
            	        popupLayout.PopupView = popupView;
            	        popupLayout.PopupView.HeightRequest = 300;
            
            
            	        // Show the SfPopupLayout
            	        popupLayout.Show();
        }

        private void OnFrameTapped4(object sender, EventArgs e)
        {
	       var popupLayout = new Syncfusion.XForms.PopupLayout.SfPopupLayout();
            
                var popupView = new Syncfusion.XForms.PopupLayout.PopupView
                {
                    ShowHeader = false,
                    ShowFooter = false,
                    ContentTemplate = new DataTemplate( () =>
                    {
            	        var content = new StackLayout
            	        {
            		        Padding = 15,
            		        Children =
            		        {
            			        new Label
            			        {
            				        Text = "Need assistance with credit repayment?", FontSize = 18, TextColor = Color.DarkGreen, FontAttributes = FontAttributes.Bold,
            				        HorizontalOptions = LayoutOptions.CenterAndExpand
            				        , VerticalOptions = LayoutOptions.CenterAndExpand
            			        },
            			        new Label
            			        {
                                    FontSize = 15,
            				         HorizontalOptions = LayoutOptions.CenterAndExpand
            				        , VerticalOptions = LayoutOptions.CenterAndExpand,
				                    FormattedText = new FormattedString
				                    {
					                    Spans =
					                    {
						                    new Span { Text = "Sometimes debts can be overwhelming and if you find yourself in this situation do not hesitate to seek for free financial consultation from certified experts. For any other financial queries you can have conversation anytime 24/7 with our LloydsAI assistant!" }
					                    }
				                    }
            			        },
            			        
            			        new Button()
            			        {
            				        TextColor = Color.White,
            				        Text = "Book consultation!", HorizontalOptions = LayoutOptions.CenterAndExpand, BackgroundColor = Color.DarkGreen,
            				        Command = new Command(() =>
				                    {
					                    Launcher.OpenAsync(
						                    "https://www.citizensadvice.org.uk/about-us/contact-us/contact-us/web-chat-service/");
            					        popupLayout.Dismiss();
            				        })
            			        }
            		        }
            	        };
            	        var wrapper = new ScrollView
            	        {
            		        Content = content
            	        };
    
            	        return wrapper;
                    })
                };
    
                // Set the PopupView of the SfPopupLayout
                popupLayout.PopupView = popupView;
                popupLayout.PopupView.HeightRequest = 350;
    
    
                // Show the SfPopupLayout
                popupLayout.Show();
        }

        private void OnFrameTapped5(object sender, EventArgs e)
        {
	        var popupLayout = new Syncfusion.XForms.PopupLayout.SfPopupLayout();
            
                var popupView = new Syncfusion.XForms.PopupLayout.PopupView
                {
                    ShowHeader = false,
                    ShowFooter = false,
                    ContentTemplate = new DataTemplate( () =>
                    {
            	        var content = new StackLayout
            	        {
            		        Padding = 15,
            		        Children =
            		        {
            			        new Label
            			        {
            				        Text = "Considering investing?", FontSize = 18, TextColor = Color.DarkGreen, FontAttributes = FontAttributes.Bold,
            				        HorizontalOptions = LayoutOptions.CenterAndExpand
            				        , VerticalOptions = LayoutOptions.CenterAndExpand
            			        },
            			        new Label
            			        {
                                    FontSize = 15,
            				         HorizontalOptions = LayoutOptions.CenterAndExpand
            				        , VerticalOptions = LayoutOptions.CenterAndExpand,
				                    FormattedText = new FormattedString
				                    {
					                    Spans =
					                    {
						                    new Span { Text = "Have you ever thought about making your money work harder for you and your family? Consider our expertly managed investment accounts and services. Ideal for those new to investing or with limited time for market research, our funds simplify your financial journey. We handle the complexities, so you don't have to." }
					                    }
				                    }
            			        },
            			        
            			        new Button()
            			        {
            				        TextColor = Color.White,
            				        Text = "Learn more", HorizontalOptions = LayoutOptions.CenterAndExpand, BackgroundColor = Color.DarkGreen,
            				        Command = new Command(() =>
				                    {
					                    Launcher.OpenAsync(
						                    "https://www.lloydsbank.com/investing/ways-to-invest/ready-made-investments.html");
            					        popupLayout.Dismiss();
            				        })
            			        }
            		        }
            	        };
            	        var wrapper = new ScrollView
            	        {
            		        Content = content
            	        };
    
            	        return wrapper;
                    })
                };
    
                // Set the PopupView of the SfPopupLayout
                popupLayout.PopupView = popupView;
                popupLayout.PopupView.HeightRequest = 350;
    
                
                popupLayout.Show();
	        
        }
	}
}

