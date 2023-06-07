using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace ICR_2022_2023_Mapa_dogadjaja.Validation.EventTag
{
    public class ExistingEventTagColorValidationRule : ValidationRule
    {
        public ExistingEventTagColorValidationRule() { }
        
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Color selectedColor = (Color) value;
            if (selectedColor == Colors.Transparent)
            {
                return new ValidationResult(false, "Boja etikete mora biti odabrana!");
            }
            
            return new ValidationResult(true, null);
        }
    }
}
