using ICR_2022_2023_Mapa_dogadjaja.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ICR_2022_2023_Mapa_dogadjaja.View.Event
{
    /// <summary>
    /// Interaction logic for AllEventsDialog.xaml
    /// </summary>
    public partial class AllEventsDialog : Window, INotifyPropertyChanged
    {
        private const string DEFAULT_TEXT_OF_INPUT_FOR_FILTERING_TABLE = "Filtriraj tabelu (Alt + 2)";

        private AllEntitiesViewModel allEntitiesViewModel;

        private ObservableCollection<Model.Event> eventsThatFitSearchCriterions = new ObservableCollection<Model.Event>();
        private ObservableCollection<Model.Event> filteredEvents = new ObservableCollection<Model.Event>();

        public AllEventsDialog(AllEntitiesViewModel allEntitiesViewModel)
        {
            InitializeComponent();

            DataContext = this;

            this.allEntitiesViewModel = allEntitiesViewModel;

            AddHotKeys();
        }

        public AllEntitiesViewModel AllEntitiesViewModel
        {
            get { return allEntitiesViewModel; }
            set
            {
                if (value != allEntitiesViewModel)
                {
                    allEntitiesViewModel = value;
                    OnPropertyChanged("AllEntitiesViewModel");
                }
            }
        }

        public ObservableCollection<Model.Event> EventsThatFitSearchCriterions
        {
            get { return eventsThatFitSearchCriterions; }
            set
            {
                if (!eventsThatFitSearchCriterions.SequenceEqual(value))
                {
                    eventsThatFitSearchCriterions = value;
                    OnPropertyChanged("EventsThatFitSearchCriterions");
                }
            }
        }

        public ObservableCollection<Model.Event> FilteredEvents
        {
            get { return filteredEvents; }
            set
            {
                if (!filteredEvents.SequenceEqual(value))
                {
                    filteredEvents = value;
                    OnPropertyChanged("FilteredEvents");
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

        // REFERENCE: https://codesamplez.com/development/wpf-hotkeys-c-sharp
        private void AddHotKeys()
        {
            try
            {
                RoutedCommand openDialogForCreatingANewEventCommand = new RoutedCommand();
                openDialogForCreatingANewEventCommand.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(openDialogForCreatingANewEventCommand, OpenDialogForCreatingANewEvent));

                RoutedCommand openDialogForEditingAnEventCommand = new RoutedCommand();
                openDialogForEditingAnEventCommand.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(openDialogForEditingAnEventCommand, OpenDialogForEditingAnEvent));

                RoutedCommand openDialogForDeletingAnEntityCommand = new RoutedCommand();
                openDialogForDeletingAnEntityCommand.InputGestures.Add(new KeyGesture(Key.Delete, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(openDialogForDeletingAnEntityCommand, OpenDialogForDeletingAnEntity));
                
                RoutedCommand openDialogForSearchingEventsCommand = new RoutedCommand();
                openDialogForSearchingEventsCommand.InputGestures.Add(new KeyGesture(Key.D1, ModifierKeys.Alt));
                CommandBindings.Add(new CommandBinding(openDialogForSearchingEventsCommand, OpenDialogForSearchingEvents));

                RoutedCommand focusOnInputForFilteringTableCommand = new RoutedCommand();
                focusOnInputForFilteringTableCommand.InputGestures.Add(new KeyGesture(Key.D2, ModifierKeys.Alt));
                CommandBindings.Add(new CommandBinding(focusOnInputForFilteringTableCommand, FocusOnInputForFilteringTable));

                RoutedCommand cancelSearchOrFilteringCommand = new RoutedCommand();
                cancelSearchOrFilteringCommand.InputGestures.Add(new KeyGesture(Key.D4, ModifierKeys.Alt));
                CommandBindings.Add(new CommandBinding(cancelSearchOrFilteringCommand, CancelSearchOrFiltering));

                RoutedCommand closeDialogCommand = new RoutedCommand();
                closeDialogCommand.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(closeDialogCommand, CloseDialog));
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
            }
        }

        private void OpenDialogForCreatingANewEvent(object sender, RoutedEventArgs e)
        {
            CreateANewEventDialog dialogForCreatingANewEvent = new CreateANewEventDialog(allEntitiesViewModel);
            dialogForCreatingANewEvent.ShowDialog();
        }

        private void OpenDialogForEditingAnEvent(object sender, RoutedEventArgs e)
        {
            Model.Event selectedEvent = (Model.Event) Table_of_events.SelectedItem;
            if (selectedEvent == null)
            {
                return;
            }

            EditAnEventDialog dialogForEditingAnEvent = new EditAnEventDialog(allEntitiesViewModel, selectedEvent);
            dialogForEditingAnEvent.ShowDialog();
        }

        private void OpenDialogForDeletingAnEntity(object sender, RoutedEventArgs e)
        {
            Model.Event selectedEvent = (Model.Event) Table_of_events.SelectedItem;
            if (selectedEvent == null)
            {
                return;
            }

            DeleteAnEntityDialog dialogForDeletingAnEntity = new DeleteAnEntityDialog(allEntitiesViewModel, selectedEvent);
            dialogForDeletingAnEntity.ShowDialog();
        }

        private void OpenDialogForSearchingEvents(object sender, RoutedEventArgs e)
        {
            SearchEventsDialog dialogForSearchingEvents = new SearchEventsDialog(allEntitiesViewModel);
            bool? dialogResult = dialogForSearchingEvents.ShowDialog();
            if (dialogResult == true)
            {
                SearchEvents();
            }
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

        private void SearchEvents()
        {
            if (Table_of_events == null)
            {
                return;
            }

            ObservableCollection<Model.Event> searchResults = EventsTableSearcher.SearchEventsInTable(allEntitiesViewModel);
            if (searchResults != null)
            {
                eventsThatFitSearchCriterions = searchResults;

                Table_of_events.ItemsSource = EventsThatFitSearchCriterions;
                Cancel_search_or_filtering_button.IsEnabled = true;
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

            filteredEvents = EventsTableFilter.FilterTableOfEvents(allEntitiesViewModel.EventsViewModel.Events, enteredText);

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

        // REFERENCE: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-close-window-dialog-box?source=recommendations&view=netdesktop-7.0
        private void CloseDialog(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
