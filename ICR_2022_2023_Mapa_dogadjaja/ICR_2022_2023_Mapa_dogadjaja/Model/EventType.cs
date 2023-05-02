using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ICR_2022_2023_Mapa_dogadjaja.Model
{
    public class EventType : INotifyPropertyChanged
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
            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged("Id");
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

            EventType other = (EventType) obj;

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

            return true;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}
