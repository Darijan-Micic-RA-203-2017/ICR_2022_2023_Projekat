using ICR_2022_2023_Mapa_dogadjaja.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ICR_2022_2023_Mapa_dogadjaja.ViewModel
{
    public class EventTypesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<EventType> eventTypes = new ObservableCollection<EventType>();

        public EventTypesViewModel()
        {
            if (eventTypes.Count > 0)
            {
                return;
            }

            // REFERENCE: https://stackoverflow.com/a/2416464
            EventType eventType01 = new EventType("TIPDOG001", "Muzički festival", 
                "Festival muzike u zatvorenom ili otvorenom prostoru.",
                "/ICR_2022_2023_Mapa_dogadjaja;component/Icons/EventTypeIcons/Muzicki_festival_ikona.png");
            EventType eventType02 = new EventType("TIPDOG002", "Filmski festival", 
                "Festival filma u zatvorenom ili otvorenom prostoru.",
                "/ICR_2022_2023_Mapa_dogadjaja;component/Icons/EventTypeIcons/Filmski_festival_ikona.jpg");
            EventType eventType03 = new EventType("TIPDOG003", "Košarkaška utakmica", 
                "Utakmica 2. najpopularnijeg timskog sporta, posmatrano na globalnom nivou.",
                "/ICR_2022_2023_Mapa_dogadjaja;component/Icons/EventTypeIcons/Kosarkaska_utakmica_ikona.png");
            EventType eventType04 = new EventType("TIPDOG004", "Humanitarna aukcija", 
                "Aukcija napravljena sa ciljem da se uplaćeni novac donira u humanitarne svrhe.",
                "/ICR_2022_2023_Mapa_dogadjaja;component/Icons/EventTypeIcons/Humanitarna_aukcija_ikona.jpg");
            eventTypes.Add(eventType01);
            eventTypes.Add(eventType02);
            eventTypes.Add(eventType03);
            eventTypes.Add(eventType04);
        }

        public ObservableCollection<EventType> EventTypes
        {
            get { return eventTypes; }
            set
            {
                if (value != eventTypes)
                {
                    eventTypes = value;
                    OnPropertyChanged("EventTypes");
                }
            }
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
