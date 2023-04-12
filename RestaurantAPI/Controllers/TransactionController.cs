using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/transation")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<TransactionDto>> GetAll([FromQuery] TransactionQuery query)
        {
            var tansactionDtos = _transactionService.GetAll(query);
            return Ok(tansactionDtos);

        }
        [HttpPost]
        [Authorize]
        public ActionResult Create([FromBody] CreateTransactionDto dto)
        {
            var id = _transactionService.Create(dto);

            return Created($"/api/transation/{id}", null);
        }
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult Delete([FromRoute] int id)
        {
            _transactionService.Remove(id);
            return NoContent();
        }
    }
}
