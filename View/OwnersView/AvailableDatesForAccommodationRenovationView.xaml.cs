using BookingProject.Model;
using BookingProject.View.OwnersViewModel;
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

namespace BookingProject.View.OwnersView
{
    /// <summary>
    /// Interaction logic for AvailableDatesForAccommodationRenovationView.xaml
    /// </summary>
    public partial class AvailableDatesForAccommodationRenovationView : Window
    {
        public AvailableDatesForAccommodationRenovationView(Accommodation selectedAccommodation, ObservableCollection<Tuple<DateTime, DateTime>> availableDates)
        {
            InitializeComponent();
            this.DataContext = new AvailableDatesForAccommodationRenovationViewModel(selectedAccommodation, availableDates);
        }
    }
}
