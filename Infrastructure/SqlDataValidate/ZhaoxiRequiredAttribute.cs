using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.SqlDataValidate
{
    public class ZhaoxiRequiredAttribute : AbstractValidateAttribute
    {
        public override bool Validate(object oValue)
        {
            return oValue != null && !string.IsNullOrWhiteSpace(oValue.ToString());
        }
    }
}
