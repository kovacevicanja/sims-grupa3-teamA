using BookingProject.Controllers;
using BookingProject.DependencyInjection;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.Repositories.Intefaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace BookingProject.View.Guest2View
{
    /// <summary>
    /// Interaction logic for TourRequestStatisticsView.xaml
    /// </summary>
    public partial class TourRequestStatisticsView : Window, INotifyPropertyChanged
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

        public TourRequestStatisticsView(int guestId)
        {
            InitializeComponent();
            this.DataContext = this;

            GuestId = guestId;

            _tourRequestController = new TourRequestController();

            UnacceptedRequestsPercentage = _tourRequestController.GetUnacceptedRequestsPercentage(GuestId);
            UnacceptedRequestsPercentageDisplay = Math.Round(UnacceptedRequestsPercentage, 2).ToString() + " %";

            AccpetedRequestsPercentage = _tourRequestController.GetAcceptedRequestsPercentage(GuestId);
            AccpetedRequestsPercentageDisplay = Math.Round(AccpetedRequestsPercentage, 2).ToString() + " %";

            var languages = Enum.GetValues(typeof(LanguageEnum)).Cast<LanguageEnum>();
            Languages = new ObservableCollection<LanguageEnum>(languages);

            NumberRequestsLanguage = _tourRequestController.GetNumberRequestsLanguage(GuestId, ChosenLanguage);
            NumberRequestsLocation = 0;

            AvarageNumberOfPeopleInAcceptedRequests = _tourRequestController.GetAvarageNumberOfPeopleInAcceptedRequests(GuestId);
        }
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
        private void Button_Click_Chart(object sender, EventArgs e)
        {
            TourRequestsChartView chart = new TourRequestsChartView(GuestId);
            chart.ShowDialog();
        }
        private void Button_Click_CountNumberForLanguage(object sender, EventArgs e) 
        {
            NumberRequestsLanguage = _tourRequestController.GetNumberRequestsLanguage(GuestId, ChosenLanguage);
        }
        private void Button_Click_CountNumberForLocation(object sender, EventArgs e) 
        {
            NumberRequestsLocation = _tourRequestController.GetNumberRequestsLocation(GuestId, EnteredCountry, EnteredCity);
        }
    }
}