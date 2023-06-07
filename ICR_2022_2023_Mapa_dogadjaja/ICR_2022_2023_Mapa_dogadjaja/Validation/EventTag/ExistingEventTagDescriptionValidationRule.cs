using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ICR_2022_2023_Mapa_dogadjaja.Validation.EventTag
{
    public class ExistingEventTagDescriptionValidationRule : ValidationRule
    {
        public ExistingEventTagDescriptionValidationRule() {}
        
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var enteredDescription = value as string;
            if (string.IsNullOrWhiteSpace(enteredDescription))
            {
                return new ValidationResult(false, "Opis etikete mora biti unet!");
            }

            string firstCharacterOfEnteredDescription = enteredDescription.Substring(0, 1);
            if (firstCharacterOfEnteredDescription.Equals(firstCharacterOfEnteredDescription.ToLower()))
            {
                return new ValidationResult(false, "Opis etikete mora započeti velikim slovom!");
            }
            
            return new ValidationResult(true, null);
        }
    }
}
