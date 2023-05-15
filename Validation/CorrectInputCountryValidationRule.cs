using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingProject.Validation
{
    public class CorrectInputCountryValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string countryName = value as string;

            if (string.IsNullOrEmpty(countryName))
            {
                return new ValidationResult(false, "Field cannot be left empty.");
            }

            string pattern = @"^(?:[A-Z][a-zA-Z]*\s)*[A-Z][a-zA-Z]*$";

            if (!Regex.IsMatch(countryName, pattern))
            {
                return new ValidationResult(false, "Try: Serbia, Czech Republic...");
            }

            if (countryName.Equals("null", StringComparison.OrdinalIgnoreCase))
            {
                return new ValidationResult(false, "Invalid input.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
