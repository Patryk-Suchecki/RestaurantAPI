using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _restaurantService.Delete(id);
            return NoContent();
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] UpdateRestaurantDto dto)
        {
            _restaurantService.Update(id, dto);
            return Ok();
        }
        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            var id = _restaurantService.Create(dto);

            return Created($"/api/restaurant/{id}", null);
        }

        [HttpPost("all")]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll([FromBody] RestaurantQuery query)
        {
            var restaurantsDtos = _restaurantService.GetAll(query);
            return Ok(restaurantsDtos);
        }

        [HttpGet("{restaurantId}")]
        public ActionResult<RestaurantDto> Get([FromRoute] int restaurantId)
        {
            var result = _restaurantService.GetById(restaurantId);
            return Ok(result);
        }
        [HttpGet("own")]
        public ActionResult<RestaurantDto> GetByOwner()
        {
            var result = _restaurantService.GetRestaurantsByOwner();
            return Ok(result);
        }

        [HttpPost("setlogo/{id}")]
        public ActionResult Upload([FromForm] IFormFile logo, [FromRoute]int id)
        {
            _restaurantService.SetLogo(logo, id);
            return Ok();
        }
    }
}
