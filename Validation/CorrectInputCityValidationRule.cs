using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace BookingProject.Validation
{
    public class CorrectInputCityValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string cityName = value as string;

            if (string.IsNullOrEmpty(cityName))
            {
                return new ValidationResult(false, "Field cannot be left empty.");
            }

            string pattern = @"^(?:[A-Z][a-zA-Z]*\s?)*$";

            if (!Regex.IsMatch(cityName, pattern))
            {
                return new ValidationResult(false, "Try: Belgrade, Novi Sad...");
            }

            if (cityName.Equals("null", StringComparison.OrdinalIgnoreCase))
            {
                return new ValidationResult(false, "Invalid input.");
            }

            return ValidationResult.ValidResult;
        }
    }
}