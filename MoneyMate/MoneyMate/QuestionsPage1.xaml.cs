using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyMate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionsPage1 : ContentPage
    {
        public QuestionsPage1()
        {
            InitializeComponent();

            // userName.Text = $"Welcome back {App.savedName}!";
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new QuestionsPage2());
        }

        private void Button_OnClicked1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
            // Navigation.PushAsync(new MyTabbedPage());
        }
    }
}