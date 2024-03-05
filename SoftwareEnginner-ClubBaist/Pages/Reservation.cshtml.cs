using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoftwareEnginner_ClubBaist.TechService;
using System.ComponentModel.DataAnnotations;
using SoftwareEnginner_ClubBaist.Models;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using SoftwareEnginner_ClubBaist.Business;
using Microsoft.Extensions.Primitives;

namespace SoftwareEnginner_ClubBaist.Pages
{
    public class ReservationModel : PageModel
    {
        IBusiness business = new Controller.Business();
       
        public string IsRegistered { set; get; } = "";

        [BindProperty]
        public string Bdate { set; get; } = "";
        [BindProperty]
        public string Btime { set; get; } = "";

        [BindProperty]
        public int numPlayer { set; get; } = 1;
        [BindProperty]
        public int numCart { set; get; }
        [BindProperty]
        public int SetMemberID { set; get; }

        public string MemberShipType { set; get; } = string.Empty;

        public string SetUserName = "";
        public List<Models.ClubBooking> TeeTimeList = null!;
        [BindProperty]
        public DateTime SelectDateForView { set; get; }


        public string CurrentDateDate { set; get; } = string.Empty;
        public string ReservationStatus { set; get; } = string.Empty;


        public string Message = "";

        public string Admin { set; get; }
        public void OnGet()
        {
            GetSession();
            ReservationStatus = "Please Select Date For Viewing Reservation"; 
        }
        public void OnPostBook()
        {
            GetSession();

            string username = HttpContext.Session.GetString("member")!;
            int memberID = business.VeryfyMemberOrAdmin(username,SetMemberID);

            int bookingID = business.GnerateBookingID();
            Models.ClubBooking bookings = new()
            {
                BookingID = bookingID,
                BookingDate = Bdate,
                BookingTime = Btime,
                NumOfPlayer = numPlayer,
                NumOfCarts = numCart
            };

            bool isValidateTime = business.IsValidateDate(Btime, bookings);
       

            string isBooked = "";
            string weekend = business.IsWeekDayOrWeekend(Bdate);
            bool isWeekendTimeValiedTime = true;
            int selectedDateHour = Convert.ToInt16(Btime.Substring(0, 2));
            if (weekend == "weekend" && MemberShipType == "Silver" && selectedDateHour <= 11)
            {
                isWeekendTimeValiedTime = false;
            }
            if (weekend == "weekend" && MemberShipType == "Bronze" && selectedDateHour <= 13)
            {
                isWeekendTimeValiedTime = false;
            }
            if (isValidateTime && isWeekendTimeValiedTime)
            {
                isBooked = business.InsertClubBooking(bookings, memberID);
            }

                   
            if (isBooked == "success" && isValidateTime && isWeekendTimeValiedTime)
            {
                Message = "Your Reservation is Successfully Booked";
                SetDefault();
            }
            else if (!isValidateTime)
            {
                Message = "Selected Previous Time Please Select Again!";
                SetDefault();
            }
            else if (!isWeekendTimeValiedTime && MemberShipType == "Silver")
            {
                Message = "Silver is Only valid after 11AM for booking on Weekends/Holiday";
            }
            else if (!isWeekendTimeValiedTime && MemberShipType == "Bronze")
            {
                Message = "Bronze is Only Valid for booking after 1PM on Weekends/Holiday";
            }
    
            else
            {
                Message = isBooked;

            }
            GetSession();
        }
        public void OnPostViewTeeTime()
        {
            SetUserName = HttpContext.Session.GetString("member")!;
           
            string result = SelectDateForView.ToString("yyyy-MM-dd");
            TeeTimeList = business.ViewTeemTime(result);
            if(TeeTimeList is null)
            {
                ReservationStatus = "No Reservation Exsits Please select other Date";
            }
     
            GetSession();
        }     
        private void SetDefault()
        {
            SetMemberID = 0;
            Bdate = "";
            Btime = "";
            numPlayer = 1;
            numCart = 0;
        }     

        private void GetSession()
        {
        
            SetUserName = HttpContext.Session.GetString("member")!;

            if (string.IsNullOrEmpty(SetUserName))
            {

                IsRegistered = "";
            }
            else
            {
               
                IsRegistered = business.IsMemberRegistered(SetUserName);
                if (IsRegistered == "True")
                {
                    MemberShipType = business.IdentifyMembershipType(SetUserName);
                    CurrentDateDate = DateTime.Now.DayOfWeek.ToString();
                }
                else
                    MemberShipType = "";

            }

        }

    }
}
