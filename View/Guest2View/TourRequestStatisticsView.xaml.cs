using BookingProject.Controllers;
using BookingProject.DependencyInjection;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.Repositories.Intefaces;
using BookingProject.View.Guest2ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace BookingProject.View.Guest2View
{
    /// <summary>
    /// Interaction logic for TourRequestStatisticsView.xaml
    /// </summary>
    public partial class TourRequestStatisticsView : Window
    {
        public TourRequestStatisticsView(int guestId, string enteredYear = "")
        {
            InitializeComponent();
            this.DataContext = new TourRequestStatisticsViewModel(guestId, enteredYear);
        }  
    }
}