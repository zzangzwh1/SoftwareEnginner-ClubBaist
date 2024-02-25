using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoftwareEnginner_ClubBaist.Business;
using System;
using System.ComponentModel.DataAnnotations;

namespace SoftwareEnginner_ClubBaist.Pages
{
    public class LoginModel : PageModel
    {
        IBusiness business = new Controller.Business();
        [BindProperty]
        [Required]
        public string Username { get; set; } = string.Empty;


        [BindProperty]
        [Required]
        public string Password { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

       // public string PassUserName { set; get; } = string.Empty;
        public void OnGet()
        {
            if(HttpContext.Session.GetString("member") != null)
            {
                HttpContext.Session.Clear();
            }
        }
        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Username))
            {
                ModelState.AddModelError("Username", "Username is reuqired ");
            }
            if (string.IsNullOrEmpty(Password))
            {
                ModelState.AddModelError("Password", "Password is reuqired ");
            }
           
                Models.ClubMember member = new()
                {
                    UserName = Username,
                    Password = Password
                };

                
                string memberName = business.MemberLogin(member);
                if (ModelState.IsValid && !string.IsNullOrEmpty(memberName))
                {
                    Message = "Success";
                    HttpContext.Session.SetString("member", memberName);
                    return RedirectToPage("/Index");

                }
                else
                {
                Message = "Invalid Username Or Password Try Again!";

                }
            
            return Page();

        }
    }
}
