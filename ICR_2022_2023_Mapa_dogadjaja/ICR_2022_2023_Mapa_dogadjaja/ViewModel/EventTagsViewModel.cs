using ICR_2022_2023_Mapa_dogadjaja.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ICR_2022_2023_Mapa_dogadjaja.ViewModel
{
    public class EventTagsViewModel
    {
        private ObservableCollection<EventTag> eventTags = new ObservableCollection<EventTag>();

        public EventTagsViewModel()
        {
            if (eventTags.Count > 0)
            {
                return;
            }

            EventTag eventTag01 = new EventTag("ODOG001", "Brown", "Muzika");
            EventTag eventTag02 = new EventTag("ODOG002", "Red", "Kratkometražni film");
            EventTag eventTag03 = new EventTag("ODOG003", "Orange", "Dugometražni film");
            EventTag eventTag04 = new EventTag("ODOG004", "Blue", "Sport");
            EventTag eventTag05 = new EventTag("ODOG005", "Black", "Dvoranski sport");
            EventTag eventTag06 = new EventTag("ODOG006", "Green", "Humanitaran");
            eventTags.Add(eventTag01);
            eventTags.Add(eventTag02);
            eventTags.Add(eventTag03);
            eventTags.Add(eventTag04);
            eventTags.Add(eventTag05);
            eventTags.Add(eventTag06);
        }

        public ObservableCollection<EventTag> EventTags
        {
            get { return eventTags; }
            set { eventTags = value; }
        }
    }
}
