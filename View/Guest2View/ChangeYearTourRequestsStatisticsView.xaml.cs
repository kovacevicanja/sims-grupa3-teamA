using BookingProject.View.Guest2ViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingProject.View.Guest2View
{
    /// <summary>
    /// Interaction logic for ChangeYearTourStatistics.xaml
    /// </summary>
    public partial class ChangeYearTourRequestsStatisticsView : Page
    {
        public ChangeYearTourRequestsStatisticsView(int guestId, NavigationService navigationService, string previousPage="")
        {
            InitializeComponent();
            this.DataContext = new ChangeYearTourRequestsStatisticsViewModel(guestId, navigationService, previousPage);
        }
    }
}