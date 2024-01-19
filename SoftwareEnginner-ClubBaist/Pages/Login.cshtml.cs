using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace SoftwareEnginner_ClubBaist.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Required]
        public string Username { get; set; }


        [BindProperty]
        [Required]
        public string Password { get; set; }

        public string Message { get; set; } = string.Empty;

        public string passUserName { set; get; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Username))
            {
                ModelState.AddModelError("Username", "Username is reuqired ");
            }
            else if (string.IsNullOrEmpty(Password))
            {
                ModelState.AddModelError("Password", "Password is reuqired ");
            }
            else
            {
                TechService.ClubMember login = new TechService.ClubMember();
                Models.ClubMember member = new()
                {
                    UserName = Username,
                    Password = Password
                };

                string memberName = login.MemberLogin(member);
                if (ModelState.IsValid && !string.IsNullOrEmpty(memberName))
                {
                    Message = "Success";
                    HttpContext.Session.SetString("member", memberName);
                    return RedirectToPage("/Index");

                }
                else
                {
                    Message = "Please Try Again";

                }
            }
            return Page();

        }
    }
}
