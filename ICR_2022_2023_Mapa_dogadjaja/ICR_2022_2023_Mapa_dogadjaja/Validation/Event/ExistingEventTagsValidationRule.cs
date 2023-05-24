using ICR_2022_2023_Mapa_dogadjaja.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ICR_2022_2023_Mapa_dogadjaja.Validation.Event
{
    public class ExistingEventTagsValidationRule : ValidationRule
    {
        public ExistingEventTagsValidationRule() { }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            EventTag currentlySelectedEventTag = (EventTag) value;
            if (currentlySelectedEventTag == null)
            {
                return new ValidationResult(false, "Etikete moraju biti odabrane!");
            }

            return new ValidationResult(true, null);
        }
    }
}
