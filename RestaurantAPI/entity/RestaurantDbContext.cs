using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.entity
{
    public class RestaurantDbContext : DbContext
    {
        private string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=RestaurantDb;Trusted_Connection=True;";
        public DbSet<Restaurant> Restaurants { get; set;}
        public DbSet<Adress> Adresses { get; set;}
        public DbSet<Dish> Dishes { get; set;}
        public DbSet<User> Users { get; set;}
        public DbSet<Role> Roles { get; set;}
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Manager> Managers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .IsRequired();

            modelBuilder.Entity<Restaurant>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(25);

            modelBuilder.Entity<Dish>()
                .Property(d => d.Name)
                .IsRequired();

            modelBuilder.Entity<Adress>()
                .Property(a => a.City)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Adress>()
                .Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Transaction>()
                 .HasOne(t => t.Restaurant)
                 .WithMany()
                 .HasForeignKey(t => t.RestaurantId)
                 .OnDelete(DeleteBehavior.NoAction);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
