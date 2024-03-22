using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Formats.Asn1.AsnWriter;

namespace SoftwareEnginner_ClubBaist.Pages
{
    public class ScoreModel : PageModel
    {

        [BindProperty]
        public string inputValue { get; set; } = "";
        //    public string InputValue1 { get; set; } = "";
        /*   [BindProperty]
           public string InputValue2 { get; set; } = "";
           [BindProperty]
           public string InputValue3 { get; set; } = "";
           [BindProperty]
           public string InputValue4 { get; set; } = "";
           [BindProperty]
           public string InputValue5 { get; set; } = "";
           [BindProperty]
           public string InputValue6 { get; set; } = "";
           [BindProperty]
           public string InputValue7 { get; set; } = "";
           [BindProperty]
           public string InputValue8 { get; set; } = "";
           [BindProperty]
           public string InputValue9 { get; set; } = "";
           [BindProperty]
           public string InputValue10 { get; set; } = "";
           [BindProperty]
           public string InputValue11 { get; set; } = "";
           [BindProperty]
           public string InputValue12 { get; set; } = "";
           [BindProperty]
           public string InputValue13 { get; set; } = "";
           [BindProperty]
           public string InputValue14 { get; set; } = "";
           [BindProperty]
           public string InputValue15 { get; set; } = "";
           [BindProperty]
           public string InputValue16 { get; set; } = "";*/

        public void OnGet()
        {
        }
        public void OnPost()
        {
            
            string result = "";
            result += inputValue;
            string s = "";
            
        }
    }
}
