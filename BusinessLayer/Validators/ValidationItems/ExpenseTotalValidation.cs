using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validators.ValidationItems
{
    class ExpenseTotalValidation : Validation
    {
        public ExpenseTotalValidation(object total) : base(total,"Expense total") { }


        public override string getMessage()
        {
            if ((decimal)validateThis == 0)
            {
                ValidationResult = ValidationResult.Fail;
                return "The claim could not be processed, the total amount claimed is zero.";
            }
            else
            {
                ValidationResult = ValidationResult.OK;
                return string.Format("The claim for amount {0} is in process", (decimal)validateThis);
            }
   
        }
    }
}
