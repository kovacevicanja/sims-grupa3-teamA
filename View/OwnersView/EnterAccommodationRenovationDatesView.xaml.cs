using BookingProject.Model;
using BookingProject.View.OwnersViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingProject.View.OwnersView
{
    /// <summary>
    /// Interaction logic for EnterAccommodationRenovationDates.xaml
    /// </summary>
    public partial class EnterAccommodationRenovationDatesView : Page
    {
        public EnterAccommodationRenovationDatesView(Accommodation selectedAccommodation, NavigationService navigationService)
        {
            InitializeComponent();
            this.DataContext = new EnterAccommodationRenovationDatesViewModel(selectedAccommodation, navigationService);
        }
    }
}
