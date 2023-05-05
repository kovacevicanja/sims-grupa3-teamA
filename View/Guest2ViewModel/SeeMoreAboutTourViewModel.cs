using BookingProject.Commands;
using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.View.Guest2View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingProject.View.Guest2ViewModel
{
    public class SeeMoreAboutTourViewModel : INotifyPropertyChanged
    {
        public Tour ChosenTour { get; set; }
        public int GuestId { get; set; }
        public User User { get; set; }  
        public RelayCommand NextCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand LogOutCommand { get; set; }
        public RelayCommand BookTourCommand { get; set; }
        public string DurationDisplay { get; set; }
        public string LanguageDisplay { get; set; }

        public SeeMoreAboutTourViewModel(Tour chosenTour, int guestId)
        {
            ChosenTour = chosenTour;
            GuestId = guestId;
            User = new User();

            CancelCommand = new RelayCommand(Button_Click_Cancel, CanExecute);
            LogOutCommand = new RelayCommand(Button_Click_LogOut, CanExecute);
            BookTourCommand = new RelayCommand(Button_Click_BookTour, CanExecute);

            DurationDisplay = ChosenTour.DurationInHours.ToString() + " h";
            string language = ChosenTour.Language.ToString();
            LanguageDisplay = char.ToUpper(language[0]) + language.Substring(1).ToLower();
        }

        private void Button_Click_LogOut(object param)
        {
            User.Id = GuestId;
            User.IsLoggedIn = false;
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            CloseWindow();
        }

        private void Button_Click_BookTour(object param)
        {
            ReservationTourView reservationTourView = new ReservationTourView(ChosenTour, GuestId);
            reservationTourView.Show();
            CloseWindow();
        }

        private void Button_Click_Cancel(object param)
        {
            SerachAndReservationToursView searchAndReservation = new SerachAndReservationToursView(GuestId);
            searchAndReservation.Show();
            CloseWindow();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _currentImageIndex = 0;

        public int CurrentImageIndex
        {
            get => _currentImageIndex;
            set
            {
                _currentImageIndex = value;
                OnPropertyChanged(nameof(CurrentImage));
                OnPropertyChanged(nameof(CanMoveToPreviousImage));
                OnPropertyChanged(nameof(CanMoveToNextImage));
            }
        }

        public TourImage CurrentImage => ChosenTour.Images[CurrentImageIndex];

        public bool CanMoveToPreviousImage => CurrentImageIndex > 0;

        public bool CanMoveToNextImage => CurrentImageIndex < ChosenTour.Images.Count - 1;

        public ICommand MoveToPreviousImageCommand => new RelayCommand(MoveToPreviousImage);

        private void MoveToPreviousImage(object param)
        {
            if (CanMoveToPreviousImage)
            {
                CurrentImageIndex--;
            }
        }

        public ICommand MoveToNextImageCommand => new RelayCommand(MoveToNextImage);

        private void MoveToNextImage(object param)
        {
            if (CanMoveToNextImage)
            {
                CurrentImageIndex++;
            }
        }

        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(SeeMoreAboutTourView)) { window.Close(); }
            }
        }

        private bool CanExecute(object param) { return true; }
    }
}
