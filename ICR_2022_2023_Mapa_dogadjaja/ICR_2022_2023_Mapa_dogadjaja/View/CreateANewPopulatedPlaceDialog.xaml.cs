using ICR_2022_2023_Mapa_dogadjaja.Model;
using ICR_2022_2023_Mapa_dogadjaja.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for CreateANewPopulatedPlaceDialog.xaml
    /// </summary>
    public partial class CreateANewPopulatedPlaceDialog : Window, INotifyPropertyChanged
    {
        private PopulatedPlace newPopulatedPlace;

        private AllEntitiesViewModel allEntitiesViewModel;

        private int validationErrorsCounter;

        public CreateANewPopulatedPlaceDialog(AllEntitiesViewModel allEntitiesViewModel)
        {
            InitializeComponent();

            DataContext = this;

            newPopulatedPlace = new PopulatedPlace();
            this.allEntitiesViewModel = allEntitiesViewModel;
            validationErrorsCounter = 0;

            AddHotKeys();

            // REFERENCE: https://stackoverflow.com/a/808190
            AddHandler(System.Windows.Controls.Validation.ErrorEvent, new RoutedEventHandler(OnErrorEvent));
        }

        public PopulatedPlace NewPopulatedPlace
        {
            get { return newPopulatedPlace; }
            set
            {
                if (value != newPopulatedPlace)
                {
                    newPopulatedPlace = value;
                    OnPropertyChanged("NewPopulatedPlace");
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
                RoutedCommand savePopulatedPlaceCommand = new RoutedCommand();
                savePopulatedPlaceCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(savePopulatedPlaceCommand, SavePopulatedPlace));

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

        private void SavePopulatedPlace(object sender, RoutedEventArgs e)
        {
            if (New_populated_place_form_grid == null)
            {
                return;
            }

            if (!Save_button.IsEnabled)
            {
                return;
            }

            allEntitiesViewModel.PopulatedPlacesViewModel.Save(newPopulatedPlace);

            DialogResult = true;
        }

        // REFERENCE: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-close-window-dialog-box?source=recommendations&view=netdesktop-7.0
        private void CloseDialog(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
