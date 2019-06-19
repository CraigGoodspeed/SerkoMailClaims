using BusinessLayer.Validators;
using BusinessLayer.Validators.ExpenseValidationItems;
using DataLayer;
using DataLayer.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Message
{
    public class ExpenseMessage : IMessage
    {
        public bool isValid
        {
            get;
            set;
        }
        public string getResponse(Expense toValidate)
        {
            Validation[] validation = new Validation[]{
                new ExpenseCostCentreValidation(toValidate.CostCentre),
                new ExpenseDateValidation(toValidate.ExpenseDate),
                new ExpenseDescriptionValidation(toValidate.Description),
                new ExpensePaymentMethodValidation(toValidate.PaymentMethod),
                new ExpenseVendorValidation(toValidate.CostCentre),
                new ExpenseTotalValidation(toValidate.Total)
            };
            StringBuilder toReturn = new StringBuilder();
            isValid = true;
            validation.ToList().ForEach(i =>
            {
                string tmpMessage = i.getMessage();
                string validationReturn = i.ValidationResult.ToString();
                string name = i.name;
                toReturn.AppendFormat(MessageConstants.HTMLTABLEROWTHREECOLUMNS, name, validationReturn, tmpMessage);
                isValid = isValid && i.ValidationResult != ValidationResult.Fail;
            }
            );
            if (isValid)
            {
                //add gst
                GSTDAO currentGST = new GSTDAO();
                decimal gst = currentGST.getCurrentMultiplier();
                toReturn.AppendFormat(MessageConstants.HTMLTABLEROWTHREECOLUMNS, "Total excl gst ", string.Empty, Math.Round((toValidate.Total / (1 + gst)),2));
            }
            else if(toValidate.isEmpty()){
                toReturn.AppendFormat(MessageConstants.HTMLTABLEROW, "We have noticed the claim is empty please verify the message sent, there is likely a closing tag that is missing");
            }
            string message = isValid ? "Claim is ready to be processed" : "Claim could not be processed please see below for error details.";
            toReturn.Insert(0, string.Format(MessageConstants.HTMLTABLEROW, message));
            
            return string.Format(MessageConstants.HTMLTABLE, toReturn.ToString());
        }
    }
}
