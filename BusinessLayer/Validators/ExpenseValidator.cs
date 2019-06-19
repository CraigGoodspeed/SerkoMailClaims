using BusinessLayer.Message;
using BusinessLayer.Validators.ExpenseValidationItems;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validators
{
    public class ExpenseValidator
    {
        Expense toValidate;
        public ExpenseValidator(Expense toValidate)
        {
           this.toValidate = toValidate;
        }
        public string getMessage()
        {
            return (new ExpenseMessage().getResponse(toValidate));
        }


    }
}
