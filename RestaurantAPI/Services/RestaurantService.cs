using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.entity;
using RestaurantAPI.Models;
using RestaurantAPI.Exceptions;
using Microsoft.AspNetCore.Authorization;
using RestaurantAPI.Authorization;
using System.Linq.Expressions;
using RestaurantAPI.Models.Validators;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Linq;

namespace RestaurantAPI.Services
{
    public interface IRestaurantService
    {
        int Create(CreateRestaurantDto dto);
        PagedResult<RestaurantDto> GetAll(RestaurantQuery query);
        RestaurantDto GetById(int id);
        void Delete(int id);
        void Update(int id, UpdateRestaurantDto dto);
        void SetLogo(IFormFile file, int id);

    }
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;
        private readonly DistanceCalculator _distanceCalculator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantService> logger
            , IAuthorizationService authorizationService, IUserContextService userContextService, DistanceCalculator distanceCalculator, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
            _distanceCalculator = distanceCalculator;
            _httpContextAccessor = httpContextAccessor;
        }
        public void Delete(int id)
        {
            _logger.LogError($"Reastaurant with id: {id} DELETE action invoked");
            var restaurant = _dbContext
                .Restaurants
                .FirstOrDefault(r => r.Id == id);

            if (restaurant is null) throw new NotFoundException("Restaurant not found");
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, restaurant
                , new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();

        }
        public void Update(int id, UpdateRestaurantDto dto)
        {
            var restaurant = _dbContext
                .Restaurants
                .FirstOrDefault(r => r.Id == id);
            if (restaurant is null) throw new NotFoundException("Restaurant not found");
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, restaurant
                , new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if(!authorizationResult.Succeeded) 
            {
                throw new ForbidException();
            }

            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.DeliveryDistance = dto.DeliveryDistance;

            _dbContext.SaveChanges();
        }
        public RestaurantDto GetById(int id)
        {
            var restaurant = _dbContext
                .Restaurants
                .Include(r => r.Adress)
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == id);

            if (restaurant is null)
            { 
                throw new NotFoundException("Restaurant not found");
            }

            var result = _mapper.Map<RestaurantDto>(restaurant);
            return result;
        }

        public PagedResult<RestaurantDto> GetAll(RestaurantQuery query)
        {
            var userAdressJson = JsonConvert.SerializeObject(query.adressDto);
            _httpContextAccessor.HttpContext.Session.SetString("AdressDto", userAdressJson);

            var baseQuery = _dbContext
                .Restaurants
                .Include(r => r.Adress)
                .Include(r => r.Dishes)
                .Where(r => query.SearchPhrase == null || (r.Name.ToLower().Contains(query.SearchPhrase.ToLower()) || r.Description.ToLower().Contains(query.SearchPhrase.ToLower())));

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
                {
                    {nameof(Restaurant.Name), r => r.Name },
                    {nameof(Restaurant.Description), r => r.Description },
                    {nameof(Restaurant.Category), r => r.Category },
                };

                var selectedColumn = columnsSelector[query.SortBy];
                baseQuery = query.sortDirection == SortDirection.ASC ? 
                    baseQuery.OrderBy(selectedColumn) : baseQuery.OrderByDescending(selectedColumn);
            }

            foreach (var restaurant in baseQuery)
            {
                var restaurantAdress = restaurant.Adress;
                var distance = _distanceCalculator.CalculateDistance(userAdressJson, JsonConvert.SerializeObject(restaurantAdress));

                if (distance <= restaurant.DeliveryDistance * 1000)
                {
                    _httpContextAccessor.HttpContext.Session.SetInt32($"restaurant_{restaurant.Id}", distance);
                }

                else baseQuery = baseQuery.Where(r => r != restaurant);
            }
            var restaurants = baseQuery
                .Skip(query.PageSize * (query.PageNumber -1))
                .Take(query.PageSize)
                .ToList();

            var totalItemsCount = baseQuery.Count();

            var restaurantsDtos = _mapper.Map<List<RestaurantDto>>(restaurants);

            var result = new PagedResult<RestaurantDto>(restaurantsDtos, totalItemsCount, query.PageSize, query.PageNumber);
            return result;
        }

        public int Create(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            restaurant.CreatedById= _userContextService.GetUserId;
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();

            return restaurant.Id;
        }
        public void SetLogo(IFormFile logo, int id)
        {
            var restaurant = _dbContext
                .Restaurants
                .FirstOrDefault(r => r.Id == id);

            if (restaurant is null)
            {
                throw new NotFoundException("Restaurant not found");
            }
            string[] allowedFileExtensions = { ".png" };
            FileValidator.ValidateFile(logo, 100000, allowedFileExtensions);

            string fileExtension = Path.GetExtension(logo.FileName);
            var rootPath = Directory.GetCurrentDirectory();
            var fileName = $"logo{fileExtension}";
            Directory.CreateDirectory($"{rootPath}/wwwroot/images/restaurants/{restaurant.Name}");
            var fullPath = $"{rootPath}/wwwroot/images/restaurants/{restaurant.Name}/{fileName}";
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                logo.CopyTo(stream);
            }
        }
    }
}
