using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoftwareEnginner_ClubBaist.Business;
using System.Runtime.InteropServices;

namespace SoftwareEnginner_ClubBaist.Pages
{
    public class IndexModel : PageModel
    {
        IBusiness business = new Controller.Business();
        public string Username { set; get; } = string.Empty;
        public List<Models.ClubMember> clubMembers = null!;
        [BindProperty]
        public string Submit { get; set; } = string.Empty;
        [BindProperty]
        public string ApproveUserName { set; get; } = string.Empty;

        public string SetUserName { set; get; } = string.Empty;
        public string Status { set; get; } = string.Empty;
        public void OnGet()
        {
            GetSessionStringAndSetMember();
    
        }
        public void OnPost()
        {
            
            switch (Submit)
            {
                case "Approve":
                    business.ModifyMemberRegister(ApproveUserName);
                    GetSessionStringAndSetMember();
                    break;

                case "Reject":
                    business.RejectMembers(ApproveUserName);
                    GetSessionStringAndSetMember();
                    break;

                default: break;
            }

        }


        public void GetSessionStringAndSetMember()
        {
             SetUserName = HttpContext.Session.GetString("member")!;
            ApproveUserName = SetUserName;
            if (SetUserName == "Admin")
            {
                Username = SetUserName;
                clubMembers = business.DisplayMember();
            }
            else
            {
                Username = business.DisplayMemberFirstName(SetUserName);
                string statusResult = business.DisplayStatus(SetUserName);
                if(statusResult.ToUpper() == "TRUE")                
                    Status = "Apporeved";
                else if(statusResult.ToUpper() == "FALSE")
                    Status = "Waiting for Admin/Committee Approval.";
                else if(statusResult.ToUpper() == "WAITING")
                    Status = "Rejected Please Contact Admin";
                else
                {
                    Status = string.Empty;
                }
            }
        }

    }
}
