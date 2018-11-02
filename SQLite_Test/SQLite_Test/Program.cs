using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SQLite;
using SQLite.CodeFirst;

namespace SQLite_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var db = new UserContext())
            {
                User user1 = new User("Ivan");
                User user2 = new User("Petya");
                User user3 = new User("Nastya");

                db.Users.Add(user1);
                db.Users.Add(user2);
                db.Users.Add(user3);
                db.SaveChanges();

                var users = db.Users;
                Console.WriteLine("Users list:");
                foreach (User u in users)
                {
                    Console.WriteLine("User #{0} - {1} with rating {2}.", u.Id, u.Name, u.Rating);
                }
            }
        }

        class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Rating { get; set; }
            public User(string name)
            {
                Name = name;
                Rating = 0;
            }
        }

        class UserContext : DbContext
        {
            public UserContext()
                : base("UserContext")
            {

            }
            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<UserContext>(modelBuilder);
                Database.SetInitializer(sqliteConnectionInitializer);
            }
            public DbSet<User> Users { get; set; }
        }
    }
}
