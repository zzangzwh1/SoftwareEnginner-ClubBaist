﻿namespace SoftwareEnginner_ClubBaist.Business
{
    public interface IBusiness
    {
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
    }
}
