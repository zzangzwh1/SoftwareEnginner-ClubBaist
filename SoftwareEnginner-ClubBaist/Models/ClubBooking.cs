using System.ComponentModel.DataAnnotations;

namespace SoftwareEnginner_ClubBaist.Models
{
    public class ClubBooking
    {
        public int BookingID { set; get; }
        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        [StringLength(50)]
        public string BookingTime { get; set; }

        [Required]
        public int NumOfPlayer { get; set; }

        [Required]
        public int NumOfCarts { get; set; }
    }
}
