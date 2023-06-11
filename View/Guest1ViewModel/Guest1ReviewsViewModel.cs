using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.View.Guest1View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.View.Guest1ViewModel
{
    public class Guest1ReviewsViewModel
    {
        public ObservableCollection<GuestGrade> Grades { get; set; }
        public GuestGradeController GuestGradeController { get; set; }
        public RelayCommand MyReviewsCommand { get; }
        public RelayCommand HomePageCommand { get; }
        public RelayCommand MyReservationsCommand { get; }
        public RelayCommand LogOutCommand { get; }
        public RelayCommand MyProfileCommand { get; }
        public RelayCommand CreateForumCommand { get; }
        public RelayCommand QuickSearchCommand { get; }
        public Guest1ReviewsViewModel()
        {
            GuestGradeController = new GuestGradeController();
            Grades = new ObservableCollection<GuestGrade>(GuestGradeController.GetSeeableGrades());
            MyReviewsCommand = new RelayCommand(Button_Click_MyReviews, CanExecute);
            HomePageCommand = new RelayCommand(Button_Click_Homepage, CanExecute);
            MyReservationsCommand = new RelayCommand(Button_Click_MyReservations, CanExecute);
            LogOutCommand = new RelayCommand(Button_Click_Logout, CanExecute);
            MyProfileCommand = new RelayCommand(Button_Click_MyProfile, CanExecute);
            CreateForumCommand = new RelayCommand(Button_Click_CreateForum, CanExecute);
            QuickSearchCommand = new RelayCommand(Button_Click_Quick_Search, CanExecute);
        }
        private bool CanExecute(object param) { return true; }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(Guest1ReviewsView)) { window.Close(); }
            }
        }

        private void Button_Click_MyReviews(object param)
        {
            var reviews = new Guest1ReviewsView();
            reviews.Show();
            CloseWindow();
        }

        private void Button_Click_Homepage(object param)
        {
            var Guest1Homepage = new Guest1HomepageView();
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
        private void Button_Click_MyProfile(object param)
        {
            var profile = new Guest1ProfileView();
            profile.Show();
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
