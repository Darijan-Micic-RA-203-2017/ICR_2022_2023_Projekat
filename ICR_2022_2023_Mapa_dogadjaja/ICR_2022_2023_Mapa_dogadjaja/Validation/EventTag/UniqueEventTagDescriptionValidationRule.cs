using ICR_2022_2023_Mapa_dogadjaja.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ICR_2022_2023_Mapa_dogadjaja.Validation.EventTag
{
    public class UniqueEventTagDescriptionValidationRule : ValidationRule
    {
        private EventTagsViewModel eventTagsViewModel;

        public UniqueEventTagDescriptionValidationRule()
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
            var enteredDescription = value as string;
            if (string.IsNullOrWhiteSpace(enteredDescription))
            {
                return new ValidationResult(false, "Opis etikete mora biti unet!");
            }

            string firstCharacterOfEnteredDescription = enteredDescription.Substring(0, 1);
            if (firstCharacterOfEnteredDescription.Equals(firstCharacterOfEnteredDescription.ToLower()))
            {
                return new ValidationResult(false, "Opis etikete mora započeti velikim slovom!");
            }

            foreach (Model.EventTag eTag in eventTagsViewModel.EventTags)
            {
                if (eTag.Description.Equals(enteredDescription))
                {
                    return new ValidationResult(false, "Već postoji etiketa sa unetim opisom!");
                }
            }

            return new ValidationResult(true, null);
        }
    }
}
