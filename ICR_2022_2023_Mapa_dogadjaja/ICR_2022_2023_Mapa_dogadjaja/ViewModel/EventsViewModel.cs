using ICR_2022_2023_Mapa_dogadjaja.Converter;
using ICR_2022_2023_Mapa_dogadjaja.DTO;
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
    public class EventsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Event> events = new ObservableCollection<Event>();
        private ObservableCollection<EventDTO> eventsDTOs = new ObservableCollection<EventDTO>();
        private JArray eventsDTOsArray;

        public EventsViewModel()
        {
            LoadAll();
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

        public ObservableCollection<EventDTO> EventsDTOs
        {
            get { return eventsDTOs; }
            set
            {
                if (value != eventsDTOs)
                {
                    eventsDTOs = value;
                    OnPropertyChanged("EventsDTOs");
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
                Uri fileUri = new Uri("../../Repository/Events.json", UriKind.Relative);
                eventsDTOsArray = JArray.Parse(File.ReadAllText(fileUri.ToString()));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                return;
            }

            // REFERENCE: https://www.newtonsoft.com/json/help/html/ToObjectType.htm
            foreach (JToken eventToken in eventsDTOsArray.Children())
            {
                JToken idToken = eventToken["Id"];
                string id = (string) idToken.ToObject(typeof(string));
                JToken tagsToken = eventToken["Tags"];
                List<string> tags = (List<string>) tagsToken.ToObject(typeof(List<string>));
                JToken nameToken = eventToken["Name"];
                string name = (string) nameToken.ToObject(typeof(string));
                JToken descriptionToken = eventToken["Description"];
                string description = (string) descriptionToken.ToObject(typeof(string));
                JToken typeToken = eventToken["Type"];
                string type = (string) typeToken.ToObject(typeof(string));
                JToken attendanceToken = eventToken["Attendance"];
                Attendance attendance = (Attendance) attendanceToken.ToObject(typeof(Attendance));
                JToken iconToken = eventToken["Icon"];
                string icon = (string) iconToken.ToObject(typeof(string));
                JToken isHumanitaryToken = eventToken["IsHumanitary"];
                bool isHumanitary = (bool) isHumanitaryToken.ToObject(typeof(bool));
                JToken averageCostsOfSustensionToken = eventToken["AverageCostsOfSustension"];
                double averageCostsOfSustension = (double) averageCostsOfSustensionToken.ToObject(typeof(double));
                JToken populatedPlaceToken = eventToken["PopulatedPlace"];
                string populatedPlace = (string) populatedPlaceToken.ToObject(typeof(string));
                JToken countryToken = eventToken["Country"];
                string country = (string) countryToken.ToObject(typeof(string));
                JToken historyOfDatesOfTheEventToken = eventToken["HistoryOfDatesOfTheEvent"];
                List<DateTime> historyOfDatesOfTheEvent = (List<DateTime>) historyOfDatesOfTheEventToken.ToObject(typeof(List<DateTime>));
                JToken dateOfTheEventToken = eventToken["DateOfTheEvent"];
                DateTime? dateOfTheEvent = (DateTime?) dateOfTheEventToken.ToObject(typeof(DateTime?));

                EventDTO eventDTO = new EventDTO(id, tags, name, description, type, attendance, icon, isHumanitary, 
                    averageCostsOfSustension, populatedPlace, country, historyOfDatesOfTheEvent, dateOfTheEvent);
                if (!eventsDTOs.Contains(eventDTO))
                {
                    eventsDTOs.Add(eventDTO);
                }
            }
        }

        // REFERENCE: https://www.newtonsoft.com/json/help/html/WriteToJsonFile.htm
        public void Save()
        {
            eventsDTOs = EventDTOConverter.ConvertToDTOsCollection(events);
            eventsDTOsArray = JArray.FromObject(eventsDTOs);

            try
            {
                Uri fileUri = new Uri("../../Repository/Events.json", UriKind.Relative);
                File.WriteAllText(fileUri.ToString(), eventsDTOsArray.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                return;
            }
        }

        public void Save(Event newEvent)
        {
            events.Add(newEvent);

            Save();
        }

        public void Delete(Event eventToDelete)
        {
            events.Remove(eventToDelete);

            Save();
        }
    }
}
