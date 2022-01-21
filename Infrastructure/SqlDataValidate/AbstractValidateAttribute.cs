using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.SqlDataValidate
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class AbstractValidateAttribute : Attribute
    {
        public abstract bool Validate(object oValue);
    }
}
