using Core.Entities;
using Core.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class DataContext : DbContext
    {
        public DbSet<File> File{ get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceStatus> InvoiceStatus { get; set; }
        public DbSet<Parameter> Parameter { get; set; }

        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = ConnectionInfo.Instance.MySQLServerConnectionString;
            optionsBuilder.UseMySQL(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<File>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<File>().Property(c => c.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Invoice>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Invoice>().Property(c => c.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<InvoiceStatus>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<InvoiceStatus>().Property(c => c.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Parameter>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<Parameter>().Property(c => c.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<User>().Property(c => c.Id).ValueGeneratedOnAdd();
        }
    }
}
