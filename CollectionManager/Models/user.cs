using System.ComponentModel.DataAnnotations;

namespace CollectionManager.Models
{
    public class User
    {
        public int userID { get; set; }
        [Required(ErrorMessage = "The username is requried")]
        public string userName { get; set; }
        [Required(ErrorMessage = "password is required")]
        public string password { get; set; }

        
    }
}
