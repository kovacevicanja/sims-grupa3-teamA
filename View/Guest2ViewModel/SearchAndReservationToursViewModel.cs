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
        public RelayCommand LogOutCommand { get; }
        public RelayCommand ShowAllToursCommand { get; }
        public RelayCommand ProfileCommand { get; }
        public RelayCommand SeeMoreCommand { get; }
        public ObservableCollection<LanguageEnum> Languages { get; set; }
        public ObservableCollection<Tour> Tours { get; set; }
        public SearchAndReservationToursViewModel(int guestId)
        {
            _tourController = new TourController();
            Tours = new ObservableCollection<Tour>(_tourController.GetAll());

            GuestId = guestId;
            User = new User();

            SearchCommand = new RelayCommand(Button_Click_Search, CanExecute);
            BookTourCommand = new RelayCommand(Button_Click_Book, CanWhenSelected);
            LogOutCommand = new RelayCommand(Button_Click_LogOut, CanExecute);
            ShowAllToursCommand = new RelayCommand(Button_Click_ShowAll, CanExecute);
            CancelCommand = new RelayCommand(Button_Click_Cancel, CanExecute);
            ProfileCommand = new RelayCommand(Button_BackToProfile, CanExecute);
            SeeMoreCommand = new RelayCommand(Button_Click_SeeMore, CanWhenSelected);

            var languages = Enum.GetValues(typeof(LanguageEnum)).Cast<LanguageEnum>();
            Languages = new ObservableCollection<LanguageEnum>(languages);
        }

        private bool CanExecute(object param) { return true; }

        private void Button_Click_SeeMore(object param)
        {
            SeeMoreAboutTourView seeMore = new SeeMoreAboutTourView(ChosenTour, GuestId);
            seeMore.Show();
            CloseWindow();
        }

        private bool CanWhenSelected(object param)
        {
            if (ChosenTour == null) { return false; }
            else { return true; }
        }

        private void Button_BackToProfile(object param)
        {
            SecondGuestProfileView secondGuestProfile = new SecondGuestProfileView(GuestId);
            secondGuestProfile.Show();
            CloseWindow();
        }

        private void Button_Click_Search(object param)
        {
            int flag = 0;
            try
            {
                if (NumOfGuests.IsEmpty())
                {
                    flag = 1;
                }
                else if (Convert.ToInt32(NumOfGuests) <= 0 && flag == 0 ) 
                {
                    MessageBox.Show("You have not entered a reasonable value to search by number of guests111.");
                }
             
                 _tourController.Search(Tours, City, Country, Duration, ChosenLanguage, NumOfGuests);
            }
            catch
            {
                MessageBox.Show("You have not entered a reasonable value to search by number of guests222.");
            }

            NumOfGuests = string.Empty;
        }

        private void Button_Click_ShowAll(object param)
        {
            _tourController.ShowAll(Tours);
        }

        private void Button_Click_Cancel(object param)
        {
            CloseWindow();
        }

        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(SerachAndReservationToursView)) { window.Close(); }
            }
        }

        private void Button_Click_Book(object param)
        {
            ReservationTourView reservationTourView = new ReservationTourView(ChosenTour, GuestId);
            reservationTourView.Show();
            CloseWindow();
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
        private void Button_Click_LogOut(object param)
        {
            User.Id = GuestId;
            User.IsLoggedIn = false;
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            CloseWindow();
        }
    }
}