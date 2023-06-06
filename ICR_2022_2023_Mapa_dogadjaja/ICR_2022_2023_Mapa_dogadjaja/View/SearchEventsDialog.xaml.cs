using ICR_2022_2023_Mapa_dogadjaja.Model;
using ICR_2022_2023_Mapa_dogadjaja.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ICR_2022_2023_Mapa_dogadjaja.View
{
    /// <summary>
    /// Interaction logic for SearchEventsDialog.xaml
    /// </summary>
    public partial class SearchEventsDialog : Window, INotifyPropertyChanged
    {
        private Event searchModelEvent;

        private EventTag currentlySelectedTag;

        private BitmapImage selectedEventIcon;

        private AllEntitiesViewModel allEntitiesViewModel;

        private int validationErrorsCounter;

        public SearchEventsDialog(AllEntitiesViewModel allEntitiesViewModel)
        {
            InitializeComponent();

            DataContext = this;

            searchModelEvent = new Event();
            currentlySelectedTag = null;
            selectedEventIcon = null;
            this.allEntitiesViewModel = allEntitiesViewModel;
            validationErrorsCounter = 0;
            
            AddHotKeys();

            // REFERENCE: https://stackoverflow.com/a/808190
            AddHandler(System.Windows.Controls.Validation.ErrorEvent, new RoutedEventHandler(OnErrorEvent));
        }

        public Event SearchModelEvent
        {
            get { return searchModelEvent; }
            set
            {
                if (value != searchModelEvent)
                {
                    searchModelEvent = value;
                    OnPropertyChanged("SearchModelEvent");
                }
            }
        }

        public EventTag CurrentlySelectedTag
        {
            get { return currentlySelectedTag; }
            set
            {
                if (value != currentlySelectedTag)
                {
                    currentlySelectedTag = value;
                    OnPropertyChanged("CurrentlySelectedTag");
                }
            }
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
                RoutedCommand openDialogForSelectingEventIconCommand = new RoutedCommand();
                openDialogForSelectingEventIconCommand.InputGestures.Add(new KeyGesture(Key.I, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(openDialogForSelectingEventIconCommand, OpenDialogForSelectingEventIcon));
                
                RoutedCommand saveEventCommand = new RoutedCommand();
                saveEventCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(saveEventCommand, SearchEvents));

                RoutedCommand closeDialogCommand = new RoutedCommand();
                closeDialogCommand.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(closeDialogCommand, CloseDialog));
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
            }
        }

        // REFERENCE: https://stackoverflow.com/a/808190
        private void OnErrorEvent(object sender, RoutedEventArgs e)
        {
            var validationEventArgs = e as ValidationErrorEventArgs;
            if (validationEventArgs == null)
            {
                throw new Exception("Unexpected event args!");
            }

            switch (validationEventArgs.Action)
            {
                case ValidationErrorEventAction.Added:
                    {
                        validationErrorsCounter++;
                        break;
                    }
                case ValidationErrorEventAction.Removed:
                    {
                        validationErrorsCounter--;
                        break;
                    }
                default:
                    {
                        throw new Exception("Unknown action!");
                    }
            }

            if (validationErrorsCounter == 0)
            {
                Search_button.IsEnabled = true;
            }
            else
            {
                Search_button.IsEnabled = false;
            }
        }

        // REFERENCE: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-open-common-system-dialog-box?view=netdesktop-7.0
        private void OpenDialogForSelectingEventIcon(object sender, RoutedEventArgs e)
        {
            if (Event_icon == null)
            {
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.gif, *.jpg, *.jpe, *.png, *.bmp, *.dib, *.tif, *.wmf, *.ras, *.eps, " +
                "*.pcx, *.pcd, *.tga, *.dds)|*.gif;*.jpg;*.jpe;*.png;*.bmp;*.dib;*.tif;*.wmf;*.ras;*.eps;*.pcx;*.pcd;*.tga;" +
                "*.dds|All files (*.*)|*.*";
            openFileDialog.Title = "Odaberite ikonu događaja";
            openFileDialog.AddExtension = true;
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.DereferenceLinks = true;
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;
            openFileDialog.ShowReadOnly = false;
            openFileDialog.ValidateNames = true;

            bool? dialogResult = openFileDialog.ShowDialog();
            if (dialogResult != true)
            {
                return;
            }

            if (!File.Exists(openFileDialog.FileName))
            {
                return;
            }

            // REFERENCE: https://stackoverflow.com/questions/6503424/how-to-programmatically-set-the-image-source?noredirect=1&lq=1
            selectedEventIcon = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
            Event_icon.Source = selectedEventIcon;
        }
        
        private void CreateNewHistoricalDateOfTheEventInput(object sender, RoutedEventArgs e)
        {
            if (Panel_for_inputs_for_historical_dates_of_the_event == null)
            {
                return;
            }

            DatePicker newHistoricalDateOfTheEventPicker = new DatePicker();
            newHistoricalDateOfTheEventPicker.VerticalContentAlignment = VerticalAlignment.Center;
            newHistoricalDateOfTheEventPicker.Margin = new Thickness(0, 0, 0, 10);

            Panel_for_inputs_for_historical_dates_of_the_event.Children.Add(newHistoricalDateOfTheEventPicker);
        }

        private void SearchEvents(object sender, RoutedEventArgs e)
        {
            if (Search_model_event_form_grid == null)
            {
                return;
            }

            if (!Search_button.IsEnabled)
            {
                return;
            }
            
            foreach (EventTag selectedTag in List_box_for_event_tags.SelectedItems)
            {
                searchModelEvent.Tags.Add(selectedTag);
            }

            if (Event_icon.Source != null)
            {
                searchModelEvent.Icon = Event_icon.Source.ToString();
            }
            else
            {
                if (searchModelEvent.Type != null)
                {
                    searchModelEvent.Icon = searchModelEvent.Type.Icon;
                }
            }

            if (IsHumanitary_option_Yes_radio_button.IsChecked == true)
            {
                searchModelEvent.IsHumanitary = true;
            }
            else if (IsHumanitary_option_No_radio_button.IsChecked == true)
            {
                searchModelEvent.IsHumanitary = false;
            }

            searchModelEvent.Attendance = (Attendance?) Combo_box_for_attendance.SelectedItem;

            foreach (UIElement childElem in Panel_for_inputs_for_historical_dates_of_the_event.Children)
            {
                DatePicker datePicker = (DatePicker) childElem;
                if (datePicker.SelectedDate.HasValue)
                {
                    searchModelEvent.HistoryOfDatesOfTheEvent.Add(datePicker.SelectedDate.Value);
                }
            }

            allEntitiesViewModel.SearchModelEvent = searchModelEvent;

            DialogResult = true;
        }

        // REFERENCE: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-close-window-dialog-box?source=recommendations&view=netdesktop-7.0
        private void CloseDialog(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
