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
      
        public string Message = "";

        public void OnGet()
        {
         
            Message = "Only Number is Valid for Submitting Score!";
        }
        public void OnPost()
        {
            StringBuilder sb = new StringBuilder();
            List<string> GetScoreDatas = business.GetScoreData(HoleScore1, HoleScore2, HoleScore3, HoleScore4, HoleScore5, HoleScore6, HoleScore7, HoleScore8, HoleScore9, HoleScore10, HoleScore11, HoleScore12, HoleScore13, HoleScore14, HoleScore15, HoleScore16, HoleScore17, HoleScore18);

            int totalScore = Convert.ToInt32(GetScoreDatas[GetScoreDatas.Count - 1]);
            GetScoreDatas.Remove(GetScoreDatas[GetScoreDatas.Count - 1]);
            string username = HttpContext.Session.GetString("member")!;
            int memberID = 0;           
            foreach (string value in GetScoreDatas)
            {                
                sb.Append(value + ',');             
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1); // Remove the last character (which is the comma)
            }
         
            if (!string.IsNullOrEmpty(username))
            {
                memberID = business.GetMemberID(username);
            }

            ClubGolfScore golfScore = new()
            {
                MemberID = memberID,
                Score = sb.ToString(),
                ScoreDate = DateTime.Now,
                TotalScore = totalScore


            };
            if(golfScore != null)
            {
                Message =  business.InsertClubScore(golfScore);

            }


        }
      
    }
}
