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
    /// Interaction logic for FindAvailableDatesForAccommodation.xaml
    /// </summary>
    public partial class FindAvailableDatesForAccommodation : Window
    {
        /// <summary>
        /// Interaction logic for ShowDatesAccommodationsReservations.xaml
        /// </summary>
        public ObservableCollection<Range> Ranges { get; set; }

        public FindAvailableDatesForAccommodation(List<(DateTime, DateTime)> ranges)
        {
            InitializeComponent();
            this.DataContext = this;

            Ranges = new ObservableCollection<Range>(ranges.Select(r => new Range { StartDate = r.Item1, EndDate = r.Item2 }).ToList());
        }


        public class Range
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }
    }
}
