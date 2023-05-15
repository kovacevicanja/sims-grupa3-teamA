using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingProject.Validation
{
    public class SearchAccommodationValidationRule : ValidationRule
    {
        private const int MaxValue = 99;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!int.TryParse((string)value, out int intValue) && !value.Equals(""))
            {
                return new ValidationResult(false, "This field must be an number!");
            }

            if (intValue <= 0 && !value.Equals(""))
            {
                return new ValidationResult(false, "This field must be greater than zero!");
            }

            return ValidationResult.ValidResult;
        }
    }
}
