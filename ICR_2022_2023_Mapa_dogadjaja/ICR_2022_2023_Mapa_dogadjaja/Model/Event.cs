using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ICR_2022_2023_Mapa_dogadjaja.Model
{
    public class Event : INotifyPropertyChanged
    {
        private string id;
        private List<EventTag> tags;
        private string name;
        private string description;
        private EventType type;
        private Attendance attendance;
        private string icon;
        private bool isHumanitary;
        private double averageCostsOfSustension;
        private PopulatedPlace populatedPlace;
        private Country country;
        private List<DateTime> historyOfDatesOfTheEvent;
        private DateTime dateOfTheEvent;

        public Event() { }

        public Event(string id, List<EventTag> tags, string name, string description, EventType type, Attendance attendance, 
            string icon, bool isHumanitary, double averageCostsOfSustension, PopulatedPlace populatedPlace, Country country, 
            List<DateTime> historyOfDatesOfTheEvent, DateTime dateOfTheEvent)
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

        public List<EventTag> Tags
        {
            get { return tags; }
            set
            {
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

        public EventType Type
        {
            get { return type; }
            set
            {
                if (!value.Equals(type))
                {
                    type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        public Attendance Attendance
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

        public bool IsHumanitary
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

        public PopulatedPlace PopulatedPlace
        {
            get { return populatedPlace; }
            set
            {
                if (!value.Equals(populatedPlace))
                {
                    populatedPlace = value;
                    OnPropertyChanged("PopulatedPlace");
                }
            }
        }

        public Country Country
        {
            get { return country; }
            set
            {
                if (!value.Equals(country))
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

        public DateTime DateOfTheEvent
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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Event other = (Event) obj;

            if (id == null)
            {
                if (other.id != null)
                {
                    return false;
                }
            }
            else if (id != other.id)
            {
                return false;
            }

            if (tags == null)
            {
                if (other.tags != null)
                {
                    return false;
                }
            }
            else if (!tags.SequenceEqual(other.tags))
            {
                return false;
            }

            if (name == null)
            {
                if (other.name != null)
                {
                    return false;
                }
            }
            else if (name != other.name)
            {
                return false;
            }

            if (description == null)
            {
                if (other.description != null)
                {
                    return false;
                }
            }
            else if (description != other.description)
            {
                return false;
            }

            if (type == null)
            {
                if (other.type != null)
                {
                    return false;
                }
            }
            else if (!type.Equals(other.type))
            {
                return false;
            }

            if (attendance != other.attendance)
            {
                return false;
            }

            if (icon == null)
            {
                if (other.icon != null)
                {
                    return false;
                }
            }
            else if (icon != other.icon)
            {
                return false;
            }

            if (isHumanitary != other.isHumanitary)
            {
                return false;
            }

            if (averageCostsOfSustension != other.averageCostsOfSustension)
            {
                return false;
            }

            if (populatedPlace == null)
            {
                if (other.populatedPlace != null)
                {
                    return false;
                }
            }
            else if (!populatedPlace.Equals(other.populatedPlace))
            {
                return false;
            }

            if (country == null)
            {
                if (other.country != null)
                {
                    return false;
                }
            }
            else if (!country.Equals(other.country))
            {
                return false;
            }

            if (historyOfDatesOfTheEvent == null)
            {
                if (other.historyOfDatesOfTheEvent != null)
                {
                    return false;
                }
            }
            else if (!historyOfDatesOfTheEvent.SequenceEqual(other.historyOfDatesOfTheEvent))
            {
                return false;
            }

            if (dateOfTheEvent == null)
            {
                if (other.dateOfTheEvent != null)
                {
                    return false;
                }
            }
            else if (dateOfTheEvent != other.dateOfTheEvent)
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}
