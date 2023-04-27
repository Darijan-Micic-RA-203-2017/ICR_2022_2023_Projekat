using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICR_2022_2023_Mapa_dogadjaja.Model
{
    public class EventType
    {
        private string id;
        private string name;
        private string description;
        private string icon;
        
        public EventType() { }

        public EventType(string id, string name, string description, string icon)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.icon = icon;
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
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

        public string Icon
        {
            get { return icon; }
            set { icon = value; }
        }
    }
}
