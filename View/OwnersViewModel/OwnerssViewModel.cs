using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.View.CustomMessageBoxes;
using BookingProject.View.OwnersView;
using BookingProject.View.OwnerView;
using BookingProject.View.OwnerViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Navigation;

namespace BookingProject.View
{
    public class OwnerssViewModel
    {
        private AccommodationController _accommodationController;
        public AccommodationOwnerGradeController _accommodationOwnerGradeController;
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public UserController _userController { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public RelayCommand AddAccommodationCommand { get; }
        public RelayCommand RateGuestsCommand { get; }
        public RelayCommand RequestsCommand { get; }
        public RelayCommand GuestGradesCommand { get; }
        public RelayCommand LogoutCommand { get; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand StatisticsCommand { get; }
        public RelayCommand RenovationsCommand { get; }
        public NavigationService NavigationService { get; set; }
        public OwnerNotificationCustomBox box { get; set; }
        public OwnerssViewModel(NavigationService navigationService)
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
            StatisticsCommand = new RelayCommand(Button_Click_Statistics, CanExecute);
            RenovationsCommand = new RelayCommand(Button_Click_Renovations, CanExecute);
            box = new OwnerNotificationCustomBox();
            NavigationService = navigationService;
            
        }
        private bool CanExecute(object param) { return true; }
        private void Button_Click_Add(object param)
        {
            NavigationService.Navigate(new AddAccommodationView(NavigationService));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void Button_Click_Renovations(object param)
        {
            //if (SelectedAccommodation == null)
            //{
            //    return;
            //}
            //var view = new EnterAccommodationRenovationDatesView(SelectedAccommodation, NavigationService);
            //view.Show();
            //CloseWindow();
        }
        private void Button_Click_Statistics(object param)
        {
            //if (SelectedAccommodation != null)
            //{
            //    var view = new AccommodationStatisticsByYearView(SelectedAccommodation, NavigationService);
            //    view.Show();
            //    CloseWindow();
            //}
        }
        private void Button_Click_Rate(object param)
        {
            //NotGradedView view = new NotGradedView(NavigationService);
            //view.Show();
            //CloseWindow();
        }
        private void Button_Click_Request(object param)
        {
            NavigationService.Navigate( new OwnersRequestView(NavigationService));

        }
        private void Button_Click_Close(object param)
        {
            NavigationService.Navigate(new AddAccommodationView(NavigationService));
        }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(OwnerssView)) { window.Close(); }
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click_Review(object param)
        {
            //GuestGradesForOwnerView view = new GuestGradesForOwnerView();
            //view.Show();
            //CloseWindow();
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
