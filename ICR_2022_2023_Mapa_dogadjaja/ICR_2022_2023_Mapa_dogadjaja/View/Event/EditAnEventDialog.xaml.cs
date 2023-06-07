using ICR_2022_2023_Mapa_dogadjaja.Model;
using ICR_2022_2023_Mapa_dogadjaja.View.Country;
using ICR_2022_2023_Mapa_dogadjaja.View.EventTag;
using ICR_2022_2023_Mapa_dogadjaja.View.EventType;
using ICR_2022_2023_Mapa_dogadjaja.View.PopulatedPlace;
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
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ICR_2022_2023_Mapa_dogadjaja.View.Event
{
    /// <summary>
    /// Interaction logic for EditAnEventDialog.xaml
    /// </summary>
    public partial class EditAnEventDialog : Window, INotifyPropertyChanged
    {
        private AllEntitiesViewModel allEntitiesViewModel;
        
        private Model.Event processedEvent;

        private Model.EventTag currentlySelectedTag;

        private BitmapImage selectedEventIcon;

        private int validationErrorsCounter;

        public EditAnEventDialog(AllEntitiesViewModel allEntitiesViewModel, Model.Event selectedEvent)
        {
            InitializeComponent();

            DataContext = this;

            this.allEntitiesViewModel = allEntitiesViewModel;
            processedEvent = selectedEvent;
            PrepareViewOfSelectedEvent();
            validationErrorsCounter = 0;

            AddHotKeys();

            // REFERENCE: https://stackoverflow.com/a/808190
            AddHandler(System.Windows.Controls.Validation.ErrorEvent, new RoutedEventHandler(OnErrorEvent));
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
        
        public Model.Event ProcessedEvent
        {
            get { return processedEvent; }
            set
            {
                if (value != processedEvent)
                {
                    processedEvent = value;
                    OnPropertyChanged("ProcessedEvent");
                }
            }
        }

        public Model.EventTag CurrentlySelectedTag
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

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void PrepareViewOfSelectedEvent()
        {
            for (int i = processedEvent.Tags.Count - 1; i >= 0; i--)
            {
                currentlySelectedTag = processedEvent.Tags[i];
            }
            
            selectedEventIcon = new BitmapImage(new Uri(processedEvent.Icon, UriKind.Absolute));
            Event_icon.Source = selectedEventIcon;

            if (processedEvent.IsHumanitary == true)
            {
                IsHumanitary_option_Yes_radio_button.IsChecked = true;
            }
            else
            {
                IsHumanitary_option_No_radio_button.IsChecked = true;
            }

            Combo_box_for_attendance.SelectedItem = processedEvent.Attendance.Value;

            if (processedEvent.HistoryOfDatesOfTheEvent.Count > 0)
            {
                Panel_for_inputs_for_historical_dates_of_the_event.Children.Clear();
            }
            foreach (DateTime historicalDate in processedEvent.HistoryOfDatesOfTheEvent)
            {
                CreateNewHistoricalDateOfTheEventInput(null, null);
                DatePicker lastDatePicker = (DatePicker)Panel_for_inputs_for_historical_dates_of_the_event
                    .Children[Panel_for_inputs_for_historical_dates_of_the_event.Children.Count - 1];
                lastDatePicker.SelectedDate = historicalDate;
            }
        }

        // REFERENCE: https://codesamplez.com/development/wpf-hotkeys-c-sharp
        private void AddHotKeys()
        {
            try
            {
                RoutedCommand openDialogForCreatingANewEventTagCommand = new RoutedCommand();
                openDialogForCreatingANewEventTagCommand.InputGestures.Add(new KeyGesture(Key.Z, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(openDialogForCreatingANewEventTagCommand, OpenDialogForCreatingANewEventTag));

                RoutedCommand openDialogForCreatingANewEventTypeCommand = new RoutedCommand();
                openDialogForCreatingANewEventTypeCommand.InputGestures.Add(new KeyGesture(Key.T, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(openDialogForCreatingANewEventTypeCommand,
                    OpenDialogForCreatingANewEventType));

                RoutedCommand openDialogForSelectingEventIconCommand = new RoutedCommand();
                openDialogForSelectingEventIconCommand.InputGestures.Add(new KeyGesture(Key.I, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(openDialogForSelectingEventIconCommand, OpenDialogForSelectingEventIcon));

                RoutedCommand openDialogForCreatingANewPopulatedPlaceCommand = new RoutedCommand();
                openDialogForCreatingANewPopulatedPlaceCommand.InputGestures.Add(new KeyGesture(Key.G, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(openDialogForCreatingANewPopulatedPlaceCommand,
                    OpenDialogForCreatingANewPopulatedPlace));

                RoutedCommand openDialogForCreatingANewCountryCommand = new RoutedCommand();
                openDialogForCreatingANewCountryCommand.InputGestures.Add(new KeyGesture(Key.R, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(openDialogForCreatingANewCountryCommand, OpenDialogForCreatingANewCountry));

                RoutedCommand saveEventCommand = new RoutedCommand();
                saveEventCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(saveEventCommand, SaveEvent));

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
                Save_button.IsEnabled = true;
            }
            else
            {
                Save_button.IsEnabled = false;
            }
        }

        private void OpenDialogForCreatingANewEventTag(object sender, RoutedEventArgs e)
        {
            CreateANewEventTagDialog dialogForCreatingANewEventTag = new CreateANewEventTagDialog(allEntitiesViewModel);
            dialogForCreatingANewEventTag.ShowDialog();
        }

        private void OpenDialogForCreatingANewEventType(object sender, RoutedEventArgs e)
        {
            CreateANewEventTypeDialog dialogForCreatingANewEventType = new CreateANewEventTypeDialog(allEntitiesViewModel);
            dialogForCreatingANewEventType.ShowDialog();
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

        private void OpenDialogForCreatingANewPopulatedPlace(object sender, RoutedEventArgs e)
        {
            CreateANewPopulatedPlaceDialog dialogForCreatingANewPopulatedPlace =
                new CreateANewPopulatedPlaceDialog(allEntitiesViewModel);
            dialogForCreatingANewPopulatedPlace.ShowDialog();
        }

        private void OpenDialogForCreatingANewCountry(object sender, RoutedEventArgs e)
        {
            CreateANewCountryDialog dialogForCreatingANewCountry = new CreateANewCountryDialog(allEntitiesViewModel);
            dialogForCreatingANewCountry.ShowDialog();
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

        private void SaveEvent(object sender, RoutedEventArgs e)
        {
            if (Processed_event_form_grid == null)
            {
                return;
            }

            if (!Save_button.IsEnabled)
            {
                return;
            }

            List<Model.EventTag> selectedTags = new List<Model.EventTag>();
            foreach (Model.EventTag selTag in List_box_for_event_tags.SelectedItems)
            {
                selectedTags.Add(selTag);
            }
            if (!processedEvent.Tags.SequenceEqual(selectedTags))
            {
                processedEvent.Tags.Clear();
                foreach (Model.EventTag selectedTag in List_box_for_event_tags.SelectedItems)
                {
                    processedEvent.Tags.Add(selectedTag);
                }
            }

            if (Event_icon.Source != null)
            {
                processedEvent.Icon = Event_icon.Source.ToString();
            }
            else
            {
                processedEvent.Icon = processedEvent.Type.Icon;
            }

            if (IsHumanitary_option_Yes_radio_button.IsChecked == true)
            {
                processedEvent.IsHumanitary = true;
            }
            else if (IsHumanitary_option_No_radio_button.IsChecked == true)
            {
                processedEvent.IsHumanitary = false;
            }

            processedEvent.Attendance = (Attendance?) Combo_box_for_attendance.SelectedItem;

            List<DateTime> historicalDates = new List<DateTime>();
            foreach (UIElement childElem in Panel_for_inputs_for_historical_dates_of_the_event.Children)
            {
                DatePicker datePicker = (DatePicker) childElem;
                if (datePicker.SelectedDate.HasValue)
                {
                    historicalDates.Add(datePicker.SelectedDate.Value);
                    processedEvent.HistoryOfDatesOfTheEvent.Add(datePicker.SelectedDate.Value);
                }
            }
            if (!processedEvent.HistoryOfDatesOfTheEvent.SequenceEqual(historicalDates))
            {
                processedEvent.HistoryOfDatesOfTheEvent = historicalDates;
            }

            allEntitiesViewModel.EventsViewModel.Save();
            
            DialogResult = true;
        }

        // REFERENCE: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-close-window-dialog-box?source=recommendations&view=netdesktop-7.0
        private void CloseDialog(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
