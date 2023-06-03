using ICR_2022_2023_Mapa_dogadjaja.Model;
using ICR_2022_2023_Mapa_dogadjaja.View;
using ICR_2022_2023_Mapa_dogadjaja.ViewModel;
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

        private AllEntitiesViewModel allEntitiesViewModel;

        private ObservableCollection<Event> eventsThatFitSearchCriterions = new ObservableCollection<Event>();
        private ObservableCollection<Event> filteredEvents = new ObservableCollection<Event>();
        
        public MainWindow()
        {
            InitializeComponent();

            allEntitiesViewModel = new AllEntitiesViewModel();
            DataContext = allEntitiesViewModel;
            
            AddHotKeys();
        }
        
        public ObservableCollection<Event> EventsThatFitSearchCriterions
        {
            get { return eventsThatFitSearchCriterions; }
            set { eventsThatFitSearchCriterions = value; }
        }
        
        public ObservableCollection<Event> FilteredEvents
        {
            get { return filteredEvents; }
            set { filteredEvents = value; }
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
            CreatingANewEventDialog dialogForCreatingANewEvent = new CreatingANewEventDialog(allEntitiesViewModel);
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
            string enteredText = inputForFilteringTable.Text;
            if (enteredText == DEFAULT_TEXT_OF_INPUT_FOR_FILTERING_TABLE)
            {
                Cancel_search_or_filtering_button.IsEnabled = false;
                Table_of_events.ItemsSource = allEntitiesViewModel.EventsViewModel.Events;
                eventsThatFitSearchCriterions.Clear();
                
                return;
            }
            
            filteredEvents.Clear();
            
            foreach (Event eve in allEntitiesViewModel.EventsViewModel.Events)
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
            Table_of_events.ItemsSource = allEntitiesViewModel.EventsViewModel.Events;
            eventsThatFitSearchCriterions.Clear();
            filteredEvents.Clear();
            Input_for_filtering_table.Text = DEFAULT_TEXT_OF_INPUT_FOR_FILTERING_TABLE;
        }
    }
}
