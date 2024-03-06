namespace SoftwareEnginner_ClubBaist.Models
{
    public class BookingReservation
    {
        public int MemberID { set; get; }
        public string FirstName { set; get; } = "";
        public string LastName { set; get; } = "";
        public int BookingId { set; get; }
        public string BookingDate { set; get; } = "";
        public string BookingTime { set; get; } = "";
        public int NumOfCars { set; get; }
        public int NumOfPlayer { set; get; }
    }
}
