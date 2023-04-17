﻿using System;
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
using BookingProject.Controllers;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.Model.Images;
using Microsoft.VisualBasic.FileIO;

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for SecondGuestView.xaml
    /// </summary>
    public partial class SecondGuestSerachAndReservationTours : Window
    {
        private TourController _tourController;
        private ObservableCollection<Tour> _tours;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string ChosenLanguage { get; set; } = string.Empty;
        public string NumOfGuests { get; set; } = string.Empty;
        public Tour ChosenTour { get; set; }
        public int GuestId { get; set; }
        public User User { get; set; }  
        public SecondGuestSerachAndReservationTours(int guestId)
        {
            InitializeComponent();
            this.DataContext = this;
            _tourController = new TourController();
            _tours = new ObservableCollection<Tour>(_tourController.GetAll());

            TourDataGrid.ItemsSource = _tours;
            GuestId = guestId;
            User = new User();

            languageComboBox.ItemsSource = new List<string>() { "ENGLISH", "SERBIAN", "GERMAN" };
        }

        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {
            _tourController.Search(_tours, City, Country, Duration, ChosenLanguage, NumOfGuests);

        }

        private void Button_Click_ShowAll(object sender, RoutedEventArgs e)
        {

            _tourController.ShowAll(_tours);
        }

        private void Button_Click_Cancel_Search(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_Book(object sender, RoutedEventArgs e)
        {
            if (ChosenTour != null)
            {
                ReservationTourView reservationTourView = new ReservationTourView(ChosenTour, GuestId);
                reservationTourView.Show();
            }
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
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
        public List <DateTime> StartingTime
        {
            get => _startingTime;
            set
            {
                _startingTime = value;
                OnPropertyChanged();
            }
        }
        private void Button_Click_LogOut(object sender, RoutedEventArgs e)
        {
            User.Id = GuestId;
            User.IsLoggedIn = false;
            SignInForm signInForm = new SignInForm();
            signInForm.ShowDialog();
        }
    }
}