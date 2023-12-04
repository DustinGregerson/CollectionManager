using CollectionManager.tools;
using Microsoft.EntityFrameworkCore;
namespace CollectionManager.Models
  
{
    public class CollectersContext : DbContext
    {
        public CollectersContext() { }

        public CollectersContext(DbContextOptions<CollectersContext> options)
           : base(options) { }

        public DbSet<User> users { get; set; }
        public DbSet<Item> items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
            new User
            {
                userID = 1,
                userName = "test1",
                password = "test1"
            },
            new User
            {
                userID = 2,
                userName = "test2",
                password = "test2"

            },
            new User
            {
                userID = 3,
                userName = "test3",
                password = "test3"

            }


            );

            byte[] img1 = imageConverter.imageToByteArray("images/amber.png");
            byte[] img2= imageConverter.imageToByteArray("images/oldPot.png");
            byte[] img3 = imageConverter.imageToByteArray("images/typeWriter.png");

            modelBuilder.Entity<Item>().HasData(
            new Item { itemID = 1, Name = "Type writer", Description = "An old type writer.", image = img3, tag = "machine", userID=1 },
            new Item { itemID = 2, Name = "Amber", Description = "Tree sap that is fossilized", image = img1, tag = "fossil", userID = 2 },
            new Item { itemID = 3, Name = "Ancient pot ", Description = "A clay pot from 100bc.", image = img2, tag ="artifact", userID = 3 }

            );
        }
    }
}
