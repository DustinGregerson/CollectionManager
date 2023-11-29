using System.ComponentModel.DataAnnotations;

namespace CollectionManager.Models
{
    public class user
    {
        public int userID { get; set; }
        [Required(ErrorMessage = "The username is requried")]
        public string userName { get; set; }
        [Required(ErrorMessage = "password is required")]
        public string password { get; set; }

        public item? item { get; set; }
    }
}
