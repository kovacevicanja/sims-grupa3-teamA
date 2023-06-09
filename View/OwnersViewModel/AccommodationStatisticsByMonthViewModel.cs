using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingProject.View.OwnersViewModel
{
    public class AccommodationStatisticsByMonthViewModel
    {
        private Accommodation _selectedAccommodation;
        private int _selectedYear;
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
        public int MostBusyMonth { get; set; }
        public NavigationService NavigationService { get; set; }
        public RelayCommand BackCommand
        {
            get; set;
        }
        public string MostBusyMonthDisplay { get; set; }
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
        public AccommodationStatisticsByMonthViewModel(int selectedYear, Accommodation selectedAccommodation, NavigationService navigationService)
        {
            _accommodationReservationController = new AccommodationReservationController();
            _requestController = new RequestAccommodationReservationController();
            _renovationController = new RecommendationRenovationController();
            _selectedAccommodation = new Accommodation();
            _selectedAccommodation = selectedAccommodation;
            _selectedYear = selectedYear;
            DateTimeFormatInfo dateTimeFormat = new DateTimeFormatInfo();
            MostBusyMonth = _accommodationReservationController.GetMostBusyMonth(_accommodationReservationController.GetAll(), _selectedYear);
            MostBusyMonthDisplay = dateTimeFormat.GetMonthName(MostBusyMonth+1);
            //MostBusyMonthDisplay = MostBusyMonth.ToString();
            NumberOfReservations = new int[12];
            NumberOfCancelledReservations = new int[12];
            NumberOfRescheduledReservations = new int[12];
            NumberOfRenovationRecommendations = new int[12];
            NumberOfReservationsDisplay = new string[12];
            NumberOfCancelledReservationsDisplay = new string[12];
            NumberOfRescheduledReservationsDisplay = new string[12];
            NumberOfRenovationRecommendationsDisplay = new string[12];
            BackCommand = new RelayCommand(Button_Click_Back, CanExecute);
            int n = 0;
            for (int i = 0; i < 12; i++)
            {

                NumberOfReservations[n] = _accommodationReservationController.CountReservationsForSpecificMonth(_selectedYear, i, _selectedAccommodation.Id);
                NumberOfReservationsDisplay[n] = NumberOfReservations[n].ToString();
                NumberOfCancelledReservations[n] = _accommodationReservationController.CountCancelledReservationsForSpecificMonth(_selectedYear,i, _selectedAccommodation.Id);
                NumberOfCancelledReservationsDisplay[n] = NumberOfCancelledReservations[n].ToString();
                NumberOfRescheduledReservations[n] = _requestController.CountRescheduledReservationsForSpecificMonth(_selectedYear,i, _selectedAccommodation.Id);
                NumberOfRescheduledReservationsDisplay[n] = NumberOfRescheduledReservations[n].ToString();
                NumberOfRenovationRecommendations[n] = _renovationController.CountAccommodationRenovationRecommendationsForSpecificMonth(_selectedYear,i, _selectedAccommodation.Id);
                NumberOfRenovationRecommendationsDisplay[n] = NumberOfRenovationRecommendations[n].ToString();
                n++;
            }
            NavigationService = navigationService;
        }
        private void Button_Click_Back(object param)
        {
            NavigationService.GoBack();
        }
        private bool CanExecute(object param) { return true; }
        public Accommodation SelectedReservation
        {
            get { return _selectedAccommodation; }
            set { _selectedAccommodation = value; OnPropertyChanged(); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
