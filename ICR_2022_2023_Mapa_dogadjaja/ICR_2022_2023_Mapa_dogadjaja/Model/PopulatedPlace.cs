using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ICR_2022_2023_Mapa_dogadjaja.Model
{
    public class PopulatedPlace : INotifyPropertyChanged
    {
        private string id;
        private string name;

        public PopulatedPlace() { }

        public PopulatedPlace(string id, string name)
        {
            this.id = id;
            this.name = name;
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

            PopulatedPlace other = (PopulatedPlace) obj;

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

            return true;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}
