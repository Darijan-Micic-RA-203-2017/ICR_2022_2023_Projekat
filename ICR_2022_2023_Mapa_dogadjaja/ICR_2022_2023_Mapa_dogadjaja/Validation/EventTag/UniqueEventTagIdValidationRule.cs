using ICR_2022_2023_Mapa_dogadjaja.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ICR_2022_2023_Mapa_dogadjaja.Validation.EventTag
{
    public class UniqueEventTagIdValidationRule : ValidationRule
    {
        private EventTagsViewModel eventTagsViewModel;

        public UniqueEventTagIdValidationRule()
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
            var enteredId = value as string;
            if (string.IsNullOrWhiteSpace(enteredId))
            {
                return new ValidationResult(false, "Oznaka etikete mora biti uneta!");
            }

            foreach (Model.EventTag eTag in eventTagsViewModel.EventTags)
            {
                if (eTag.Id.Equals(enteredId))
                {
                    return new ValidationResult(false, "Već postoji etiketa sa unetom oznakom!");
                }
            }

            return new ValidationResult(true, null);
        }
    }
}
