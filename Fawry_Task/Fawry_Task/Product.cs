using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawry_Task
{
    public class Product
    {
        public string Name { get; }
        public double Price { get; }
        public int Quantity { get; set; }
        public IExpirable? ExpirableItem { get; }
        public IShippable? ShippableItem { get; }

        public Product(string name, double price, int quantity, IExpirable? expirableItem = null, IShippable? shippableItem = null)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            ExpirableItem = expirableItem;
            ShippableItem = shippableItem;
        }
    }
}
