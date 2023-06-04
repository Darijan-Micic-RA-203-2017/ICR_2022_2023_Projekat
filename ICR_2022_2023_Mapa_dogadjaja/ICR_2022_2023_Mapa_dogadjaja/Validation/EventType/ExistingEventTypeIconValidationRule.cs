using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ICR_2022_2023_Mapa_dogadjaja.Validation.EventType
{
    public class ExistingEventTypeIconValidationRule : ValidationRule
    {
        public ExistingEventTypeIconValidationRule() { }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            BitmapImage selectedIcon = (BitmapImage) value;
            if (selectedIcon == null)
            {
                return new ValidationResult(false, "Ikona mora biti odabrana!");
            }

            return new ValidationResult(true, null);
        }
    }
}
