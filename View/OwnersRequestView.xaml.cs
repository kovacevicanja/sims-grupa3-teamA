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
    /// Interaction logic for OwnersRequestView.xaml
    /// </summary>
    public partial class OwnersRequestView : Window
    {
        private ReservationMovingRequestController _requestController;
        public ObservableCollection<ReservationMovingRequest> Requests { get; set; }
        public ReservationMovingRequest SelectedMovingRequest { get; set; }
        public OwnersRequestView()
        {
            InitializeComponent();
            this.DataContext = this;
            _requestController = new ReservationMovingRequestController();
            int ownerId = 1;                                                                                                   //change to logged user
            Requests = new ObservableCollection<ReservationMovingRequest>(_requestController.GetAllRequestForOwner(ownerId));
        }
        private void Button_Click_View(object sender, RoutedEventArgs e)
        {
            if (SelectedMovingRequest == null)
            {
                return;
            }
            OwnersApprovingDenyingRequestView view = new OwnersApprovingDenyingRequestView(SelectedMovingRequest);
            view.Show();
        }
    }
}
