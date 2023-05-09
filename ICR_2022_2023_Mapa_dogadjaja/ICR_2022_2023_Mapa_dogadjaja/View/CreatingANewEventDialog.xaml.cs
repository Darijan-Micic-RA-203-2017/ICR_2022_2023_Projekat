using ICR_2022_2023_Mapa_dogadjaja.ViewModel;
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
using System.Windows.Shapes;

namespace ICR_2022_2023_Mapa_dogadjaja.View
{
    /// <summary>
    /// Interaction logic for CreatingANewEventDialog.xaml
    /// </summary>
    public partial class CreatingANewEventDialog : Window
    {
        private AllEntitiesViewModel allEntitiesViewModel;

        public CreatingANewEventDialog()
        {
            InitializeComponent();

            allEntitiesViewModel = new AllEntitiesViewModel();
            DataContext = allEntitiesViewModel;
            
            AddHotKeys();
        }
        
        // REFERENCE: https://codesamplez.com/development/wpf-hotkeys-c-sharp
        private void AddHotKeys()
        {
            try
            {
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

        private void SaveEvent(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        // REFERENCE: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-close-window-dialog-box?source=recommendations&view=netdesktop-7.0
        private void CloseDialog(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
