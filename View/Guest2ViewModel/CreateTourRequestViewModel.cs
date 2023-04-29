using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.View.CustomMessageBoxes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.View.Guest2ViewModel
{
    public class CreateTourRequestViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<LanguageEnum> Languages { get; set; }
        public LanguageEnum ChosenLanguage { get; set; }
        public TourLocationController _tourLocationController { get; set; }
        public TourRequestController _tourRequestController { get; set; }
        public UserController _userController { get; set; }
        public int GuestId { get; set; }
        public CustomMessageBox CustomMessageBox;
        public RelayCommand CancelCommand { get; }
        public RelayCommand CreateTourRequestCommand { get; }

        public CreateTourRequestViewModel(int guestId)
        {
            var languages = Enum.GetValues(typeof(LanguageEnum)).Cast<LanguageEnum>();
            Languages = new ObservableCollection<LanguageEnum>(languages);

            _tourRequestController = new TourRequestController();
            _tourLocationController = new TourLocationController();
            _userController = new UserController();

            GuestId = guestId;
            CustomMessageBox = new CustomMessageBox();

            StartDate = DateTime.Today;
            EndDate = DateTime.Today;

            CancelCommand = new RelayCommand(Button_Click_Cancel, CanExecute);
            CreateTourRequestCommand = new RelayCommand(Button_Click_CreateTourRequest, CanExecute);
        }

        private bool CanExecute(object param) { return true; }
 
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(CreateTourRequestView)) { window.Close(); }
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }
        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (value != _endDate)
                {
                    _endDate = value;
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
        private string _tourLanguage;
        public string TourLanguage
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
        private int _guestsNumber;
        public int GuestsNumber
        {
            get => _guestsNumber;
            set
            {
                if (value != _guestsNumber)
                {
                    _guestsNumber = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _country;
        public string Country
        {
            get => _country;
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _city;
        public string City
        {
            get => _city;
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click_CreateTourRequest(object param)
        {
            TourRequest tourRequest = new TourRequest();

            Location location = new Location();
            location.City = City;
            location.Country = Country;
            _tourLocationController.Create(location);
            tourRequest.Location = location;

            tourRequest.Description = Description;
            tourRequest.StartDate = StartDate;
            tourRequest.EndDate = EndDate;
            tourRequest.Language = ChosenLanguage;
            tourRequest.GuestsNumber = GuestsNumber;
            tourRequest.Status = Domain.Enums.TourRequestStatus.PENDING;
            tourRequest.Guest.Id = GuestId;

            _tourRequestController.Create(tourRequest);

            CustomMessageBox.ShowCustomMessageBox("You have successfully created a tour request. If you want, you can create more of them.");

            City = "";
            Country = "";
            Description = "";
            GuestsNumber = 0;
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
        }
        private void Button_Click_Cancel(object param)
        {
            CloseWindow();
        }
    }
}
