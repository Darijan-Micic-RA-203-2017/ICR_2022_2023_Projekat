using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICR_2022_2023_Mapa_dogadjaja.Model
{
    public class Event
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
            set { id = value; }
        }

        public List<EventTag> Tags
        {
            get { return tags; }
            set { tags = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public EventType Type
        {
            get { return type; }
            set { type = value; }
        }

        public Attendance Attendance
        {
            get { return attendance; }
            set { attendance = value; }
        }

        public string Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        public bool IsHumanitary
        {
            get { return isHumanitary; }
            set { isHumanitary = value; }
        }

        public double AverageCostsOfSustension
        {
            get { return averageCostsOfSustension; }
            set { averageCostsOfSustension = value; }
        }

        public PopulatedPlace PopulatedPlace
        {
            get { return populatedPlace; }
            set { populatedPlace = value; }
        }

        public Country Country
        {
            get { return country; }
            set { country = value; }
        }

        public List<DateTime> HistoryOfDatesOfTheEvent
        {
            get { return historyOfDatesOfTheEvent; }
            set { historyOfDatesOfTheEvent = value; }
        }

        public DateTime DateOfTheEvent
        {
            get { return dateOfTheEvent; }
            set { dateOfTheEvent = value; }
        }
    }
}
