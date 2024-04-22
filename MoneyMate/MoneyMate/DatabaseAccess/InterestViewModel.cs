using System;
using MySqlConnector;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MoneyMate.DatabaseAccess;
using MoneyMate;


public class InterestViewModel : INotifyPropertyChanged
{
    private string _totalInterest;
    public string TotalInterest
    {
        get { return _totalInterest; }
        set { 
            _totalInterest = value; 
            OnPropertyChanged(); 
        }
    }

    public InterestViewModel()
    {
        
    }

    public async Task InitializeAsync()
    {
        await FetchDataAsync();  // Convert FetchData to an async method
    }

    public async Task<string> FetchDataAsync()
    {
        string connectionString = "your_connection_string_here"; // Ensure this is your actual connection string
        string query = $@"SELECT SUM(transaction_Amount) AS Total 
                     FROM transaction t 
                     WHERE t.account_ID IN 
                         (SELECT account_ID 
                          FROM account 
                          WHERE customer_ID = {App.savedID} 
                          AND product_ID IN (103, 104))
                     AND t.transaction_Reference = 'Lloyds Bank';";

        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                MySqlCommand command = new MySqlCommand(query, connection);
                var result = command.ExecuteScalar();
                TotalInterest = result != DBNull.Value ? result.ToString() : "Oops, seems you don't have a saving account";
                OnPropertyChanged(nameof(TotalInterest)); // Notify any bound elements of the update
                return TotalInterest; // Return the fetched or default message
            }
        }
        catch (Exception ex)
        {
            // Handle potential exceptions that could occur during database access
            TotalInterest = $"Error accessing data: {ex.Message}";
            OnPropertyChanged(nameof(TotalInterest));
            return TotalInterest; // Return error message
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}