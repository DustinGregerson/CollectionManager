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
            FileStream fileStream = new FileStream("images/amber.png",FileMode.Open,FileAccess.Read);
            MemoryStream memoryStream=new MemoryStream();
            fileStream.CopyTo( memoryStream );
            byte[] img1 = memoryStream.ToArray();

            memoryStream.Position = 0;
            fileStream = new FileStream("images/oldPot.png", FileMode.Open, FileAccess.Read);
            byte[] img2= memoryStream.ToArray();

            memoryStream.Position = 0;
            fileStream = new FileStream("images/typeWriter.png", FileMode.Open, FileAccess.Read);
            byte[] img3 = memoryStream.ToArray();

            modelBuilder.Entity<Item>().HasData(
            new Item { itemID = 1, Name = "Type writter", Description = "An old type writter", image = img3, tag = "machine", userID=1 },
            new Item { itemID = 2, Name = "Amber", Description = "an old tree fossil", image = img1, tag = "fossil", userID = 2 },
            new Item { itemID = 3, Name = "Ancient pot ", Description = "clay pot from 100bc", image = img2, tag ="artifact", userID = 3 }

            );
        }
    }
}
