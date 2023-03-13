using BookingProject.Controller;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for SearchAccommodationsView.xaml
    /// </summary>
    public partial class SearchAccommodationsView : Window
    {
        private AccommodationController _accommodationController;
        private ObservableCollection<Accommodation> _accommodations;
        private ObservableCollection<Accommodation> _allAccommodations;
        public SearchAccommodationsView(string accName, string city, string state, string type, string numOfGuests, string minNumOfDays)
        {

            InitializeComponent();
            this.DataContext = this;
            _accommodationController = new AccommodationController();
            _allAccommodations = new ObservableCollection<Accommodation>(_accommodationController.GetAll());
            _accommodations = new ObservableCollection<Accommodation>(_accommodationController.Search(_allAccommodations, accName, city, state, type, numOfGuests, minNumOfDays));
            AccommodationDataGrid.ItemsSource = _accommodations;
        }

    }
}
