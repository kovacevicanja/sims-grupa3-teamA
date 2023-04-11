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
        public Guest2 Guest { get; set; }
        public UserController UserController { get; set; }  
        public User User { get; set; }  
        public SecondGuestProfile(int idGuest)
        {
            InitializeComponent();
            this.DataContext = this;
           
            this.GuestId = idGuest;
            //Guest2Controller = new Guest2Controller();
            UserController = new UserController();
            User = new User();
        }

        private void Button_Click_MyTours(object sender, RoutedEventArgs e)
        {
            SecondGuestMyTours secondGuestMyTours = new SecondGuestMyTours(GuestId);
            secondGuestMyTours.Show();
        }

        private void Button_Click_SecondGuestView(object sender, RoutedEventArgs e)
        {
            SecondGuestView secondGuestView = new SecondGuestView(GuestId);
            secondGuestView.Show();
        }

        private void Button_Click_MyVouchers (object sender, RoutedEventArgs e)
        {
            SecondGuestMyVouchersView secondGuestMyVouchers = new SecondGuestMyVouchersView(GuestId);
            secondGuestMyVouchers.ShowDialog();
        }

        private void Button_Click_LogOut(object sender, RoutedEventArgs e)
        {
            UserController.GetByID(GuestId);
            User.Id = GuestId;
            User.IsLoggedIn = false;
            SignInForm signInForm = new SignInForm();
            signInForm.ShowDialog();
        }
    }
}
