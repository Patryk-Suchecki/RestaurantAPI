using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models
{
    public class PagedResult<T>
    {
        private List<RestaurantDto> restaurantsDtos;
        private int pageSize;
        private int pageNumber;

        public List<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int ItemFrom { get; set; }
        public int ItemTo { get; set; }
        public int TotalItemsCount { get; set; }
        public List<int> Distances { get; set; }

        public PagedResult(List<T> items, int totalCount, int pageSize, int pageNumber, List<int> distances = null)
        {
            Items = items;
            TotalItemsCount = totalCount;
            ItemFrom = pageSize * (pageNumber -1)+ 1;
            ItemTo = ItemFrom + pageSize -1;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            Distances = distances;

        }

        public PagedResult(List<RestaurantDto> restaurantsDtos, int totalItemsCount, int pageSize, int pageNumber)
        {
            this.restaurantsDtos = restaurantsDtos;
            TotalItemsCount = totalItemsCount;
            this.pageSize = pageSize;
            this.pageNumber = pageNumber;
            

        }
    }
}
