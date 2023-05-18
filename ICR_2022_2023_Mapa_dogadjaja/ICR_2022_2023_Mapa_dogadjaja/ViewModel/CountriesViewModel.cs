using ICR_2022_2023_Mapa_dogadjaja.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ICR_2022_2023_Mapa_dogadjaja.ViewModel
{
    public class CountriesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Country> countries = new ObservableCollection<Country>();

        public CountriesViewModel()
        {
            if (countries.Count > 0)
            {
                return;
            }

            Country country01 = new Country("DRZ001", "Srbija");
            Country country02 = new Country("DRZ002", "Hrvatska");
            countries.Add(country01);
            countries.Add(country02);
        }

        public ObservableCollection<Country> Countries
        {
            get { return countries; }
            set
            {
                if (value != countries)
                {
                    countries = value;
                    OnPropertyChanged("Countries");
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
