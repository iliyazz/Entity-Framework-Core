namespace P03.MinionNames
{
    using System.Text;
    using Microsoft.Data.SqlClient;

    public class StartUp
    {

            static async Task Main(string[] args)
            {
                await using SqlConnection sqlConnection = new SqlConnection(Config.ConnectionString);
                await sqlConnection.OpenAsync();
     
                int villainId = int.Parse(Console.ReadLine());
     
                string result = await GetVillainWithAllMinionsByIdAsync(sqlConnection, villainId);
                Console.WriteLine(result);
            }
     
            static async Task<string> GetVillainWithAllMinionsByIdAsync(SqlConnection sqlConnection, int VillainId)
            {
     
     
                SqlCommand getVillainNameCommand = new SqlCommand(SqlQueries.GetVillainNameById, sqlConnection);
                getVillainNameCommand.Parameters.AddWithValue("@Id", VillainId);
     
                object? villainNameObj = await getVillainNameCommand.ExecuteScalarAsync();
     
                if (villainNameObj == null)
                {
                    return $"No villain with ID {VillainId} exists in the database.";
                }
                string villainName = (string)villainNameObj;
     
                SqlCommand getAllMinionsCommand = new SqlCommand(SqlQueries.getAllMinionsByVillainId, sqlConnection);
                getAllMinionsCommand.Parameters.AddWithValue("@Id", VillainId);
                SqlDataReader minionsReader = await getAllMinionsCommand.ExecuteReaderAsync();
                string output = GenerateVillainWithMinionsOutput(villainName, minionsReader);
                return output;
            }
     
            private static string GenerateVillainWithMinionsOutput(string villainName, SqlDataReader minionsReader)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Villain: {villainName}");
                if (!minionsReader.HasRows)
                {
                    sb.AppendLine("(no minions)");
                }
                else
                {
                    while (minionsReader.Read())
                    {
                        long rowNumber = (long)minionsReader["RowNum"];
                        string minionName = (string)minionsReader["Name"];
                        int minionAge = (int)minionsReader["Age"];
                        sb.AppendLine($"{rowNumber}. {minionName} {minionAge}");
                    }
                }
                return sb.ToString().Trim();
            }
        
    }

}