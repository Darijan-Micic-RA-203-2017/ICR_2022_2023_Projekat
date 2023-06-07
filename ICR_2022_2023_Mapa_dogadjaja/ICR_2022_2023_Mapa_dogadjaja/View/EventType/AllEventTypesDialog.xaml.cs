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

namespace ICR_2022_2023_Mapa_dogadjaja.View.EventType
{
    /// <summary>
    /// Interaction logic for AllEventTypesDialog.xaml
    /// </summary>
    public partial class AllEventTypesDialog : Window, INotifyPropertyChanged
    {
        private AllEntitiesViewModel allEntitiesViewModel;

        public AllEventTypesDialog(AllEntitiesViewModel allEntitiesViewModel)
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
                RoutedCommand openDialogForCreatingANewEventTypeCommand = new RoutedCommand();
                openDialogForCreatingANewEventTypeCommand.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(openDialogForCreatingANewEventTypeCommand, OpenDialogForCreatingANewEventType));

                RoutedCommand openDialogForEditingAnEventTypeCommand = new RoutedCommand();
                openDialogForEditingAnEventTypeCommand.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(openDialogForEditingAnEventTypeCommand, OpenDialogForEditingAnEventType));

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

        private void OpenDialogForCreatingANewEventType(object sender, RoutedEventArgs e)
        {
            CreateANewEventTypeDialog dialogForCreatingANewEventType = new CreateANewEventTypeDialog(allEntitiesViewModel);
            dialogForCreatingANewEventType.ShowDialog();
        }

        private void OpenDialogForEditingAnEventType(object sender, RoutedEventArgs e)
        {
            Model.EventType selectedEventType = (Model.EventType) Table_of_event_types.SelectedItem;
            if (selectedEventType == null)
            {
                return;
            }

            EditAnEventTypeDialog dialogForEditingAnEventType = new EditAnEventTypeDialog(allEntitiesViewModel, selectedEventType);
            dialogForEditingAnEventType.ShowDialog();
        }

        private void OpenDialogForDeletingAnEntity(object sender, RoutedEventArgs e)
        {
            Model.EventType selectedEventType = (Model.EventType) Table_of_event_types.SelectedItem;
            if (selectedEventType == null)
            {
                return;
            }

            DeleteAnEntityDialog dialogForDeletingAnEntity = new DeleteAnEntityDialog(allEntitiesViewModel, selectedEventType);
            dialogForDeletingAnEntity.ShowDialog();
        }

        // REFERENCE: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-close-window-dialog-box?source=recommendations&view=netdesktop-7.0
        private void CloseDialog(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        // REFERENCE: https://social.msdn.microsoft.com/Forums/silverlight/en-US/062a2fc8-802d-4390-b2c8-ec73153e1911/column-width-in-percentage-for-datagrid?forum=silverlightcontrols
        private void Table_of_event_types_SizeChanged(object sender, SizeChangedEventArgs e)
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
