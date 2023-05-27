using BookingProject.Controller;
using BookingProject.Domain;
using BookingProject.View.OwnersView;
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
using System.Windows.Threading;

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : Window
    {
        DispatcherTimer timer;

        double panelWidth;
        bool hidden;
        public UserController UserController { get; set; }

        public MenuView()
        {
            InitializeComponent();
            UserController = new UserController();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 0);
            timer.Tick += Timer_Tick;
            
           
            panelWidth = sidePanel.Width;
            hidden = true;
            sidePanel.Width = 34;
            //if (UserController.GetLoggedUser().numberOfSignIn == 1)
            //{
                FrameHomePage.Content = new WelcomeToBookingView(this.FrameHomePage.NavigationService);
            //} else
            //{
            //    FrameHomePage.Content = new OwnerssView(this.FrameHomePage.NavigationService);
            //}
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (hidden)
            {
                sidePanel.Width += 1;
                if (sidePanel.ActualWidth >= panelWidth)
                {
                    timer.Stop();
                    hidden= false;
                    sidePanel.Width = 150;
                }
            }
            else
            {
                sidePanel.Width -= 1;
                if (sidePanel.ActualWidth <= 34)
                {
                    timer.Stop();
                    hidden = true;
                }
            }
            panelWidth = sidePanel.ActualWidth;
        }
        private void Button_Click_View(object sender, RoutedEventArgs e)
        {
            MenuView view = new MenuView();
            view.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }
        private void PanelHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton==MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Button_Click_Profile(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_Accommodations(object sender, RoutedEventArgs e)
        {
            FrameHomePage.Content = new OwnerssView(this.FrameHomePage.NavigationService);
        }
        private void Button_Click_Guests(object sender, RoutedEventArgs e)
        {
            FrameHomePage.Content = new NotGradedView(this.FrameHomePage.NavigationService);
        }
        private void Button_Click_Statistics(object sender, RoutedEventArgs e)
        {
            FrameHomePage.Content = new ChoseAccommodationForStatisticsView(this.FrameHomePage.NavigationService);
        }
        private void Button_Click_Requests(object sender, RoutedEventArgs e)
        {
            FrameHomePage.Content = new OwnersRequestView(this.FrameHomePage.NavigationService);
        }
        private void Button_Click_Suggestions(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click_Renovations(object sender, RoutedEventArgs e)
        {
            FrameHomePage.Content = new AccommodationRenovationsView(this.FrameHomePage.NavigationService);
        }
        private void Button_Click_Forums(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click_Reviews(object sender, RoutedEventArgs e)
        {
            FrameHomePage.Content = new GuestGradesForOwnerView(this.FrameHomePage.NavigationService);
        }
        private void Button_Click_Logout(object sender, RoutedEventArgs e)
        {
            UserController.GetLoggedUser().IsLoggedIn = false;
            UserController.Save();
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            this.Close();
        }
    }
    
}
