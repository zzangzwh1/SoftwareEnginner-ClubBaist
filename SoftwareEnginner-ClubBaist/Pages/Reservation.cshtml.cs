using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoftwareEnginner_ClubBaist.TechService;
using System.ComponentModel.DataAnnotations;
using SoftwareEnginner_ClubBaist.Models;

namespace SoftwareEnginner_ClubBaist.Pages
{
    public class ReservationModel : PageModel
    {

        //  public string CurrentSessionUser { set; get; } = string.Empty;
        public string IsRegistered { set; get; } = "";

        [BindProperty]
        [Required(ErrorMessage = "Please Select Date")]
        public string Bdate { set; get; }
        [BindProperty]
        [Required(ErrorMessage = "Please Select Time")]
        public string Btime { set; get; } = "";

        [BindProperty]
        [Required(ErrorMessage = "Please Insert Number Of Player")]
        [RegularExpression("^[1-4]+$", ErrorMessage = "Number Of Player: Please enter a valid number ex) Minimum 1 Max 4")]
        public int numPlayer { set; get; } = 1;
        [BindProperty]
        [Required(ErrorMessage = "Please Insert Number Of Cart")]
        [RegularExpression("^[0-4]+$", ErrorMessage = "Number Of Cart: Please enter a valid number ex) Minimum 1 Max 4")]
        public int numCart { set; get; }
        [BindProperty]
        [Required(ErrorMessage = "Please Insert your MemberID")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Your MemberID is Number!")]
        public int SetMemberID { set; get; }


        public string SetUserName = "";


        public List<Models.ClubBooking> TeeTimeList = null!;
        [BindProperty]
        public DateTime SelectDateForView { set; get; }


        public string Message = "";
        public void OnGet()
        {
            GetSession();
        }
        public void OnPostBook()
        {
           
            string username = HttpContext.Session.GetString("member")!;
            int memberID = VeryfyMemberOrAdmin(username);

            int bookingID = GnerateBookingID();
            Models.ClubBooking Bookings = new()
            {
                BookingID = bookingID,
                BookingDate = Bdate,
                BookingTime = Btime,
                NumOfPlayer = numPlayer,
                NumOfCarts = numCart
            };
            string isBooked = InsertClubBooking(Bookings, memberID);
            if (isBooked == "success")
            {
                Message = "Your Reservation is Successfully Booked";
                SetDefault();
            }
            else
            {
                Message = isBooked;

            }
            GetSession();
        }
        private int VeryfyMemberOrAdmin(string memberStauts)
        {
            return memberStauts != "Admin" ? GetMemberID() : SetMemberID;
        }
        public void SetDefault()
        {
            Bdate = "";
            Btime = "";
            numPlayer = 1;
            numCart = 0;
        }
        public void OnPostViewTeeTime()
        {

            string result = SelectDateForView.ToString("yyyy-MM-dd");
            TeeTimeList = ViewTeemTime(result);
            GetSession();
        }
        public List<Models.ClubBooking> ViewTeemTime(string selectedDate)
        {
            TechService.ClubBooking booking = new TechService.ClubBooking();

            List<Models.ClubBooking> bookedItems = booking.DisplayTeeTimeList(selectedDate);
            return bookedItems;


        }
        private int GnerateBookingID()
        {
            Random random = new Random();
            return random.Next(100000000, 999999999);
        }

        private string InsertClubBooking(Models.ClubBooking clubBookings, int memberID)
        {
            //  string username = HttpContext.Session.GetString("member")!;
            TechService.ClubBooking booking = new TechService.ClubBooking();
            return booking.InsertIntoClubBooking(clubBookings, memberID);

        }
        private int GetMemberID()
        {
            TechService.ClubBooking booking = new TechService.ClubBooking();
            string memberName = HttpContext.Session.GetString("member")!;
            int memberID = booking.GetMemberID(memberName);
            return memberID;
        }
        private void GetSession()
        {
            SetUserName = HttpContext.Session.GetString("member")!;
            if (string.IsNullOrEmpty(SetUserName))
                IsRegistered = "";
            else
            {
                TechService.ClubBooking booking = new TechService.ClubBooking();
                IsRegistered = booking.IsMemberRegistered(SetUserName);
            }

        }



    }
}
