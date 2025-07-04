using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawry_Task
{
    public class ShippingService : IShippingService
    {
        // the price of shipping is 5 per kg :)
        public double CalculateShippingCost(List<IShippable> items)
        {
            double totalWeight = 0;
            foreach (var item in items)
            {
                totalWeight += item.GetWeight();
            }
            return totalWeight * 5;
        }
    }
}
