using ICR_2022_2023_Mapa_dogadjaja.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ICR_2022_2023_Mapa_dogadjaja.View.PopulatedPlace
{
    /// <summary>
    /// Interaction logic for AllPopulatedPlacesDialog.xaml
    /// </summary>
    public partial class AllPopulatedPlacesDialog : Window, INotifyPropertyChanged
    {
        private AllEntitiesViewModel allEntitiesViewModel;

        public AllPopulatedPlacesDialog(AllEntitiesViewModel allEntitiesViewModel)
        {
            InitializeComponent();

            DataContext = this;

            this.allEntitiesViewModel = allEntitiesViewModel;

            AddHotKeys();
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
                RoutedCommand openDialogForCreatingANewPopulatedPlaceCommand = new RoutedCommand();
                openDialogForCreatingANewPopulatedPlaceCommand.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(openDialogForCreatingANewPopulatedPlaceCommand, 
                    OpenDialogForCreatingANewPopulatedPlace));

                RoutedCommand openDialogForEditingAPopulatedPlaceCommand = new RoutedCommand();
                openDialogForEditingAPopulatedPlaceCommand.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(openDialogForEditingAPopulatedPlaceCommand, 
                    OpenDialogForEditingAPopulatedPlace));

                RoutedCommand openDialogForDeletingAnEntityCommand = new RoutedCommand();
                openDialogForDeletingAnEntityCommand.InputGestures.Add(new KeyGesture(Key.Delete, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(openDialogForDeletingAnEntityCommand, OpenDialogForDeletingAnEntity));

                RoutedCommand closeDialogCommand = new RoutedCommand();
                closeDialogCommand.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(closeDialogCommand, CloseDialog));
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
            }
        }

        private void OpenDialogForCreatingANewPopulatedPlace(object sender, RoutedEventArgs e)
        {
            CreateANewPopulatedPlaceDialog dialogForCreatingANewPopulatedPlace = 
                new CreateANewPopulatedPlaceDialog(allEntitiesViewModel);
            dialogForCreatingANewPopulatedPlace.ShowDialog();
        }

        private void OpenDialogForEditingAPopulatedPlace(object sender, RoutedEventArgs e)
        {
            Model.PopulatedPlace selectedPopulatedPlace = (Model.PopulatedPlace) Table_of_populated_places.SelectedItem;
            if (selectedPopulatedPlace == null)
            {
                return;
            }

            EditAPopulatedPlaceDialog dialogForEditingAPopulatedPlace = 
                new EditAPopulatedPlaceDialog(allEntitiesViewModel, selectedPopulatedPlace);
            dialogForEditingAPopulatedPlace.ShowDialog();
        }

        private void OpenDialogForDeletingAnEntity(object sender, RoutedEventArgs e)
        {
            Model.PopulatedPlace selectedPopulatedPlace = (Model.PopulatedPlace) Table_of_populated_places.SelectedItem;
            if (selectedPopulatedPlace == null)
            {
                return;
            }

            DeleteAnEntityDialog dialogForDeletingAnEntity = new DeleteAnEntityDialog(allEntitiesViewModel, selectedPopulatedPlace);
            dialogForDeletingAnEntity.ShowDialog();
        }

        // REFERENCE: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-close-window-dialog-box?source=recommendations&view=netdesktop-7.0
        private void CloseDialog(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        // REFERENCE: https://social.msdn.microsoft.com/Forums/silverlight/en-US/062a2fc8-802d-4390-b2c8-ec73153e1911/column-width-in-percentage-for-datagrid?forum=silverlightcontrols
        private void Table_of_populated_places_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;

            foreach (DataGridColumn dgColumn in dataGrid.Columns)
            {
                if (dgColumn.MinWidth > 0 && dgColumn.ActualWidth > 0)
                {
                    if (dataGrid.MinWidth > 0)
                    {
                        dgColumn.Width = new DataGridLength(dgColumn.MinWidth / dataGrid.MinWidth * dataGrid.ActualWidth);
                    }
                    else
                    {
                        dgColumn.Width = new DataGridLength(dgColumn.MinWidth * (dataGrid.ActualWidth - 14) / 100);
                    }
                }
            }
        }
    }
}
