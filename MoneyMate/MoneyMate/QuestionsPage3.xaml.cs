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
    public partial class QuestionsPage3 : ContentPage
    {
        public QuestionsPage3()
        {
            InitializeComponent();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }
    }
}