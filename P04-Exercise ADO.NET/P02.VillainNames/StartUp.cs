namespace P02Villain_Names
{
    using System.Text;
    using Microsoft.Data.SqlClient;

    public class StartUp
    {

        static async Task Main(string[] args)
        {
            await using SqlConnection sqlConnection = new SqlConnection(Config.ConnectionString);
            await sqlConnection.OpenAsync();


            string result = await GetAllVillainsAndCountOfTheirMinionsAsync(sqlConnection);
            Console.WriteLine(result);
        }
        static async Task<string> GetAllVillainsAndCountOfTheirMinionsAsync(SqlConnection sqlConnection)//P02Villain Names
        {
            StringBuilder sb = new StringBuilder();
            SqlCommand sqlCommand = new SqlCommand(SqlQueries.GetAllVillainsAndCountOfTheirMinions, sqlConnection);
            SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
            while (reader.Read())
            {
                string villainName = (string)reader["Name"];
                int minionsCount = (int)reader["MinionsCount"];
                sb.Append($"{villainName} - {minionsCount}");
            }
            return sb.ToString().TrimEnd();
        }
    }

}