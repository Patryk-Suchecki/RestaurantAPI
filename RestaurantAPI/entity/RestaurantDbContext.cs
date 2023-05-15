using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.entity
{
    public class RestaurantDbContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set;}
        public DbSet<Adress> Adresses { get; set;}
        public DbSet<Dish> Dishes { get; set;}
        public DbSet<User> Users { get; set;}
        public DbSet<Role> Roles { get; set;}
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Manager> Managers { get; set; }

        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
        {

        }
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

            modelBuilder.Entity<Dish>(eb =>
            {
                eb.Property(d => d.Price).HasPrecision(18, 2);
                eb.Property(d => d.Name).IsRequired();
            });
                

            modelBuilder.Entity<Adress>(eb =>
            {
                eb.Property(a => a.City).IsRequired().HasMaxLength(50);
                eb.Property(a => a.Street).IsRequired().HasMaxLength(50);
            });
                

            modelBuilder.Entity<Transaction>(eb =>
            {
                eb.HasOne(t => t.Restaurant).WithMany().HasForeignKey(t => t.RestaurantId).OnDelete(DeleteBehavior.NoAction);
                eb.Property(t => t.Price).HasPrecision(18, 2);
            });
        }
    }
}
