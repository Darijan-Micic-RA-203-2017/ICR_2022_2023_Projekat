using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ICR_2022_2023_Mapa_dogadjaja.Validation.Event
{
    public class AverageCostsOfSustensionForEventValidationRule : ValidationRule
    {
        public AverageCostsOfSustensionForEventValidationRule() { }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var enteredAverageCostsOfSustensionAsString = value as string;
            if (string.IsNullOrWhiteSpace(enteredAverageCostsOfSustensionAsString))
            {
                return new ValidationResult(false, "Cena troškova mora biti uneta!");
            }

            double averageCostsOfSustension;
            if (!double.TryParse(enteredAverageCostsOfSustensionAsString, out averageCostsOfSustension))
            {
                return new ValidationResult(false, "Nije unet broj!");
            }

            if (averageCostsOfSustension < 0)
            {
                return new ValidationResult(false, "Nije unet pozitivan broj!");
            }

            return new ValidationResult(true, null);
        }
    }
}
