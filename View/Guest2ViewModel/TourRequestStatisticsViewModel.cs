using BookingProject.Commands;
using BookingProject.Controllers;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.View.Guest2View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace BookingProject.View.Guest2ViewModel
{
    public class TourRequestStatisticsViewModel : INotifyPropertyChanged
    {
        public int GuestId { get; set; }
        public TourRequestController _tourRequestController { get; set; }
        public double AccpetedRequestsPercentage { get; set; }
        public string AccpetedRequestsPercentageDisplay { get; set; }
        public double UnacceptedRequestsPercentage { get; set; }
        public string UnacceptedRequestsPercentageDisplay { get; set; }
        public ObservableCollection<LanguageEnum> Languages { get; set; }
        public LanguageEnum ChosenLanguage { get; set; }
        public double AvarageNumberOfPeopleInAcceptedRequests { get; set; }
        public string EnteredYear { get; set; }
        public RelayCommand CountNumberForLanguagesCommand { get; }
        public RelayCommand CountNumberForLocationCommand { get; }
        public RelayCommand ChangeYearCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand LanguageChartCommand { get; }
        public RelayCommand LocationChartCommand { get; }
        public RelayCommand ChartPieCommand { get; }
        public User User { get; }
        public NavigationService NavigationService { get; set; }

        public TourRequestStatisticsViewModel(int guestId, NavigationService navigationService, string enteredYear = "")
        {
            GuestId = guestId;

            _tourRequestController = new TourRequestController();

            EnteredYear = enteredYear;

            UnacceptedRequestsPercentage = _tourRequestController.GetUnacceptedRequestsPercentage(GuestId, EnteredYear);
            UnacceptedRequestsPercentageDisplay = Math.Round(UnacceptedRequestsPercentage, 2).ToString() + " %";

            AccpetedRequestsPercentage = _tourRequestController.GetAcceptedRequestsPercentage(GuestId, EnteredYear);
            AccpetedRequestsPercentageDisplay = Math.Round(AccpetedRequestsPercentage, 2).ToString() + " %";

            var languages = Enum.GetValues(typeof(LanguageEnum)).Cast<LanguageEnum>();
            Languages = new ObservableCollection<LanguageEnum>(languages);

            NumberRequestsLanguage = _tourRequestController.GetNumberRequestsLanguage(GuestId, ChosenLanguage, EnteredYear);
            NumberRequestsLocation = _tourRequestController.GetNumberRequestsLocation(GuestId, EnteredCountry, EnteredCity, EnteredYear);

            AvarageNumberOfPeopleInAcceptedRequests = _tourRequestController.GetAvarageNumberOfPeopleInAcceptedRequests(GuestId, EnteredYear);

            CountNumberForLanguagesCommand = new RelayCommand(Button_Click_CountNumberForLanguage, CanExecute);
            CountNumberForLocationCommand = new RelayCommand(Button_Click_CountNumberForLocation, CanExecute);
            ChangeYearCommand = new RelayCommand(Button_Click_ChangeTheYear, CanExecute);
            CancelCommand = new RelayCommand(Button_Cancel, CanExecute);
            LanguageChartCommand = new RelayCommand(Button_ChartLangauge, CanExecute);
            LocationChartCommand = new RelayCommand(Button_ChartLocation, CanExecute);
            ChartPieCommand = new RelayCommand(Button_ChartPie, CanExecute);

            User = new User();
            NavigationService = navigationService;
        }

        private bool CanExecute(object param) { return true; }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private string _enteredCity;
        public string EnteredCity
        {
            get => _enteredCity;
            set
            {
                if (value != _enteredCity)
                {
                    _enteredCity = value;
                    OnPropertyChanged(nameof(EnteredCity));
                }
            }
        }
        private string _enteredCountry;
        public string EnteredCountry
        {
            get => _enteredCountry;
            set
            {
                if (value != _enteredCountry)
                {
                    _enteredCountry = value;
                    OnPropertyChanged(nameof(EnteredCountry));
                }
            }
        }
        private int _numberRequestsLanguage;
        public int NumberRequestsLanguage
        {
            get { return _numberRequestsLanguage; }
            set
            {
                if (_numberRequestsLanguage != value)
                {
                    _numberRequestsLanguage = value;
                    OnPropertyChanged(nameof(NumberRequestsLanguage));
                }
            }
        }
        private int _numberRequestsLocation;
        public int NumberRequestsLocation
        {
            get { return _numberRequestsLocation; }
            set
            {
                if (_numberRequestsLocation != value)
                {
                    _numberRequestsLocation = value;
                    OnPropertyChanged(nameof(NumberRequestsLocation));
                }
            }
        }
        private void Button_Click_CountNumberForLanguage(object param)
        {
            NumberRequestsLanguage = _tourRequestController.GetNumberRequestsLanguage(GuestId, ChosenLanguage, EnteredYear);
        }
        private void Button_Click_CountNumberForLocation(object param)
        {
            NumberRequestsLocation = _tourRequestController.GetNumberRequestsLocation(GuestId, EnteredCountry, EnteredCity, EnteredYear);

        }
        private void Button_Click_ChangeTheYear(object param)
        {
            NavigationService.Navigate(new ChangeYearTourRequestsStatisticsView(GuestId, NavigationService));
        }

        private void Button_Cancel(object param)
        {
            NavigationService.GoBack();
        }

        private void Button_ChartLangauge(object param)
        {
            NavigationService.Navigate(new TourRequestsLanguageChartView(GuestId, NavigationService));
        }

        private void Button_ChartLocation(object param)
        {
            NavigationService.Navigate(new TourRequestsLocationChartView(GuestId, NavigationService));
        }

        private void Button_ChartPie(object param)
        {
            NavigationService.Navigate(new TourRequestStatisticsPieChart(GuestId, NavigationService));
        }
    }
}