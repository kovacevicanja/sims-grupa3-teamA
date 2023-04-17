using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain;
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
    /// Interaction logic for AccommodationRequestsView.xaml
    /// </summary>
    public partial class AccommodationRequestsView : Window
    {
        public ObservableCollection<RequestAccommodationReservation> _requests;
        public RequestAccommodationReservationController requestAccommodationReservationController;
        public UserController userController;
        public AccommodationRequestsView()
        {
            InitializeComponent();
            this.DataContext = this;
            requestAccommodationReservationController = new RequestAccommodationReservationController();
            userController = new UserController();
            _requests = new ObservableCollection<RequestAccommodationReservation>(requestAccommodationReservationController.GetAllForUser(userController.GetLoggedUser()));
            RequestsDataGrid.ItemsSource = _requests;
        }
    }
}
