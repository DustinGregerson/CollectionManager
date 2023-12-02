namespace CollectionManager.Models
{
    public class Login
    {
        public String userName { get; set; }
        public String passWord { get; set; }
        public int loginError { get; set; }
        public int createAccountError { get; set; }
    }
}
