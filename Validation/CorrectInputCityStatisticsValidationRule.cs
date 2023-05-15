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
    public class CorrectInputCityStatisticsValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string cityName = value as string;

            if (string.IsNullOrEmpty(cityName))
            {
                return ValidationResult.ValidResult;
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
