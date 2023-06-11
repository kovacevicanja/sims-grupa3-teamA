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
using System.Xml.Serialization;

namespace BookingProject.View.GuideView
{
    /// <summary>
    /// Interaction logic for GuestListView.xaml
    /// </summary>
    public partial class GuestListView : Window
    {

        public GuestListView(TourTimeInstance chosenTour, KeyPoint chosenKeyPoint)
        {
            InitializeComponent();
            GuestListViewModel ViewModel = new GuestListViewModel(chosenTour, chosenKeyPoint);
            this.DataContext = ViewModel;
            GuestDataGrid.ItemsSource = ViewModel._guests;
        }

    }
}
