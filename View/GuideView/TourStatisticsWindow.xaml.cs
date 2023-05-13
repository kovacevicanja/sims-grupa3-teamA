using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.View.GuideViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace BookingProject.View.GuideView
{
    /// <summary>
    /// Interaction logic for TourStatisticsWindow.xaml
    /// </summary>
    /// 

    public partial class TourStatisticsWindow : Window
    {
        public TourStatisticsWindow(string pickedYear)
        {
            InitializeComponent();
            TourStatisticsViewModel ViewModel = new TourStatisticsViewModel(pickedYear);
            this.DataContext = ViewModel;
            TourDataGrid.ItemsSource = ViewModel._instances;
        }

    }
}
