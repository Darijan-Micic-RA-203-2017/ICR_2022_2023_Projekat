using ICR_2022_2023_Mapa_dogadjaja.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ICR_2022_2023_Mapa_dogadjaja.Validation.EventType
{
    public class UniqueEventTypeNameValidationRule : ValidationRule
    {
        private EventTypesViewModel eventTypesViewModel;

        public UniqueEventTypeNameValidationRule()
        {
            eventTypesViewModel = new EventTypesViewModel();
        }

        public EventTypesViewModel EventTypesViewModel
        {
            get { return eventTypesViewModel; }
            set { eventTypesViewModel = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var enteredName = value as string;
            if (string.IsNullOrWhiteSpace(enteredName))
            {
                return new ValidationResult(false, "Naziv tipa događaja mora biti unet!");
            }

            string firstCharacterOfEnteredName = enteredName.Substring(0, 1);
            if (firstCharacterOfEnteredName.Equals(firstCharacterOfEnteredName.ToLower()))
            {
                return new ValidationResult(false, "Naziv tipa događaja mora započeti velikim slovom!");
            }

            foreach (Model.EventType eType in eventTypesViewModel.EventTypes)
            {
                if (eType.Name.Equals(enteredName))
                {
                    return new ValidationResult(false, "Već postoji tip događaja sa unetim nazivom!");
                }
            }

            return new ValidationResult(true, null);
        }
    }
}
