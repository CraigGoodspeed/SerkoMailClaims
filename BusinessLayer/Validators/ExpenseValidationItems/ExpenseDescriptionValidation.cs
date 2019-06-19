using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validators.ExpenseValidationItems
{
    class ExpenseDescriptionValidation: WarningValidation
    {
        public ExpenseDescriptionValidation(object description) : base(description, "Description") { }

        protected override string failMessage()
        {
            return "The claim can be processed however there is not a description, please include a description for future reference.";
        }

        protected override string passMessage()
        {
            return validateThis.ToString();
        }

        public override bool performValidation()
        {
            return validateThis != null;
        }
    }
}
