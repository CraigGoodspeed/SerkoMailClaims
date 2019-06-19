using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validators.ExpenseValidationItems
{
    class ExpenseDateValidation : WarningValidation
    {
        public ExpenseDateValidation(object date) : base(date, "Expense date ") { }
    
        public override bool performValidation()
        {
            return validateThis != null;
        }

        protected override string failMessage()
        {
            return "The claim can be processed however there is not a date or the date could not be parsed from the content, please include a date in the correct format (yyyy/MM/dd) for future reference.";
        }

        protected override string passMessage()
        {
            return ((DateTime)validateThis).ToString("yyyy/MM/dd HH:mm");
        }
    }
}
