using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.View
{
    public class OwnersViewModel
    {
        private AccommodationController _accommodationController;
        public AccommodationOwnerGradeController _accommodationOwnerGradeController;
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public UserController _userController { get; set; }
        public RelayCommand AddAccommodationCommand { get; }
        public RelayCommand RateGuestsCommand { get; }
        public RelayCommand RequestsCommand { get; }
        public RelayCommand GuestGradesCommand { get; }
        public RelayCommand LogoutCommand { get; }
        public RelayCommand CloseCommand { get; }
        public OwnersViewModel()
        {
            _userController = new UserController();
            _accommodationController = new AccommodationController();
            _accommodationOwnerGradeController = new AccommodationOwnerGradeController();
            if (!_accommodationOwnerGradeController.IsOwnerSuperOwner(SignInForm.LoggedInUser.Id))
            {
                //SuperOwnerImage.Visibility = Visibility.Hidden;
            }
            Accommodations = new ObservableCollection<Accommodation>(_accommodationController.GetAllForOwner(SignInForm.LoggedInUser.Id));
            AddAccommodationCommand = new RelayCommand(Button_Click_Add, CanExecute);
            RateGuestsCommand = new RelayCommand(Button_Click_Rate, CanExecute);
            RequestsCommand = new RelayCommand(Button_Click_Request, CanExecute);
            GuestGradesCommand = new RelayCommand(Button_Click_Review, CanExecute);
            LogoutCommand = new RelayCommand(Button_Click_LogOut, CanExecute);
            CloseCommand = new RelayCommand(Button_Click_Close, CanExecute);
        }
        private bool CanExecute(object param) { return true; }
        private void Button_Click_Add(object param)
        {
            AddAccommodationView addAccommodationsView = new AddAccommodationView();
            addAccommodationsView.Show();
            CloseWindow();
        }
        private void Button_Click_Rate(object param)
        {
            NotGradedView view = new NotGradedView();
            view.Show();
            CloseWindow();
        }
        private void Button_Click_Request(object param)
        {
            OwnersRequestView view = new OwnersRequestView();
            view.Show();
            CloseWindow(); 

        }
        private void Button_Click_Close(object param)
        {
            CloseWindow();
        }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(OwnerView)) { window.Close(); }
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click_Review(object param)
        {
            GuestGradesForOwnerView view = new GuestGradesForOwnerView();
            view.Show();
            CloseWindow();
        }

        private void Button_Click_LogOut(object param)
        {
            LogoutUser();
            SignInForm signInForm = new SignInForm();
            signInForm.ShowDialog();
            CloseWindow();
        }
        public void LogoutUser()
        {
            _userController.GetLoggedUser().IsLoggedIn = false;
            _userController.Save();
        }
    }
}
