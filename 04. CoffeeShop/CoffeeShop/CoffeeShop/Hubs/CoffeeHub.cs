using CoffeeShop.Models;
using CoffeeShop.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace CoffeeShop.Hubs
{
    public class CoffeeHub : Hub
    {
        private readonly IOrderServcie orderService;

        public CoffeeHub(IOrderServcie _orderServcie )
        {
            this.orderService = _orderServcie;
        }

        public async Task GetUpdateForOrder(int orderId)
        {
            CheckResult result;

            do
            {
                result = orderService.GetUpdate(orderId);
                if(result.New)
                {
                    await Clients.Caller.SendAsync("ReceiveUpdate", result.Update);
                }
            }
            while (!result.Finished);

            await Clients.Caller.SendAsync("Finished");
        }


    }
}
