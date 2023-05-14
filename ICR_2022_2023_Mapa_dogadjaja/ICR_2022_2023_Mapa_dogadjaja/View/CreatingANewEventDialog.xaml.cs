using ICR_2022_2023_Mapa_dogadjaja.Model;
using ICR_2022_2023_Mapa_dogadjaja.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    public partial class CreatingANewEventDialog : Window
    {
        private AllEntitiesViewModel allEntitiesViewModel;

        private string pathToSelectedEventIcon;

        public CreatingANewEventDialog()
        {
            InitializeComponent();
            
            allEntitiesViewModel = new AllEntitiesViewModel();
            DataContext = allEntitiesViewModel;

            pathToSelectedEventIcon = "";
            
            AddHotKeys();
        }
        
        public string PathToSelectedEventIcon
        {
            get { return pathToSelectedEventIcon; }
            set { pathToSelectedEventIcon = value; }
        }
        
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
            
            pathToSelectedEventIcon = openFileDialog.FileName;
            allEntitiesViewModel.PathToSelectedEventIcon = pathToSelectedEventIcon;
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
