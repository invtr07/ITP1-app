using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyMate.ParentPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CashflowTabs : TabbedPage
    {
        public CashflowTabs ()
        {
            InitializeComponent();
        }
    }
}
