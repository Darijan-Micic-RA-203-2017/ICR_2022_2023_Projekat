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
    }
}
