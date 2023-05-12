using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
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
using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.Model.Images;
using BookingProject.View.GuideViewModel;
using Microsoft.VisualBasic.FileIO;

namespace BookingProject.View.GuideView
{
    /// <summary>
    /// Interaction logic for SecondGuestView.xaml
    /// </summary>
    public partial class LiveToursList : Window
    {

        public LiveToursList()
        {
            InitializeComponent();
            LiveToursListViewModel ViewModel = new LiveToursListViewModel();
            this.DataContext = ViewModel;
            TourDataGrid.ItemsSource = ViewModel._instances;

        }


    }     
}