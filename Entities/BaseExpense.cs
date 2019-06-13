using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public abstract class BaseExpense
    {
        public bool IsValid { get; set; }
        abstract void Validate();
        abstract void Save();
        abstract string GetMessage();
        public string ClaimExpense()
        {
            Validate();
            Save();
            return GetMessage();
        }
    }
}
