using Microsoft.EntityFrameworkCore;
namespace CollectionManager.Models
  
{
    public class UsersContext : DbContext
    {
        public UsersContext() { }

        public UsersContext(DbContextOptions<UsersContext> options)
           : base(options) { }

        public DbSet<User> users { get; set; }
        public DbSet<Item> items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Item>().HasData(
                new Item {itemID=1, Name="Type writter",Description="An old type writter",image=new byte[0],tag="machine"},
                new Item {itemID=2, Name = "Amber", Description = "an old tree fossil", image = new byte[0],tag = "fossil" },
                new Item {itemID=3, Name = "Ancient pot ", Description = "clay pot from 100bc", image = new byte[0],tag= "artifact" }

            );
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


            )  ;
        }
    }
}
