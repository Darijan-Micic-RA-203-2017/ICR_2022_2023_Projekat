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
    public class EventTypesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<EventType> eventTypes = new ObservableCollection<EventType>();
        private JArray eventTypesArray;

        public EventTypesViewModel()
        {
            LoadAll();
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

        public EventType FindById(string eventTypeId)
        {
            EventType foundEventType = null;
            foreach (EventType eType in eventTypes)
            {
                if (eType.Id == eventTypeId)
                {
                    foundEventType = eType;
                    break;
                }
            }

            return foundEventType;
        }

        // REFERENCE: https://www.newtonsoft.com/json/help/html/ReadJson.htm
        public void LoadAll()
        {
            try
            {
                Uri fileUri = new Uri("../../Repository/EventTypes.json", UriKind.Relative);
                eventTypesArray = JArray.Parse(File.ReadAllText(fileUri.ToString()));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                return;
            }

            // REFERENCE: https://www.newtonsoft.com/json/help/html/ToObjectType.htm
            foreach (JToken token in eventTypesArray.Children())
            {
                EventType eventType = (EventType) token.ToObject(typeof(EventType));
                if (!eventTypes.Contains(eventType))
                {
                    eventTypes.Add(eventType);
                }
            }
        }

        // REFERENCE: https://www.newtonsoft.com/json/help/html/WriteToJsonFile.htm
        public void Save()
        {
            eventTypesArray = JArray.FromObject(eventTypes);

            try
            {
                Uri fileUri = new Uri("../../Repository/EventTypes.json", UriKind.Relative);
                File.WriteAllText(fileUri.ToString(), eventTypesArray.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                return;
            }
        }

        public void Save(EventType newEventType)
        {
            eventTypes.Add(newEventType);

            Save();
        }

        public void Delete(EventType eventTypeToDelete)
        {
            eventTypes.Remove(eventTypeToDelete);

            Save();
        }
    }
}
