using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.View.GuideView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.View.GuideViewModel
{
    public class GuideHomeViewModel
    {
        private readonly UserController _userController;
        public string GuideName { get; }
        public double GuideRating{ get; }
        public RelayCommand LogoutCommand { get; }
        public RelayCommand OneCommand { get; }
        public RelayCommand TwoCommand { get; }
        public RelayCommand ThreeCommand { get; }
        public RelayCommand FourCommand { get; }
        public RelayCommand FiveCommand { get; }

        public RelayCommand SuggestionCommand { get; }
        public RelayCommand CreateCommand { get; }

        public GuideHomeViewModel()
        {
            _userController = new UserController();
            LogoutCommand = new RelayCommand(Button_Click_Logout, CanExecute);
            OneCommand = new RelayCommand(Button_Click_1, CanExecute);
            TwoCommand = new RelayCommand(Button_Click_2, CanExecute);
            ThreeCommand = new RelayCommand(Button_Click_3, CanExecute);
            FourCommand = new RelayCommand(Button_Click_4, CanExecute);
            FiveCommand = new RelayCommand(Button_Click_5, CanExecute);
            SuggestionCommand = new RelayCommand(Button_Click_S, CanExecute);
            CreateCommand = new RelayCommand(Button_Click_N, CanExecute);
            GuideRating = 5.5;
            GuideName= _userController.GetLoggedUser().Name;

        }
        private void Button_Click_1(object param)
        {
            MyToursWindow myToursWindow = new MyToursWindow();
            myToursWindow.Show();
            CloseWindow();
        }
        public void LogoutUser()
        {
            _userController.GetLoggedUser().IsLoggedIn = false;
            _userController.Save();
        }

        private bool CanExecute(object param) { return true; }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(GuideHomeWindow)) { window.Close(); }
            }
        }

        private void Button_Click_Logout(object param)
        {
            LogoutUser();
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            CloseWindow();
        }

        private void Button_Click_2(object param)
        {
            TourStatisticsWindow tourStatisticsWindow = new TourStatisticsWindow("all");
            tourStatisticsWindow.Show();
            CloseWindow();
        }
        private void Button_Click_3(object param)
        {
            LiveToursList liveTourList = new LiveToursList();
            liveTourList.Show();
            CloseWindow();
        }


        private void Button_Click_4(object param)
        {
            AllTourRequestsView allTourRequestsView = new AllTourRequestsView();
            allTourRequestsView.Show();
            CloseWindow();
        }

        private void Button_Click_5(object param)
        {
            RequestStatisticsChoiceView choiceView = new RequestStatisticsChoiceView();
            choiceView.Show();
            CloseWindow();
        }

        private void Button_Click_S(object param)
        {
            SuggestionChoiceView choiceView = new SuggestionChoiceView();
            choiceView.Show();
            CloseWindow();
        }
        private void Button_Click_N(object param)
        {
            TourCreationWindow view = new TourCreationWindow();
            view.Show();
            CloseWindow();

        }
    }
}
