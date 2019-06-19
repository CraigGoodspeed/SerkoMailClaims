using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validators.ExpenseValidationItems
{
    class ExpenseVendorValidation : WarningValidation
    {
        public ExpenseVendorValidation(object item) : base(item, "Vendor validation ") { }
        protected override string failMessage()
        {
            return "No cost vendor included, for completeness it would be great to include this. ";
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
