using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ICR_2022_2023_Mapa_dogadjaja.Validation.Event
{
    public class DateOfTheEventValidationRule : ValidationRule
    {
        public DateOfTheEventValidationRule() { }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime? enteredDateOfTheEvent = (DateTime?)value;
            if (!enteredDateOfTheEvent.HasValue)
            {
                return new ValidationResult(false, "Datum održavanja događaja mora biti unet!");
            }

            return new ValidationResult(true, null);
        }
    }
}
