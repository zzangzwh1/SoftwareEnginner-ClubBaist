namespace SoftwareEnginner_ClubBaist.Models
{
    public class ViewEveryScore
    {
        public string FullName { get; set; } = "";
        public string DateofBirth { get; set; } = "";
        public string Score { get; set; } = "";
        public int TotalScore { get; set; }
        public DateTime ScoreDate { get; set; }
        public int MemberID { get; set; }
     

    }
    public class ViewEveryReservation
    {
        public int MemberID { get; set; }
        public string BookingDate { set; get; } = "";
        public int NumOfPlayer { set; get; }
        public string BookingTime { set; get; } = "";
        public int NumOfCarts { set; get;  } 
    
    }
}
