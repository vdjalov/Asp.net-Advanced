using CoffeeShop.Models;
using CoffeeShop.Services.Interfaces;

namespace CoffeeShop.Services
{
    public class OrderService : IOrderServcie
    {
        private readonly string[] status = new[] { "Espresso", "Latte", "Cappuccino", "Americano", "Beans" };
        private readonly Random random;
        private readonly IList<int> indexes;

        public OrderService()
        {
            this.random = new Random();
            this.indexes = new List<int>();
        }

        public CheckResult GetUpdate(int orderId)
        {
            Thread.Sleep(1000);
            var index = this.indexes[orderId - 1];

            if(this.random.Next(0, 4) == 2)
            {
                if(this.status.Length > this.indexes[orderId -1])
                {
                    var result = new CheckResult
                    {
                        New = true,
                        Update = this.status[index],
                        Finished = this.status.Length - 1 == index
                    };

                    this.indexes[orderId - 1]++;
                    return result;
                }
            }

            return new CheckResult { New = false };

        }

        public int NewOrder()
        {
            this.indexes.Add(0);
            return this.indexes.Count;
        }
    }
}
