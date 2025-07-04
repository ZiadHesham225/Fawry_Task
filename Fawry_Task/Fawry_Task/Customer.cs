using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawry_Task
{
    public class Customer
    {
        public string Name { get; }
        public double Balance { get; private set; }

        public Customer(string name, double balance)
        {
            Name = name;
            Balance = balance;
        }

        public void BuyItems(double amount)
        {
            if (amount > Balance)
            {
                throw new InvalidOperationException($"Insufficient balance!!!\n Your balance is {Balance} and The total cost is {amount}");
            }
            Balance -= amount;
        }
    }
}
