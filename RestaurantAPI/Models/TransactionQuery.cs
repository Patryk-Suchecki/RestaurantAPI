namespace RestaurantAPI.Models
{
    public class TransactionQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public SortDirection sortDirection { get; set; }
    }
}
