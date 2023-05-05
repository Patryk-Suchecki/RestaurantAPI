namespace RestaurantAPI.Models
{
    public class RestaurantQuery
    {
        public string SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public SortDirection sortDirection { get; set; }
        public AdressDto adressDto { get; set; }

    }
}
