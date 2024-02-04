using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoftwareEnginner_ClubBaist.TechService;
using System.ComponentModel.DataAnnotations;
using SoftwareEnginner_ClubBaist.Models;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;

namespace SoftwareEnginner_ClubBaist.Pages
{
    public class ReservationModel : PageModel
    {

        //  public string CurrentSessionUser { set; get; } = string.Empty;
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

        public string MemberShipType { set; get; }

        public string SetUserName = "";
        public List<Models.ClubBooking> TeeTimeList = null!;
        [BindProperty]
        public DateTime SelectDateForView { set; get; }
        

        public string CurrentDateDate { set; get; }


        public string Message = "";
        public void OnGet()
        {
            GetSession();
        }
        public void OnPostBook()
        {
            GetSession();

            string username = HttpContext.Session.GetString("member")!;
            int memberID = VeryfyMemberOrAdmin(username);

            int bookingID = GnerateBookingID();
            Models.ClubBooking bookings = new()
            {
                BookingID = bookingID,
                BookingDate = Bdate,
                BookingTime = Btime,
                NumOfPlayer = numPlayer,
                NumOfCarts = numCart
            };

            bool isValidateTime = IsValidateDate(Btime, bookings);
       

            string isBooked = "";
            string weekend = IsWeekDayOrWeekend(Bdate);
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
                isBooked = InsertClubBooking(bookings, memberID);
            }

                   
            if (isBooked == "success" && isValidateTime && isWeekendTimeValiedTime)
            {
                Message = "Your Reservation is Successfully Booked";
                SetDefault();
            }
            else if (!isValidateTime)
            {
                Message = "Selected Previous Time Please Selec Again!";
                SetDefault();
            }
            else if (!isWeekendTimeValiedTime && MemberShipType == "Silver")
            {
                Message = "Silver is Only valid after 11AM for booking on Weekends";
            }
            else if (!isWeekendTimeValiedTime && MemberShipType == "Bronze")
            {
                Message = "Bronze is Only Valid for booking after 1PM on Weekends";
            }
    
            else
            {
                Message = isBooked;

            }
            GetSession();
        }
        private string IsWeekDayOrWeekend(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return "";
            }
            else
            {
                DateTime dateTime;
                if (DateTime.TryParse(date, out dateTime))
                {
                    DayOfWeek dayOfWeek = dateTime.DayOfWeek;

                    if (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday)
                    {
                        return "weekend";
                      
                    }
                  
                }
            }
            return "weekday";
      
        }
        private bool IsValidateDate(string BTime, Models.ClubBooking bookings)
        {
            bool isValidate = true;
            string currentTime = DateTime.Now.ToString("yyyy-MM-dd");
            int currentHour = DateTime.Now.Hour;
            int currentMins = DateTime.Now.Minute;
            int hour = Convert.ToInt32(Btime.Substring(0, 2));
            int mins = Convert.ToInt32(Btime.Substring(4, 2));
            if (currentTime == bookings.BookingDate &&
   (currentHour > hour || (currentHour == hour && currentMins > mins)))
            {
                isValidate = false;
            }
            return isValidate;
        }
        private int VeryfyMemberOrAdmin(string memberStauts)
        {
            return memberStauts != "Admin" ? GetMemberID() : SetMemberID;
        }
        private void SetDefault()
        {
            SetMemberID = 0;
            Bdate = "";
            Btime = "";
            numPlayer = 1;
            numCart = 0;
        }
        private void OnPostViewTeeTime()
        {

            string result = SelectDateForView.ToString("yyyy-MM-dd");
            TeeTimeList = ViewTeemTime(result);
            GetSession();
        }
        private List<Models.ClubBooking> ViewTeemTime(string selectedDate)
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
            {

                IsRegistered = "";
            }
            else
            {
                TechService.ClubBooking booking = new TechService.ClubBooking();
                IsRegistered = booking.IsMemberRegistered(SetUserName);
                if (IsRegistered == "True")
                {
                    MemberShipType = booking.IdentifyMembershipType(SetUserName);
                    CurrentDateDate = DateTime.Now.DayOfWeek.ToString();
                }
                else
                    MemberShipType = "";

            }

        }

    }
}
