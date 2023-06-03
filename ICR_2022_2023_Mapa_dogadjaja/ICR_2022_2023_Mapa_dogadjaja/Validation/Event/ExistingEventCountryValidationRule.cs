using ICR_2022_2023_Mapa_dogadjaja.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ICR_2022_2023_Mapa_dogadjaja.Validation.Event
{
    public class ExistingEventCountryValidationRule : ValidationRule
    {
        public ExistingEventCountryValidationRule() { }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Model.Country selectedCountry = (Model.Country) value;
            if (selectedCountry == null)
            {
                return new ValidationResult(false, "Država mora biti odabrana!");
            }

            return new ValidationResult(true, null);
        }
    }
}
