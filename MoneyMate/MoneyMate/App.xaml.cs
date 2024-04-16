using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion;

namespace MoneyMate
{
    public partial class App : Application
    {
        public static string savedName;
        public static string savedSurname;
        public static string savedID;
        
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

