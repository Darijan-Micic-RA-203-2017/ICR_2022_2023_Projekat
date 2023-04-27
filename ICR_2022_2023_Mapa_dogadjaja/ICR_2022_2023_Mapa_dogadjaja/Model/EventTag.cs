using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICR_2022_2023_Mapa_dogadjaja.Model
{
    public class EventTag
    {
        private string id;
        private string color;
        private string description;

        public EventTag() { }

        public EventTag(string id, string color, string description)
        {
            this.id = id;
            this.color = color;
            this.description = description;
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}
