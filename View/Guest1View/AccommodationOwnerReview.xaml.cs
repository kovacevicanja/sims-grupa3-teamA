using BookingProject.Model;
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
using System.Windows.Shapes;

namespace BookingProject.View.Guest1View
{
    /// <summary>
    /// Interaction logic for AccommodationOwnerReview.xaml
    /// </summary>
    public partial class AccommodationOwnerReview : Window
    {
        public AccommodationOwnerReview(AccommodationReservation selectedReservation)
        {
            InitializeComponent();
            this.DataContext = new AccommodationOwnerReviewViewModel(selectedReservation);
        }
    }
}