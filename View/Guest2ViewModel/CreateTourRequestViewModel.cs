﻿using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.Validation;
using BookingProject.View.CustomMessageBoxes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.WebPages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

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
        public RelayCommand LogOutCommand { get; }
        public User User { get; set; }
        public int Flag { get; set; }
        public CorrectInputCityValidationRule correctInputCityvalidationRule { get; set; }

        public CreateTourRequestViewModel(int guestId)
        {
            var languages = Enum.GetValues(typeof(LanguageEnum)).Cast<LanguageEnum>();
            Languages = new ObservableCollection<LanguageEnum>(languages);

            _tourRequestController = new TourRequestController();
            _tourLocationController = new TourLocationController();
            _userController = new UserController();

            Flag = 0;

            GuestId = guestId;
            CustomMessageBox = new CustomMessageBox();

            StartDate = DateTime.Today;
            EndDate = DateTime.Today;

            CancelCommand = new RelayCommand(Button_Click_Cancel, CanExecute);
            CreateTourRequestCommand = new RelayCommand(Button_Click_CreateTourRequest, CanExecute);
            LogOutCommand = new RelayCommand(Button_Click_LogOut, CanExecute);

            User = new User();

            correctInputCityvalidationRule = new CorrectInputCityValidationRule();
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
            get { return _city; }
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));

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

            tourRequest.Description = Description;
            tourRequest.StartDate = StartDate;
            tourRequest.EndDate = EndDate;
            tourRequest.Language = ChosenLanguage;
            tourRequest.GuestsNumber = GuestsNumber;
            tourRequest.Status = Domain.Enums.TourRequestStatus.PENDING;
            tourRequest.Guest.Id = GuestId;

            /*string input = GuestsNumber.ToString();
            ValidationResult result = validationRule.Validate(input, CultureInfo.CurrentCulture);

            if (result.IsValid)
            {
                _tourRequestController.Create(tourRequest);

                CustomMessageBox.ShowCustomMessageBox("You have successfully created a tour request. If you want, you can create more of them.");

                City = "";
                Country = "";
                Description = "";
                GuestsNumber = 0;
                StartDate = DateTime.Today;
                EndDate = DateTime.Today;
            }
            else
            { 
                CustomMessageBox.ShowCustomMessageBox(result.ErrorContent.ToString());
            }
            */

            ValidationResult resultCity = correctInputCityvalidationRule.Validate(location.City, CultureInfo.CurrentCulture);

            if (string.IsNullOrEmpty(location.City) || string.IsNullOrEmpty(location.Country) || (tourRequest.StartDate == DateTime.Today
                && tourRequest.EndDate == DateTime.Today) || tourRequest.GuestsNumber == 0)
            {
                CustomMessageBox.ShowCustomMessageBox("You can not create a tour request. Some of the required fields are not filled out.");
            }
            else
            {
                if (!(location.City.IsEmpty() || location.Country.IsEmpty()) && resultCity.IsValid)
                {
                    _tourLocationController.Create(location);
                    tourRequest.Location = location;

                    _tourRequestController.Create(tourRequest);
    
                    CustomMessageBox.ShowCustomMessageBox("You have successfully created a tour request. If you want, you can create more of them.");

                    City = "";
                    Country = "";
                    Description = "";
                    GuestsNumber = 0;
                    StartDate = DateTime.Today;
                    EndDate = DateTime.Today;
                }
                else
                {
                    MessageBox.Show("Niste uneli pravilno grad.");
                }
            }
        }

        private void Button_Click_Cancel(object param)
        {
            SecondGuestProfileView profile = new SecondGuestProfileView(GuestId);
            profile.Show();
            CloseWindow();
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