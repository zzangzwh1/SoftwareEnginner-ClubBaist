using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SoftwareEnginner_ClubBaist.Pages
{
    public class IndexModel : PageModel
    {
        public string Username;
        public void OnGet()
        {
            Username = HttpContext.Session.GetString("member");

        }
    }
}
