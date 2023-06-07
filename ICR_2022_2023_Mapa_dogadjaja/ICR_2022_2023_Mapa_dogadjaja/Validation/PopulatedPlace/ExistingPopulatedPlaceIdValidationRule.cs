using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ICR_2022_2023_Mapa_dogadjaja.Validation.PopulatedPlace
{
    public class ExistingPopulatedPlaceIdValidationRule : ValidationRule
    {
        public ExistingPopulatedPlaceIdValidationRule() { }
        
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var enteredId = value as string;
            if (string.IsNullOrWhiteSpace(enteredId))
            {
                return new ValidationResult(false, "Oznaka grada mora biti uneta!");
            }
            
            return new ValidationResult(true, null);
        }
    }
}
