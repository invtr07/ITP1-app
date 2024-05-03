using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;

namespace MoneyMate.DatabaseAccess
{
    public class DatabaseControl2
    {
        private static string connectionString =
            "server=dbhost.cs.man.ac.uk;user=y95106bt;password=Maxwell8899;database=y95106bt";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
        public static async Task<List<App.MoneyPotDetails>> FetchMoneyPots()
        {
            List<App.MoneyPotDetails> pots = new List<App.MoneyPotDetails>();
            using (var conn = GetConnection())
            {
                await conn.OpenAsync();
                var cmd = new MySqlCommand("SELECT PotId, PotName, TargetAmount, CurrentAmount, DueDate FROM MoneyPots", conn);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        pots.Add(new App.MoneyPotDetails
                        {
                            PotId = reader.GetInt32("PotId"),
                            PotName = reader.GetString("PotName"),
                            TargetAmount = reader.GetDouble("TargetAmount"),
                            CurrentAmount = reader.GetDouble("CurrentAmount"),
                            DueDate = reader.GetDateTime("DueDate")
                        });
                    }
                }
            }
            return pots;
        }
        
        public static async Task InsertMoneyPot(App.MoneyPotDetails pot)
        {
            using (var conn = GetConnection())
            {
                await conn.OpenAsync();
                var cmd = new MySqlCommand(
                    @"INSERT INTO moneypot (potName, savingGoal, amount, dueDate, customer_ID) VALUES (@PotName, @TargetAmount, @CurrentAmount, @DueDate, @customer_ID)"
                    , conn);
                cmd.Parameters.AddWithValue("@PotName", pot.PotName);
                cmd.Parameters.AddWithValue("@TargetAmount", pot.TargetAmount);
                cmd.Parameters.AddWithValue("@CurrentAmount", pot.CurrentAmount);
                cmd.Parameters.AddWithValue("@DueDate", pot.DueDate);
                cmd.Parameters.AddWithValue("@customer_ID", App.savedID);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public static async Task UpdateMoneyPot(App.MoneyPotDetails pot)
        {
            using (var conn = GetConnection())
            {
                await conn.OpenAsync();
                var cmd = new MySqlCommand(
                    @"UPDATE MoneyPots SET PotName=@PotName, TargetAmount=@TargetAmount, CurrentAmount=@CurrentAmount, DueDate=@DueDate WHERE PotId=@PotId"
                    , conn);
                cmd.Parameters.AddWithValue("@PotName", pot.PotName);
                cmd.Parameters.AddWithValue("@TargetAmount", pot.TargetAmount);
                cmd.Parameters.AddWithValue("@CurrentAmount", pot.CurrentAmount);
                cmd.Parameters.AddWithValue("@DueDate", pot.DueDate);
                cmd.Parameters.AddWithValue("@PotId", pot.PotId);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public static async Task DeleteMoneyPot(int potId)
        {
            using (var conn = GetConnection())
            {
                await conn.OpenAsync();
                var cmd = new MySqlCommand(@"DELETE FROM MoneyPots WHERE PotId=@PotId", conn);
                cmd.Parameters.AddWithValue("@PotId", potId);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}