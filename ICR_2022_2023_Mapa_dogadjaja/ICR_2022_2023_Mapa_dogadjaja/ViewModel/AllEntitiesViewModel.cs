using ICR_2022_2023_Mapa_dogadjaja.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ICR_2022_2023_Mapa_dogadjaja.ViewModel
{
    public class AllEntitiesViewModel
    {
        private CountriesViewModel countriesViewModel;
        private PopulatedPlacesViewModel populatedPlacesViewModel;
        private EventTypesViewModel eventTypesViewModel;
        private EventTagsViewModel eventTagsViewModel;
        private EventsViewModel eventsViewModel;
        
        public AllEntitiesViewModel()
        {
            countriesViewModel = new CountriesViewModel();
            populatedPlacesViewModel = new PopulatedPlacesViewModel();
            eventTypesViewModel = new EventTypesViewModel();
            eventTagsViewModel = new EventTagsViewModel();
            eventsViewModel = new EventsViewModel();
        }

        public ObservableCollection<Country> Countries
        {
            get { return countriesViewModel.Countries; }
            set { countriesViewModel.Countries = value; }
        }

        public ObservableCollection<PopulatedPlace> PopulatedPlaces
        {
            get { return populatedPlacesViewModel.PopulatedPlaces; }
            set { populatedPlacesViewModel.PopulatedPlaces = value; }
        }

        public ObservableCollection<EventType> EventTypes
        {
            get { return eventTypesViewModel.EventTypes; }
            set { eventTypesViewModel.EventTypes = value; }
        }

        public ObservableCollection<EventTag> EventTags
        {
            get { return eventTagsViewModel.EventTags; }
            set { eventTagsViewModel.EventTags = value; }
        }

        public ObservableCollection<Event> Events
        {
            get { return eventsViewModel.Events; }
            set { eventsViewModel.Events = value; }
        }
    }
}
