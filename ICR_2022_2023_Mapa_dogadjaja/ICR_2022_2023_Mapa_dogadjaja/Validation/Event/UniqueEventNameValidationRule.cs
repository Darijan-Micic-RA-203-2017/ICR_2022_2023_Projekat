using ICR_2022_2023_Mapa_dogadjaja.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ICR_2022_2023_Mapa_dogadjaja.Validation.Event
{
    public class UniqueEventNameValidationRule : ValidationRule
    {
        private EventsViewModel eventsViewModel;

        public UniqueEventNameValidationRule()
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
            var enteredName = value as string;
            if (string.IsNullOrWhiteSpace(enteredName))
            {
                return new ValidationResult(false, "Naziv mora biti unet!");
            }

            string firstCharacterOfEnteredName = enteredName.Substring(0, 1);
            if (firstCharacterOfEnteredName.Equals(firstCharacterOfEnteredName.ToLower()))
            {
                return new ValidationResult(false, "Naziv mora započeti velikim slovom ili cifrom!");
            }
            
            foreach (Model.Event e in eventsViewModel.Events)
            {
                if (e.Name.Equals(enteredName))
                {
                    return new ValidationResult(false, "Već postoji događaj sa unetim imenom!");
                }
            }

            return new ValidationResult(true, null);
        }
    }
}
