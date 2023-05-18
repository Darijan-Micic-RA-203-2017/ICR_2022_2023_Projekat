using ICR_2022_2023_Mapa_dogadjaja.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ICR_2022_2023_Mapa_dogadjaja.ViewModel
{
    public class EventsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Event> events = new ObservableCollection<Event>();

        public EventsViewModel()
        {
            if (events.Count > 0)
            {
                return;
            }

            CountriesViewModel countriesViewModel = new CountriesViewModel();
            ObservableCollection<Country> countries = countriesViewModel.Countries;
            Country country01 = countries.First();

            PopulatedPlacesViewModel populatedPlacesViewModel = new PopulatedPlacesViewModel();
            ObservableCollection<PopulatedPlace> populatedPlaces = populatedPlacesViewModel.PopulatedPlaces;
            PopulatedPlace populatedPlace01 = populatedPlaces[0];
            PopulatedPlace populatedPlace02 = populatedPlaces[1];
            PopulatedPlace populatedPlace03 = populatedPlaces[2];

            EventTypesViewModel eventTypesViewModel = new EventTypesViewModel();
            ObservableCollection<EventType> eventTypes = eventTypesViewModel.EventTypes;
            EventType eventType01 = eventTypes[0];
            EventType eventType02 = eventTypes[1];
            EventType eventType03 = eventTypes[2];
            EventType eventType04 = eventTypes[3];

            EventTagsViewModel eventTagsViewModel = new EventTagsViewModel();
            ObservableCollection<EventTag> eventTags = eventTagsViewModel.EventTags;
            EventTag eventTag01 = eventTags[0];
            EventTag eventTag02 = eventTags[1];
            EventTag eventTag03 = eventTags[2];
            EventTag eventTag04 = eventTags[3];
            EventTag eventTag05 = eventTags[4];
            EventTag eventTag06 = eventTags[5];

            // REFERENCE: https://stackoverflow.com/a/2416464
            Event event01 = new Event("DOG001", new List<EventTag>() { eventTag01 }, "Exit", 
                "Exit se održava u julu svake godine na Petrovaradinskoj tvrđavi.", eventType01, Attendance.OVER_10000,
                "/ICR_2022_2023_Mapa_dogadjaja;component/Icons/EventIcons/EXIT_ikona.jpg", false, 250000.0, populatedPlace01, 
                country01, new List<DateTime>() { }, new DateTime(2023, 7, 11));
            Event event02 = new Event("DOG002", new List<EventTag>() { eventTag02, eventTag03 }, "Kustendorf", 
                "Kustendorf se održava 24. januara svake godine u etno naselju Drvengrad na planini Mećavnik.", eventType02, 
                Attendance.OVER_10000, "/ICR_2022_2023_Mapa_dogadjaja;component/Icons/EventIcons/Kustendorf_ikona.png", false, 
                200000.0, populatedPlace02, country01, new List<DateTime>() { }, new DateTime(2023, 1, 24));
            Event event03 = new Event("DOG003", new List<EventTag>() { eventTag04, eventTag05 }, "Partizan - Barselona", 
                "Utakmica 32. kola ligaškog dela Evrolige u Štark areni.", eventType03, Attendance.OVER_10000,
                "/ICR_2022_2023_Mapa_dogadjaja;component/Icons/EventIcons/Partizan-Barselona_ikona.jpg", false, 110000.0, 
                populatedPlace03, country01, new List<DateTime>() { }, new DateTime(2023, 4, 6));
            Event event04 = new Event("DOG004", new List<EventTag>() { eventTag06 }, "Aukcija sportskih dresova Miloša Nikolića", 
                "Uplaćen novac za kupovinu potpisanih dresova najpoznatijih sportista Evrope iskoristiće se kao donacija " + 
                "Institutu za majku i dete u svrhu nabavke novih inkubatora za bebe.", eventType04, Attendance.BELOW_1000,
                "/ICR_2022_2023_Mapa_dogadjaja;component/Icons/EventIcons/Aukcija_sportskih_dresova_Milosa_Nikolica_ikona.jpg", 
                true, 50.0, populatedPlace03, country01, new List<DateTime>() { }, new DateTime(2023, 5, 30));
            events.Add(event01);
            events.Add(event02);
            events.Add(event03);
            events.Add(event04);
        }

        public ObservableCollection<Event> Events
        {
            get { return events; }
            set
            {
                if (value != events)
                {
                    events = value;
                    OnPropertyChanged("Events");
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
