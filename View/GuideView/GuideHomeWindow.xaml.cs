using BookingProject.Controller;
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

namespace BookingProject.View.GuideView
{
    /// <summary>
    /// Interaction logic for GuideHomeWindow.xaml
    /// </summary>
    /// 
    public partial class GuideHomeWindow : Window
    {
        private readonly UserController _userController;
        public GuideHomeWindow()
        {
            InitializeComponent();
            var app = Application.Current as App;
            _userController = app.UserController;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MyToursWindow myToursWindow= new MyToursWindow();
            myToursWindow.Show();
            Close();
        }

        public void LogoutUser()
        {
            _userController.GetLoggedUser().IsLoggedIn = false;
            _userController.Save();

        }
        private void Button_Click_Logout(object sender, RoutedEventArgs e)
        {
            LogoutUser();
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            TourStatisticsWindow tourStatisticsWindow = new TourStatisticsWindow("all");
            tourStatisticsWindow.Show();
            Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            LiveToursList liveTourList= new LiveToursList();
            liveTourList.Show();
            Close(); 
        }
    }
}
