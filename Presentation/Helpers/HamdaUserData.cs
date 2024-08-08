namespace HAMDA.Helpers
{
    public class HamdaUserData
    {
        public static Guid UserId
        {
            get
            {
                return new HamdaWrapper().CurrentUser.UserId;
            }
        }
        
        public static string UserName
        {
            get
            {
                return new HamdaWrapper().CurrentUser.UserName;
            }
        }
        
        public static string Email
        {
            get
            {
                return new HamdaWrapper().CurrentUser.Email;
            }
        }
    }
}
