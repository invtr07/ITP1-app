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

        public static MySqlConnection dbConnection = new MySqlConnection("server=dbhost.cs.man.ac.uk;user=y95106bt;password=Maxwell8899;database=y95106bt");

        public App ()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF5cXmZCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXtec3RdRGhcVkFxXEE=");

            InitializeComponent();

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

