using ICR_2022_2023_Mapa_dogadjaja.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ICR_2022_2023_Mapa_dogadjaja.DTO
{
    public class EventDTO : INotifyPropertyChanged
    {
        private string id;
        private List<string> tags;
        private string name;
        private string description;
        private string type;
        private Attendance? attendance;
        private string icon;
        private bool? isHumanitary;
        private double averageCostsOfSustension;
        private string populatedPlace;
        private string country;
        private List<DateTime> historyOfDatesOfTheEvent;
        private DateTime? dateOfTheEvent;

        public EventDTO()
        {
            tags = new List<string>();
            historyOfDatesOfTheEvent = new List<DateTime>();
        }
        
        public EventDTO(string id, List<string> tags, string name, string description, string type, Attendance? attendance, 
            string icon, bool? isHumanitary, double averageCostsOfSustension, string populatedPlace, string country, 
            List<DateTime> historyOfDatesOfTheEvent, DateTime? dateOfTheEvent)
        {
            this.id = id;
            this.tags = tags;
            this.name = name;
            this.description = description;
            this.type = type;
            this.attendance = attendance;
            this.icon = icon;
            this.isHumanitary = isHumanitary;
            this.averageCostsOfSustension = averageCostsOfSustension;
            this.populatedPlace = populatedPlace;
            this.country = country;
            this.historyOfDatesOfTheEvent = historyOfDatesOfTheEvent;
            this.dateOfTheEvent = dateOfTheEvent;
        }

        public string Id
        {
            get { return id; }
            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public List<string> Tags
        {
            get { return tags; }
            set
            {
                if (value == null)
                {
                    tags = null;
                    OnPropertyChanged("Tags");

                    return;
                }

                if (!value.SequenceEqual(tags))
                {
                    tags = value;
                    OnPropertyChanged("Tags");
                }
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                if (value != description)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public string Type
        {
            get { return type; }
            set
            {
                if (value != type)
                {
                    type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        public Attendance? Attendance
        {
            get { return attendance; }
            set
            {
                if (value != attendance)
                {
                    attendance = value;
                    OnPropertyChanged("Attendance");
                }
            }
        }

        public string Icon
        {
            get { return icon; }
            set
            {
                if (value != icon)
                {
                    icon = value;
                    OnPropertyChanged("Icon");
                }
            }
        }

        public bool? IsHumanitary
        {
            get { return isHumanitary; }
            set
            {
                if (value != isHumanitary)
                {
                    isHumanitary = value;
                    OnPropertyChanged("IsHumanitary");
                }
            }
        }

        public double AverageCostsOfSustension
        {
            get { return averageCostsOfSustension; }
            set
            {
                if (value != averageCostsOfSustension)
                {
                    averageCostsOfSustension = value;
                    OnPropertyChanged("AverageCostsOfSustension");
                }
            }
        }

        public string PopulatedPlace
        {
            get { return populatedPlace; }
            set
            {
                if (value != populatedPlace)
                {
                    populatedPlace = value;
                    OnPropertyChanged("PopulatedPlace");
                }
            }
        }

        public string Country
        {
            get { return country; }
            set
            {
                if (value != country)
                {
                    country = value;
                    OnPropertyChanged("Country");
                }
            }
        }

        public List<DateTime> HistoryOfDatesOfTheEvent
        {
            get { return historyOfDatesOfTheEvent; }
            set
            {
                if (!value.SequenceEqual(historyOfDatesOfTheEvent))
                {
                    historyOfDatesOfTheEvent = value;
                    OnPropertyChanged("HistoryOfDatesOfTheEvent");
                }
            }
        }

        public DateTime? DateOfTheEvent
        {
            get { return dateOfTheEvent; }
            set
            {
                if (value != dateOfTheEvent)
                {
                    dateOfTheEvent = value;
                    OnPropertyChanged("DateOfTheEvent");
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
