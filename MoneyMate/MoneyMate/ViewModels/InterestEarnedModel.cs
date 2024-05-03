using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace MoneyMate.ViewModels
{
    public class InterestEarnedModel : INotifyPropertyChanged
    {
        private decimal _totalAmount;
        public decimal TotalAmount
        {
            get => _totalAmount;
            set
            {
                if (_totalAmount != value)
                {
                    _totalAmount = value;
                    OnPropertyChanged();
                }
            }
        }

        public InterestEarnedModel()
        {
            // Initialize with a default value if necessary, e.g., 0
            TotalAmount = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}