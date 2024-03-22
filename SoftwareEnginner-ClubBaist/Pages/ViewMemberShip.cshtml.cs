using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoftwareEnginner_ClubBaist.Business;


namespace SoftwareEnginner_ClubBaist.Pages
{
    public class ViewMemberShipModel : PageModel
    {
        public List<Models.ClubMember> clubMemeber = null!;
        IBusiness business = new Controller.Business();
        public void OnGet()
        {           
            clubMemeber = business.GetClubClubMember();
        }
    }
}
