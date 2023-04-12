using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.Model.Images;
using Microsoft.VisualBasic.FileIO;

namespace BookingProject.View.GuideView
{
    /// <summary>
    /// Interaction logic for SecondGuestView.xaml
    /// </summary>
    public partial class LiveToursList : Window
    {
        private TourTimeInstanceController _tourTimeInstanceController;
        private TourStartingTimeController _tourStartingTimeController;
        private ObservableCollection<TourTimeInstance> _instances;


        public TourTimeInstance ChosenTour { get; set; }



        public LiveToursList()
        {
            InitializeComponent();
            this.DataContext = this;
            _tourTimeInstanceController = new TourTimeInstanceController();
            _tourStartingTimeController = new TourStartingTimeController();

            _instances = new ObservableCollection<TourTimeInstance>(FilterTours(_tourTimeInstanceController.GetAll()));
            TourDataGrid.ItemsSource = _instances;

        }

        private TourState _state;
        public TourState State
        {
                get => _state;
                set
            {
                    if (value != _state)
                    {
                        _state = value;
                        OnPropertyChanged();
                    }
                }
            }




        public List<TourTimeInstance> FilterTours(List<TourTimeInstance> tours)
        {
            List<TourTimeInstance> filteredTours= new List<TourTimeInstance>();
            List<TourTimeInstance> exceptionFilteredTours = new List<TourTimeInstance>();
            foreach (TourTimeInstance tour in tours)
            {
                if (tour.State==TourState.STARTED)
                {
                    exceptionFilteredTours.Add(tour);
                    return exceptionFilteredTours;
                }


                if (TodayCheck(tour))
                {
                    filteredTours.Add(tour);
                }
            }
            return filteredTours;
        }

        public bool TodayCheck(TourTimeInstance tour)
        {
            TourDateTime tourDate = new TourDateTime();
            tourDate = _tourStartingTimeController.GetByID(tour.DateId);
                if (tourDate == null)
                {
                    return false;
                }
                if(tourDate.StartingDateTime.Date == DateTime.Now.Date && tour.State!=TourState.COMPLETED && tour.State!=TourState.CANCELLED)
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



        private void Button_Click_Start(object sender, RoutedEventArgs e)
        {
            if (ChosenTour != null)
            {
                LiveTourView liveTourView = new LiveTourView(ChosenTour);
                liveTourView.Show();
                Close();
            }
        }

        private void Button_Click_Home (object sender, RoutedEventArgs e)
        {
            GuideHomeWindow guideHomeWindow = new GuideHomeWindow();
            guideHomeWindow.Show();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

