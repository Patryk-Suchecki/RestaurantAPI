using System.Runtime.Serialization;

namespace RestaurantAPI.entity
{
    public class Adress
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        [IgnoreDataMember]
        public virtual Restaurant Restaurant { get; set; }
        public virtual Transaction Transaction { get; set; }
    }
}
