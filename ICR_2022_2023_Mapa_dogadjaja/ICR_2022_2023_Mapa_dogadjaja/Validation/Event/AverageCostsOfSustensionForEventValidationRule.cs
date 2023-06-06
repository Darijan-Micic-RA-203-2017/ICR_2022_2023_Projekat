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
        private bool isTriggeredInSearchEventsDialog;

        public AverageCostsOfSustensionForEventValidationRule() { }

        public bool IsTriggeredInSearchEventsDialog
        {
            get { return isTriggeredInSearchEventsDialog; }
            set { isTriggeredInSearchEventsDialog = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var enteredAverageCostsOfSustensionAsString = value as string;
            if (!isTriggeredInSearchEventsDialog)
            {
                if (string.IsNullOrWhiteSpace(enteredAverageCostsOfSustensionAsString))
                {
                    return new ValidationResult(false, "Cena troškova mora biti uneta!");
                }
            }

            double averageCostsOfSustension;
            if (!double.TryParse(enteredAverageCostsOfSustensionAsString, out averageCostsOfSustension))
            {
                return new ValidationResult(false, "Nije unet broj!");
            }
            
            if (!isTriggeredInSearchEventsDialog)
            {
                if (averageCostsOfSustension <= 0.0)
                {
                    return new ValidationResult(false, "Nije unet pozitivan broj!");
                }
            }
            else
            {
                if (averageCostsOfSustension < 0.0)
                {
                    return new ValidationResult(false, "Nije unet pozitivan broj!");
                }
            }

            return new ValidationResult(true, null);
        }
    }
}
