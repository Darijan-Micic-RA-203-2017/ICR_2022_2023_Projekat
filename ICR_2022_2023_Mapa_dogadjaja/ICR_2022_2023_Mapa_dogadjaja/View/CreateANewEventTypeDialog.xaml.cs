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
    /// Interaction logic for CreateANewEventTypeDialog.xaml
    /// </summary>
    public partial class CreateANewEventTypeDialog : Window, INotifyPropertyChanged
    {
        private EventType newEventType;

        private BitmapImage selectedEventTypeIcon;

        private AllEntitiesViewModel allEntitiesViewModel;

        private int validationErrorsCounter;

        public CreateANewEventTypeDialog(AllEntitiesViewModel allEntitiesViewModel)
        {
            InitializeComponent();

            DataContext = this;

            newEventType = new EventType();
            selectedEventTypeIcon = null;
            this.allEntitiesViewModel = allEntitiesViewModel;
            validationErrorsCounter = 0;

            AddHotKeys();

            // REFERENCE: https://stackoverflow.com/a/808190
            AddHandler(System.Windows.Controls.Validation.ErrorEvent, new RoutedEventHandler(OnErrorEvent));
        }

        public EventType NewEventType
        {
            get { return newEventType; }
            set
            {
                if (value != newEventType)
                {
                    newEventType = value;
                    OnPropertyChanged("NewEventType");
                }
            }
        }

        public BitmapImage SelectedEventTypeIcon
        {
            get { return selectedEventTypeIcon; }
            set
            {
                if (value != selectedEventTypeIcon)
                {
                    selectedEventTypeIcon = value;
                    OnPropertyChanged("SelectedEventTypeIcon");
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
                RoutedCommand openDialogForSelectingEventTypeIconCommand = new RoutedCommand();
                openDialogForSelectingEventTypeIconCommand.InputGestures.Add(new KeyGesture(Key.I, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(openDialogForSelectingEventTypeIconCommand, 
                    OpenDialogForSelectingEventTypeIcon));

                RoutedCommand saveEventTypeCommand = new RoutedCommand();
                saveEventTypeCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(saveEventTypeCommand, SaveEventType));

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

        // REFERENCE: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-open-common-system-dialog-box?view=netdesktop-7.0
        private void OpenDialogForSelectingEventTypeIcon(object sender, RoutedEventArgs e)
        {
            if (Event_type_icon == null)
            {
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.gif, *.jpg, *.jpe, *.png, *.bmp, *.dib, *.tif, *.wmf, *.ras, *.eps, " +
                "*.pcx, *.pcd, *.tga, *.dds)|*.gif;*.jpg;*.jpe;*.png;*.bmp;*.dib;*.tif;*.wmf;*.ras;*.eps;*.pcx;*.pcd;*.tga;" +
                "*.dds|All files (*.*)|*.*";
            openFileDialog.Title = "Odaberite ikonu tipa događaja";
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
            selectedEventTypeIcon = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
            Event_type_icon.Source = selectedEventTypeIcon;
        }

        private void SaveEventType(object sender, RoutedEventArgs e)
        {
            if (New_event_type_form_grid == null)
            {
                return;
            }

            if (!Save_button.IsEnabled)
            {
                return;
            }
            
            newEventType.Icon = Event_type_icon.Source.ToString();
            
            allEntitiesViewModel.EventTypesViewModel.Save(newEventType);

            DialogResult = true;
        }

        // REFERENCE: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-close-window-dialog-box?source=recommendations&view=netdesktop-7.0
        private void CloseDialog(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
