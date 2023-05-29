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
        
        public CreatingANewEventDialog()
        {
            InitializeComponent();
            
            DataContext = this;

            newEvent = new Event();
            currentlySelectedTag = null;
            selectedEventIcon = null;
            allEntitiesViewModel = new AllEntitiesViewModel();
            
            AddHotKeys();
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

        private void SaveEvent(object sender, RoutedEventArgs e)
        {
            if (New_event_form_grid == null)
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

            StringBuilder newEventStringBuilder = new StringBuilder();
            newEventStringBuilder.AppendLine("Oznaka: " + newEvent.Id);
            newEventStringBuilder.AppendLine("Naziv: " + newEvent.Name);
            int i = 0;
            foreach (EventTag t in newEvent.Tags)
            {
                newEventStringBuilder.AppendLine(i + ". etiketa: " + t.Description);
                i++;
            }
            if (newEvent.Type != null)
            {
                newEventStringBuilder.AppendLine("Tip: " + newEvent.Type.Name);
            }
            newEventStringBuilder.AppendLine("Opis: " + newEvent.Description);
            newEventStringBuilder.AppendLine("Ikona: " + newEvent.Icon);
            newEventStringBuilder.AppendLine("Humanitaran: " + newEvent.IsHumanitary);
            newEventStringBuilder.AppendLine("Posecenost: " + newEvent.Attendance);
            newEventStringBuilder.AppendLine("Troskovi: " + newEvent.AverageCostsOfSustension);
            if (newEvent.PopulatedPlace != null)
            {
                newEventStringBuilder.AppendLine("Grad: " + newEvent.PopulatedPlace.Name);
            }
            if (newEvent.Country != null)
            {
                newEventStringBuilder.AppendLine("Drzava: " + newEvent.Country.Name);
            }
            newEventStringBuilder.AppendLine("Istorija datuma: " + newEvent.HistoryOfDatesOfTheEvent);
            newEventStringBuilder.AppendLine("Datum: " + newEvent.DateOfTheEvent);
            MessageBox.Show(newEventStringBuilder.ToString());

            allEntitiesViewModel.EventsViewModel.Events.Add(newEvent);
            MessageBox.Show("Broj dogadjaja: " + allEntitiesViewModel.EventsViewModel.Events.Count);

            //DialogResult = true;
        }

        // REFERENCE: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-close-window-dialog-box?source=recommendations&view=netdesktop-7.0
        private void CloseDialog(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
