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
        private MainWindow mainVindow;

        public CreatingANewEventDialog(MainWindow mainVindow)
        {
            InitializeComponent();

            this.mainVindow = mainVindow;

            DataContext = this.mainVindow;

            AddHotKeys();
        }

        public MainWindow MainWindow
        {
            get { return mainVindow; }
            set { mainVindow = value; }
        }

        // REFERENCE: https://codesamplez.com/development/wpf-hotkeys-c-sharp
        private void AddHotKeys()
        {
            try
            {
                RoutedCommand closeDialogCommand = new RoutedCommand();
                closeDialogCommand.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(closeDialogCommand, CloseDialog));
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
            }
        }

        private void CloseDialog(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
