using SoftwareEnginner_ClubBaist.Models;

namespace SoftwareEnginner_ClubBaist.Business
{
    public interface IBusiness
    {
        public List<BookingReservation> GetBookingReservations(int memberID);
        public bool AddClubMember(Models.ClubMember members);
        public string SetDateofBirthDate(string date);
        public string MemberLogin(Models.ClubMember members);
        public string DisplayStatus(string username);
        public void RejectMembers(string username);
        public void ModifyMemberRegister(string username);
        public List<Models.ClubMember> DisplayMember();
        public string DisplayMemberFirstName(string username);
        public string IsWeekDayOrWeekend(string date);
        public bool IsValidateDate(string bTime, Models.ClubBooking bookings);
        public int GetMemberID(string memeberName);
        public int GnerateBookingID();
        public string InsertClubBooking(Models.ClubBooking clubBookings, int memberID);
        public List<Models.ClubBooking> ViewTeemTime(string selectedDate);
        public int VeryfyMemberOrAdmin(string username, int memberID);
        public string IsMemberRegistered(string setUserName);
        public string IdentifyMembershipType(string setUserName);
        public bool IsHolday(string selectedDate);
        public List<Models.ClubMember> GetClubClubMember();
        public string UpdateReservation(Models.ClubBooking clubMember);
        public List<string> GetScoreData(string Test1, string Test2, string Test3, string Test4, string Test5, string Test6, string Test7, string Test8, string Test9, string Test10, string Test11, string Test12, string Test13, string Test14, string Test15, string Test16, string Test17, string Test18);
        public string InsertClubScore(ClubGolfScore golfScore);
        public string IsMemberApproved(string username);
        public int IsMemberIDApprovedAndRegister(int memberID);
        public List<Models.ViewEveryScore> ViewEveryScores(DateTime FromDate, DateTime ToDate, int memberId);
        public List<Models.ViewEveryReservation> ViewEveryReservations(DateTime FromDate, DateTime ToDate, int memberId);
        public List<Models.ViewEveryScore> ViewEveryScoresWithNoRangeDate(int memberID);
        public List<Models.ViewEveryReservation> ViewEveryReservationsNoRange(int memberId);
        public List<Models.ViewEveryScore> GetEveryMemberWithRangeDate(DateTime FromDate, DateTime ToDate);
        public List<Models.ViewEveryReservation> ViewEveryReservationsWithRange(DateTime FromDate, DateTime ToDate);
    }
}
