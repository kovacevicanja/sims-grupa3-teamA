using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingProject.Validation
{
    public class NavigationInputCountry : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = value as string;

            if (string.IsNullOrEmpty(input) || input.Trim() == "Enter country here")
            {
                return new ValidationResult(false, "Enter country here");
            }

            return ValidationResult.ValidResult;

        }
    }
}
