using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Model.Enums;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using BookingProject.View.Guest2View;
using System.Web.WebPages;
using BookingProject.View.CustomMessageBoxes;
using System.Windows.Navigation;
using BookingProject.Repositories;

namespace BookingProject.View.Guest2ViewModel
{
    public class SearchAndReservationToursViewModel : INotifyPropertyChanged
    {
        private TourController _tourController;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string ChosenLanguage { get; set; } = string.Empty;
        public string NumOfGuests { get; set; } = string.Empty;
        public Tour ChosenTour { get; set; }
        public int GuestId { get; set; }
        public User User { get; set; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand BookTourCommand { get; }
        public RelayCommand ShowAllToursCommand { get; }
        public RelayCommand SeeMoreCommand { get; }
        public ObservableCollection<LanguageEnum> Languages { get; set; }
        public ObservableCollection<Tour> Tours { get; set; }
        public CustomMessageBox CustomMessageBox { get; set; }
        public NavigationService NavigationService { get; set; }
        public SearchAndReservationToursViewModel(int guestId, NavigationService navigationService)
        {
            _tourController = new TourController();

            Tours = new ObservableCollection<Tour>(_tourController.LoadAgain());

            NavigationService = navigationService;

            GuestId = guestId;
            User = new User();

            SearchCommand = new RelayCommand(Button_Click_Search, CanExecute);
            BookTourCommand = new RelayCommand(Button_Click_Book, CanWhenSelected);
            ShowAllToursCommand = new RelayCommand(Button_Click_ShowAll, CanExecute);
            CancelCommand = new RelayCommand(Button_Click_Cancel, CanExecute);
            SeeMoreCommand = new RelayCommand(Button_Click_SeeMore, CanWhenSelected);

            var languages = Enum.GetValues(typeof(LanguageEnum)).Cast<LanguageEnum>();
            Languages = new ObservableCollection<LanguageEnum>(languages);

            CustomMessageBox = new CustomMessageBox();
        }

        private bool CanExecute(object param) { return true; }

        private void Button_Click_SeeMore(object param)
        {
            NavigationService.Navigate(new SeeMoreAboutTourView(ChosenTour, GuestId, NavigationService));
        }

        private bool CanWhenSelected(object param)
        {
            if (ChosenTour == null) return false; 
            else return true; 
        }

        private void Button_Click_Search(object param)
        {
            try
            {
                if (NumOfGuests.IsEmpty() && Duration.IsEmpty())
                {
                    _tourController.Search(Tours, City, Country, Duration, ChosenLanguage, NumOfGuests);
                }
                else if (Convert.ToInt32(NumOfGuests) <= 0 || Convert.ToInt32(Duration) <= 0)
                {
                    CustomMessageBox.ShowCustomMessageBox("Check that you have correctly entered the number of guests and the duration of the tour.");
                }
                else
                {
                    _tourController.Search(Tours, City, Country, Duration, ChosenLanguage, NumOfGuests);
                }
            }
            catch
            {
                CustomMessageBox.ShowCustomMessageBox("Check that you have correctly entered the number of guests and the duration of the tour.");
            }

            NumOfGuests = string.Empty;
        }

        private void Button_Click_ShowAll(object param)
        {
            _tourController.ShowAll(Tours);
        }

        private void Button_Click_Cancel(object param)
        {
            NavigationService.GoBack();
        }

        private void Button_Click_Book(object param)
        {
            NavigationService.Navigate(new ReservationTourView(ChosenTour, GuestId, NavigationService));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _tourName;
        public string TourName
        {
            get => _tourName;
            set
            {
                if (value != _tourName)
                {
                    _tourName = value;
                    OnPropertyChanged();
                }
            }
        }

        private Location _location;
        public Location Location
        {
            get => _location;
            set
            {
                if (value != _location)
                {
                    _location = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        private LanguageEnum _tourLanguage;
        public LanguageEnum TourLanguage
        {
            get => _tourLanguage;
            set
            {
                if (value != _tourLanguage)
                {
                    _tourLanguage = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _maxGuests;
        public int MaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _durationInHours;
        public double DurationInHours
        {
            get => _durationInHours;
            set
            {
                if (value != _durationInHours)
                {
                    _durationInHours = value;
                    OnPropertyChanged();
                }
            }
        }
        private List<DateTime> _startingTime;
        public List<DateTime> StartingTime
        {
            get => _startingTime;
            set
            {
                _startingTime = value;
                OnPropertyChanged();
            }
        }
    }
}