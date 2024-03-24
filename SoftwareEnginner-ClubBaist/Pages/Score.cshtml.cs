using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoftwareEnginner_ClubBaist.Business;
using SoftwareEnginner_ClubBaist.Models;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static System.Formats.Asn1.AsnWriter;

namespace SoftwareEnginner_ClubBaist.Pages
{
    public class ScoreModel : PageModel
    {

        IBusiness business = new Controller.Business();

        [BindProperty]
        public string HoleScore1 { get; set; } = "";
        [BindProperty]

        public string HoleScore2 { get; set; } = "";
        [BindProperty]

        public string HoleScore3 { get; set; } = "";
        [BindProperty]

        public string HoleScore4 { get; set; } = "";
        [BindProperty]

        public string HoleScore5 { get; set; } = "";
        [BindProperty]

        public string HoleScore6 { get; set; } = "";
        [BindProperty]

        public string HoleScore7 { get; set; } = "";
        [BindProperty]

        public string HoleScore8 { get; set; } = "";
        [BindProperty]

        public string HoleScore9 { get; set; } = "";
        [BindProperty]

        public string HoleScore10 { get; set; } = "";
        [BindProperty]
        public string HoleScore11 { get; set; } = "";
        [BindProperty]
        public string HoleScore12 { get; set; } = "";
        [BindProperty]
        public string HoleScore13 { get; set; } = "";
        [BindProperty]
        public string HoleScore14 { get; set; } = "";
        [BindProperty]
        public string HoleScore15 { get; set; } = "";
        [BindProperty]
        public string HoleScore16 { get; set; } = "";
        [BindProperty]
        public string HoleScore17 { get; set; } = "";

        [BindProperty]
        public string HoleScore18 { get; set; } = "";
        [BindProperty]
        public int MemberID { set; get; }
        [BindProperty]
        public DateTime Bdate { get; set; }

        public string Message = "";
        public string IsRegistered { set; get; } = "";
        public string UserName = "";
        public ClubGolfScore golfScore = null!;
        public string[] SplitScore = null!;
        public void OnGet()
        {
            GetSession();
            Message = "Only Number is Valid for Submitting Score!";
        }
        public void OnPost()
        {
            GetSession();
            StringBuilder sb = new StringBuilder();
            List<string> GetScoreDatas = business.GetScoreData(HoleScore1, HoleScore2, HoleScore3, HoleScore4, HoleScore5, HoleScore6, HoleScore7, HoleScore8, HoleScore9, HoleScore10, HoleScore11, HoleScore12, HoleScore13, HoleScore14, HoleScore15, HoleScore16, HoleScore17, HoleScore18);

            int totalScore = Convert.ToInt32(GetScoreDatas[GetScoreDatas.Count - 1]);
            GetScoreDatas.Remove(GetScoreDatas[GetScoreDatas.Count - 1]);           
            int memberId = 0;    
            
            foreach (string value in GetScoreDatas)
            {                
                sb.Append(value + ',');             
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1); // Remove the last character (which is the comma)
            }
            bool isValid = true;
            if(UserName == "Admin")
            {
                memberId = business.IsMemberIDApprovedAndRegister(MemberID);
                if (memberId <= 0)
                    isValid = false;
            }         
            else if (!string.IsNullOrEmpty(UserName) && UserName != "Admin")
            {
                memberId = business.GetMemberID(UserName);
            }
            else
            {
                isValid = false;
            }



            golfScore = new()
            {
                MemberID = memberId,
                Score = sb.ToString(),
                ScoreDate = Bdate,
                TotalScore = totalScore


            };
          

            if (golfScore != null && isValid)
            {
                SplitScore = sb.ToString().Split(',');
                Message =  business.InsertClubScore(golfScore);

            }
            else
            {
                Message = "Not Valid Please Try Again!!";
            }


        }
        private void GetSession()
        {

            UserName = HttpContext.Session.GetString("member")!;

            if (string.IsNullOrEmpty(UserName))
            {

                IsRegistered = "";
            }
            else
            {

                IsRegistered = business.IsMemberApproved(UserName);
               
            }

        }

    }
}
