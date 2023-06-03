using ICR_2022_2023_Mapa_dogadjaja.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ICR_2022_2023_Mapa_dogadjaja.ViewModel
{
    public class EventsTableFilter
    {
        public EventsTableFilter() { }

        public static ObservableCollection<Event> FilterTableOfEvents(ObservableCollection<Event> events, string enteredText)
        {
            ObservableCollection<Event> filteredEvents = new ObservableCollection<Event>();
            if (string.IsNullOrWhiteSpace(enteredText))
            {
                return filteredEvents;
            }

            foreach (Event eve in events)
            {
                if (eve.Id.StartsWith(enteredText, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!filteredEvents.Contains(eve))
                    {
                        filteredEvents.Add(eve);
                    }
                    continue;
                }

                foreach (EventTag eTag in eve.Tags)
                {
                    if (eTag.Id.StartsWith(enteredText, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (!filteredEvents.Contains(eve))
                        {
                            filteredEvents.Add(eve);
                        }
                        continue;
                    }

                    if (eTag.Color.StartsWith(enteredText, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (!filteredEvents.Contains(eve))
                        {
                            filteredEvents.Add(eve);
                        }
                        continue;
                    }

                    if (eTag.Description.StartsWith(enteredText, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (!filteredEvents.Contains(eve))
                        {
                            filteredEvents.Add(eve);
                        }
                        continue;
                    }
                }

                if (eve.Name.StartsWith(enteredText, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!filteredEvents.Contains(eve))
                    {
                        filteredEvents.Add(eve);
                    }
                    continue;
                }

                if (eve.Description.StartsWith(enteredText, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!filteredEvents.Contains(eve))
                    {
                        filteredEvents.Add(eve);
                    }
                    continue;
                }

                if (eve.Type.Id.StartsWith(enteredText, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!filteredEvents.Contains(eve))
                    {
                        filteredEvents.Add(eve);
                    }
                    continue;
                }
                if (eve.Type.Name.StartsWith(enteredText, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!filteredEvents.Contains(eve))
                    {
                        filteredEvents.Add(eve);
                    }
                    continue;
                }
                if (eve.Type.Description.StartsWith(enteredText, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!filteredEvents.Contains(eve))
                    {
                        filteredEvents.Add(eve);
                    }
                    continue;
                }
                if (eve.Type.Icon.StartsWith(enteredText, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!filteredEvents.Contains(eve))
                    {
                        filteredEvents.Add(eve);
                    }
                    continue;
                }

                if (eve.Attendance.ToString().StartsWith(enteredText, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!filteredEvents.Contains(eve))
                    {
                        filteredEvents.Add(eve);
                    }
                    continue;
                }

                if (eve.Icon.StartsWith(enteredText, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!filteredEvents.Contains(eve))
                    {
                        filteredEvents.Add(eve);
                    }
                    continue;
                }

                if (eve.IsHumanitary.ToString().StartsWith(enteredText, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!filteredEvents.Contains(eve))
                    {
                        filteredEvents.Add(eve);
                    }
                    continue;
                }

                if (eve.AverageCostsOfSustension.ToString().StartsWith(enteredText, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!filteredEvents.Contains(eve))
                    {
                        filteredEvents.Add(eve);
                    }
                    continue;
                }

                if (eve.PopulatedPlace.Id.StartsWith(enteredText, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!filteredEvents.Contains(eve))
                    {
                        filteredEvents.Add(eve);
                    }
                    continue;
                }
                if (eve.PopulatedPlace.Name.StartsWith(enteredText, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!filteredEvents.Contains(eve))
                    {
                        filteredEvents.Add(eve);
                    }
                    continue;
                }

                if (eve.Country.Id.StartsWith(enteredText, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!filteredEvents.Contains(eve))
                    {
                        filteredEvents.Add(eve);
                    }
                    continue;
                }
                if (eve.Country.Name.StartsWith(enteredText, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!filteredEvents.Contains(eve))
                    {
                        filteredEvents.Add(eve);
                    }
                    continue;
                }

                foreach (DateTime historicalDOTE in eve.HistoryOfDatesOfTheEvent)
                {
                    if (historicalDOTE.ToString("MM/dd/yyyy").StartsWith(enteredText, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (!filteredEvents.Contains(eve))
                        {
                            filteredEvents.Add(eve);
                        }
                        continue;
                    }
                }

                if (eve.DateOfTheEvent.Value.ToString("MM/dd/yyyy").StartsWith(enteredText, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!filteredEvents.Contains(eve))
                    {
                        filteredEvents.Add(eve);
                    }
                    continue;
                }
            }

            return filteredEvents;
        }
    }
}
