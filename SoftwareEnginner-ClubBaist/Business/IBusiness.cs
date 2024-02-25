namespace SoftwareEnginner_ClubBaist.Business
{
    public interface IBusiness
    {
        public bool AddClubMember(Models.ClubMember members);
        public string SetDateofBirthDate(string date);
        public string MemberLogin(Models.ClubMember members);
        public string DisplayStatus(string username);
        public void RejectMembers(string username);
        public void ModifyMemberRegister(string username);
        public List<Models.ClubMember> DisplayMember();
        public string DisplayMemberFirstName(string username);
    }
}
