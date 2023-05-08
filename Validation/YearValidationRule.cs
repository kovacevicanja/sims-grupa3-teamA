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
    public class YearValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult(false, "Please enter a year.");
            }

            string yearStr = value.ToString();

            string pattern = @"^(?!0)\d{4}$";
            if (!Regex.IsMatch(yearStr, pattern))
            {
                return new ValidationResult(false, "Invalid year format. Try: 1999, 2023...");
            }

            int year = int.Parse(yearStr);

            if (year > DateTime.Now.Year)
            {
                return new ValidationResult(false, "Year cannot be in the future.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
