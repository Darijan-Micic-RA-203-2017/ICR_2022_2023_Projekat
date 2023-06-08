using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ICR_2022_2023_Mapa_dogadjaja.View.Help
{
    /// <summary>
    /// Interaction logic for StartingPage.xaml
    /// </summary>
    public partial class StartingPage : Page
    {
        public StartingPage()
        {
            InitializeComponent();
        }

        private void GoToSearchEventsVideoPage(object sender, RoutedEventArgs e)
        {
            SearchEventsVideoPage searchEventsVideoPage = new SearchEventsVideoPage();
            NavigationService.Navigate(searchEventsVideoPage);
        }

        private void GoToFilterTableOfEventsVideoPage(object sender, RoutedEventArgs e)
        {
            FilterTableOfEventsVideoPage filterTableOfEventsVideoPage = new FilterTableOfEventsVideoPage();
            NavigationService.Navigate(filterTableOfEventsVideoPage);
        }
    }
}
