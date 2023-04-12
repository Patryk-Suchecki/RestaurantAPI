namespace RestaurantAPI.Models
{
    public class CreateTransactionDto
    {
        public decimal Price { get; set; }
        public int RestaurantId { get; set; }
        public DateTime Date { get; set; }

        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
    }
}
