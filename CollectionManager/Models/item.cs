using System.ComponentModel.DataAnnotations;
namespace CollectionManager.Models
{
    public class Item
    {
        [Required(ErrorMessage = "Item id must be set.")]
        public int itemID { get; set; }
        [Required(ErrorMessage = "The Item must have a name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The Item must have a description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "You must show off your item")]
        //varbinary(MAX) will need this later
        public byte[] image { get; set; }

        [Required(ErrorMessage = "You must enter a tag")]
        public String tag { get; set; }

        public User? user { get; set; }

    }
}
