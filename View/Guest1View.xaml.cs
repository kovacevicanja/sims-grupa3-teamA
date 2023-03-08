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

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for Guest1View.xaml
    /// </summary>
    public partial class Guest1View : Window
    {
        public Guest1View()
        {
            InitializeComponent();
        }

        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {
            SearchAccommodationsView searchAccommodationsView = new SearchAccommodationsView();
            searchAccommodationsView.Show();
        }

        private void Button_Click_Book(object sender, RoutedEventArgs e)
        {
            ReservationAccommodationView reservationAccommodationView = new ReservationAccommodationView();
            reservationAccommodationView.Show();
        }
    }
}
