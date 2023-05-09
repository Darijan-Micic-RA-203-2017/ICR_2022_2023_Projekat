using ICR_2022_2023_Mapa_dogadjaja.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ICR_2022_2023_Mapa_dogadjaja.ViewModel
{
    public class EventTypesViewModel
    {
        private ObservableCollection<EventType> eventTypes = new ObservableCollection<EventType>();

        public EventTypesViewModel()
        {
            if (eventTypes.Count > 0)
            {
                return;
            }

            EventType eventType01 = new EventType("TIPDOG001", "Muzički festival", "Opis", "Ikona");
            EventType eventType02 = new EventType("TIPDOG002", "Filmski festival", "Opis", "Ikona");
            EventType eventType03 = new EventType("TIPDOG003", "Košarkaška utakmica", "Opis", "Ikona");
            EventType eventType04 = new EventType("TIPDOG004", "Humanitarna aukcija", "Opis", "Ikona");
            eventTypes.Add(eventType01);
            eventTypes.Add(eventType02);
            eventTypes.Add(eventType03);
            eventTypes.Add(eventType04);
        }

        public ObservableCollection<EventType> EventTypes
        {
            get { return eventTypes; }
            set { eventTypes = value; }
        }
    }
}
