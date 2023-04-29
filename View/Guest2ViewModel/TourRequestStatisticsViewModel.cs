using BookingProject.Commands;
using BookingProject.Controllers;
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

        public TourRequestStatisticsViewModel(int guestId, string enteredYear = "")
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
            NumberRequestsLocation = 0;

            AvarageNumberOfPeopleInAcceptedRequests = _tourRequestController.GetAvarageNumberOfPeopleInAcceptedRequests(GuestId, EnteredYear);

            CountNumberForLanguagesCommand = new RelayCommand(Button_Click_CountNumberForLanguage, CanExecute);
            CountNumberForLocationCommand = new RelayCommand(Button_Click_CountNumberForLocation, CanExecute);
            ChangeYearCommand = new RelayCommand(Button_Click_ChangeTheYear, CanExecute);
            CancelCommand = new RelayCommand(Button_Cancel, CanExecute);
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
            ChangeYearTourRequestsStatisticsView changeYearTourStatisticsView = new ChangeYearTourRequestsStatisticsView(GuestId);
            changeYearTourStatisticsView.ShowDialog();
            CloseWindow();
        }

        private void Button_Cancel(object param)
        {
            CloseWindow();
        }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(TourRequestStatisticsView)) { window.Close(); }
            }
        }
    }
}
