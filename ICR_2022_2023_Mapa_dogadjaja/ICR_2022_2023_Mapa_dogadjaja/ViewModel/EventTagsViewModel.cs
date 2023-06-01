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
    public class EventTagsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<EventTag> eventTags = new ObservableCollection<EventTag>();
        private JArray eventTagsArray;

        public EventTagsViewModel()
        {
            LoadAll();
        }

        public ObservableCollection<EventTag> EventTags
        {
            get { return eventTags; }
            set
            {
                if (value != eventTags)
                {
                    eventTags = value;
                    OnPropertyChanged("EventTags");
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
                Uri uriToFile = new Uri("../../Repository/EventTags.json", UriKind.Relative);
                eventTagsArray = JArray.Parse(File.ReadAllText(uriToFile.ToString()));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                return;
            }

            // REFERENCE: https://www.newtonsoft.com/json/help/html/ToObjectType.htm
            foreach (JToken token in eventTagsArray.Children())
            {
                EventTag eventTag = (EventTag) token.ToObject(typeof(EventTag));
                if (!eventTags.Contains(eventTag))
                {
                    eventTags.Add(eventTag);
                }
            }
        }

        // REFERENCE: https://www.newtonsoft.com/json/help/html/WriteToJsonFile.htm
        public void Save()
        {
            eventTagsArray = JArray.FromObject(eventTags);

            try
            {
                Uri uriToFile = new Uri("../../Repository/EventTags.json", UriKind.Relative);
                File.WriteAllText(uriToFile.ToString(), eventTagsArray.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                return;
            }
        }

        public void Save(EventTag newEventTag)
        {
            eventTags.Add(newEventTag);

            Save();
        }
    }
}
