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
    public class PopulatedPlacesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<PopulatedPlace> populatedPlaces = new ObservableCollection<PopulatedPlace>();
        private JArray populatedPlacesArray;

        public PopulatedPlacesViewModel()
        {
            LoadAll();
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

        // REFERENCE: https://www.newtonsoft.com/json/help/html/ReadJson.htm
        public void LoadAll()
        {
            try
            {
                Uri uriToFile = new Uri("../../Repository/PopulatedPlaces.json", UriKind.Relative);
                populatedPlacesArray = JArray.Parse(File.ReadAllText(uriToFile.ToString()));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                return;
            }

            // REFERENCE: https://www.newtonsoft.com/json/help/html/ToObjectType.htm
            foreach (JToken token in populatedPlacesArray.Children())
            {
                PopulatedPlace populatedPlace = (PopulatedPlace) token.ToObject(typeof(PopulatedPlace));
                if (!populatedPlaces.Contains(populatedPlace))
                {
                    populatedPlaces.Add(populatedPlace);
                }
            }
        }

        // REFERENCE: https://www.newtonsoft.com/json/help/html/WriteToJsonFile.htm
        public void Save()
        {
            populatedPlacesArray = JArray.FromObject(populatedPlaces);

            try
            {
                Uri uriToFile = new Uri("../../Repository/PopulatedPlaces.json", UriKind.Relative);
                File.WriteAllText(uriToFile.ToString(), populatedPlacesArray.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                return;
            }
        }

        public void Save(PopulatedPlace newPopulatedPlace)
        {
            populatedPlaces.Add(newPopulatedPlace);

            Save();
        }
    }
}
