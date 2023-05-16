using BookingProject.Model;
using BookingProject.View.OwnerViewModel;
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

namespace BookingProject.View.OwnerView
{
    /// <summary>
    /// Interaction logic for AccommodationStatisticsByYearView.xaml
    /// </summary>
    public partial class AccommodationStatisticsByYearView : Window
    {
        public AccommodationStatisticsByYearView(Accommodation selectedAccommodation)
        {
            InitializeComponent();
            this.DataContext = new AccommodationStatisticsByYearViewModel(selectedAccommodation);
        }
    }
}
