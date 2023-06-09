using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.View.CustomMessageBoxes;
using BookingProject.View.OwnersView;
using BookingProject.View.OwnerViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for GuestGradesForOwnerView.xaml
    /// </summary>
    public partial class GuestGradesForOwnerView : Page
    {
        public OwnerCustomMessageBox OwnerCustomMessageBox { get; set; }
        public GuestGradesForOwnerView(NavigationService navigationService)
        {
            InitializeComponent();
            this.DataContext = new GuestGradesForOwnerViewModel(navigationService);
            OwnerCustomMessageBox = new OwnerCustomMessageBox();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var grade = ((Button)sender).DataContext as AccommodationOwnerGrade;
            OwnerCustomMessageBox.ShowCustomMessageBox("Accommodation name: " + grade.Accommodation.AccommodationName + Environment.NewLine + " Location: " + grade.Accommodation.Location.City + ", " + grade.Accommodation.Location.Country
                + Environment.NewLine + " Guest name: " + grade.AccommodationReservation.Guest.Name + " " + grade.AccommodationReservation.Guest.Surname + Environment.NewLine + " Reservation dates: " + grade.AccommodationReservation.InitialDate.ToShortDateString() + "-" +
                grade.AccommodationReservation.EndDate.ToShortDateString() + Environment.NewLine + " Cleanliness: " + grade.Cleanliness + Environment.NewLine + " Owner corectness: " + grade.OwnerCorectness + Environment.NewLine + " Additional comment: " + grade.AdditionalComment);
        }
    }
}
