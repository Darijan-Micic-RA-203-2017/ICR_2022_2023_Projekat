using ICR_2022_2023_Mapa_dogadjaja.ViewModel;
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
    public class UniqueEventTagColorValidationRule : ValidationRule
    {
        private EventTagsViewModel eventTagsViewModel;

        public UniqueEventTagColorValidationRule()
        {
            eventTagsViewModel = new EventTagsViewModel();
        }

        public EventTagsViewModel EventTagsViewModel
        {
            get { return eventTagsViewModel; }
            set { eventTagsViewModel = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Color selectedColor = (Color) value;
            if (selectedColor == Colors.Transparent)
            {
                return new ValidationResult(false, "Boja etikete mora biti odabrana!");
            }

            foreach (Model.EventTag eTag in eventTagsViewModel.EventTags)
            {
                if (eTag.Color.Equals(selectedColor.ToString()))
                {
                    return new ValidationResult(false, "Već postoji etiketa odabrane boje!");
                }
            }

            return new ValidationResult(true, null);
        }
    }
}
