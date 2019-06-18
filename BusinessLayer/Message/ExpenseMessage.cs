using BusinessLayer.Validators;
using DataLayer;
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
        public string getResponse(List<Validation> validation)
        {
            StringBuilder toReturn = new StringBuilder();
            isValid = true;
            validation.ForEach(i =>
            {
                string tmpMessage = i.getMessage();
                string validationReturn = i.ValidationResult.ToString();
                string name = i.name;
                toReturn.AppendFormat(MessageConstants.HTMLTABLEROWTHREECOLUMNS, name, validationReturn, tmpMessage);
                isValid = isValid && i.ValidationResult != ValidationResult.Fail;
            }
            );
            string message = isValid ? "Claim is ready to be processed" : "Claim could not be processed please see below for error details.";
            toReturn.Insert(0, string.Format(MessageConstants.HTMLTABLEROW, message));
            return string.Format(MessageConstants.HTMLTABLE, toReturn.ToString());
        }
    }
}
