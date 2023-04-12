namespace RestaurantAPI.entity
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }

        public int RestaurantId { get; set; }
        public int UserId { get; set; }
        public int AdressId { get; set; }

        public virtual Adress Adress { get; set; }
        public virtual User User { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
