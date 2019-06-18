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
        public abstract string getMessage();
    }
    public enum ValidationResult{
        OK,
        Warning,
        Fail
    }
}
