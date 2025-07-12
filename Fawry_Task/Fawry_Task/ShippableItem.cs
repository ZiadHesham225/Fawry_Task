using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawry_Task
{
    public class ShippableItem : IShippable
    {
        private double weight;
        public ShippableItem(double weight)
        {
            this.weight = weight;
        }
        public double GetWeight()
        {
            return weight;
        }
    }
}
