using System.Collections.Generic;
using System.Linq;
using RestaurantAPI.entity;

namespace RestaurantAPI
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbContext;
        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if(!_dbContext.Roles.Any())
                {
                    var reoles = GetRoles();
                    _dbContext.Roles.AddRange(reoles);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }
            }

        }
        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name= "User",
                },
                new Role()
                {
                    Name= "Manager",
                },
                new Role()
                {
                    Name= "Admin",
                }
            };
            return roles;
        }
        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "KFC",
                    ContactEmail = "contact@kfc.com",
                    DeliveryDistance = 50,
                    Dishes = new List<Dish>()
                    {
                       new Dish()
                       {
                           Name = "Nashille Hot Chicken",
                           Price = 10.30M,
                       },

                       new Dish()
                       {
                            Name = "Chicken Nuggets",
                            Price = 5.30M
                       },
                    },
                    Adress = new Adress()
                    {
                        City = "Kraków",
                        Street = "Długa 5",
                        PostalCode = "30-001"

                    }
                },
            };
            return restaurants;
        }
    }
}
