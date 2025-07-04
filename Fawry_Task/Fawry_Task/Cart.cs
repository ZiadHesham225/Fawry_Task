using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawry_Task
{
    public class Cart
    {
        private Customer customer;
        private Dictionary<Product, int> items;

        public Cart(Customer customer)
        {
            this.customer = customer;
            items = new Dictionary<Product, int>();
        }

        public void AddProduct(Product product, int quantity)
        {
            if (product.ExpirableItem != null && product.ExpirableItem.IsExpired())
            {
                Console.WriteLine($"Product '{product.Name}' is expired and can't be added.");
                return;
            }

            if (quantity > product.Quantity)
            {
                Console.WriteLine($"Insufficient stock for '{product.Name}'.");
                return;
            }

            if (items.ContainsKey(product))
            {
                items[product] += quantity;
            }
            else
            {
                items[product] = quantity;
            }
            product.Quantity -= quantity;
        }

        public void Checkout(IShippingService shippingService)
        {
            if (items.Count == 0)
            {
                Console.WriteLine("Cart is empty.");
                return;
            }

            double subtotal = 0;
            foreach (var item in items)
            {
                Product product = item.Key;
                int quantity = item.Value;
                subtotal += product.Price * quantity;
            }

            List<IShippable> shippableItems = new List<IShippable>();
            foreach (var item in items)
            {
                Product product = item.Key;
                if (product.ShippableItem != null)
                {
                    int quantity = item.Value;
                    for (int i = 0; i < quantity; i++)
                    {
                        shippableItems.Add(product.ShippableItem);
                    }
                }
            }

            double shippingCost = shippingService.CalculateShippingCost(shippableItems);
            double total = subtotal + shippingCost;

            try
            {
                customer.BuyItems(total);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Checkout failed: " + e.Message);
                return;
            }

            Console.WriteLine("Order Details:");
            Console.WriteLine($"Subtotal: ${subtotal:F2}");
            Console.WriteLine($"Shipping Fees: ${shippingCost:F2}");
            Console.WriteLine($"Total Amount Paid: ${total:F2}");
            Console.WriteLine($"Customer Balance After Payment: ${customer.Balance:F2}");
        }
    }
}
