using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ICR_2022_2023_Mapa_dogadjaja.View.Help
{
    /// <summary>
    /// Interaction logic for HelpDialog.xaml
    /// </summary>
    public partial class HelpDialog : Window, INotifyPropertyChanged
    {
        public HelpDialog()
        {
            InitializeComponent();

            DataContext = this;

            AddHotKeys();
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

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

        // REFERENCE: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-close-window-dialog-box?source=recommendations&view=netdesktop-7.0
        private void CloseDialog(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
