using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.entity;

namespace RestaurantAPI.Controllers
{
    public interface IWebSocketHub
    {
        Task ReceiveOrderProgress(int id, Transaction orderProgress);
    }

    public class WebSocketHub : Hub<IWebSocketHub>
    {
        private readonly RestaurantDbContext _dbContext;

        public WebSocketHub(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task GetOrderProgress(int id)
        {
            var orderProgress = await _dbContext.Transactions.FirstOrDefaultAsync(t => t.Id == id);
            await Clients.Caller.ReceiveOrderProgress(id, orderProgress);
        }
    }
}