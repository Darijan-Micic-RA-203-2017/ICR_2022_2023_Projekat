using ICR_2022_2023_Mapa_dogadjaja.Model;
using ICR_2022_2023_Mapa_dogadjaja.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ICR_2022_2023_Mapa_dogadjaja
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string DEFAULT_TEXT_OF_INPUT_FOR_FILTERING_TABLE = "Filtriraj tabelu (Alt + 2)";
        private const string DEFAULT_TEXT_OF_INPUT_FOR_FILTERING_MAP = "Filtriraj mapu (Alt + 3)";
        
        private ObservableCollection<Event> events = new ObservableCollection<Event>();
        private ObservableCollection<EventTag> eventTags = new ObservableCollection<EventTag>();
        private ObservableCollection<EventType> eventTypes = new ObservableCollection<EventType>();
        private ObservableCollection<PopulatedPlace> populatedPlaces = new ObservableCollection<PopulatedPlace>();
        private ObservableCollection<Country> countries = new ObservableCollection<Country>();

        private ObservableCollection<Event> eventsThatFitSearchCriterions = new ObservableCollection<Event>();
        private ObservableCollection<EventTag> eventTagsThatFitSearchCriterions = new ObservableCollection<EventTag>();
        private ObservableCollection<EventType> eventTypesThatFitSearchCriterions = new ObservableCollection<EventType>();
        private ObservableCollection<PopulatedPlace> populatedPlacesThatFitSearchCriterions = new ObservableCollection<PopulatedPlace>();
        private ObservableCollection<Country> countriesThatFitSearchCriterions = new ObservableCollection<Country>();

        private ObservableCollection<Event> filteredEvents = new ObservableCollection<Event>();
        private ObservableCollection<EventTag> filteredEventTags = new ObservableCollection<EventTag>();
        private ObservableCollection<EventType> filteredEventTypes = new ObservableCollection<EventType>();
        private ObservableCollection<PopulatedPlace> filteredPopulatedPlaces = new ObservableCollection<PopulatedPlace>();
        private ObservableCollection<Country> filteredCountries = new ObservableCollection<Country>();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            
            Country country01 = new Country("DRZ001", "Srbija");
            Country country02 = new Country("DRZ002", "Hrvatska");
            countries.Add(country01);
            countries.Add(country02);
            
            PopulatedPlace populatedPlace01 = new PopulatedPlace("GRAD001", "Novi Sad");
            PopulatedPlace populatedPlace02 = new PopulatedPlace("GRAD002", "Drvengrad");
            PopulatedPlace populatedPlace03 = new PopulatedPlace("GRAD003", "Beograd");
            populatedPlaces.Add(populatedPlace01);
            populatedPlaces.Add(populatedPlace02);
            populatedPlaces.Add(populatedPlace03);
            
            EventType eventType01 = new EventType("TIPDOG001", "Muzički festival", "Opis", "Ikona");
            EventType eventType02 = new EventType("TIPDOG002", "Filmski festival", "Opis", "Ikona");
            EventType eventType03 = new EventType("TIPDOG003", "Košarkaška utakmica", "Opis", "Ikona");
            EventType eventType04 = new EventType("TIPDOG004", "Humanitarna aukcija", "Opis", "Ikona");
            eventTypes.Add(eventType01);
            eventTypes.Add(eventType02);
            eventTypes.Add(eventType03);
            eventTypes.Add(eventType04);
            
            EventTag eventTag01 = new EventTag("ODOG001", "Brown", "Muzika");
            EventTag eventTag02 = new EventTag("ODOG002", "Red", "Kratkometražni film");
            EventTag eventTag03 = new EventTag("ODOG003", "Orange", "Dugometražni film");
            EventTag eventTag04 = new EventTag("ODOG004", "Blue", "Sport");
            EventTag eventTag05 = new EventTag("ODOG005", "Black", "Dvoranski sport");
            EventTag eventTag06 = new EventTag("ODOG006", "Green", "Humanitaran");
            eventTags.Add(eventTag01);
            eventTags.Add(eventTag02);
            eventTags.Add(eventTag03);
            eventTags.Add(eventTag04);
            eventTags.Add(eventTag05);
            eventTags.Add(eventTag06);
            
            Event event01 = new Event("DOG001", new List<EventTag>() {eventTag01}, "Exit", 
                "Exit se održava u julu svake godine na Petrovaradinskoj tvrđavi.", eventType01, Attendance.OVER_10000, 
                "Icons/EventIcons/EXIT_ikona.jpg", false, 250000.0, populatedPlace01, country01, new List<DateTime>() {}, 
                new DateTime(2023, 7, 11));
            Event event02 = new Event("DOG002", new List<EventTag>() {eventTag02, eventTag03}, "Kustendorf", 
                "Kustendorf se održava 24. januara svake godine u etno naselju Drvengrad na planini Mećavnik.", eventType02, 
                Attendance.OVER_10000, "Icons/EventIcons/Kustendorf_ikona.png", false, 200000.0, populatedPlace02, country01, 
                new List<DateTime>() { }, new DateTime(2023, 1, 24));
            Event event03 = new Event("DOG003", new List<EventTag>() {eventTag04, eventTag05}, "Partizan - Barselona",
                "Utakmica 32. kola ligaškog dela Evrolige u Štark areni.", eventType03, Attendance.OVER_10000, 
                "Icons/EventIcons/Partizan-Barselona_ikona.jpg", false, 110000.0, populatedPlace03, country01, 
                new List<DateTime>() {}, new DateTime(2023, 4, 6));
            Event event04 = new Event("DOG004", new List<EventTag>() {eventTag06}, "Aukcija sportskih dresova Miloša Nikolića", 
                "Uplaćen novac za kupovinu potpisanih dresova najpoznatijih sportista Evrope iskoristiće se kao donacija " + 
                "Institutu za majku i dete u svrhu nabavke novih inkubatora za bebe.", eventType04, Attendance.BELOW_1000, 
                "Icons/EventIcons/Aukcija_sportskih_dresova_Milosa_Nikolica_ikona.jpg", true, 50.0, populatedPlace03, country01, 
                new List<DateTime>() {}, new DateTime(2023, 5, 30));
            events.Add(event01);
            events.Add(event02);
            events.Add(event03);
            events.Add(event04);
            
            AddHotKeys();
        }

        public ObservableCollection<Event> Events
        {
            get { return events; }
            set { events = value; }
        }

        public ObservableCollection<EventTag> EventTags
        {
            get { return eventTags; }
            set { eventTags = value; }
        }

        public ObservableCollection<EventType> EventTypes
        {
            get { return eventTypes; }
            set { eventTypes = value; }
        }

        public ObservableCollection<PopulatedPlace> PopulatedPlaces
        {
            get { return populatedPlaces; }
            set { populatedPlaces = value; }
        }

        public ObservableCollection<Country> Countries
        {
            get { return countries; }
            set { countries = value; }
        }

        public ObservableCollection<Event> EventsThatFitSearchCriterions
        {
            get { return eventsThatFitSearchCriterions; }
            set { eventsThatFitSearchCriterions = value; }
        }

        public ObservableCollection<EventTag> EventTagsThatFitSearchCriterions
        {
            get { return eventTagsThatFitSearchCriterions; }
            set { eventTagsThatFitSearchCriterions = value; }
        }

        public ObservableCollection<EventType> EventTypesThatFitSearchCriterions
        {
            get { return eventTypesThatFitSearchCriterions; }
            set { eventTypesThatFitSearchCriterions = value; }
        }

        public ObservableCollection<PopulatedPlace> PopulatedPlacesThatFitSearchCriterions
        {
            get { return populatedPlacesThatFitSearchCriterions; }
            set { populatedPlacesThatFitSearchCriterions = value; }
        }

        public ObservableCollection<Country> CountriesThatFitSearchCriterions
        {
            get { return countriesThatFitSearchCriterions; }
            set { countriesThatFitSearchCriterions = value; }
        }

        public ObservableCollection<Event> FilteredEvents
        {
            get { return filteredEvents; }
            set { filteredEvents = value; }
        }

        public ObservableCollection<EventTag> FilteredEventTags
        {
            get { return filteredEventTags; }
            set { filteredEventTags = value; }
        }

        public ObservableCollection<EventType> FilteredEventTypes
        {
            get { return filteredEventTypes; }
            set { filteredEventTypes = value; }
        }

        public ObservableCollection<PopulatedPlace> FilteredPopulatedPlaces
        {
            get { return filteredPopulatedPlaces; }
            set { filteredPopulatedPlaces = value; }
        }

        public ObservableCollection<Country> FilteredCountries
        {
            get { return filteredCountries; }
            set { filteredCountries = value; }
        }

        // REFERENCE: https://codesamplez.com/development/wpf-hotkeys-c-sharp
        private void AddHotKeys()
        {
            try
            {
                RoutedCommand closeApplicationCommand = new RoutedCommand();
                closeApplicationCommand.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(closeApplicationCommand, CloseApplication));

                RoutedCommand openDialogForCreatingANewEventCommand = new RoutedCommand();
                openDialogForCreatingANewEventCommand.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(openDialogForCreatingANewEventCommand, OpenDialogForCreatingANewEvent));

                RoutedCommand openDialogForEditingAnEventCommand = new RoutedCommand();
                openDialogForEditingAnEventCommand.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(openDialogForEditingAnEventCommand, OpenDialogForEditingAnEvent));

                RoutedCommand openDialogForDeletingAnEntityCommand = new RoutedCommand();
                openDialogForDeletingAnEntityCommand.InputGestures.Add(new KeyGesture(Key.Delete, ModifierKeys.None));
                CommandBindings.Add(new CommandBinding(openDialogForDeletingAnEntityCommand, OpenDialogForDeletingAnEntity));
                
                RoutedCommand openDialogForAllEventTagsCommand = new RoutedCommand();
                openDialogForAllEventTagsCommand.InputGestures.Add(new KeyGesture(Key.I, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(openDialogForAllEventTagsCommand, OpenDialogForAllEventTags));

                RoutedCommand openDialogForAllEventTypesCommand = new RoutedCommand();
                openDialogForAllEventTypesCommand.InputGestures.Add(new KeyGesture(Key.T, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(openDialogForAllEventTypesCommand, OpenDialogForAllEventTypes));

                RoutedCommand openDialogForAllPopulatedPlacesCommand = new RoutedCommand();
                openDialogForAllPopulatedPlacesCommand.InputGestures.Add(new KeyGesture(Key.G, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(openDialogForAllPopulatedPlacesCommand, OpenDialogForAllPopulatedPlaces));

                RoutedCommand openDialogForAllCountriesCommand = new RoutedCommand();
                openDialogForAllCountriesCommand.InputGestures.Add(new KeyGesture(Key.R, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(openDialogForAllCountriesCommand, OpenDialogForAllCountries));

                RoutedCommand focusOnInputForFilteringTableCommand = new RoutedCommand();
                focusOnInputForFilteringTableCommand.InputGestures.Add(new KeyGesture(Key.D2, ModifierKeys.Alt));
                CommandBindings.Add(new CommandBinding(focusOnInputForFilteringTableCommand, FocusOnInputForFilteringTable));

                RoutedCommand cancelSearchOrFilteringCommand = new RoutedCommand();
                cancelSearchOrFilteringCommand.InputGestures.Add(new KeyGesture(Key.D4, ModifierKeys.Alt));
                CommandBindings.Add(new CommandBinding(cancelSearchOrFilteringCommand, CancelSearchOrFiltering));
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
            }
        }

        private void CloseApplication(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OpenDialogForCreatingANewEvent(object sender, RoutedEventArgs e)
        {
            CreatingANewEventDialog dialogForCreatingANewEvent = new CreatingANewEventDialog(this);
            dialogForCreatingANewEvent.ShowDialog();
        }

        private void OpenDialogForEditingAnEvent(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Dijalog: Izmeni događaj Ctrl + E");
        }

        private void OpenDialogForDeletingAnEntity(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Dijalog: Obriši entitet Delete");
        }

        private void OpenDialogForAllEventTags(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Dijalog: Sve etikete Ctrl + I");
        }

        private void OpenDialogForAllEventTypes(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Dijalog: Svi tipovi događaja Ctrl + T");
        }

        private void OpenDialogForAllPopulatedPlaces(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Dijalog: Svi gradovi Ctrl + G");
        }

        private void OpenDialogForAllCountries(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Dijalog: Sve države Ctrl + R");
        }

        private void OpenHelpDialog(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Dijalog: Pomoć Alt + P");
        }

        private void FocusOnInputForFilteringTable(object sender, RoutedEventArgs e)
        {
            Input_for_filtering_table.Focus();
        }

        // REFERENCE: https://social.msdn.microsoft.com/Forums/silverlight/en-US/062a2fc8-802d-4390-b2c8-ec73153e1911/column-width-in-percentage-for-datagrid?forum=silverlightcontrols
        private void Table_of_events_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid) sender;
            
            foreach (DataGridColumn dgColumn in dataGrid.Columns)
            {
                if (dgColumn.MinWidth > 0 && dgColumn.ActualWidth > 0)
                {
                    if (dataGrid.MinWidth > 0)
                    {
                        dgColumn.Width = new DataGridLength(dgColumn.MinWidth / dataGrid.MinWidth * dataGrid.ActualWidth);
                    }
                    else
                    {
                        dgColumn.Width = new DataGridLength(dgColumn.MinWidth * (dataGrid.ActualWidth - 14) / 100);
                    }
                }
            }
        }

        private void FilterTable(object sender, TextChangedEventArgs e)
        {
            if (Table_of_events == null)
            {
                return;
            }

            TextBox inputForFilteringTable = (TextBox) sender;
            
            string enteredText = inputForFilteringTable.Text.ToLower();
            if (enteredText == DEFAULT_TEXT_OF_INPUT_FOR_FILTERING_TABLE)
            {
                Cancel_search_or_filtering_button.IsEnabled = false;
                Table_of_events.ItemsSource = Events;
                eventsThatFitSearchCriterions.Clear();
                
                return;
            }
            
            filteredEvents.Clear();

            foreach (Event eve in events)
            {
                if (eve.Id.ToLower() == enteredText)
                {
                    filteredEvents.Add(eve);
                    continue;
                }

                foreach (EventTag eTag in eve.Tags)
                {
                    if (eTag.Id.ToLower() == enteredText)
                    {
                        filteredEvents.Add(eve);
                        continue;
                    }

                    if (eTag.Color.ToLower() == enteredText)
                    {
                        filteredEvents.Add(eve);
                        continue;
                    }

                    if (eTag.Description.ToLower() == enteredText)
                    {
                        filteredEvents.Add(eve);
                        continue;
                    }
                }

                if (eve.Name.ToLower() == enteredText)
                {
                    filteredEvents.Add(eve);
                    continue;
                }

                if (eve.Description.ToLower() == enteredText)
                {
                    filteredEvents.Add(eve);
                    continue;
                }

                if (eve.Type.Id.ToLower() == enteredText)
                {
                    filteredEvents.Add(eve);
                    continue;
                }
                if (eve.Type.Name.ToLower() == enteredText)
                {
                    filteredEvents.Add(eve);
                    continue;
                }
                if (eve.Type.Description.ToLower() == enteredText)
                {
                    filteredEvents.Add(eve);
                    continue;
                }
                if (eve.Type.Icon.ToLower() == enteredText)
                {
                    filteredEvents.Add(eve);
                    continue;
                }

                if (eve.Attendance.ToString().ToLower() == enteredText)
                {
                    filteredEvents.Add(eve);
                    continue;
                }

                if (eve.Icon.ToLower() == enteredText)
                {
                    filteredEvents.Add(eve);
                    continue;
                }

                if (eve.IsHumanitary.ToString().ToLower() == enteredText)
                {
                    filteredEvents.Add(eve);
                    continue;
                }

                if (eve.AverageCostsOfSustension.ToString().ToLower() == enteredText)
                {
                    filteredEvents.Add(eve);
                    continue;
                }

                if (eve.PopulatedPlace.Id.ToLower() == enteredText)
                {
                    filteredEvents.Add(eve);
                    continue;
                }
                if (eve.PopulatedPlace.Name.ToLower() == enteredText)
                {
                    filteredEvents.Add(eve);
                    continue;
                }

                if (eve.Country.Id.ToLower() == enteredText)
                {
                    filteredEvents.Add(eve);
                    continue;
                }
                if (eve.Country.Name.ToLower() == enteredText)
                {
                    filteredEvents.Add(eve);
                    continue;
                }

                foreach (DateTime historicalDOTE in eve.HistoryOfDatesOfTheEvent)
                {
                    if (historicalDOTE.ToString("MM/dd/yyyy").ToLower() == enteredText)
                    {
                        if (!filteredEvents.Contains(eve))
                        {
                            filteredEvents.Add(eve);
                        }
                        continue;
                    }
                }

                if (eve.DateOfTheEvent.ToString("MM/dd/yyyy").ToLower() == enteredText)
                {
                    filteredEvents.Add(eve);
                    continue;
                }
            }

            Table_of_events.ItemsSource = FilteredEvents;
            Cancel_search_or_filtering_button.IsEnabled = true;
        }

        private void CancelSearchOrFiltering(object sender, RoutedEventArgs e)
        {
            if (!Cancel_search_or_filtering_button.IsEnabled)
            {
                return;
            }
            
            Cancel_search_or_filtering_button.IsEnabled = false;
            Table_of_events.ItemsSource = Events;
            eventsThatFitSearchCriterions.Clear();
            filteredEvents.Clear();
            Input_for_filtering_table.Text = DEFAULT_TEXT_OF_INPUT_FOR_FILTERING_TABLE;
        }
    }
}
