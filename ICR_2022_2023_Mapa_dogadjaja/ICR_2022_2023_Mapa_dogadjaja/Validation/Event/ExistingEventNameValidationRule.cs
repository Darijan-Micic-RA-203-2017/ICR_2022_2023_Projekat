using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ICR_2022_2023_Mapa_dogadjaja.Validation.Event
{
    public class ExistingEventNameValidationRule : ValidationRule
    {
        public ExistingEventNameValidationRule() { }
        
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var enteredName = value as string;
            if (string.IsNullOrWhiteSpace(enteredName))
            {
                return new ValidationResult(false, "Naziv događaja mora biti unet!");
            }

            string firstCharacterOfEnteredName = enteredName.Substring(0, 1);
            if (!int.TryParse(firstCharacterOfEnteredName, out _))
            {
                if (firstCharacterOfEnteredName.Equals(firstCharacterOfEnteredName.ToLower()))
                {
                    return new ValidationResult(false, "Naziv događaja mora započeti velikim slovom ili cifrom!");
                }
            }
            
            return new ValidationResult(true, null);
        }
    }
}
