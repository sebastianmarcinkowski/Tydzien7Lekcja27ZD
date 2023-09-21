using System.Data.Entity;
using Tydzien7Lekcja27ZD.Models.Configurations;
using Tydzien7Lekcja27ZD.Models.Domains;
using Tydzien7Lekcja27ZD.Properties;

namespace Tydzien7Lekcja27ZD
{
    public class ApplicationDbContext : DbContext
    {
        public static string ConnectionString =
            $"Server={Settings.Default.DBServerName}\\{Settings.Default.DBInstanceName};" +
            $"Database={Settings.Default.DBName};" +
            $"User Id={Settings.Default.DBUser};" +
            $"Password={Settings.Default.DBPassword}";

        public ApplicationDbContext()
            : base(ConnectionString)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StudentConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new RatingConfiguration());
        }
    }
}