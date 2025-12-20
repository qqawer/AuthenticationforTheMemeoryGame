using Microsoft.EntityFrameworkCore;
using AuthenticationforTheMemeoryGame.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace AuthenticationforTheMemeoryGame.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        //Mapping to database
        public DbSet<User> Users => Set<User>();
        public DbSet<Score> Scores => Set<Score>();
        public DbSet<Ad> Ads => Set<Ad>();

        //Configure table relationships and indexes
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique()
                .HasDatabaseName("IX_Users_Username");//Create a unique index for Username

            // Configure Score entity,one-to-many relationship between User and Score
            modelBuilder.Entity<Score>()
                .HasOne(s => s.User)
                .WithMany(u => u.Scores)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            // Create a composite index for the Scores table (to optimize the ranking query: ascending by completion time + descending by submission time)
            modelBuilder.Entity<Score>()
                .HasIndex(s => new { s.CompletionTimeSeconds, s.CompleteAt })
                .HasDatabaseName("IX_Scores_CompletionTimeSeconds_CompleteAt")
                .IsDescending(false, true);// CompletionTimeSeconds ascending, CompleteAt descending





        }
    }
}
