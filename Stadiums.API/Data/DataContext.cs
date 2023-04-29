using Microsoft.EntityFrameworkCore;
using Stadiums.Shared.Entities;

namespace Stadiums.API.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext>options): base(options)  
        
        {
            
        }

        public DbSet<Ticket>Tickets { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Record> Records { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Ticket>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Goal>().HasIndex(c => c.Name).IsUnique();
        }

    }
}
