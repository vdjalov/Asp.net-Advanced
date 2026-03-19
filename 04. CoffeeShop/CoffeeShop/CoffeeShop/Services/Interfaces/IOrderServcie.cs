using CoffeeShop.Models;

namespace CoffeeShop.Services.Interfaces
{
    public interface IOrderServcie
    {
        public CheckResult GetUpdate(int orderId);
        public int NewOrder();
    }
}
