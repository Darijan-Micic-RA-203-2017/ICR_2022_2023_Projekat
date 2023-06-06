using ICR_2022_2023_Mapa_dogadjaja.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICR_2022_2023_Mapa_dogadjaja.ViewModel
{
    public class EventsTableSearcher
    {
        public EventsTableSearcher() { }

        public static ObservableCollection<Event> SearchEventsInTable(AllEntitiesViewModel allEntitiesViewModel)
        {
            if (allEntitiesViewModel.SearchModelEvent == null)
            {
                return null;
            }

            string id = allEntitiesViewModel.SearchModelEvent.Id;
            List<EventTag> tags = allEntitiesViewModel.SearchModelEvent.Tags;
            string name = allEntitiesViewModel.SearchModelEvent.Name;
            string description = allEntitiesViewModel.SearchModelEvent.Description;
            EventType type = allEntitiesViewModel.SearchModelEvent.Type;
            Attendance? attendance = allEntitiesViewModel.SearchModelEvent.Attendance;
            string icon = allEntitiesViewModel.SearchModelEvent.Icon;
            bool? isHumanitary = allEntitiesViewModel.SearchModelEvent.IsHumanitary;
            double averageCostsOfSustension = allEntitiesViewModel.SearchModelEvent.AverageCostsOfSustension;
            PopulatedPlace populatedPlace = allEntitiesViewModel.SearchModelEvent.PopulatedPlace;
            Country country = allEntitiesViewModel.SearchModelEvent.Country;
            List<DateTime> historyOfDatesOfTheEvent = allEntitiesViewModel.SearchModelEvent.HistoryOfDatesOfTheEvent;
            DateTime? dateOfTheEvent = allEntitiesViewModel.SearchModelEvent.DateOfTheEvent;

            ObservableCollection<Event> eventsThatFitSearchCriterions = new ObservableCollection<Event>();
            foreach (Event eve in allEntitiesViewModel.EventsViewModel.Events)
            {
                if (!string.IsNullOrWhiteSpace(id))
                {
                    if (eve.Id.StartsWith(id, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (!eventsThatFitSearchCriterions.Contains(eve))
                        {
                            eventsThatFitSearchCriterions.Add(eve);
                        }
                    }
                    else
                    {
                        eventsThatFitSearchCriterions.Remove(eve);
                        continue;
                    }
                }

                if (tags.Count > 0)
                {
                    bool isEventTaggedWithAtLeastOneSearchedTag = false;
                    foreach (EventTag eTag in tags)
                    {
                        if (eve.Tags.Contains(eTag))
                        {
                            isEventTaggedWithAtLeastOneSearchedTag = true;
                            if (!eventsThatFitSearchCriterions.Contains(eve))
                            {
                                eventsThatFitSearchCriterions.Add(eve);
                            }

                            break;
                        }
                    }

                    if (!isEventTaggedWithAtLeastOneSearchedTag)
                    {
                        eventsThatFitSearchCriterions.Remove(eve);
                        continue;
                    }
                }
                
                if (!string.IsNullOrWhiteSpace(name))
                {
                    if (eve.Name.StartsWith(name, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (!eventsThatFitSearchCriterions.Contains(eve))
                        {
                            eventsThatFitSearchCriterions.Add(eve);
                        }
                    }
                    else
                    {
                        eventsThatFitSearchCriterions.Remove(eve);
                        continue;
                    }
                }

                if (!string.IsNullOrWhiteSpace(description))
                {
                    if (eve.Description.StartsWith(description, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (!eventsThatFitSearchCriterions.Contains(eve))
                        {
                            eventsThatFitSearchCriterions.Add(eve);
                        }
                    }
                    else
                    {
                        eventsThatFitSearchCriterions.Remove(eve);
                        continue;
                    }
                }

                if (type != null)
                {
                    if (eve.Type.Equals(type))
                    {
                        if (!eventsThatFitSearchCriterions.Contains(eve))
                        {
                            eventsThatFitSearchCriterions.Add(eve);
                        }
                    }
                    else
                    {
                        eventsThatFitSearchCriterions.Remove(eve);
                        continue;
                    }
                }

                if (attendance != null)
                {
                    if (eve.Attendance == attendance)
                    {
                        if (!eventsThatFitSearchCriterions.Contains(eve))
                        {
                            eventsThatFitSearchCriterions.Add(eve);
                        }
                    }
                    else
                    {
                        eventsThatFitSearchCriterions.Remove(eve);
                        continue;
                    }
                }

                if (!string.IsNullOrWhiteSpace(icon))
                {
                    if (eve.Icon == icon)
                    {
                        if (!eventsThatFitSearchCriterions.Contains(eve))
                        {
                            eventsThatFitSearchCriterions.Add(eve);
                        }
                    }
                    else
                    {
                        eventsThatFitSearchCriterions.Remove(eve);
                        continue;
                    }
                }

                if (isHumanitary.HasValue)
                {
                    if (eve.IsHumanitary == isHumanitary.Value)
                    {
                        if (!eventsThatFitSearchCriterions.Contains(eve))
                        {
                            eventsThatFitSearchCriterions.Add(eve);
                        }
                    }
                    else
                    {
                        eventsThatFitSearchCriterions.Remove(eve);
                        continue;
                    }
                }

                if (averageCostsOfSustension > 0.0)
                {
                    if (eve.AverageCostsOfSustension == averageCostsOfSustension)
                    {
                        if (!eventsThatFitSearchCriterions.Contains(eve))
                        {
                            eventsThatFitSearchCriterions.Add(eve);
                        }
                    }
                    else
                    {
                        eventsThatFitSearchCriterions.Remove(eve);
                        continue;
                    }
                }

                if (populatedPlace != null)
                {
                    if (eve.PopulatedPlace.Equals(populatedPlace))
                    {
                        if (!eventsThatFitSearchCriterions.Contains(eve))
                        {
                            eventsThatFitSearchCriterions.Add(eve);
                        }
                    }
                    else
                    {
                        eventsThatFitSearchCriterions.Remove(eve);
                        continue;
                    }
                }

                if (country != null)
                {
                    if (eve.Country.Equals(country))
                    {
                        if (!eventsThatFitSearchCriterions.Contains(eve))
                        {
                            eventsThatFitSearchCriterions.Add(eve);
                        }
                    }
                    else
                    {
                        eventsThatFitSearchCriterions.Remove(eve);
                        continue;
                    }
                }

                if (historyOfDatesOfTheEvent.Count > 0)
                {
                    bool hasEventBeenHeldOnAtLeastOneSearchedHistoricalDate = false;
                    foreach (DateTime hDate in historyOfDatesOfTheEvent)
                    {
                        if (eve.HistoryOfDatesOfTheEvent.Contains(hDate))
                        {
                            hasEventBeenHeldOnAtLeastOneSearchedHistoricalDate = true;
                            if (!eventsThatFitSearchCriterions.Contains(eve))
                            {
                                eventsThatFitSearchCriterions.Add(eve);
                            }

                            break;
                        }
                    }

                    if (!hasEventBeenHeldOnAtLeastOneSearchedHistoricalDate)
                    {
                        eventsThatFitSearchCriterions.Remove(eve);
                        continue;
                    }
                }
                
                if (dateOfTheEvent.HasValue)
                {
                    if (eve.DateOfTheEvent == dateOfTheEvent.Value)
                    {
                        if (!eventsThatFitSearchCriterions.Contains(eve))
                        {
                            eventsThatFitSearchCriterions.Add(eve);
                        }
                    }
                    else
                    {
                        eventsThatFitSearchCriterions.Remove(eve);
                        continue;
                    }
                }
            }
            
            return eventsThatFitSearchCriterions;
        }
    }
}
