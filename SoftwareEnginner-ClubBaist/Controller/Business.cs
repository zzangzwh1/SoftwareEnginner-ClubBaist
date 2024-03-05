using SoftwareEnginner_ClubBaist.Business;
using System.Text;
namespace SoftwareEnginner_ClubBaist.Controller
{
    public class Business : IBusiness
    {

        #region GetMember
        public List<Models.ClubMember> GetClubClubMember()
        {
            TechService.ClubMember clubMenber = new TechService.ClubMember();
            List<Models.ClubMember> clubMemebers =  clubMenber.GetMembers();
            return clubMemebers;
        }

        #endregion

        #region SignUP
        public bool AddClubMember(Models.ClubMember members)
        {
            TechService.ClubMember clubMenber = new TechService.ClubMember();
            bool isSuccess = clubMenber.AddClubMember(members);
            return isSuccess;
        }

        public string SetDateofBirthDate(string date)
        {
            StringBuilder dateString = new StringBuilder();

            for (int i = 0; i < date.Length; i++)
            {
                if (i == 4 || i == 6)
                {
                    dateString.Append('-');
                }

                dateString.Append(date[i]);

            }
            return dateString.ToString();
        }
        #endregion

        #region Login
        public string MemberLogin(Models.ClubMember members)
        {
            TechService.ClubMember login = new TechService.ClubMember();
            string memberName = login.MemberLogin(members);
            return memberName;
        }
        #endregion


        #region  Index / Home /Admin
        public string DisplayStatus(string username)
        {
            TechService.ClubMember memeber = new TechService.ClubMember();
            string status = memeber.MemberStatus(username);
            return status;
        }
        public void RejectMembers(string username)
        {

            TechService.ClubMember memeber = new TechService.ClubMember();
            memeber.RejectMember(username);
        }

        public void ModifyMemberRegister(string username)
        {
            TechService.ClubMember memeber = new TechService.ClubMember();
            memeber.ApproveMember(username);

        }
        public List<Models.ClubMember> DisplayMember()
        {
            TechService.ClubMember memeber = new TechService.ClubMember();
            List<Models.ClubMember> clubMembers = memeber.GetMemberForApprove();
            return clubMembers;
        }
        public string DisplayMemberFirstName(string username)
        {

            TechService.ClubMember memeber = new TechService.ClubMember();
            return memeber.GetMemberName(username);

        }
        #endregion

        #region Reservation

        public string IsWeekDayOrWeekend(string date)
        {
            string s = "";
          //  string holidays = date.Substring(5, 5);
            if (string.IsNullOrEmpty(date))
            {
                return "";
            }
            else if (!IsHolday(date))
            {
                return "weekend";
            }
            else
            {
                DateTime dateTime;
                if (DateTime.TryParse(date, out dateTime))
                {
                    DayOfWeek dayOfWeek = dateTime.DayOfWeek;

                    if (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday)
                    {
                        return "weekend";

                    }

                }
            }
            return "weekday";

        }
        public bool IsHolday(string selectedDate)
        {
            string holiday = selectedDate.Substring(5, 5);
            if (holiday == "01-01" || holiday == "03-29"  || holiday == "05-20" || holiday =="07-01" || holiday == "08-05" || holiday == "09-02" || holiday =="09-30" || holiday =="10-24"|| holiday == "11-11" ||  holiday =="12-25" )
            {
                return false;
            }
            return true;
        }
        public bool IsValidateDate(string bTime, Models.ClubBooking bookings)
        {
            bool isValidate = true;
            string currentTime = DateTime.Now.ToString("yyyy-MM-dd");
            int currentHour = DateTime.Now.Hour;
            int currentMins = DateTime.Now.Minute;
            int hour = Convert.ToInt32(bTime.Substring(0, 2));
            int mins = Convert.ToInt32(bTime.Substring(4, 2));
            if (currentTime == bookings.BookingDate &&
   (currentHour > hour || (currentHour == hour && currentMins > mins)))
            {
                isValidate = false;
            }
            return isValidate;
        }
        public int GetMemberID(string memeberName)
        {
            TechService.ClubBooking booking = new TechService.ClubBooking();
            // string memberName = HttpContext.Session.GetString("member")!;
            int memberID = booking.GetMemberID(memeberName);
            return memberID;
        }
        public int GnerateBookingID()
        {
            Random random = new Random();
            return random.Next(100000000, 999999999);
        }

        public string InsertClubBooking(Models.ClubBooking clubBookings, int memberID)
        {
            //  string username = HttpContext.Session.GetString("member")!;
            TechService.ClubBooking booking = new TechService.ClubBooking();
            return booking.InsertIntoClubBooking(clubBookings, memberID);

        }
        public List<Models.ClubBooking> ViewTeemTime(string selectedDate)
        {
            TechService.ClubBooking booking = new TechService.ClubBooking();

            List<Models.ClubBooking> bookedItems = booking.DisplayTeeTimeList(selectedDate);
            return bookedItems;


        }
        public int VeryfyMemberOrAdmin(string username, int memberID)
        {
            return username != "Admin" ? GetMemberID(username) : memberID;
        }
        public string IsMemberRegistered(string setUserName)
        {
            TechService.ClubBooking booking = new TechService.ClubBooking();
            string isRegister = booking.IsMemberRegistered(setUserName);
            return isRegister;
        }

        public string IdentifyMembershipType(string setUserName)
        {
            TechService.ClubBooking booking = new TechService.ClubBooking();
            string memebershipType = booking.IdentifyMembershipType(setUserName);
            return memebershipType;
        }

        #endregion
    }
}
