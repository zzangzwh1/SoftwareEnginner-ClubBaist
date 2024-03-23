using Microsoft.Data.SqlClient;
using SoftwareEnginner_ClubBaist.Models;
using System.Data;

namespace SoftwareEnginner_ClubBaist.TechService
{
    public class ClubScore
    {
        private static string? _connectionString;
        public ClubScore()
        {
            ConfigurationBuilder databaseUserBuilder = new ConfigurationBuilder();
            databaseUserBuilder.SetBasePath(Directory.GetCurrentDirectory());
            databaseUserBuilder.AddJsonFile("appsettings.json");
            IConfiguration databaseUsersConfiguration = databaseUserBuilder.Build();
            _connectionString = databaseUsersConfiguration.GetConnectionString("BAIST3150");

        }
        public string InsertClubScore(ClubGolfScore golfScore)
        {
            string result = "Golf Score is Successfully Added!";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("CreateClubScore", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        command.Parameters.AddWithValue("@MemberID", golfScore.MemberID).SqlDbType = SqlDbType.Int;
                        command.Parameters.AddWithValue("@Score", golfScore.Score).SqlDbType = SqlDbType.NVarChar;
                        command.Parameters.AddWithValue("@TotalScore", golfScore.TotalScore).SqlDbType = SqlDbType.Int;
                        command.Parameters.AddWithValue("@ScoreDate", golfScore.ScoreDate).SqlDbType = SqlDbType.DateTime;

                        command.ExecuteNonQuery();


                    }
                    catch (Exception ex)
                    {

                        result = ex.Message;

                    }
                    finally
                    {
                        conn.Close();
                    }


                }
            }
            return result;
        }
    }
}
