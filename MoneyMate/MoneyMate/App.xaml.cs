using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion;
using MySqlConnector;

namespace MoneyMate
{
    public partial class App : Application
    {
        public static string savedName;
        public static string savedSurname;
        public static string savedID;
        public static decimal savedTotalInterest;
        public static decimal paidInterest;
        
        public static decimal income;
        public static decimal expenses;
        
        public static decimal persCurrStartingBalance; //starting balance
        public static decimal personalCurrentBalance; //calculated personal balance
        
        public static decimal savingsStartingBalance1;
        public static decimal savingsStartingBalance2;
        
        
        public static decimal creditStartingBalance1;
        public static decimal creditStartingBalance2;
        
       

        public static MySqlConnection dbConnection = new MySqlConnection("server=dbhost.cs.man.ac.uk;user=y95106bt;password=Maxwell8899;database=y95106bt");

        public App ()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF5cXmZCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXtec3RdRGhcVkFxXEE=");

            InitializeComponent();
            
            // App.savingsStartingBalance = new decimal[2];
            // App.creditStartingBalance = new decimal[2];

            MainPage = new NavigationPage(new OnboardingPage());
        }
        
        protected override void OnStart ()
        {
            
        }

        protected override void OnSleep ()
        {
            
        }

        protected override void OnResume ()
        {
            
        }
    }
}

