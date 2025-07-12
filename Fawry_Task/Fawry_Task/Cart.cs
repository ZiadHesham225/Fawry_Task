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
                Console.WriteLine($"No enough stock for '{product.Name}'.");
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
            double totalWeight = 0;

            foreach (var item in items)
            {
                Product product = item.Key;
                int quantity = item.Value;

                if (product.ShippableItem != null)
                {
                    double itemWeight = product.ShippableItem.GetWeight() * quantity;
                    totalWeight += itemWeight;

                    for (int i = 0; i < quantity; i++)
                    {
                        shippableItems.Add(product.ShippableItem);
                    }
                }
            }
            if (shippableItems.Count > 0)
            {
                Console.WriteLine("** shipment notice **");
                foreach (var item in items)
                {
                    Product product = item.Key;
                    int quantity = item.Value;

                    if (product.ShippableItem != null)
                    {
                        double itemWeight = product.ShippableItem.GetWeight() * quantity;
                        string weightFormat = itemWeight >= 1 ? $"{itemWeight:F1}kg" : $"{itemWeight * 1000:F0}g";
                        Console.WriteLine($"{quantity}x {product.Name}\t\t{weightFormat}");
                    }
                }
            }
            if (totalWeight > 0)
            {
                string totalWeightFormat = totalWeight >= 1 ? $"{totalWeight:F1}kg" : $"{totalWeight * 1000:F0}g";
                Console.WriteLine($"Total package weight {totalWeightFormat}");
            }

            Console.WriteLine();

            Console.WriteLine("** receipt **");
            foreach (var item in items)
            {
                Product product = item.Key;
                int quantity = item.Value;
                double itemTotal = product.Price * quantity;
                Console.WriteLine($"{quantity}x {product.Name}\t\t{itemTotal:F0}");
            }

            Console.WriteLine("----------------------");
            Console.WriteLine($"Subtotal\t\t{subtotal:F0}");

            double shippingCost = shippingService.CalculateShippingCost(shippableItems);
            if (shippingCost > 0) Console.WriteLine($"Shipping\t\t{shippingCost:F0}");

            double total = subtotal + shippingCost;
            Console.WriteLine($"Amount\t\t\t{total:F0}");

            try
            {
                customer.BuyItems(total);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Checkout failed: " + e.Message);
                return;
            }
        }
    }
}
