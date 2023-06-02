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
    /// Interaction logic for CreatingANewEventDialog.xaml
    /// </summary>
    public partial class CreatingANewEventDialog : Window, INotifyPropertyChanged
    {
        private Event newEvent;

        private EventTag currentlySelectedTag;
        
        private BitmapImage selectedEventIcon;

        private AllEntitiesViewModel allEntitiesViewModel;

        private int validationErrorsCounter;
        
        public CreatingANewEventDialog(AllEntitiesViewModel allEntitiesViewModel)
        {
            InitializeComponent();
            
            DataContext = this;

            newEvent = new Event();
            currentlySelectedTag = null;
            selectedEventIcon = null;
            this.allEntitiesViewModel = allEntitiesViewModel;
            validationErrorsCounter = 0;

            AddHotKeys();

            // REFERENCE: https://stackoverflow.com/a/808190
            AddHandler(System.Windows.Controls.Validation.ErrorEvent, new RoutedEventHandler(OnErrorEvent));
        }

        public Event NewEvent
        {
            get { return newEvent; }
            set
            {
                if (value != newEvent)
                {
                    newEvent = value;
                    OnPropertyChanged("NewEvent");
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
                CommandBindings.Add(new CommandBinding(saveEventCommand, SaveEvent));

                RoutedCommand closeDialogCommand = new RoutedCommand();
                saveEventCommand.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(saveEventCommand, CloseDialog));
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

        private void SaveEvent(object sender, RoutedEventArgs e)
        {
            if (New_event_form_grid == null)
            {
                return;
            }
            
            if (!Save_button.IsEnabled)
            {
                return;
            }
            
            foreach (EventTag selectedTag in List_box_for_event_tags.SelectedItems)
            {
                newEvent.Tags.Add(selectedTag);
            }
            
            if (Event_icon.Source != null)
            {
                newEvent.Icon = Event_icon.Source.ToString();
            }
            else
            {
                newEvent.Icon = newEvent.Type.Icon;
            }

            if (IsHumanitary_option_Yes_radio_button.IsChecked == true)
            {
                newEvent.IsHumanitary = true;
            }
            else
            {
                newEvent.IsHumanitary = false;
            }

            newEvent.Attendance = (Attendance) Combo_box_for_attendance.SelectedItem;

            foreach (UIElement childElem in Panel_for_inputs_for_historical_dates_of_the_event.Children)
            {
                DatePicker datePicker = (DatePicker) childElem;
                if (datePicker.SelectedDate.HasValue)
                {
                    newEvent.HistoryOfDatesOfTheEvent.Add(datePicker.SelectedDate.Value);
                }
            }
            
            allEntitiesViewModel.EventsViewModel.Save(newEvent);
            
            DialogResult = true;
        }
        
        // REFERENCE: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-close-window-dialog-box?source=recommendations&view=netdesktop-7.0
        private void CloseDialog(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
