using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingProject.Validation
{
    public class Guest1IntegerNumberValidationRule : ValidationRule
    {
        private const int MaxValue = 99;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            
            if (string.IsNullOrEmpty((string)value))
            {
                return new ValidationResult(false, "This field is required!");
            }

            if (!int.TryParse((string)value, out int intValue))
            {
                return new ValidationResult(false, "Number of guests must be an number!");
            }

            if (intValue <= 0)
            {
                return new ValidationResult(false, "Number of guests must be greater than zero!");
            }

            if (intValue > MaxValue)
            {
                return new ValidationResult(false, "Only two digits are allowed for this field!");
            }
            return ValidationResult.ValidResult;
        }
    }
}
