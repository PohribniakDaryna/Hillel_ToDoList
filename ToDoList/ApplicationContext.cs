using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList
{
    public class ApplicationContext : DbContext
    {
        public DbSet<TaskItem> Tasks => Set<TaskItem>();
        public ApplicationContext(DbContextOptions options) : base(options)
             => Database.EnsureCreated();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TaskItem>()
                .HasKey(x => x.Id);
        }
    }
}