using BusinessObjects;

namespace DataAccessObjects
{
    public class AccountDAO
    {
        public static AccountMember GetAccountById(string accountId)
        {
            AccountMember am = new AccountMember();
            if (accountId.Equals("PS0001"))
            {
                am.MemberId = accountId;
                am.MemberPassword = "@1";
                am.MemberRole = 1;
            }
            return am;
        }
    }
}
