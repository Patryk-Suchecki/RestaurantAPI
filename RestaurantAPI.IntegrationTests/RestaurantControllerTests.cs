using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.entity;

namespace RestaurantAPI.IntegrationTests
{
    
    public class RestaurantControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private HttpClient _client;

        public RestaurantControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var dbContextOptions = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<RestaurantDbContext>));
                    services.Remove(dbContextOptions);

                    services.AddDbContext<RestaurantDbContext>(options => options.UseInMemoryDatabase("RestaurantDb"));
                });
            })
                .CreateClient();
        }
        [Theory]
        [InlineData("PageSize=10&PageNumber=1")]
        [InlineData("PageSize=15&PageNumber=2")]
        [InlineData("PageSize=5&PageNumber=3")]
        public async Task GetAll_WithQueryParameters_ReturnsOkResult(string queryParams)
        {
            // act

            var response = await _client.GetAsync("/api/restaurant?" + queryParams);

            // assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("PageSize=100&PageNumber=1")]
        [InlineData("PageSize=11&PageNumber=2")]
        [InlineData("")]
        [InlineData(null)]
        public async Task GetAll_WithInvalidQueryParams_ReturnBadRequest(string queryParams)
        {
            // act

            var response = await _client.GetAsync("/api/restaurant?" + queryParams);

            // assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
