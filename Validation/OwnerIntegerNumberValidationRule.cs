using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingProject.Validation
{
    public class OwnerIntegerNumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            if (string.IsNullOrEmpty((string)value))
            {
                return new ValidationResult(false, "This must be filled!");
            }

            if (!int.TryParse((string)value, out int intValue))
            {
                return new ValidationResult(false, "This field has to be an number!");
            }

            if (intValue <= 0)
            {
                return new ValidationResult(false, "Number must be positive!");
            }
            return ValidationResult.ValidResult;
        }
    }
}
