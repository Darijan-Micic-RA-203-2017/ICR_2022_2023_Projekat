using ICR_2022_2023_Mapa_dogadjaja.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace ICR_2022_2023_Mapa_dogadjaja.ViewModel
{
    public class CountriesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Country> countries = new ObservableCollection<Country>();
        private JArray countriesArray;

        public CountriesViewModel()
        {
            LoadAll();
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

        // REFERENCE: https://www.newtonsoft.com/json/help/html/ReadJson.htm
        public void LoadAll()
        {
            try
            {
                Uri uriToFile = new Uri("../../Repository/Countries.json", UriKind.Relative);
                countriesArray = JArray.Parse(File.ReadAllText(uriToFile.ToString()));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                return;
            }

            // REFERENCE: https://www.newtonsoft.com/json/help/html/ToObjectType.htm
            foreach (JToken token in countriesArray.Children())
            {
                Country country = (Country) token.ToObject(typeof(Country));
                if (!countries.Contains(country))
                {
                    countries.Add(country);
                }
            }
        }

        // REFERENCE: https://www.newtonsoft.com/json/help/html/WriteToJsonFile.htm
        public void Save()
        {
            countriesArray = JArray.FromObject(countries);

            try
            {
                Uri uriToFile = new Uri("../../Repository/Countries.json", UriKind.Relative);
                File.WriteAllText(uriToFile.ToString(), countriesArray.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                return;
            }
        }

        public void Save(Country newCountry)
        {
            countries.Add(newCountry);

            Save();
        }
    }
}
