using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validators
{
    public abstract class Validation
    {
        protected Validation(object item, string name)
        {
            this.validateThis = item;
            this.name = name;
        }
        protected object validateThis { get; set; }
        public ValidationResult ValidationResult{get; protected set;}
        public string name { get; private set; }
        protected abstract string failMessage();
        protected abstract string passMessage();
        protected abstract ValidationResult validationResultOnFail();//either warning or fail
        public abstract bool performValidation();
        public string getMessage()
        {
            string toReturn;
            if(performValidation())
            {
                toReturn = passMessage();
                ValidationResult = Validators.ValidationResult.OK;
            }
            else
            {
                toReturn = failMessage();
                ValidationResult = validationResultOnFail();
            }
            return toReturn;
        }
    }
    public enum ValidationResult{
        OK,
        Warning,
        Fail
    }
}
