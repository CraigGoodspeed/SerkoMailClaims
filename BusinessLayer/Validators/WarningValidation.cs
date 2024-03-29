﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validators
{
    public abstract class WarningValidation : Validation
    {
        protected WarningValidation(object item, string name) : base(item, name) { }

        protected override ValidationResult validationResultOnFail()
        {
            return Validators.ValidationResult.Warning;
        }
    }
}
