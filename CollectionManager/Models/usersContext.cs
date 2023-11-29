using Microsoft.EntityFrameworkCore;
namespace CollectionManager.Models
  
{
    public class UsersContext : DbContext
    {
        public UsersContext() { }

        public UsersContext(DbContextOptions<usersContext> options)
           : base(options) { }

        public DbSet<user> users { get; set; }
        public DbSet<item> items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<item>().HasData(
                new item {itemID=1, Name="Type writter",Description="An old type writter",image=new byte[0] },
                new item {itemID=2, Name = "Amber", Description = "An old type writter", image = new byte[0]},
                new item {itemID=3, Name = "Ancient pot ", Description = "An old type writter", image = new byte[0]}

            );
            modelBuilder.Entity<user>().HasData(
            new user
            {
                userID = 1,
                userName = "test1",
                password = "test1"
               
            },
            new user
            {
                userID = 2,
                userName = "test2",
                password = "test2"
                
            },
            new user
            {
                userID = 3,
                userName = "test3",
                password = "test3"
             
            }


            )  ;
        }
    }
}
