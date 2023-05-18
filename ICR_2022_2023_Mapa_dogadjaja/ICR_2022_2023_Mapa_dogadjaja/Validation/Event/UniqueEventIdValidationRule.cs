using ICR_2022_2023_Mapa_dogadjaja.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
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

            MessageBox.Show("Uneta oznaka: " + enteredId);

            foreach (Model.Event e in eventsViewModel.Events)
            {
                if (e.Id.Equals(enteredId))
                {
                    MessageBox.Show("Pronađen je događaj sa istom oznakom!");

                    return new ValidationResult(false, "Već postoji događaj sa unetom oznakom!");
                }
            }

            MessageBox.Show("Uneta oznaka jeste jedinstvena.");

            return new ValidationResult(true, null);
        }
    }
}
