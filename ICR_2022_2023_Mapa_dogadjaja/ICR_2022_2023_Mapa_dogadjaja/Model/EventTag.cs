using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ICR_2022_2023_Mapa_dogadjaja.Model
{
    public class EventTag : INotifyPropertyChanged
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
            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public string Color
        {
            get { return color; }
            set
            {
                if (value != color)
                {
                    color = value;
                    OnPropertyChanged("Color");
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

            EventTag other = (EventTag) obj;

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

            if (color == null)
            {
                if (other.color != null)
                {
                    return false;
                }
            }
            else if (color != other.color)
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
            
            return true;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}
