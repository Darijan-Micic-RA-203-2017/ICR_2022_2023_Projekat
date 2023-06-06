using ICR_2022_2023_Mapa_dogadjaja.DTO;
using ICR_2022_2023_Mapa_dogadjaja.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ICR_2022_2023_Mapa_dogadjaja.ViewModel
{
    public class AllEntitiesViewModel : INotifyPropertyChanged
    {
        private Event searchModelEvent;
        private CountriesViewModel countriesViewModel;
        private PopulatedPlacesViewModel populatedPlacesViewModel;
        private EventTypesViewModel eventTypesViewModel;
        private EventTagsViewModel eventTagsViewModel;
        private EventsViewModel eventsViewModel;
        
        public AllEntitiesViewModel()
        {
            searchModelEvent = null;
            countriesViewModel = new CountriesViewModel();
            populatedPlacesViewModel = new PopulatedPlacesViewModel();
            eventTypesViewModel = new EventTypesViewModel();
            eventTagsViewModel = new EventTagsViewModel();
            eventsViewModel = new EventsViewModel();

            ConnectEventsToOtherEntities();
        }

        public Event SearchModelEvent
        {
            get { return searchModelEvent; }
            set
            {
                if (value != searchModelEvent)
                {
                    searchModelEvent = value;
                    OnPropertyChanged("SearchModelEvent");
                }
            }
        }

        public CountriesViewModel CountriesViewModel
        {
            get { return countriesViewModel; }
            set
            {
                if (value != countriesViewModel)
                {
                    countriesViewModel = value;
                    OnPropertyChanged("CountriesViewModel");
                }
            }
        }
        
        public PopulatedPlacesViewModel PopulatedPlacesViewModel
        {
            get { return populatedPlacesViewModel; }
            set
            {
                if (value != populatedPlacesViewModel)
                {
                    populatedPlacesViewModel = value;
                    OnPropertyChanged("PopulatedPlacesViewModel");
                }
            }
        }
        
        public EventTypesViewModel EventTypesViewModel
        {
            get { return eventTypesViewModel; }
            set
            {
                if (value != eventTypesViewModel)
                {
                    eventTypesViewModel = value;
                    OnPropertyChanged("EventTypesViewModel");
                }
            }
        }
        
        public EventTagsViewModel EventTagsViewModel
        {
            get { return eventTagsViewModel; }
            set
            {
                if (value != eventTagsViewModel)
                {
                    eventTagsViewModel = value;
                    OnPropertyChanged("EventTagsViewModel");
                }
            }
        }
        
        public EventsViewModel EventsViewModel
        {
            get { return eventsViewModel; }
            set
            {
                if (value != eventsViewModel)
                {
                    eventsViewModel = value;
                    OnPropertyChanged("EventsViewModel");
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

        public void ConnectEventsToOtherEntities()
        {
            foreach (EventDTO eventDTO in eventsViewModel.EventsDTOs)
            {
                List<EventTag> tags = new List<EventTag>();
                foreach (string tagId in eventDTO.Tags)
                {
                    EventTag tag = eventTagsViewModel.FindById(tagId);
                    tags.Add(tag);
                }

                EventType type = eventTypesViewModel.FindById(eventDTO.Type);

                PopulatedPlace populatedPlace = populatedPlacesViewModel.FindById(eventDTO.PopulatedPlace);

                Country country = countriesViewModel.FindById(eventDTO.Country);

                Event eve = new Event(eventDTO.Id, tags, eventDTO.Name, eventDTO.Description, type, eventDTO.Attendance, 
                    eventDTO.Icon, eventDTO.IsHumanitary, eventDTO.AverageCostsOfSustension, populatedPlace, country, 
                    eventDTO.HistoryOfDatesOfTheEvent, eventDTO.DateOfTheEvent);
                eventsViewModel.Events.Add(eve);
            }
        }

        public void DereferenceEventsFromEntityToDelete(object entityToDelete)
        {
            if (entityToDelete is EventTag)
            {
                EventTag convertedSelectedEventTag = (EventTag) entityToDelete;
                foreach (Event eve in eventsViewModel.Events)
                {
                    eve.Tags.Remove(convertedSelectedEventTag);
                }
            }
            else if (entityToDelete is EventType)
            {
                EventType convertedSelectedEventType = (EventType) entityToDelete;
                foreach (Event eve in eventsViewModel.Events)
                {
                    if (eve.Type.Equals(convertedSelectedEventType))
                    {
                        eve.Type = null;
                    }
                }
            }
            else if (entityToDelete is PopulatedPlace)
            {
                PopulatedPlace convertedSelectedPopulatedPlace = (PopulatedPlace) entityToDelete;
                foreach (Event eve in eventsViewModel.Events)
                {
                    if (eve.PopulatedPlace.Equals(convertedSelectedPopulatedPlace))
                    {
                        eve.PopulatedPlace = null;
                    }
                }
            }
            else if (entityToDelete is Country)
            {
                Country convertedSelectedCountry = (Country) entityToDelete;
                foreach (Event eve in eventsViewModel.Events)
                {
                    if (eve.Country.Equals(convertedSelectedCountry))
                    {
                        eve.Country = null;
                    }
                }
            }
            else
            {
                return;
            }

            eventsViewModel.Save();
        }
    }
}
