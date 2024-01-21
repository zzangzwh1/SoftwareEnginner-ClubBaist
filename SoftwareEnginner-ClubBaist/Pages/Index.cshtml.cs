using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
            }
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
