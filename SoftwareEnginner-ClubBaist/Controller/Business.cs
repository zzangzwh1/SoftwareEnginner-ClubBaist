using SoftwareEnginner_ClubBaist.Business;
using System.Text;
namespace SoftwareEnginner_ClubBaist.Controller
{
    public class Business : IBusiness
    {
        #region SignUP
        public bool AddClubMember(Models.ClubMember members)
        {
            TechService.ClubMember clubMenber = new TechService.ClubMember();
            bool isSuccess = clubMenber.AddClubMember(members);
            return isSuccess;
        }

        public string SetDateofBirthDate(string date)
        {
            StringBuilder dateString = new StringBuilder();

            for (int i = 0; i < date.Length; i++)
            {
                if (i == 4 || i == 6)
                {
                    dateString.Append('-');
                }

                dateString.Append(date[i]);

            }
            return dateString.ToString();
        }
        #endregion

        #region Login
        public string MemberLogin(Models.ClubMember members)
        {
            TechService.ClubMember login = new TechService.ClubMember();
            string memberName = login.MemberLogin(members);
            return memberName;
        }
        #endregion
    }
}
