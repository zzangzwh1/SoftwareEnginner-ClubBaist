using Microsoft.Data.SqlClient;
using System.Data;


namespace SoftwareEnginner_ClubBaist.TechService
{
    public class ClubBooking
    {
        private static string? _connectionString;
        public ClubBooking()
        {
            ConfigurationBuilder databaseUserBuilder = new ConfigurationBuilder();
            databaseUserBuilder.SetBasePath(Directory.GetCurrentDirectory());
            databaseUserBuilder.AddJsonFile("appsettings.json");
            IConfiguration databaseUsersConfiguration = databaseUserBuilder.Build();
            _connectionString = databaseUsersConfiguration.GetConnectionString("BAIST3150");
        }
        public string IdentifyMembershipType(string username)
        {
            string result = "";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("IdentifyMembershipType", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {

                                while (reader.Read())
                                {
                                    result = reader["memershipType"].ToString() ?? "";
                                }
                            }
                            else
                            {
                                result = "";
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error Occurred - {ex.Message}");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return result;

          

        }
        public List<Models.ClubBooking> DisplayTeeTimeList(string bookingDate)
        {
           var teeTimeView = new List<Models.ClubBooking>();
           
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("GetBookedData", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        command.Parameters.AddWithValue("@BookingDate", bookingDate);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {

                                while (reader.Read())
                                {
                                    Models.ClubBooking teeTimeItems = new Models.ClubBooking();

                                    teeTimeItems.BookingID = (int)reader["BookingID"];
                                    teeTimeItems.FullName = (string)reader["Member Name"];
                                    teeTimeItems.NumOfPlayer = (int)reader["# of Player"];
                                    //string phone = (string)reader["Phone"];
                                    Models.ClubMember member = new Models.ClubMember();
                                    teeTimeItems.members = member;
                                    member.Phone = (string)reader["Phone"];
                                    teeTimeItems.NumOfCarts = (int)reader["# of Carts"];
                                    teeTimeItems.BookingDate = (string)reader["Date"];
                                    teeTimeItems.BookingTime = (string)reader["Time"];

                                    teeTimeView.Add(teeTimeItems);
                                }


                            }
                            else
                            {
                                teeTimeView = null!;
                            }



                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error Occurred - {ex.Message}");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            string test = "";
            return teeTimeView;
        }
        public string InsertIntoClubBooking(Models.ClubBooking booking, int memberID)
        {
            //bool isBooked = true;
            string result = "success";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("InsertDataIntoClubBookAndMemberToBook", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        command.Parameters.AddWithValue("@MemberId", memberID).SqlDbType = SqlDbType.Int;
                        command.Parameters.AddWithValue("@BookingID", booking.BookingID).SqlDbType = SqlDbType.Int;
                        command.Parameters.AddWithValue("@BookingDate", booking.BookingDate).SqlDbType = SqlDbType.NVarChar;
                        command.Parameters.AddWithValue("@BookingTime", booking.BookingTime).SqlDbType = SqlDbType.NVarChar;
                        command.Parameters.AddWithValue("@NumOfPalyer", booking.NumOfPlayer).SqlDbType = SqlDbType.Int;
                        command.Parameters.AddWithValue("@NumOfCarts", booking.NumOfCarts).SqlDbType = SqlDbType.Int;

                        command.ExecuteNonQuery();
                       

                    }
                    catch (Exception ex)
                    {
                        result = ex.Message;
                        Console.WriteLine(ex.Message);
//                        isBooked = false;

                    }
                    finally
                    {
                        conn.Close();
                    }


                }
                return result;

            }
        }
        public int GetMemberID(string username)
        {

            string result = "";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("IsMemberRegister", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        command.Parameters.AddWithValue("@UserName", username);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {

                                while (reader.Read())
                                {


                                    result = reader["MemberId"].ToString() ?? "-1";
                                }


                            }
                            else
                            {
                                result = "-1";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error Occurred - {ex.Message}");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return Convert.ToInt32(result);
        }
        public string IsMemberRegistered(string username)
        {

            string result = "";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("IsMemberRegister", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {

                                while (reader.Read())
                                {
                                    result = reader["IsRegistered"].ToString() ?? "";
                                }
                            }
                            else
                            {
                                result = "";
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error Occurred - {ex.Message}");
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
