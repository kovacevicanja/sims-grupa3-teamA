using BookingProject.Controller;
using BookingProject.Controllers;
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
    /// Interaction logic for SecondGuestProfile.xaml
    /// </summary>
    public partial class SecondGuestProfile : Window
    {
        private int GuestId { get; set; }
        private Guest2Controller Guest2Controller { get; set; } 
        public SecondGuestProfile(int idGuest)
        {
            InitializeComponent();
            this.DataContext = this;
            this.GuestId = idGuest;
            Guest2Controller = new Guest2Controller();   
        }

        private void Button_Click_MyTours(object sender, RoutedEventArgs e)
        {
            SecondGuestMyTours secondGuestMyTours = new SecondGuestMyTours(Guest2Controller.GetByID(GuestId));
            secondGuestMyTours.Show();
        }

        private void Button_Click_SecondGuestView(object sender, RoutedEventArgs e)
        {
            SecondGuestView secondGuestView = new SecondGuestView(Guest2Controller.GetByID(GuestId));
            secondGuestView.Show();
        }
    }
}
