using RestaurantAPI.entity;

namespace RestaurantAPI.Models
{
    public class UserStatisticsDto
    {
        public string Email { get; set; }
        public int NumberOfOrders { get; set; }
        public int NumberOfLogins { get; set; }
    }
}