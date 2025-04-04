using AuthAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.DataBase
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        public DbSet<User> User {get; set;}
        public DbSet<Session> Session {get; set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasKey(u => u.Id);
            modelBuilder.Entity<Session>()
            .HasKey(s => s.Id);

            modelBuilder.Entity<User>()
            .HasIndex(u => u.UserName).IsUnique();
            modelBuilder.Entity<User>()
            .HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<Session>()
            .HasOne(s => s.User)
            .WithOne(u => u.Session)
            .HasForeignKey<Session>(s => s.UserId);
        }
    }
}