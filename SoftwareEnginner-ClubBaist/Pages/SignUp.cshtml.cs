using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace SoftwareEnginner_ClubBaist.Pages
{
    public class SignUpModel : PageModel
    {
        [BindProperty]
        [Required]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Invalid Username,Only Letter valid")]
        public string UserName { get; set; } = "";

        [BindProperty]
        [Required]
        // [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Invalid Password, at least 8 long, at least 1 letter, 1 digit,1 special character, 1 Lowercase, 1 uppercase.")]
        public string Password { get; set; } = "";

        [BindProperty]
        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = "";

        [BindProperty]
        [Required]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Invalid First Name,Only Letter valid")]
        public string FirstName { get; set; } = "";

        [BindProperty]
        [Required]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Invaliad Last Name, Only Letter valid")]
        public string LastName { get; set; } = "";

        [BindProperty]
        [Required]
        [RegularExpression(@"^[A-Za-z]\d[A-Za-z] \d[A-Za-z]\d$", ErrorMessage = "invliad Postal Code ex)T6G 2T4")]
        public string PostalCode { get; set; } = "";

        [BindProperty]
        public string Address { get; set; } = "";

        [BindProperty]
        public string Occupation { get; set; } = "";

        [BindProperty]
        public string CompanyName { get; set; } = "";

        [BindProperty]
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email,ex)wcho2@nait.ca")]
        public string Email { get; set; } = "";

        [BindProperty]
        [Required]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Invalid Date of Birth ex)19990504")]
        public string DateOfBirth { get; set; } = "";

        [BindProperty]
        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Invalid Phone ex)7802003879 or 17802003879")]
        public string Phone { get; set; } = "";

        [BindProperty]
        [Required(ErrorMessage = "Please Select Membership Type")]
        public string MembershipType { get; set; } = "";

        public string Message { get; set; } = "";
        public void OnGet()
        {
            Message = "";

        }
        public void OnPost()
        {

            string dateOfBirth = "";
            if (!string.IsNullOrEmpty(DateOfBirth))
            {
                dateOfBirth = DateofBirthDate(DateOfBirth);

            }

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
                DateOfBirth = dateOfBirth,
                MembershipType = MembershipType,
                Phone = Phone

            };


            if (ModelState.IsValid)
            {

                TechService.ClubMember clubMenber = new TechService.ClubMember();
                bool isRegister = clubMenber.AddClubMember(members);
                Message = "Member is Successfully Added!";
                EmptyStrings();
            }
            else
            {
                Message = "Member is Not Added Try Again!";
            }
       
            //  EmptyStrings();


        }
        private string DateofBirthDate(string date)
        {
            StringBuilder dateString = new StringBuilder();

            for (int i = 0; i < DateOfBirth.Length; i++)
            {
                if (i == 4 || i == 6)
                {
                    dateString.Append('-');
                }

                dateString.Append(DateOfBirth[i]);

            }
            return dateString.ToString();
        }
        /*  private bool IsValidPhone(string phone)
          {
              if (!Regex.IsMatch(phone, @"^[0-9]+$") || !string.IsNullOrEmpty(phone))
              {
                  return false;
              }
              else
              {
                  return true;
              }
          }
          private bool IsValidDateOfBirth(string dateOfBirth)
          {
              return Regex.IsMatch(dateOfBirth, @"^\d{4}-\d{2}-\d{2}$");
          }
          private bool IsValidEmail(string email)
          {
              return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
          }
          private bool IsValidPostalCode(string postalCode)
          {
              return Regex.IsMatch(postalCode, @"^[A-Za-z]\d[A-Za-z] \d[A-Za-z]\d$");
          }
          private bool IsValidName(string name)
          {
              return Regex.IsMatch(name, @"^[A-Za-z]+$");
          }*/
        private void EmptyStrings()
        {
            UserName = "";
            Password = "";
            ConfirmPassword = "";
            FirstName = "";
            LastName = "";
            PostalCode = "";
            Address = "";
            DateOfBirth = "";
            Occupation = "";
            CompanyName = "";
            Email = "";
            Phone = "";
            MembershipType = "";
        }
    }
}
