using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingProject.Validation
{
    public class OwnerDurationValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            if (string.IsNullOrEmpty((string)value))
            {
                return new ValidationResult(false, "*");
            }

            if (!int.TryParse((string)value, out int intValue))
            {
                return new ValidationResult(false, "*");
            }

            if (intValue <= 0)
            {
                return new ValidationResult(false, "*");
            }
            return ValidationResult.ValidResult;
        }
    }
}
