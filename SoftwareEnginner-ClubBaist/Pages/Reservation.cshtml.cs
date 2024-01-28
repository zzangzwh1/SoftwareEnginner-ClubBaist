using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoftwareEnginner_ClubBaist.TechService;
using System.ComponentModel.DataAnnotations;

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
        public string Btime { set; get; }
        [BindProperty]
        [Required(ErrorMessage = "Please Insert Number Of Player")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Please enter a valid number")]
        public string numPlayer { set; get; }
        [BindProperty]
        public string numCart { set; get; }
        public void OnGet()
        {
            GetSession();
        }
        public void OnPostBook()
        {
            string s = "";
        }
        public void GetSession()
        {
            string setUserName = HttpContext.Session.GetString("member")!;
            if (string.IsNullOrEmpty(setUserName))
                IsRegistered = "";
            else
            {
                ClubBooking booking = new ClubBooking();

                IsRegistered = booking.IsMemberRegistered(setUserName);
            }
           //CurrentSessionUser = setUserName;


        }

        public void DisplayTable()
        {

        }
      
    }
}
