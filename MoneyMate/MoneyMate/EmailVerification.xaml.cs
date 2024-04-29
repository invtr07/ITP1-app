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
    public partial class EmailVerification : ContentPage
    {
        public EmailVerification()
        {
            InitializeComponent();
        }

        private void OnSendCodeClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MyTabbedPage());
        }

        private void OnVerifyCodeClicked(object sender, EventArgs e)
        {
            
        }
        
        private string GenerateVerificationCode()
        {
            return new Random().Next(1000, 9999).ToString(); // Generate a 4-digit code
        }
    }
}