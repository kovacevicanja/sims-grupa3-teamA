using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingProject.Validation.Guest1Validation
{
    public class RequiredOptionValidationRule : ValidationRule 
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var selectedItem = (string)value;

            if (string.IsNullOrEmpty(selectedItem))
            {
                return new ValidationResult(false, "Select an option from the list.");
            }

            return ValidationResult.ValidResult;
        }

    }
}
