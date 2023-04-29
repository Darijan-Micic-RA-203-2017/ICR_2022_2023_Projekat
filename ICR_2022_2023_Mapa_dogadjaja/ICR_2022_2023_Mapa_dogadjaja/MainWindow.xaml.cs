using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            InitializeComponent();

            AddHotKeys();
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
            MessageBox.Show("Create a new event Ctrl + N");
        }

        private void OpenDialogForEditingAnEvent(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Edit an event Ctrl + E");
        }

        private void OpenDialogForDeletingAnEntity(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Delete an entity Delete");
        }

        private void OpenDialogForAllEventTags(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("All event tags Ctrl + I");
        }

        private void OpenDialogForAllEventTypes(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("All event types Ctrl + T");
        }

        private void OpenDialogForAllPopulatedPlaces(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("All populated places Ctrl + G");
        }

        private void OpenDialogForAllCountries(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("All countries Ctrl + R");
        }

        private void OpenHelpDialog(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Help Alt + P");
        }
    }
}
