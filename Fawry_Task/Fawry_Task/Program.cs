namespace Fawry_Task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== TEST CASE 1: Normal Checkout ===");
            NormalCheckoutTest();

            Console.WriteLine("\n=== TEST CASE 2: Expired Products ===");
            ExpiredProductsTest();

            Console.WriteLine("\n=== TEST CASE 3: Insufficient Stock ===");
            InsufficientStockTest();

            Console.WriteLine("\n=== TEST CASE 4: Insufficient Balance ===");
            InsufficientBalanceTest();

            Console.WriteLine("\n=== TEST CASE 5: Empty Cart ===");
            EmptyCartTest();

            Console.WriteLine("\n=== TEST CASE 6: Non-shippable Items Only ===");
            NonShippableItemsTest();
        }

        static void NormalCheckoutTest()
        {
            var cheeseExpirable = new ExpirableItem(new DateTime(2025, 7, 15));
            var cheeseShippable = new ShippableItem(0.5);
            var cheese = new Product("Cheese", 10.0, 100, cheeseExpirable, cheeseShippable);

            var tvShippable = new ShippableItem(15.0);
            var tv = new Product("TV", 500.0, 10, null, tvShippable);

            var biscuitsExpirable = new ExpirableItem(new DateTime(2025, 7, 20));
            var biscuits = new Product("Biscuits", 5.0, 200, biscuitsExpirable);

            var mobile = new Product("Mobile", 300.0, 50);

            Customer customer = new Customer("Ziad Hesham", 1000.0);
            Cart cart = new Cart(customer);

            cart.AddProduct(cheese, 2);
            cart.AddProduct(tv, 1);
            cart.AddProduct(biscuits, 3);
            cart.AddProduct(mobile, 1);

            IShippingService shippingService = new ShippingService();
            cart.Checkout(shippingService);
        }

        static void ExpiredProductsTest()
        {
            var expiredMilkExpirable = new ExpirableItem(new DateTime(2024, 6, 1));
            var expiredMilkShippable = new ShippableItem(1.0);
            var expiredMilk = new Product("Expired Milk", 15.0, 50, expiredMilkExpirable, expiredMilkShippable);

            var freshBreadExpirable = new ExpirableItem(new DateTime(2025, 8, 1));
            var freshBreadShippable = new ShippableItem(0.3);
            var freshBread = new Product("Fresh Bread", 8.0, 30, freshBreadExpirable, freshBreadShippable);

            Customer customer = new Customer("Alice Smith", 500.0);
            Cart cart = new Cart(customer);

            cart.AddProduct(expiredMilk, 2);
            cart.AddProduct(freshBread, 1);

            IShippingService shippingService = new ShippingService();
            cart.Checkout(shippingService);
        }

        static void InsufficientStockTest()
        {
            var laptopShippable = new ShippableItem(2.5);
            var laptop = new Product("Laptop", 800.0, 3, null, laptopShippable);

            Customer customer = new Customer("Bob Johnson", 5000.0);
            Cart cart = new Cart(customer);

            cart.AddProduct(laptop, 5);


            IShippingService shippingService = new ShippingService();
            cart.Checkout(shippingService);
        }

        static void InsufficientBalanceTest()
        {
            var expensiveWatchShippable = new ShippableItem(0.5);
            var expensiveWatch = new Product("Expensive Watch", 2000.0, 5, null, expensiveWatchShippable);

            var cheapPenShippable = new ShippableItem(0.1);
            var cheapPen = new Product("Cheap Pen", 2.0, 100, null, cheapPenShippable);

            Customer customer = new Customer("Charlie Brown", 100.0);
            Cart cart = new Cart(customer);

            cart.AddProduct(expensiveWatch, 1);
            cart.AddProduct(cheapPen, 1);

            IShippingService shippingService = new ShippingService();
            cart.Checkout(shippingService);
        }

        static void EmptyCartTest()
        {
            Customer customer = new Customer("Diana Prince", 1000.0);
            Cart cart = new Cart(customer);

            IShippingService shippingService = new ShippingService();
            cart.Checkout(shippingService);
        }

        static void NonShippableItemsTest()
        {
            var ebook = new Product("E-book", 12.0, 1000);
            var softwareLicense = new Product("Software License", 50.0, 500);
            var onlineCourse = new Product("Online Course", 99.0, 200);

            Customer customer = new Customer("Eve Adams", 500.0);
            Cart cart = new Cart(customer);

            cart.AddProduct(ebook, 2);
            cart.AddProduct(softwareLicense, 1);
            cart.AddProduct(onlineCourse, 1);

            IShippingService shippingService = new ShippingService();
            cart.Checkout(shippingService);
        }
    }
}
