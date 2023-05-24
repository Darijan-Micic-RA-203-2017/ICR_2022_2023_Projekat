using ICR_2022_2023_Mapa_dogadjaja.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ICR_2022_2023_Mapa_dogadjaja.Validation.Event
{
    public class ExistingEventPopulatedPlaceValidationRule : ValidationRule
    {
        public ExistingEventPopulatedPlaceValidationRule() { }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            PopulatedPlace selectedPopulatedPlace = (PopulatedPlace) value;
            if (selectedPopulatedPlace == null)
            {
                return new ValidationResult(false, "Grad mora biti odabran!");
            }

            return new ValidationResult(true, null);
        }
    }
}
