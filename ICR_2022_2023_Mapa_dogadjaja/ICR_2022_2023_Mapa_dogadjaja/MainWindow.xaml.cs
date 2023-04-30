using ICR_2022_2023_Mapa_dogadjaja.Model;
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
        private ObservableCollection<Event> events;
        private ObservableCollection<EventTag> eventTags;
        private ObservableCollection<EventType> eventTypes;
        private ObservableCollection<PopulatedPlace> populatedPlaces;
        private ObservableCollection<Country> countries;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            countries = new ObservableCollection<Country>();
            Country country01 = new Country(1, "Srbija");
            Country country02 = new Country(2, "Hrvatska");
            countries.Add(country01);
            countries.Add(country02);

            populatedPlaces = new ObservableCollection<PopulatedPlace>();
            PopulatedPlace populatedPlace01 = new PopulatedPlace(1, "Novi Sad");
            PopulatedPlace populatedPlace02 = new PopulatedPlace(2, "Drvengrad");
            PopulatedPlace populatedPlace03 = new PopulatedPlace(3, "Beograd");
            populatedPlaces.Add(populatedPlace01);
            populatedPlaces.Add(populatedPlace02);
            populatedPlaces.Add(populatedPlace03);

            eventTypes = new ObservableCollection<EventType>();
            EventType eventType01 = new EventType("1", "Muzički festival", "Opis", "Ikona");
            EventType eventType02 = new EventType("2", "Filmski festival", "Opis", "Ikona");
            EventType eventType03 = new EventType("3", "Košarkaška utakmica", "Opis", "Ikona");
            EventType eventType04 = new EventType("4", "Humanitarna aukcija", "Opis", "Ikona");
            eventTypes.Add(eventType01);
            eventTypes.Add(eventType02);
            eventTypes.Add(eventType03);
            eventTypes.Add(eventType04);

            eventTags = new ObservableCollection<EventTag>();
            EventTag eventTag01 = new EventTag("1", "Brown", "Muzika");
            EventTag eventTag02 = new EventTag("2", "Red", "Kratkometražni film");
            EventTag eventTag03 = new EventTag("3", "Orange", "Dugometražni film");
            EventTag eventTag04 = new EventTag("4", "Blue", "Sport");
            EventTag eventTag05 = new EventTag("5", "Black", "Dvoranski sport");
            EventTag eventTag06 = new EventTag("6", "Green", "Humanitaran");
            eventTags.Add(eventTag01);
            eventTags.Add(eventTag02);
            eventTags.Add(eventTag03);
            eventTags.Add(eventTag04);
            eventTags.Add(eventTag05);
            eventTags.Add(eventTag06);

            events = new ObservableCollection<Event>();
            Event event01 = new Event("E001", new List<EventTag>() {eventTag01}, "Exit", 
                "Exit se održava u julu svake godine na Petrovaradinskoj tvrđavi.", eventType01, Attendance.OVER_10000, 
                "Ikona", false, 250000.0, populatedPlace01, country01, new List<DateTime>() {}, new DateTime(2023, 7, 11));
            Event event02 = new Event("E002", new List<EventTag>() {eventTag02, eventTag03}, "Kustendorf", 
                "Kustendorf se održava 24. januara svake godine u etno naselju Drvengrad na planini Mećavnik.", eventType02, 
                Attendance.OVER_10000, "Ikona", false, 200000.0, populatedPlace02, country01, new List<DateTime>() { }, 
                new DateTime(2023, 1, 24));
            Event event03 = new Event("E003", new List<EventTag>() {eventTag04, eventTag05}, "Partizan - Barselona",
                "Utakmica 32. kola ligaškog dela Evrolige u Štark areni.", eventType03, Attendance.OVER_10000, "Ikona", false, 
                110000.0, populatedPlace03, country01, new List<DateTime>() {}, 
                new DateTime(2023, 4, 6));
            Event event04 = new Event("E004", new List<EventTag>() {eventTag06}, "Aukcija sportskih dresova Miloša Nikolića", 
                "Uplaćen novac za kupovinu potpisanih dresova najpoznatijih sportista Evrope iskoristiće se kao donacija " + 
                "Institutu za majku i dete u svrhu nabavke novih inkubatora za bebe.", eventType04, Attendance.BELOW_1000, 
                "Ikona", true, 50.0, populatedPlace03, country01, new List<DateTime>() {}, 
                new DateTime(2023, 5, 30));
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
            MessageBox.Show("Create a new event Ctrl + N");
        }

        private void OpenDialogForEditingAnEvent(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Edit an event Ctrl + E");
        }

        private void OpenDialogForDeletingAnEntity(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Delete an entity Delete");
        }

        private void OpenDialogForAllEventTags(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("All event tags Ctrl + I");
        }

        private void OpenDialogForAllEventTypes(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("All event types Ctrl + T");
        }

        private void OpenDialogForAllPopulatedPlaces(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("All populated places Ctrl + G");
        }

        private void OpenDialogForAllCountries(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("All countries Ctrl + R");
        }

        private void OpenHelpDialog(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Help Alt + P");
        }
    }
}
