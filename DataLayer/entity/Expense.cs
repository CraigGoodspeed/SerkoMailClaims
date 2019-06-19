using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public partial class Expense
    {
        public bool isEmpty()
        {
            return
                CostCentre == "UNKNOWN"
                &&
                Total == 0
                &&
                string.IsNullOrEmpty(Description)
                &&
                string.IsNullOrEmpty(PaymentMethod)
                &&
                ExpenseDate == null
                &&
                string.IsNullOrEmpty(Vendor);
        }
    }
}
