using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ICR_2022_2023_Mapa_dogadjaja.Model
{
    public class Event : IEditableObject, INotifyPropertyChanged
    {
        private struct EventData
        {
            internal string id;
            internal List<EventTag> tags;
            internal string name;
            internal string description;
            internal EventType type;
            internal Attendance attendance;
            internal string icon;
            internal bool isHumanitary;
            internal double averageCostsOfSustension;
            internal PopulatedPlace populatedPlace;
            internal Country country;
            internal List<DateTime> historyOfDatesOfTheEvent;
            internal DateTime? dateOfTheEvent;
        }
        
        private EventData eventData;
        private EventData backupData;
        private bool inTxn = false;

        public Event()
        {
            eventData = new EventData();
            eventData.id = "";
            backupData = new EventData();
        }

        public Event(string id, List<EventTag> tags, string name, string description, EventType type, Attendance attendance,
            string icon, bool isHumanitary, double averageCostsOfSustension, PopulatedPlace populatedPlace, Country country,
            List<DateTime> historyOfDatesOfTheEvent, DateTime? dateOfTheEvent)
        {
            eventData = new EventData();
            eventData.id = id;
            eventData.tags = tags;
            eventData.name = name;
            eventData.description = description;
            eventData.type = type;
            eventData.attendance = attendance;
            eventData.icon = icon;
            eventData.isHumanitary = isHumanitary;
            eventData.averageCostsOfSustension = averageCostsOfSustension;
            eventData.populatedPlace = populatedPlace;
            eventData.country = country;
            eventData.historyOfDatesOfTheEvent = historyOfDatesOfTheEvent;
            eventData.dateOfTheEvent = dateOfTheEvent;

            backupData = new EventData();
        }

        public string Id
        {
            get { return eventData.id; }
            set
            {
                if (value != eventData.id)
                {
                    eventData.id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public List<EventTag> Tags
        {
            get { return eventData.tags; }
            set
            {
                if (!value.SequenceEqual(eventData.tags))
                {
                    eventData.tags = value;
                    OnPropertyChanged("Tags");
                }
            }
        }

        public string Name
        {
            get { return eventData.name; }
            set
            {
                if (value != eventData.name)
                {
                    eventData.name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Description
        {
            get { return eventData.description; }
            set
            {
                if (value != eventData.description)
                {
                    eventData.description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public EventType Type
        {
            get { return eventData.type; }
            set
            {
                if (!value.Equals(eventData.type))
                {
                    eventData.type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        public Attendance Attendance
        {
            get { return eventData.attendance; }
            set
            {
                if (value != eventData.attendance)
                {
                    eventData.attendance = value;
                    OnPropertyChanged("Attendance");
                }
            }
        }

        public string Icon
        {
            get { return eventData.icon; }
            set
            {
                if (value != eventData.icon)
                {
                    eventData.icon = value;
                    OnPropertyChanged("Icon");
                }
            }
        }

        public bool IsHumanitary
        {
            get { return eventData.isHumanitary; }
            set
            {
                if (value != eventData.isHumanitary)
                {
                    eventData.isHumanitary = value;
                    OnPropertyChanged("IsHumanitary");
                }
            }
        }

        public double AverageCostsOfSustension
        {
            get { return eventData.averageCostsOfSustension; }
            set
            {
                if (value != eventData.averageCostsOfSustension)
                {
                    eventData.averageCostsOfSustension = value;
                    OnPropertyChanged("AverageCostsOfSustension");
                }
            }
        }

        public PopulatedPlace PopulatedPlace
        {
            get { return eventData.populatedPlace; }
            set
            {
                if (!value.Equals(eventData.populatedPlace))
                {
                    eventData.populatedPlace = value;
                    OnPropertyChanged("PopulatedPlace");
                }
            }
        }

        public Country Country
        {
            get { return eventData.country; }
            set
            {
                if (!value.Equals(eventData.country))
                {
                    eventData.country = value;
                    OnPropertyChanged("Country");
                }
            }
        }

        public List<DateTime> HistoryOfDatesOfTheEvent
        {
            get { return eventData.historyOfDatesOfTheEvent; }
            set
            {
                if (!value.SequenceEqual(eventData.historyOfDatesOfTheEvent))
                {
                    eventData.historyOfDatesOfTheEvent = value;
                    OnPropertyChanged("HistoryOfDatesOfTheEvent");
                }
            }
        }

        public DateTime? DateOfTheEvent
        {
            get { return eventData.dateOfTheEvent; }
            set
            {
                if (value != eventData.dateOfTheEvent)
                {
                    eventData.dateOfTheEvent = value;
                    OnPropertyChanged("DateOfTheEvent");
                }
            }
        }
        
        public void BeginEdit()
        {
            if (!inTxn)
            {
                backupData = eventData;
                inTxn = true;
            }
        }

        public void EndEdit()
        {
            if (inTxn)
            {
                backupData = new EventData();
                inTxn = false;
            }
        }

        public void CancelEdit()
        {
            if (inTxn)
            {
                eventData = backupData;
                inTxn = false;
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

            Event other = (Event)obj;

            if (eventData.id == null)
            {
                if (other.eventData.id != null)
                {
                    return false;
                }
            }
            else if (eventData.id != other.eventData.id)
            {
                return false;
            }

            if (eventData.tags == null)
            {
                if (other.eventData.tags != null)
                {
                    return false;
                }
            }
            else if (!eventData.tags.SequenceEqual(other.eventData.tags))
            {
                return false;
            }

            if (eventData.name == null)
            {
                if (other.eventData.name != null)
                {
                    return false;
                }
            }
            else if (eventData.name != other.eventData.name)
            {
                return false;
            }

            if (eventData.description == null)
            {
                if (other.eventData.description != null)
                {
                    return false;
                }
            }
            else if (eventData.description != other.eventData.description)
            {
                return false;
            }

            if (eventData.type == null)
            {
                if (other.eventData.type != null)
                {
                    return false;
                }
            }
            else if (!eventData.type.Equals(other.eventData.type))
            {
                return false;
            }

            if (eventData.attendance != other.eventData.attendance)
            {
                return false;
            }

            if (eventData.icon == null)
            {
                if (other.eventData.icon != null)
                {
                    return false;
                }
            }
            else if (eventData.icon != other.eventData.icon)
            {
                return false;
            }

            if (eventData.isHumanitary != other.eventData.isHumanitary)
            {
                return false;
            }

            if (eventData.averageCostsOfSustension != other.eventData.averageCostsOfSustension)
            {
                return false;
            }

            if (eventData.populatedPlace == null)
            {
                if (other.eventData.populatedPlace != null)
                {
                    return false;
                }
            }
            else if (!eventData.populatedPlace.Equals(other.eventData.populatedPlace))
            {
                return false;
            }

            if (eventData.country == null)
            {
                if (other.eventData.country != null)
                {
                    return false;
                }
            }
            else if (!eventData.country.Equals(other.eventData.country))
            {
                return false;
            }

            if (eventData.historyOfDatesOfTheEvent == null)
            {
                if (other.eventData.historyOfDatesOfTheEvent != null)
                {
                    return false;
                }
            }
            else if (!eventData.historyOfDatesOfTheEvent.SequenceEqual(other.eventData.historyOfDatesOfTheEvent))
            {
                return false;
            }

            if (eventData.dateOfTheEvent == null)
            {
                if (other.eventData.dateOfTheEvent != null)
                {
                    return false;
                }
            }
            else if (eventData.dateOfTheEvent != other.eventData.dateOfTheEvent)
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return eventData.id.GetHashCode();
        }
    }
}
