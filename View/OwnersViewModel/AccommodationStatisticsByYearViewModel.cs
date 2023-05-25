using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Model;
using BookingProject.View.OwnersView;
using BookingProject.View.OwnerView;
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
using System.Windows.Navigation;

namespace BookingProject.View.OwnerViewModel
{
    public class AccommodationStatisticsByYearViewModel
    {
        private Accommodation _selectedAccommodation;
        public ObservableCollection<int> YearOption { get; set; }
        public int[] NumberOfReservations;
        public string[] NumberOfReservationsDisplay { get; set; }
        public int[] NumberOfCancelledReservations;
        public string[] NumberOfCancelledReservationsDisplay { get; set; }
        public AccommodationReservationController _accommodationReservationController { get; set; }
        public int[] NumberOfRescheduledReservations;
        public string[] NumberOfRescheduledReservationsDisplay { get; set; }
        public RequestAccommodationReservationController _requestController { get; set; }
        public int[] NumberOfRenovationRecommendations;
        public string[] NumberOfRenovationRecommendationsDisplay { get; set; }
        public RecommendationRenovationController _renovationController { get; set; }
        public int ChosenYear { get; set; }
        public int MostBusyYear { get; set; }
        public string MostBusyYearDisplay { get; set; }
        public NavigationService NavigationService { get; set; }
        //private ComboBoxItem _chosenYear;
        //public ComboBoxItem ChosenYear
        //{
        //    get { return _chosenYear; }
        //    set
        //    {
        //        _chosenYear = value;
        //        OnPropertyChanged(); // Implement INotifyPropertyChanged to notify the UI of property changes
        //    }
        //}
        public RelayCommand OpenSecondWindowCommand { get; }
        public int[] NumberOfReservationss
        {
            get { return NumberOfReservations; }
            set { NumberOfReservations = value; }
        }
        public int[] NumberOfCancelledReservationss
        {
            get { return NumberOfCancelledReservations; }
            set { NumberOfCancelledReservations = value; }
        }
        public int[] NumberOfRescheduledReservationss
        {
            get { return NumberOfRescheduledReservations; }
            set { NumberOfRescheduledReservations = value; }
        }
        public int[] NumberOfRenovationRecommendationss
        {
            get { return NumberOfRenovationRecommendations; }
            set { NumberOfRenovationRecommendations = value; }
        }
        public AccommodationStatisticsByYearViewModel(Accommodation selectedAccommodation, NavigationService navigationService)
        {
            _accommodationReservationController = new AccommodationReservationController();
            _requestController = new RequestAccommodationReservationController();
            _renovationController = new RecommendationRenovationController();
            _selectedAccommodation = new Accommodation();
            _selectedAccommodation = selectedAccommodation;
            YearOption = new ObservableCollection<int>();
            MostBusyYear = _accommodationReservationController.FindTheMostBusyYear(_accommodationReservationController.GetAll());
            MostBusyYearDisplay = MostBusyYear.ToString();
            NumberOfReservations = new int[3];
            NumberOfCancelledReservations = new int[3];
            NumberOfRescheduledReservations = new int[3];
            NumberOfRenovationRecommendations= new int[3];
            NumberOfReservationsDisplay = new string[3];
            NumberOfCancelledReservationsDisplay = new string[3];
            NumberOfRescheduledReservationsDisplay = new string[3];
            NumberOfRenovationRecommendationsDisplay = new string[3];
            for (int i = 2023; i >= 2000; i--)
            {
                YearOption.Add(i);
            }
            int n = 0;
            for (int i = 2023; i >= 2021; i--)
            {
                
                NumberOfReservations[n] = _accommodationReservationController.CountReservationsForSpecificYear(i, _selectedAccommodation.Id);
                NumberOfReservationsDisplay[n] = NumberOfReservations[n].ToString();
                NumberOfCancelledReservations[n] = _accommodationReservationController.CountCancelledReservationsForSpecificYear(i, _selectedAccommodation.Id);
                NumberOfCancelledReservationsDisplay[n] = NumberOfCancelledReservations[n].ToString();
                NumberOfRescheduledReservations[n] = _requestController.CountRescheduledReservationsForSpecificYear(i, _selectedAccommodation.Id);
                NumberOfRescheduledReservationsDisplay[n] = NumberOfRescheduledReservations[n].ToString();
                NumberOfRenovationRecommendations[n] = _renovationController.CountAccommodationRenovationRecommendationsForSpecificYear(i, _selectedAccommodation.Id);
                NumberOfRenovationRecommendationsDisplay[n] = NumberOfRenovationRecommendations[n].ToString();
                n++;
            }
            OpenSecondWindowCommand = new RelayCommand(Button_Click_Open, CanExecute);
            NavigationService = navigationService;
        }
        private bool CanExecute(object param) { return true; }
        public Accommodation SelectedReservation
        {
            get { return _selectedAccommodation; }
            set { _selectedAccommodation = value; OnPropertyChanged(); }
        }
        private void Button_Click_Open(object param)
        {
            //var view = new AccommodationStatisticsByMonthView(ChosenYear, _selectedAccommodation);
            //view.Show();
            //CloseWindow();
            NavigationService.Navigate(new AccommodationStatisticsByMonthView(ChosenYear, _selectedAccommodation, NavigationService));
        }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(AccommodationStatisticsByYearView)) { window.Close(); }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
