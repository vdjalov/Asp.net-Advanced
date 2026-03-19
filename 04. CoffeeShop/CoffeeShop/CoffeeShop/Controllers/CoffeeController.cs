using CoffeeShop.Hubs;
using CoffeeShop.Models;
using CoffeeShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CoffeeShop.Controllers
{
    public class CoffeeController : Controller
    {
        private readonly IOrderServcie orderService;
        private readonly CoffeeHub coffeeHub;

        public CoffeeController(IOrderServcie _orderService)
        {
            this.orderService = _orderService;
            this.coffeeHub = new CoffeeHub(this.orderService);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OrderCoffee([FromBody]Order order)
        {
            await this.coffeeHub.Clients.All.SendAsync("ReceiveOrder", order);
            var orderId = this.orderService.NewOrder();

            return this.Accepted(orderId);
        }
    }
}
