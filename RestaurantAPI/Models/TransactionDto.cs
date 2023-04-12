using RestaurantAPI.entity;

namespace RestaurantAPI.Models
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string RestaurantName { get; set; }
    }
}
