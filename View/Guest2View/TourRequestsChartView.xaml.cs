using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingProject.View.Guest2View
{
    /// <summary>
    /// Interaction logic for TourRequestsChartView.xaml
    /// </summary>
    public partial class TourRequestsChartView : Window
    {
        public TourRequestsChartView(int guestId)
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
