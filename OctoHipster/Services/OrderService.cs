using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using OctoHipster.Logic;
using OctoHipster.Models;

namespace OctoHipster.Services
{
    public class OrderService
    {
        List<OrderItem> items; 

        public OrderService()
        {
            var json = Assembly.GetExecutingAssembly().GetFileContents("users.json");
            items = JsonConvert.DeserializeObject<List<OrderItem>>(json);
        }

        readonly Random random = new Random();

        public IEnumerable<Order> GetOrdersForCustomer(Customer customer)
        {
            // HAHA, RANDOM DATA
            var orderCount = random.Next(0, 4);

            var orders = new List<Order>();

            for (int i = 0; i < orderCount; i++)
            {
                orders.Add(CreateRandomOrder());
            }

            return orders;
        }

        private Order CreateRandomOrder()
        {
            var order = new Order();

            var orderItemCount = random.Next(1, 4);

            for (int i = 0; i < orderItemCount; i++)
            {
                var index = random.Next(0, items.Count);
                order.Items.Add(items[index]);
            }

            return order;
        }
    }
}