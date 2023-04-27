using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICR_2022_2023_Mapa_dogadjaja.Model
{
    public class PopulatedPlace
    {
        private int id;
        private string name;

        public PopulatedPlace() { }

        public PopulatedPlace(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
