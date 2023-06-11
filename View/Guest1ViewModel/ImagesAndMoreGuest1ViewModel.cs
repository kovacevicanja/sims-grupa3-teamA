using BookingProject.Commands;
using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.View.Guest1View;
using BookingProject.View.Guest1View.Tutorials;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingProject.View.Guest1ViewModel
{
	public class ImagesAndMoreGuest1ViewModel : INotifyPropertyChanged
    {
        public RelayCommand HomePageCommand { get; }
        public RelayCommand MyReservationsCommand { get; }
        public RelayCommand LogOutCommand { get; }
        public RelayCommand MyReviewsCommand { get; }
        public RelayCommand MyProfileCommand { get; }
        public RelayCommand ViewTutorialCommand { get; }
        public Accommodation SelectedAccommodation { get; set; }
        public RelayCommand CreateForumCommand { get; }
        public RelayCommand QuickSearchCommand { get; }
        public ImagesAndMoreGuest1ViewModel(Accommodation selectedAccommodation)
		{
            SelectedAccommodation = selectedAccommodation;
            HomePageCommand = new RelayCommand(Button_Click_Homepage, CanExecute);
            MyReservationsCommand = new RelayCommand(Button_Click_MyReservations, CanExecute);
            LogOutCommand = new RelayCommand(Button_Click_Logout, CanExecute);
            MyReviewsCommand = new RelayCommand(Button_Click_MyReviews, CanExecute);
            MyProfileCommand = new RelayCommand(Button_Click_MyProfile, CanExecute);
            ViewTutorialCommand = new RelayCommand(Button_Click_ViewTutorial, CanExecute);
            CreateForumCommand = new RelayCommand(Button_Click_CreateForum, CanExecute);
            QuickSearchCommand = new RelayCommand(Button_Click_Quick_Search, CanExecute);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private bool CanExecute(object param) { return true; }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(ImagesAndMoreGuest1View)) { window.Close(); }
            }
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

        public AccommodationImage CurrentImage => SelectedAccommodation.Images[CurrentImageIndex];

        public bool CanMoveToPreviousImage => CurrentImageIndex > 0;

        public bool CanMoveToNextImage => CurrentImageIndex < SelectedAccommodation.Images.Count - 1;

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
        private void Button_Click_MyReservations(object param)
        {
            var res = new Guest1Reservations();
            res.Show();
            CloseWindow();
        }
        private void Button_Click_Homepage(object param)
        {
            var ghp = new Guest1HomepageView();
            ghp.Show();
            CloseWindow();
        }

        private void Button_Click_Logout(object param)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            CloseWindow();
        }

        private void Button_Click_MyReviews(object param)
        {
            var reviews = new Guest1ReviewsView();
            reviews.Show();
            CloseWindow();
        }
        private void Button_Click_MyProfile(object param)
        {
            var profile = new Guest1ProfileView();
            profile.Show();
            CloseWindow();
        }
        private void Button_Click_ViewTutorial(object param)
		{
            var tutorial = new ImagesAndMoreTutorialView(SelectedAccommodation);
            tutorial.Show();
            CloseWindow();
		}
        private void Button_Click_CreateForum(object param)
        {
            var forum = new OpenForumView();
            forum.Show();
            CloseWindow();
        }

        private void Button_Click_Quick_Search(object param)
        {
            var quickS = new QuickSearchView();
            quickS.Show();
            CloseWindow();
        }
    }
}
