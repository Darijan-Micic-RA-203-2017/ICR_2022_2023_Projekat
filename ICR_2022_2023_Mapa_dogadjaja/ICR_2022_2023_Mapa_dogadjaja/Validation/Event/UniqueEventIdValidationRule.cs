using ICR_2022_2023_Mapa_dogadjaja.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ICR_2022_2023_Mapa_dogadjaja.Validation.Event
{
    public class UniqueEventIdValidationRule : ValidationRule
    {
        private EventsViewModel eventsViewModel;

        public UniqueEventIdValidationRule()
        {
            eventsViewModel = new EventsViewModel();
        }

        public EventsViewModel EventsViewModel
        {
            get { return eventsViewModel; }
            set { eventsViewModel = value; }
        }
        
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var enteredId = value as string;
            if (string.IsNullOrWhiteSpace(enteredId))
            {
                return new ValidationResult(false, "Oznaka događaja mora biti uneta!");
            }
            
            foreach (DTO.EventDTO eDTO in eventsViewModel.EventsDTOs)
            {
                if (eDTO.Id.Equals(enteredId))
                {
                    return new ValidationResult(false, "Već postoji događaj sa unetom oznakom!");
                }
            }
            
            return new ValidationResult(true, null);
        }
    }
}
