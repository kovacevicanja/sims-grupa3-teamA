using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.DependencyInjection;
using BookingProject.Domain.Images;
using BookingProject.Model;
using BookingProject.Repositories.Intefaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.View
{
    public class AccommodationOwnerReviewViewModel
    {
        public AccommodationOwnerGradeController AccommodationOwnerGradeController { get; set; }
        public ObservableCollection<AccommodationOwnerGrade> Grades { get; set; }
        private AccommodationReservation _selectedReservation;
        public AccommodationImageController AccommodationImageController { get; set; }
        public AccommodationGuestImageController AccommodationGuestImageController { get; set; }
        public UserController UserController { get; set; }
        public AccommodationOwnerGrade grade;
        public ObservableCollection<int> CleanlinessOption { get; set; }
        public int chosenCleanliness { get; set; }
        public ObservableCollection<int> CorectnessOption { get; set; }
        public int chosenCorectness { get; set; }
        public string UrlPicture { get; set; }
        public RelayCommand HomePageCommand { get; }
        public RelayCommand LogOutCommand { get; }
        public RelayCommand MyReservationsCommand { get; }
        public RelayCommand AddPictureCommand { get; }
        public RelayCommand ReviewCommand { get; }
        public AccommodationOwnerReviewViewModel(AccommodationReservation selectedReservation)
        {
            AccommodationOwnerGradeController = new AccommodationOwnerGradeController();
            AccommodationImageController = new AccommodationImageController();
            AccommodationGuestImageController = new AccommodationGuestImageController();
            UserController = new UserController();
            grade = new AccommodationOwnerGrade();
            grade.Id = Injector.CreateInstance<IAccommodationOwnerGradeRepository>().GenerateId();
            _selectedReservation = new AccommodationReservation();
            _selectedReservation = selectedReservation;
            CleanlinessOption = new ObservableCollection<int>();
            CorectnessOption = new ObservableCollection<int>();
            fillOptions(CleanlinessOption, CorectnessOption);

            HomePageCommand = new RelayCommand(Button_Click_Homepage, CanExecute);
            LogOutCommand = new RelayCommand(Button_Click_Logout, CanExecute);
            MyReservationsCommand = new RelayCommand(Button_Click_MyReservations, CanExecute);
            AddPictureCommand = new RelayCommand(Button_Click_AddPicture, CanExecute);
            ReviewCommand = new RelayCommand(Button_Click_Review, CanExecute);

        }
        private bool CanExecute(object param) { return true; }

        private void fillOptions(ObservableCollection<int> CleanlinessOption, ObservableCollection<int> CorectnessOption)
        {
            for (int i = 1; i < 6; i++)
            {
                CleanlinessOption.Add(i);
                CorectnessOption.Add(i);
            }
        }

        public AccommodationReservation SelectedReservation
        {
            get
            {
                return _selectedReservation;
            }
            set
            {
                _selectedReservation = value;
                OnPropertyChanged();
            }
        }

        private int _cleanliness;
        public int Cleanliness
        {
            get => _cleanliness;
            set
            {
                if (value != _cleanliness)
                {
                    _cleanliness = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _corectness;
        public int Corectness
        {
            get => _corectness;
            set
            {
                if (value != _corectness)
                {
                    _corectness = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _url;
        public string Url
        {
            get => _url;
            set
            {
                if (value != _url)
                {
                    _url = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _reccommendation;
        public string Reccommendation
        {
            get => _reccommendation;
            set
            {
                if (value != _reccommendation)
                {
                    _reccommendation = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click_Review(object param)
        {
            AccommodationOwnerGradeController.MakeGrade(grade, _selectedReservation, chosenCleanliness, chosenCorectness, Comment, Reccommendation);
            CloseWindow(); //ovo je umesto this.Close()
        }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(AccommodationOwnerReview)) { window.Close(); }
            }
        }

        private void Button_Click_AddPicture(object param)
        {
            if (UrlPicture != "")
            {
                AccommodationGuestImage Picture = new AccommodationGuestImage();
                Picture.Url = UrlPicture;
                Picture.Guest.Id = UserController.GetLoggedUser().Id;
                Picture.Grade.Id = grade.Id;
                grade.guestImages.Add(Picture);
                AccommodationGuestImageController.Create(Picture);
                AccommodationGuestImageController.SaveImage();
            }
            else
            {
                MessageBox.Show("Photo url can not be empty");
            }

            UrlPicture = string.Empty;
        }

        private void Button_Click_Homepage(object param)
        {
            var Guest1Homepage = new Guest1Homepage();
            Guest1Homepage.Show();
            CloseWindow();
        }

        private void Button_Click_MyReservations(object param)
        {
            var Guest1Reservations = new Guest1Reservations();
            Guest1Reservations.Show();
            CloseWindow();
        }

        private void Button_Click_Logout(object param)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            CloseWindow();
        }
    }
}
