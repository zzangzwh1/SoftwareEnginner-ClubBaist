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


        #region  Index / Home /Admin
        public string DisplayStatus(string username)
        {
            TechService.ClubMember memeber = new TechService.ClubMember();
            string status = memeber.MemberStatus(username);
            return status;
        }
        public void RejectMembers(string username)
        {

            TechService.ClubMember memeber = new TechService.ClubMember();
            memeber.RejectMember(username);
        }

        public void ModifyMemberRegister(string username)
        {
            TechService.ClubMember memeber = new TechService.ClubMember();
            memeber.ApproveMember(username);

        }
        public List<Models.ClubMember> DisplayMember()
        {
            TechService.ClubMember memeber = new TechService.ClubMember();
            List<Models.ClubMember> clubMembers = memeber.GetMemberForApprove();
            return clubMembers;
        }
        public string DisplayMemberFirstName(string username)
        {

            TechService.ClubMember memeber = new TechService.ClubMember();
            return memeber.GetMemberName(username);

        }
        #endregion
    }
}
