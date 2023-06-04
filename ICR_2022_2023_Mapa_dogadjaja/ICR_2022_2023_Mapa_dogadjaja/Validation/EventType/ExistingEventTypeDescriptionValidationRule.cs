using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ICR_2022_2023_Mapa_dogadjaja.Validation.EventType
{
    public class ExistingEventTypeDescriptionValidationRule : ValidationRule
    {
        public ExistingEventTypeDescriptionValidationRule() { }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var enteredDescription = value as string;
            if (string.IsNullOrWhiteSpace(enteredDescription))
            {
                return new ValidationResult(false, "Opis tipa događaja mora biti unet!");
            }

            return new ValidationResult(true, null);
        }
    }
}
