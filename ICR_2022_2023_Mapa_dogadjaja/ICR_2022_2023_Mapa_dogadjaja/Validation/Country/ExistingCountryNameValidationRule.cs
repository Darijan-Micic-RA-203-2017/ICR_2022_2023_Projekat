using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ICR_2022_2023_Mapa_dogadjaja.Validation.Country
{
    public class ExistingCountryNameValidationRule : ValidationRule
    {
        public ExistingCountryNameValidationRule() { }
        
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var enteredName = value as string;
            if (string.IsNullOrWhiteSpace(enteredName))
            {
                return new ValidationResult(false, "Naziv države mora biti unet!");
            }

            string firstCharacterOfEnteredName = enteredName.Substring(0, 1);
            if (firstCharacterOfEnteredName.Equals(firstCharacterOfEnteredName.ToLower()))
            {
                return new ValidationResult(false, "Naziv države mora započeti velikim slovom!");
            }
            
            return new ValidationResult(true, null);
        }
    }
}
