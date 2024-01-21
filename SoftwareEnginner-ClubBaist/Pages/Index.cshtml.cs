using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SoftwareEnginner_ClubBaist.Pages
{
    public class IndexModel : PageModel
    {
        public string Username { set; get; } = string.Empty;
        public List<Models.ClubMember> clubMembers = null!;
        public void OnGet()
        {
            Username = HttpContext.Session.GetString("member")!;
          
            if(Username == "This is:ClubBaistAdmin")
            {
                DisplayMember();
            }

        }
        
        public void DisplayMember()
        {
            TechService.ClubMember memeber = new TechService.ClubMember();
            clubMembers = memeber.GetMemberForApprove();
        }
    }
}
