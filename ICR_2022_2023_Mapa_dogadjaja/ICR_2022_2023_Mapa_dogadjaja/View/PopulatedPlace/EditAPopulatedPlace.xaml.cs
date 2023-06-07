using ICR_2022_2023_Mapa_dogadjaja.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ICR_2022_2023_Mapa_dogadjaja.View.PopulatedPlace
{
    /// <summary>
    /// Interaction logic for EditAPopulatedPlaceDialog.xaml
    /// </summary>
    public partial class EditAPopulatedPlaceDialog : Window, INotifyPropertyChanged
    {
        private AllEntitiesViewModel allEntitiesViewModel;

        private Model.PopulatedPlace processedPopulatedPlace;

        private int validationErrorsCounter;

        public EditAPopulatedPlaceDialog(AllEntitiesViewModel allEntitiesViewModel, Model.PopulatedPlace selectedPopulatedPlace)
        {
            InitializeComponent();

            DataContext = this;

            this.allEntitiesViewModel = allEntitiesViewModel;
            processedPopulatedPlace = selectedPopulatedPlace;
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

        public Model.PopulatedPlace ProcessedPopulatedPlace
        {
            get { return processedPopulatedPlace; }
            set
            {
                if (value != processedPopulatedPlace)
                {
                    processedPopulatedPlace = value;
                    OnPropertyChanged("ProcessedPopulatedPlace");
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
            if (Processed_populated_place_form_grid == null)
            {
                return;
            }

            if (!Save_button.IsEnabled)
            {
                return;
            }

            allEntitiesViewModel.PopulatedPlacesViewModel.Save();

            DialogResult = true;
        }

        // REFERENCE: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-close-window-dialog-box?source=recommendations&view=netdesktop-7.0
        private void CloseDialog(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
