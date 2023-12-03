namespace CollectionManager.Models
{
    public class Login
    {
        public string userName { get; set; }
        public string passWord { get; set; }
        public int loginError { get; set; }

        public int createAccountError { get; set; }
    }
}
