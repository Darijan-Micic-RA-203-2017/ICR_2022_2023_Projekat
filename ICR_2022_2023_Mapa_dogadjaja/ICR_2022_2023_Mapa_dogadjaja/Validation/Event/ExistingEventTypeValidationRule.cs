using ICR_2022_2023_Mapa_dogadjaja.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ICR_2022_2023_Mapa_dogadjaja.Validation.Event
{
    public class ExistingEventTypeValidationRule : ValidationRule
    {
        public ExistingEventTypeValidationRule() { }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Model.EventType selectedEventType = (Model.EventType) value;
            if (selectedEventType == null)
            {
                return new ValidationResult(false, "Tip mora biti odabran!");
            }

            return new ValidationResult(true, null);
        }
    }
}
