using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.Model.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for TourStatisticsWindow.xaml
    /// </summary>
    /// 

    public partial class TourStatisticsWindow : Window
    {

        private TourTimeInstanceController _tourTimeInstanceController;
        private TourStartingTimeController _tourStartingTimeController;
        private TourReservationController _tourReservationController;
        private TourController _tourController;
        private ObservableCollection<TourTimeInstance> _instances;
        public TourTimeInstance ChosenTour { get; set; }
        public string PickedYear { get; set; }
        public TourStatisticsWindow(string pickedYear)
        {
            InitializeComponent();
            this.DataContext = this;
            _tourTimeInstanceController = new TourTimeInstanceController();
            _tourStartingTimeController = new TourStartingTimeController();
            _tourReservationController = new TourReservationController();
            PickedYear = pickedYear;
            _tourController= new TourController();
            _instances = new ObservableCollection<TourTimeInstance>(FilterTours(TourYearFilter(_tourTimeInstanceController.GetAll())));
            TopTour= getMostVisitedTourName();
            TourDataGrid.ItemsSource = _instances;
        }

        public List<TourTimeInstance> FilterTours(List<TourTimeInstance> tours)
        {
            List<TourTimeInstance> filteredTours = new List<TourTimeInstance>();

            foreach (TourTimeInstance tour in tours)
            {
                if (tour.State == TourState.COMPLETED)
                {
                    filteredTours.Add(tour);
                }
            }
            return filteredTours;
        }

        public List<TourTimeInstance> TourYearFilter(List<TourTimeInstance> tours)
        {
            List<TourTimeInstance> filteredTours = new List<TourTimeInstance>();
            if (PickedYear.Equals("all"))
            {
                return tours;
            }
            else
            {

                int pickedYear = int.Parse(PickedYear);
                foreach (TourTimeInstance tour in tours)
                {
                    if (_tourStartingTimeController.GetByID(tour.DateId).StartingDateTime.Year == pickedYear)
                    {
                        filteredTours.Add(tour);
                    }
                }
                return filteredTours;
            }
        }

        public string TopTour { get; set; } = "LOREM IPSUM";

        public string getMostVisitedTourName()
        {
            string maxTourName = "Lorem Ipsum";
            int guestCount = 0;
            foreach(TourTimeInstance instance in _instances)
            {
                if (guestCount < countTourGuests(instance.TourId))
                {
                    guestCount = countTourGuests(instance.TourId);
                    maxTourName = _tourController.GetByID(instance.TourId).Name;
                }

            }

            return maxTourName;
        }

        public int countTourGuests(int TourId)
        {
            int guestCount = 0;
            foreach(TourReservation reservation in _tourReservationController.GetAll())
            {
                if (reservation.Tour.Id == TourId)
                {
                    guestCount += reservation.GuestsNumberPerReservation;
                }
            }

            return guestCount;
        }






        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ChangeStatsYearWindow changeStatsYearWindow = new ChangeStatsYearWindow();
            changeStatsYearWindow.Show();
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            GuestReviewsWindow guestReviewsWindow = new GuestReviewsWindow(ChosenTour);
            guestReviewsWindow.Show();
            Close();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SelectedTourStatsWindow selectedTourStatsWindow = new SelectedTourStatsWindow(ChosenTour);
            selectedTourStatsWindow.Show();
            Close();


        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            GuideHomeWindow guideHomeWindow = new GuideHomeWindow();
            guideHomeWindow.Show();
            Close();

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
