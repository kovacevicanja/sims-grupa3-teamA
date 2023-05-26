using BookingProject.Controller;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingProject.View.OwnersView
{
    /// <summary>
    /// Interaction logic for BookingCalendarView.xaml
    /// </summary>
    public partial class BookingCalendarView : Page
    {
        public AccommodationReservationController Controller { get; set; }
        public BookingCalendarView(Accommodation selectedAccommodation, NavigationService navigationService)
        {
            InitializeComponent();
            this.DataContext = new BookingCalendarViewModel(selectedAccommodation, navigationService);
            Controller = new AccommodationReservationController();
            PopulateOccupiedDates();
        }
        private void PopulateOccupiedDates()
        {
            foreach (var reservation in Controller.GetAll())
            {
                DateTime startDate = reservation.InitialDate;
                DateTime endDate = reservation.EndDate;
                for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    ReservationCalendar.BlackoutDates.Add(new CalendarDateRange(date));
                }
            }
        }
        

    }
}
