using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace SoftwareEnginner_ClubBaist.Pages
{
    public class SignUpModel : PageModel
    {
        [BindProperty]
        [Required]
        public string UserName { get; set; }

        [BindProperty]
        [Required]
        // [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Invalid Password, at least 8 long, at least 1 letter, 1 digit,1 special character, 1 Lowercase, 1 uppercase.")]
        public string Password { get; set; }


        [BindProperty]
        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        [Required]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Invalid First Name,Only Letter valid")]
        public string FirstName { get; set; }

        [BindProperty]
        [Required]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Invaliad Last Name, Only Letter valid")]
        public string LastName { get; set; }

        [BindProperty]
        [Required]
        [RegularExpression(@"^[A-Za-z]\d[A-Za-z] \d[A-Za-z]\d$", ErrorMessage = "invliad Postal Code ex)T6G 2T4")]
        public string PostalCode { get; set; }

        [BindProperty]
        public string Address { get; set; }

        [BindProperty]
        public string Occupation { get; set; }

        [BindProperty]
        public string CompanyName { get; set; }

        [BindProperty]
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email,ex)wcho2@nait.ca")]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "Invalid Date of Birth ex)YYYY-MM-DD")]
        public string DateOfBirth { get; set; }

        [BindProperty]
        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Invalid Phone ex)7802003879 or 17802003879")]
        public string Phone { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please Select Membership")]
        public string MembershipType { get; set; }

        public string Message { get; set; }
        public void OnGet()
        {
            Message = "";


        }
        public void OnPost()
        {

            Models.ClubMember members = new()
            {
                UserName = UserName,

                Password = Password,
                FirstName = FirstName,
                LastName = LastName,
                PostalCode = PostalCode,
                Address = Address,
                Occupation = Occupation,
                CompanyName = CompanyName,
                Email = Email,
                DateOfBirth = DateOfBirth,

                MembershipType = MembershipType,
                Phone = Phone

            };

            TechService.ClubMember clubMenber = new TechService.ClubMember();
            bool isRegister = clubMenber.AddClubMember(members);

            if (isRegister)
            {
                Message = "Member is Successfully Added!";
                EmptyStrings();
            }
            else
            {
                Message = "Member is Not Added Try Again!";
            }


        }
        public void EmptyStrings()
        {
            UserName = "";
            Password = "";
            ConfirmPassword = "";
            FirstName = "";
            LastName = "";
            PostalCode = "";
            Address = "";
            Occupation = "";
            CompanyName = "";
            Email = "";
            Phone = "";
            MembershipType = "";
        }
    }
}
