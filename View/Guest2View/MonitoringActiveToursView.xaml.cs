using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.View.Guest2ViewModel;
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
    /// Interaction logic for MonitoringActiveToursView.xaml
    /// </summary>
    public partial class MonitoringActiveToursView : Window
    {
        public MonitoringActiveToursView(Tour tour, List<int> activeToursIds, int guestId)
        {
            InitializeComponent();
            this.DataContext = new MonitoringActiveToursViewModel(tour, activeToursIds, guestId);
        }
    }
}
