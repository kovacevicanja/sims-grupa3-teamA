using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingProject.Validation
{
    public class IntegerNumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                if (Convert.ToInt32(value) > 0)
                {
                    return ValidationResult.ValidResult;
                }
                else
                {
                    return new ValidationResult(false, "Number of guests should be an integer value11111.");
                }
            }
            catch
            {
                return new ValidationResult(false, "Number of guests should be an integer value22222.");
            }
        }
    }
}