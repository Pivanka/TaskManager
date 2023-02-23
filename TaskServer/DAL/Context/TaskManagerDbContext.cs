using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class TaskManagerDbContext : DbContext
    {
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .HasOne(comment => comment.User)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
            base.OnModelCreating(modelBuilder);
        }
    }
}
