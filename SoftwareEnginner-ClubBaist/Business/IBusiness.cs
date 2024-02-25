namespace SoftwareEnginner_ClubBaist.Business
{
    public interface IBusiness
    {
        public bool AddClubMember(Models.ClubMember members);
        public string SetDateofBirthDate(string date);
        public string MemberLogin(Models.ClubMember members);
    }
}
