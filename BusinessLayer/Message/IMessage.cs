using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Message
{
    interface IMessage
    {
        bool isValid { get; set; }

        string getResponse(List<BusinessLayer.Validators.Validation> validateThis);
    }
}
