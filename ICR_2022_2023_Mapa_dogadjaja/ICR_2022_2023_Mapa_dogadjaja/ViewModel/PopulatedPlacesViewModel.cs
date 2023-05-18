using ICR_2022_2023_Mapa_dogadjaja.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ICR_2022_2023_Mapa_dogadjaja.ViewModel
{
    public class PopulatedPlacesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<PopulatedPlace> populatedPlaces = new ObservableCollection<PopulatedPlace>();

        public PopulatedPlacesViewModel()
        {
            if (populatedPlaces.Count > 0)
            {
                return;
            }

            PopulatedPlace populatedPlace01 = new PopulatedPlace("GRAD001", "Novi Sad");
            PopulatedPlace populatedPlace02 = new PopulatedPlace("GRAD002", "Drvengrad");
            PopulatedPlace populatedPlace03 = new PopulatedPlace("GRAD003", "Beograd");
            populatedPlaces.Add(populatedPlace01);
            populatedPlaces.Add(populatedPlace02);
            populatedPlaces.Add(populatedPlace03);
        }

        public ObservableCollection<PopulatedPlace> PopulatedPlaces
        {
            get { return populatedPlaces; }
            set
            {
                if (value != populatedPlaces)
                {
                    populatedPlaces = value;
                    OnPropertyChanged("PopulatedPlaces");
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
