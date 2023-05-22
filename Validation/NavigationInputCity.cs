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
    public class NavigationInputCity : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = value as string;

            if (string.IsNullOrEmpty(input) || input.Trim() == "Enter city here")
            {
                return new ValidationResult(false, "Enter city here");
            }

            return ValidationResult.ValidResult;

        }
    }
}
