using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.InteropServices;

namespace SoftwareEnginner_ClubBaist.Pages
{
    public class IndexModel : PageModel
    {
        public string Username { set; get; } = string.Empty;
        public List<Models.ClubMember> clubMembers = null!;
        [BindProperty]
        public string Submit { get; set; } = string.Empty;
        [BindProperty]
        public string ApproveUserName { set; get; } = string.Empty;

        public string Status { set; get; } = String.Empty;
        public void OnGet()
        {
            GetSessionStringAndSetMember();
    
        }
        public void OnPost()
        {

            switch (Submit)
            {
                case "Approve":
                    ModifyMemberRegister(ApproveUserName);
                    GetSessionStringAndSetMember();
                    break;

                case "Reject":
                    RejectMembers(ApproveUserName);
                    GetSessionStringAndSetMember();
                    break;

                default: break;
            }

        }


        public void GetSessionStringAndSetMember()
        {
            string setUserName = HttpContext.Session.GetString("member")!;
            ApproveUserName = setUserName;
            if (setUserName == "Admin")
            {
                Username = setUserName;
                DisplayMember();
            }
            else
            {
                Username = DisplayMemberFirstName(setUserName);
                string statusResult = DisplayStatus(setUserName);
                if(statusResult.ToUpper() == "TRUE")                
                    Status = "Apporeved";
                else if(statusResult.ToUpper() == "FALSE")
                    Status = "Not Approved, Rejected";
                else if(statusResult.ToUpper() == "WAITING")
                    Status = "Waiting On Status";
                else
                {
                    Status = string.Empty;
                }
            }
        }
        private string DisplayStatus(string username)
        {
            TechService.ClubMember memeber = new TechService.ClubMember();
            string status = memeber.MemberStatus(username);
            return status;
        }
        public void RejectMembers(string username)
        {

            TechService.ClubMember memeber = new TechService.ClubMember();
            memeber.RejectMember(username);
        }
        public void ModifyMemberRegister(string username)
        {
            TechService.ClubMember memeber = new TechService.ClubMember();
            memeber.ApproveMember(username);

        }
        public void DisplayMember()
        {
            TechService.ClubMember memeber = new TechService.ClubMember();
            clubMembers = memeber.GetMemberForApprove();
        }
        public string DisplayMemberFirstName(string username)
        {

            TechService.ClubMember memeber = new TechService.ClubMember();
            return memeber.GetMemberName(username);

        }
    }
}
