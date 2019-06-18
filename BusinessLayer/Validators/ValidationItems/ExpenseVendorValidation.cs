using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validators.ValidationItems
{
    class ExpenseCostCentreValidation : Validation
    {
        public ExpenseCostCentreValidation(object costCentre) : base(costCentre, "Cost centre ") { }
        public override string getMessage()
        {
            if ((string)validateThis == "UNKNOWN")
            {
                ValidationResult = ValidationResult.Warning;
                return "The claim can be processed however there is not a vendor associated, please include a vendor for future reference.";
            }
            ValidationResult = ValidationResult.OK;
            return string.Empty;
        }
    }
}
