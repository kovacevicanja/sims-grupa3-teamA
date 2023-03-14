using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.Model.Images;
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
    /// Interaction logic for OwnerView.xaml
    /// </summary>
    public partial class OwnerView : Window
    {
        private AccommodationController _accommodationController;
        private ObservableCollection<Accommodation> _accommodations;
        public Accommodation ChosenAccommodation { get; set; }
        public AccommodationImage Image { get; set; }
        public OwnerView()
        {
            InitializeComponent();
            this.DataContext = this;
            _accommodationController = new AccommodationController();
            _accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetAll());
            AccommodationDataGrid.ItemsSource = _accommodations;
        }
        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            AddAccommodationView addAccommodationsView = new AddAccommodationView();
            addAccommodationsView.Show();
        }
        private void Button_Click_Add_Photo(object sender, RoutedEventArgs e)
        {
            AddPhotosToAccommodationView addPhotosToAccommodationView = new AddPhotosToAccommodationView(ChosenAccommodation);
            addPhotosToAccommodationView.Show();
        }
    }
}
