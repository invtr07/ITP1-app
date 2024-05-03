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
    public partial class QuestionsPage2 : ContentPage
    {
        public QuestionsPage2()
        {
            InitializeComponent();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new QuestionsPage3());
        }
    }
}