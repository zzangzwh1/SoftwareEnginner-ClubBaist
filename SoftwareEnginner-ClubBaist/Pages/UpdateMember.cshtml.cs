using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoftwareEnginner_ClubBaist.Business;
using SoftwareEnginner_ClubBaist.Controller;
using SoftwareEnginner_ClubBaist.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SoftwareEnginner_ClubBaist.Pages
{
    public class UpdateMemberModel : PageModel
    {

        IBusiness business = new Controller.Business();
        [BindProperty]
      
        public int aNumOfPlayer { set; get; }
        [BindProperty]
        public int aNumOfCart { set; get; }
        [BindProperty]
        public string aDate { set; get; } = "";
        [BindProperty]
        public string aTime { set; get; } = "";
        [BindProperty]
        public string aFullname { set; get; } = "";
        [BindProperty]
        public int aID { set; get; }
        [BindProperty]
        public string UpdateResult { get; set; } = string.Empty;

        public string Username = "";
        [BindProperty]
        public string MemberID { set; get; } = "";
        [BindProperty]
        public int aBookingID { set; get; }
        public string Admin { set; get; } = "";
        public string Message { set; get; } = "";

        public List<BookingReservation> reservations = null;
        public Models.ClubBooking clubMember = null;


        public void OnGet()
        {
            Username = HttpContext.Session.GetString("member")!;
        }
        public void OnPostFindMember()
        {
            reservations = business.GetBookingReservations(Convert.ToInt16(MemberID));
            Username = HttpContext.Session.GetString("member")!;
                                  
            if (reservations == null)
            {
                Message = "Not Exists";
              
            }
            else
            {
                Message = "";
            }
        }
        public void OnPostUpdate()
        {
            reservations = business.GetBookingReservations(Convert.ToInt16(MemberID));
            Username = HttpContext.Session.GetString("member")!;
        
            clubMember = new()
            {
                BookingDate = aDate,
                BookingID = aBookingID,
                NumOfCarts = aNumOfCart,
                NumOfPlayer = aNumOfPlayer,
                FullName = aFullname,
                BookingTime = aTime

            };
            string isSuccess = business.UpdateReservation(clubMember);
            Message = isSuccess;


            Username = HttpContext.Session.GetString("member")!;
        }
    }
}
