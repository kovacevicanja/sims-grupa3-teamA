using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.View.GuideViewModel;
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

namespace BookingProject.View.GuideView
{
    /// <summary>
    /// Interaction logic for GuestReviewsWindow.xaml
    /// </summary>
    /// 

    public partial class GuestReviewsWindow : Window
    {
        public GuestReviewsWindow(TourTimeInstance chosenTour)
        {
            InitializeComponent();
            GuestReviewsViewModel ViewModel = new GuestReviewsViewModel(chosenTour);
            this.DataContext = ViewModel;
            TourDataGrid.ItemsSource = ViewModel._grades;
        }

    }
}
