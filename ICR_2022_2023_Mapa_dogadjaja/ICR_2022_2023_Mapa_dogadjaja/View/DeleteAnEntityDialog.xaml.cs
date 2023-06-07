using ICR_2022_2023_Mapa_dogadjaja.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace ICR_2022_2023_Mapa_dogadjaja.View
{
    /// <summary>
    /// Interaction logic for DeleteAnEntityDialog.xaml
    /// </summary>
    public partial class DeleteAnEntityDialog : Window, INotifyPropertyChanged
    {
        private AllEntitiesViewModel allEntitiesViewModel;

        private object selectedEntity;
        
        public DeleteAnEntityDialog(AllEntitiesViewModel allEntitiesViewModel, object selectedEntity)
        {
            InitializeComponent();

            DataContext = this;

            this.allEntitiesViewModel = allEntitiesViewModel;
            this.selectedEntity = selectedEntity;
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

        public object SelectedEntity
        {
            get { return selectedEntity; }
            set
            {
                if (value != selectedEntity)
                {
                    selectedEntity = value;
                    OnPropertyChanged("SelectedEntity");
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

        private void DeleteEntity(object sender, RoutedEventArgs e)
        {
            if (Delete_an_entity_dialog_grid == null)
            {
                return;
            }

            allEntitiesViewModel.DereferenceEventsFromEntityToDelete(selectedEntity);

            if (selectedEntity is Model.Event)
            {
                Model.Event convertedSelectedEvent = (Model.Event) selectedEntity;
                allEntitiesViewModel.EventsViewModel.Delete(convertedSelectedEvent);
            }
            else if (selectedEntity is Model.EventTag)
            {
                Model.EventTag convertedSelectedEventTag = (Model.EventTag) selectedEntity;
                allEntitiesViewModel.EventTagsViewModel.Delete(convertedSelectedEventTag);
            }
            else if (selectedEntity is Model.EventType)
            {
                Model.EventType convertedSelectedEventType = (Model.EventType) selectedEntity;
                allEntitiesViewModel.EventTypesViewModel.Delete(convertedSelectedEventType);
            }
            else if (selectedEntity is Model.PopulatedPlace)
            {
                Model.PopulatedPlace convertedSelectedPopulatedPlace = (Model.PopulatedPlace) selectedEntity;
                allEntitiesViewModel.PopulatedPlacesViewModel.Delete(convertedSelectedPopulatedPlace);
            }
            else if (selectedEntity is Model.Country)
            {
                Model.Country convertedSelectedCountry = (Model.Country) selectedEntity;
                allEntitiesViewModel.CountriesViewModel.Delete(convertedSelectedCountry);
            }
            else
            {
                DialogResult = false;
            }

            DialogResult = true;
        }

        // REFERENCE: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-close-window-dialog-box?source=recommendations&view=netdesktop-7.0
        private void CloseDialog(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
