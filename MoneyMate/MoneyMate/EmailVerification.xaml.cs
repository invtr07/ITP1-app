using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MoneyMate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmailVerification : ContentPage
    {
        private string connString = "server=dbhost.cs.man.ac.uk;user=y95106bt;password=Maxwell8899;database=y95106bt";
        private static string _verificationCode; // Static field to store the verification code
        private static string _userEmail;
        public EmailVerification()
        {
            InitializeComponent();
        }

        private async void OnSendCodeClicked(object sender, EventArgs e)
        {
            _userEmail = EmailEntry.Text;
            if (!string.IsNullOrEmpty(_userEmail) && EmailExists())
            {
                // DisplayAlert("success", "Email already exists", "OK");
                _verificationCode = GenerateVerificationCode();
                bool emailSent = await SendEmailAsync(_userEmail, _verificationCode);
                if (emailSent)
                {
                    CodeEntry.IsVisible = true;
                    VerifyCodeButton.IsVisible = true;
                    SendCodeButton.IsEnabled = false;
                }
                else
                {
                   await DisplayAlert("Error", "Failed to send verification code. Please try again.", "OK");
                }
            }
            else
            {
               await  DisplayAlert("Error", "Email does not exist", "OK");
            }
            // Navigation.PushAsync(new MyTabbedPage());
        }

        private async void OnVerifyCodeClicked(object sender, EventArgs e)
        {
            string enteredCode = CodeEntry.Text;
            if (enteredCode == _verificationCode)
            {
                await DisplayAlert("Success", "Email verified successfully!", "OK");
                // Navigate to another page or update the app state
            }
            else
            {
                await DisplayAlert("Error", "Verification code is incorrect. Please try again.", "OK");
            }
        }
        
        public bool EmailExists()
        {
            using (var conn = new MySqlConnection(connString))
            {
                conn.Open();
                var query = @"SELECT COUNT(1) FROM customer WHERE customer_ID = @UserID";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", App.savedID);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                } 
                
            }
        }
        private string GenerateVerificationCode()
        {
            return new Random().Next(1000, 9999).ToString(); // Generate a 4-digit code
        }
        
        private async Task<bool> SendEmailAsync(string email, string code)
        {
            var apiKey = "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"; // Replace with your actual API Key
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("muratnurali777@gmail.com", "Money Mate");
            var subject = "Your Verification Code";
            var to = new EmailAddress(email);
            var plainTextContent = $"Your verification code is: {code}";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, null);
            var response = await client.SendEmailAsync(msg);

            return response.StatusCode == System.Net.HttpStatusCode.Accepted;
        }
    }
}