using BookingProject.Controller;
using BookingProject.Model.Enums;
using BookingProject.Model;
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
    /// Interaction logic for MyToursWindow.xaml
    /// </summary>
    public partial class MyToursWindow : Window
    {

        private TourTimeInstanceController _tourTimeInstanceController;
        private TourStartingTimeController _tourStartingTimeController;
        private ObservableCollection<TourTimeInstance> _instances;
        public TourTimeInstance ChosenTour { get; set; }
        public MyToursWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            _tourTimeInstanceController = new TourTimeInstanceController();
            _tourStartingTimeController = new TourStartingTimeController();
            _instances = new ObservableCollection<TourTimeInstance>(FilterTours(_tourTimeInstanceController.GetAll()));
            TourDataGrid.ItemsSource = _instances;
        }
        public List<TourTimeInstance> FilterTours(List<TourTimeInstance> tours)
        {
            List<TourTimeInstance> filteredTours = new List<TourTimeInstance>();
            foreach (TourTimeInstance tour in tours)
            {
                if (tour.State!=TourState.CANCELLED)
                {
                    filteredTours.Add(tour);
                }
            }
            return filteredTours;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            GuideHomeWindow guideHomeWindow = new GuideHomeWindow();
            guideHomeWindow.Show();
            Close();
        }
        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            if (ChosenTour != null && IsNotLate(ChosenTour))
            {
                TourCancellationWindow tourCancellationWindow = new TourCancellationWindow(ChosenTour);
                tourCancellationWindow.Show();
                Close();
            }
        }
        private bool IsNotLate(TourTimeInstance tour)
        {
            TourDateTime tourDate = new TourDateTime();
            tourDate = _tourStartingTimeController.GetByID(tour.DateId);
            TimeSpan ts = tourDate.StartingDateTime - DateTime.Now;
            if (ts> TimeSpan.FromHours(48))
            {
                return true;
            }
            return false;
        }
        private void Button_Click_Create(object sender, RoutedEventArgs e)
        {
            TourCreationWindow tourCreationWindow = new TourCreationWindow();
            tourCreationWindow.Show();
            Close();
        }
    }
}
