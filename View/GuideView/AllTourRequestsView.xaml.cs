using BookingProject.View.Guest2ViewModel;
using BookingProject.View.GuideViewModel;
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
    /// Interaction logic for TourRequestsView.xaml
    /// </summary>
    public partial class AllTourRequestsView : Window
    {
        public AllTourRequestsView()
        {
            InitializeComponent();
            AllTourRequestsViewModel ViewModel = new AllTourRequestsViewModel();
            this.DataContext = ViewModel;
            TourDataGrid.ItemsSource = ViewModel.TourRequests;
        }

    }
}
