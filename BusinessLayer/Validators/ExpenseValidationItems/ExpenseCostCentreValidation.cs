using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validators.ExpenseValidationItems
{
    class ExpenseCostCentreValidation : WarningValidation
    {
        public ExpenseCostCentreValidation(object costCentre) : base(costCentre, "Cost centre ") { }
    
        public override bool performValidation()
        {
            return (string)validateThis != "UNKNOWN";
        }

        protected override string failMessage()
        {
            return "The claim can be processed however there is not a cost centre associated, please include a cost centre for future reference.";
        }

        protected override string passMessage()
        {
            return validateThis.ToString();
        }
    }
}
