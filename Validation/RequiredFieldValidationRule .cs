using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingProject.Validation
{
    public class RequiredFieldValidationRule : ValidationRule
    {
        //public string ErrorMessage { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty((value ?? "").ToString()))
            {
                return new ValidationResult(false, "This must be filled");
            }

            return ValidationResult.ValidResult;
        }
    }
}