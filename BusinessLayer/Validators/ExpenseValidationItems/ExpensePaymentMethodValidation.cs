using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validators.ExpenseValidationItems
{
    class ExpensePaymentMethodValidation: WarningValidation
    {
        public ExpensePaymentMethodValidation(object paymentMethod) : base(paymentMethod, "Payment method") { }

        protected override string failMessage()
        {
            return "The claim can be processed however there is no payment method, please include a description for future reference.";
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
