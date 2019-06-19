using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validators.ExpenseValidationItems
{
    class ExpenseTotalValidation : FailValidation
    {
        public ExpenseTotalValidation(object total) : base(total,"Expense total") { }

        protected override string failMessage()
        {
            return "The claim could not be processed, the total amount claimed is zero.";
        }

        protected override string passMessage()
        {
            return string.Format("The claim for amount {0} is in process", (decimal)validateThis);
        }

        public override bool performValidation()
        {
            return (decimal)validateThis != 0;
        }
    }
}
