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
    /// Interaction logic for Guest1Homepage.xaml
    /// </summary>
    public partial class Guest1Homepage : Window
    {
        public Guest1Homepage()
        {
            InitializeComponent();
        }
        private void Button_Click_Homepage(object sender, RoutedEventArgs e)
        {
            var Guest1Homepage = new Guest1Homepage();
            Guest1Homepage.Show();
            this.Close();
        }

        private void Button_Click_MyReservations(object sender, RoutedEventArgs e)
        {
            var Guest1Reservations = new Guest1Reservations();
            Guest1Reservations.Show();
            this.Close();
        }

        private void Button_Click_Logout(object sender, RoutedEventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            this.Close();
        }
    }
}
