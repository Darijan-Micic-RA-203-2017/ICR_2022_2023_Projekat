using ICR_2022_2023_Mapa_dogadjaja.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ICR_2022_2023_Mapa_dogadjaja.View.EventTag
{
    /// <summary>
    /// Interaction logic for EditAnEventTagDialog.xaml
    /// </summary>
    public partial class EditAnEventTagDialog : Window, INotifyPropertyChanged
    {
        private AllEntitiesViewModel allEntitiesViewModel;

        private Model.EventTag processedEventTag;

        private Color selectedEventTagColor;

        private int validationErrorsCounter;

        public EditAnEventTagDialog(AllEntitiesViewModel allEntitiesViewModel, Model.EventTag selectedEventTag)
        {
            InitializeComponent();

            DataContext = this;

            this.allEntitiesViewModel = allEntitiesViewModel;
            processedEventTag = selectedEventTag;
            PrepareViewOfSelectedEventTag();
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

        public Model.EventTag ProcessedEventTag
        {
            get { return processedEventTag; }
            set
            {
                if (value != processedEventTag)
                {
                    processedEventTag = value;
                    OnPropertyChanged("ProcessedEventTag");
                }
            }
        }

        public Color SelectedEventTagColor
        {
            get { return selectedEventTagColor; }
            set
            {
                if (value != selectedEventTagColor)
                {
                    selectedEventTagColor = value;
                    OnPropertyChanged("SelectedEventTagColor");
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

        private void PrepareViewOfSelectedEventTag()
        {
            string selectedAlpha = processedEventTag.Color.Substring(1, 2);
            byte a = byte.Parse(selectedAlpha, System.Globalization.NumberStyles.HexNumber);
            string selectedRed = processedEventTag.Color.Substring(3, 2);
            byte r = byte.Parse(selectedRed, System.Globalization.NumberStyles.HexNumber);
            string selectedGreen = processedEventTag.Color.Substring(5, 2);
            byte g = byte.Parse(selectedGreen, System.Globalization.NumberStyles.HexNumber);
            string selectedBlue = processedEventTag.Color.Substring(7, 2);
            byte b = byte.Parse(selectedBlue, System.Globalization.NumberStyles.HexNumber);

            SelectedEventTagColor = Color.FromArgb(a, r, g, b);
        }

        // REFERENCE: https://codesamplez.com/development/wpf-hotkeys-c-sharp
        private void AddHotKeys()
        {
            try
            {
                RoutedCommand openColorPickerCommand = new RoutedCommand();
                openColorPickerCommand.InputGestures.Add(new KeyGesture(Key.B, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(openColorPickerCommand, OpenColorPicker));

                RoutedCommand saveEventTagCommand = new RoutedCommand();
                saveEventTagCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(saveEventTagCommand, SaveEventTag));

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
        private void OpenColorPicker(object sender, RoutedEventArgs e)
        {
            if (Color_picker == null)
            {
                return;
            }

            Color_picker.Focus();
        }

        private void SaveEventTag(object sender, RoutedEventArgs e)
        {
            if (Processed_event_tag_form_grid == null)
            {
                return;
            }

            if (!Save_button.IsEnabled)
            {
                return;
            }

            processedEventTag.Color = Color_picker.Color.ToString();

            allEntitiesViewModel.EventTagsViewModel.Save();

            DialogResult = true;
        }

        // REFERENCE: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-close-window-dialog-box?source=recommendations&view=netdesktop-7.0
        private void CloseDialog(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
