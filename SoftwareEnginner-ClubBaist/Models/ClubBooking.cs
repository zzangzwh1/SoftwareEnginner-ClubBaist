using System.ComponentModel.DataAnnotations;
using SoftwareEnginner_ClubBaist.Models;

namespace SoftwareEnginner_ClubBaist.Models
{
    public class ClubBooking
    {
        public int BookingID { set; get; }
        public string BookingDate { get; set; } = "";
        public string BookingTime { get; set; } = "";

        public int NumOfPlayer { get; set; }
        public int NumOfCarts { get; set; }
        public string FullName { get; set; } = "";

        public ClubMember members { get; set; }
    }
}
