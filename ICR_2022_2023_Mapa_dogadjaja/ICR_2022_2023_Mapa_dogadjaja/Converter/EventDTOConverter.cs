using ICR_2022_2023_Mapa_dogadjaja.DTO;
using ICR_2022_2023_Mapa_dogadjaja.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ICR_2022_2023_Mapa_dogadjaja.Converter
{
    public class EventDTOConverter
    {
        public EventDTOConverter() { }
        
        public static EventDTO ConvertToDTO(Event eve)
        {
            if (eve == null)
            {
                return null;
            }
            
            EventDTO dto = new EventDTO();
            dto.Id = eve.Id;
            dto.Tags = new List<string>();
            foreach (EventTag eTag in eve.Tags)
            {
                dto.Tags.Add(eTag.Id);
            }
            dto.Name = eve.Name;
            dto.Description = eve.Description;
            dto.Type = eve.Type.Id;
            dto.Attendance = eve.Attendance;
            dto.Icon = eve.Icon;
            dto.IsHumanitary = eve.IsHumanitary;
            dto.AverageCostsOfSustension = eve.AverageCostsOfSustension;
            dto.PopulatedPlace = eve.PopulatedPlace.Id;
            dto.Country = eve.Country.Id;
            dto.HistoryOfDatesOfTheEvent = eve.HistoryOfDatesOfTheEvent;
            dto.DateOfTheEvent = eve.DateOfTheEvent;

            return dto;
        }
        
        public static ObservableCollection<EventDTO> ConvertToDTOsCollection(ObservableCollection<Event> eventsCollection)
        {
            if (eventsCollection == null)
            {
                return null;
            }

            ObservableCollection<EventDTO> dtosCollection = new ObservableCollection<EventDTO>();
            foreach (Event eve in eventsCollection)
            {
                dtosCollection.Add(ConvertToDTO(eve));
            }

            return dtosCollection;
        }
    }
}
