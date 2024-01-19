using System.Data;

using System.Net;
using SoftwareEnginner_ClubBaist.Models;
using Microsoft.Data.SqlClient;
namespace SoftwareEnginner_ClubBaist.TechService
{
    public class ClubMember
    {
        private static string? _connectionString;
        public ClubMember()
        {
            ConfigurationBuilder databaseUserBuilder = new ConfigurationBuilder();
            databaseUserBuilder.SetBasePath(Directory.GetCurrentDirectory());
            databaseUserBuilder.AddJsonFile("appsettings.json");
            IConfiguration databaseUsersConfiguration = databaseUserBuilder.Build();
            _connectionString = databaseUsersConfiguration.GetConnectionString("BAIST3150");
        }

        #region Login Member

        public string MemberLogin(Models.ClubMember member)
        {
            string firstName = "";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("MemberLogin", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        command.Parameters.AddWithValue("@UserName", member.UserName).SqlDbType = SqlDbType.NVarChar;
                        command.Parameters.AddWithValue("@Password", member.Password).SqlDbType = SqlDbType.NVarChar;


                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    firstName = (string)reader["firstname"];
                                }
                            }
                            else
                            {
                                firstName = "";
                            }
                        }


                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }


                }
            }
            return firstName;
        }

        #endregion
        #region Add ClubMember
        public bool AddClubMember(Models.ClubMember member)
        {

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("CreateMember", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        command.Parameters.AddWithValue("@UserName", member.UserName).SqlDbType = SqlDbType.NVarChar;
                        command.Parameters.AddWithValue("@Password", member.Password).SqlDbType = SqlDbType.NVarChar;
                        command.Parameters.AddWithValue("@FirstName", member.FirstName).SqlDbType = SqlDbType.NVarChar;
                        command.Parameters.AddWithValue("@LastName", member.LastName).SqlDbType = SqlDbType.NVarChar;
                        command.Parameters.AddWithValue("@PostalCode", member.PostalCode).SqlDbType = SqlDbType.NVarChar;
                        command.Parameters.AddWithValue("@Address", member.Address).SqlDbType = SqlDbType.NVarChar;
                        command.Parameters.AddWithValue("@Occupation", member.Occupation).SqlDbType = SqlDbType.NVarChar;
                        command.Parameters.AddWithValue("@CompanyName", member.CompanyName).SqlDbType = SqlDbType.NVarChar;
                        command.Parameters.AddWithValue("@Email", member.Email).SqlDbType = SqlDbType.NVarChar;
                        command.Parameters.AddWithValue("@DateOfBirth", member.DateOfBirth).SqlDbType = SqlDbType.NVarChar;
                        command.Parameters.AddWithValue("@Phone", member.Phone).SqlDbType = SqlDbType.NVarChar;
                        command.Parameters.AddWithValue("@MembershipType", member.MembershipType).SqlDbType = SqlDbType.NVarChar;




                        command.ExecuteNonQuery();


                    }
                    catch (Exception ex)
                    {

                        return false;
                    }
                    finally
                    {
                        conn.Close();
                    }


                }
            }
            return true;
        }
        #endregion
    }
}
