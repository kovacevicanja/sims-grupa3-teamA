using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BookingProject.View.Guest2View
{
    /// <summary>
    /// Interaction logic for TourRequestsView.xaml
    /// </summary>
    public partial class TourRequestsView : Window
    {
        public int GuestId { get; set; }
        public TourRequestController _tourRequestController;
        public ObservableCollection<TourRequest> _tourRequests;

        public event PropertyChangedEventHandler PropertyChanged;

        public TourRequestsView(int guestId)
        {
            InitializeComponent();
            this.DataContext = this;

            GuestId = guestId;

            _tourRequestController = new TourRequestController();
            _tourRequests = new ObservableCollection<TourRequest>(_tourRequestController.GetGuestRequests(guestId, ""));
            MyRequestsDataGrid.ItemsSource = _tourRequests;
        }
        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}