namespace P04.AddMinion
{
    using System.Text;
    using Microsoft.Data.SqlClient;

    public class StartUp
    {

        static async Task Main(string[] args)
        {
            await using SqlConnection sqlConnection = new SqlConnection(Config.ConnectionString);
            await sqlConnection.OpenAsync();

            string[] minionArgs = Console.ReadLine().Split(": ", StringSplitOptions.RemoveEmptyEntries).ToArray();

            string[] villainArgs = Console.ReadLine().Split(": ", StringSplitOptions.RemoveEmptyEntries).ToArray();

            string result = await AddNewMinionAsync(sqlConnection, minionArgs[1], villainArgs[1]);
            Console.WriteLine(result);
        }

        static async Task<string> AddNewMinionAsync(SqlConnection sqlConnection,  string minionInfo, string villainName)
        {
            StringBuilder sb = new StringBuilder();
            string[] minionArgs = minionInfo.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();
            string minionName = minionArgs[0];
            int minionAge = int.Parse(minionArgs[1]);
            string townName = minionArgs[2];

            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
            try
            {
                int townId = await GetTownIdOrAddByNameAsync(sqlConnection, sqlTransaction, sb, townName);
                int villainId = await GetVillainGetIdOrAddByNameAsync(sqlConnection, sqlTransaction, sb, villainName);
                int minionId = await AddNewMinionAndReturnIdAsync(sqlConnection, sqlTransaction, minionName, minionAge, townId);
                await SetMinionToBeServantOfVillainAsync(sqlConnection, sqlTransaction, minionId, villainId);
                sb.AppendLine($"Successfully added {minionName} to be minion of {villainName}.");
                await sqlTransaction.CommitAsync();
            }
            catch (Exception e)
            {
                await sqlTransaction.RollbackAsync();
            }

            return sb.ToString().TrimEnd();

        }

        private static async Task SetMinionToBeServantOfVillainAsync(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int minionId, int villainId)
        {
            SqlCommand addMinionVillainCommand =
                new SqlCommand(SqlQueries.SetMinionToBeServantOfVillain, sqlConnection, sqlTransaction);
            addMinionVillainCommand.Parameters.AddWithValue("@minionId", minionId);
            addMinionVillainCommand.Parameters.AddWithValue("@villainId", villainId);
            await addMinionVillainCommand.ExecuteNonQueryAsync();
            
        }

        private static async Task<int> AddNewMinionAndReturnIdAsync(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string minionName,
            int minionAge, int townId)
        {
            SqlCommand addMinionCommand = new SqlCommand(SqlQueries.AddNewMinion, sqlConnection, sqlTransaction);
            addMinionCommand.Parameters.AddWithValue("@Name", minionName);
            addMinionCommand.Parameters.AddWithValue("@Age", minionAge);
            addMinionCommand.Parameters.AddWithValue("@townId", townId);
            await addMinionCommand.ExecuteNonQueryAsync();

            SqlCommand getMinionIdCommand = new SqlCommand(SqlQueries.GetMinionByName, sqlConnection, sqlTransaction);
            getMinionIdCommand.Parameters.AddWithValue("@Name", minionName);
            int minionId = (int)await getMinionIdCommand.ExecuteScalarAsync();
            return minionId;
        }

        private static async Task<int> GetVillainGetIdOrAddByNameAsync(SqlConnection sqlConnection, SqlTransaction sqlTransaction, StringBuilder sb,
            string villainName)
        {
            SqlCommand GetVillainIdCommand = new SqlCommand(SqlQueries.GetVillainIdByName, sqlConnection, sqlTransaction);
            GetVillainIdCommand.Parameters.AddWithValue("@Name", villainName);
            int? villainId = (int?)await GetVillainIdCommand.ExecuteScalarAsync();
            if (!villainId.HasValue)
            {
                SqlCommand addVillainCommand =
                    new SqlCommand(SqlQueries.AddVillainWithDefaultEvilnessFactor, sqlConnection, sqlTransaction);
                addVillainCommand.Parameters.AddWithValue("@villainName", villainName);
                await addVillainCommand.ExecuteNonQueryAsync();
                villainId = (int?)await GetVillainIdCommand.ExecuteScalarAsync();
                sb.AppendLine($"Villain {villainName} was added to the database.");
            }
            return villainId.Value;
        }

        private static async Task<int> GetTownIdOrAddByNameAsync(SqlConnection sqlConnection, SqlTransaction sqlTransaction, StringBuilder sb, string townName)
        {
            SqlCommand getTownIdCommand = new SqlCommand(SqlQueries.GetTownIdByName, sqlConnection, sqlTransaction);
            getTownIdCommand.Parameters.AddWithValue("@townName", townName);
            int? townId = (int?)await getTownIdCommand.ExecuteScalarAsync();
            if (!townId.HasValue)
            {
                SqlCommand addNewTownCommand = new SqlCommand(SqlQueries.AddNewTown, sqlConnection, sqlTransaction);
                addNewTownCommand.Parameters.AddWithValue("@townName", townName);
                await addNewTownCommand.ExecuteNonQueryAsync();
                townId = (int)await getTownIdCommand.ExecuteScalarAsync();
                sb.AppendLine($"Town {townName} was added to the database.");
            }
            return townId.Value;
        }
    }
}