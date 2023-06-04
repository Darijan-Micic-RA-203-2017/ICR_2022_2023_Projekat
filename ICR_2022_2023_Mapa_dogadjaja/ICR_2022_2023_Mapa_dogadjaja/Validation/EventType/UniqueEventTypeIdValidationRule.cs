using ICR_2022_2023_Mapa_dogadjaja.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ICR_2022_2023_Mapa_dogadjaja.Validation.EventType
{
    public class UniqueEventTypeIdValidationRule : ValidationRule
    {
        private EventTypesViewModel eventTypesViewModel;

        public UniqueEventTypeIdValidationRule()
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
            var enteredId = value as string;
            if (string.IsNullOrWhiteSpace(enteredId))
            {
                return new ValidationResult(false, "Oznaka tipa događaja mora biti uneta!");
            }

            foreach (Model.EventType eType in eventTypesViewModel.EventTypes)
            {
                if (eType.Id.Equals(enteredId))
                {
                    return new ValidationResult(false, "Već postoji tip događaja sa unetom oznakom!");
                }
            }

            return new ValidationResult(true, null);
        }
    }
}
