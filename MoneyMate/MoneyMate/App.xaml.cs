using System;
using System.Collections.Generic;
using MoneyMate.DatabaseAccess;
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

        public static List<OverdraftDetails> arrangedOver;
        public static List<OverdraftDetails> unarrangedOver;
        
        public static List<BudgetDetails> budgetLimits;
        public static List<MoneyPotDetails> moneyPots;

       

        public static MySqlConnection dbConnection = new MySqlConnection("server=dbhost.cs.man.ac.uk;user=y95106bt;password=Maxwell8899;database=y95106bt");

        public App ()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF5cXmZCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXtec3RdRGhcVkFxXEE=");

            InitializeComponent();
            
            

            MainPage = new NavigationPage(new OnboardingPage());
        }
        
        public class OverdraftDetails
        {
            public string productName { get; set; }
            public decimal dailyInterestRate { get; set; }
            public decimal interestFreeOverdraftLimit { get; set; }
        }

        public class BudgetDetails
        {
            public double LimitAmount { get; set; }
            public string Period { get; set; }
            public string Category { get; set; }
            public bool ThirdParty { get; set; }
        }
        
        public class MoneyPotDetails
        {
            public string PotName { get; set; }
            public DateTime DueDate { get; set; }
            public double TargetAmount { get; set; }
            public double CurrentAmount { get; set; } = 0;
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

