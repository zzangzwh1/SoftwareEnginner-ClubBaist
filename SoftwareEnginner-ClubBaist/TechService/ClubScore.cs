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
        #region MemberApproved

        public List<Models.ViewEveryScore> ViewEveryScores(DateTime FromDate, DateTime ToDate, int memberId)
        {
            List<Models.ViewEveryScore> viewScore = new List<Models.ViewEveryScore>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("ViewEveryScore", conn))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        command.Parameters.AddWithValue("@DateFrom", FromDate).SqlDbType = SqlDbType.DateTime;
                        command.Parameters.AddWithValue("@DateTo", ToDate).SqlDbType = SqlDbType.DateTime;
                        command.Parameters.AddWithValue("@MemberID", memberId).SqlDbType = SqlDbType.Int;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                              
                                while (reader.Read())
                                {
                                    Models.ViewEveryScore reservation = new Models.ViewEveryScore
                                    {
                                        FullName = (string)reader["FullName"],
                                        DateofBirth = (string)reader["DateOfBirth"],
                                        Score = (string)reader["Score"],
                                        ScoreDate = (DateTime)reader["ScoreDate"],
                                        TotalScore = (int)reader["TotalScore"],
                                        MemberID = (int)reader["MemberID"],


                                    };


                                    viewScore.Add(reservation);
                                }
                            }
                            else
                            {
                                viewScore = null!;
                            }


                        }
                    }
                    catch (Exception ex)
                    {
                        viewScore = null!;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return viewScore;


        }
        public List<Models.ViewEveryReservation> ViewEveryReservations(DateTime FromDate, DateTime ToDate, int memberId)
        {
            List<Models.ViewEveryReservation> viewReservations = new List<Models.ViewEveryReservation>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("ViewEveryReservation", conn))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        command.Parameters.AddWithValue("@DateFrom", FromDate).SqlDbType = SqlDbType.DateTime;
                        command.Parameters.AddWithValue("@DateTo", ToDate).SqlDbType = SqlDbType.DateTime;
                        command.Parameters.AddWithValue("@MemberID", memberId).SqlDbType = SqlDbType.Int;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                string d = "";
                                while (reader.Read())
                                {
                                    Models.ViewEveryReservation reservation = new Models.ViewEveryReservation
                                    {
                                        BookingDate = (string)reader["BookingDate"],
                                        NumOfCarts = (int)reader["NumOfCarts"],
                                        BookingTime = (string)reader["BookingTime"],
                                        NumOfPlayer = (int)reader["NumOfPalyer"],
                                        MemberID = (int)reader["MemberID"]



                                    };


                                    viewReservations.Add(reservation);
                                }
                            }
                            else
                            {
                                viewReservations = null!;
                            }


                        }
                    }
                    catch (Exception ex)
                    {
                        viewReservations = null!;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return viewReservations;


        }

        public int IsMemberIDApprovedAndRegister(int memberID)
        {
            int status = 0;


            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("IsMemberIDRegisterAndApproved", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        command.Parameters.AddWithValue("@MemberID", memberID).SqlDbType = SqlDbType.Int;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    status = (int)reader["MemberID"];


                                }
                            }
                            else
                            {
                                status = 0;
                            }
                        }


                    }
                    catch (Exception ex)
                    {

                        status = 0;
                    }
                    finally
                    {
                        conn.Close();
                    }


                }
            }
            return status;
        }
        public string IsMemberApproved(string username)
        {
            string status = "";


            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("IsMemberApproved", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        command.Parameters.AddWithValue("@UserName", username).SqlDbType = SqlDbType.NVarChar;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    status = reader["MemberId"].ToString()!;
                                    if (status == "")
                                        status = "Approved";

                                }
                            }
                            else
                            {
                                status = "";
                            }
                        }


                    }
                    catch (Exception ex)
                    {

                        status = "";
                    }
                    finally
                    {
                        conn.Close();
                    }


                }
            }
            return status;


        }
        #endregion

        #region InsertClubScore
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
        #endregion
    }
}
