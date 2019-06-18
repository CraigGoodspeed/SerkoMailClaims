using BusinessLayer.Message;
using BusinessLayer.Validators.ValidationItems;
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
        Validation[] validationItems;
        
        public ExpenseValidator(Expense toValidate)
        {
            validationItems = new Validation[]{
                new ExpenseTotalValidation(toValidate.Total),
                new ExpenseCostCentreValidation(toValidate.CostCentre)
            };
        }
        public string getMessage()
        {
            return (new ExpenseMessage().getResponse(validationItems.ToList()));
        }


    }
}
