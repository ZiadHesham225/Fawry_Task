using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawry_Task
{
    public class ExpirableItem : IExpirable
    {
        private DateTime expirationDate;

        public ExpirableItem(DateTime expirationDate)
        {
            this.expirationDate = expirationDate;
        }

        public bool IsExpired()
        {
            return DateTime.Now > expirationDate;
        }
    }
}
