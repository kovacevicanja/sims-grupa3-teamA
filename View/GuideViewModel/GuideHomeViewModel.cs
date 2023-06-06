using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Styles;
using BookingProject.View.GuideView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using BookingProject.Localization;
using System.Globalization;
using System.Threading;

namespace BookingProject.View.GuideViewModel
{
    public class GuideHomeViewModel : INotifyPropertyChanged
    {
        public static string sep = System.IO.Path.DirectorySeparatorChar.ToString();
        private App app;
        private readonly UserController _userController;
        public string GuideName { get; }
        public double GuideRating { get; }
        public RelayCommand LogoutCommand { get; }
        public RelayCommand OneCommand { get; }
        public RelayCommand TwoCommand { get; }
        public RelayCommand ThreeCommand { get; }
        public RelayCommand FourCommand { get; }
        public RelayCommand FiveCommand { get; }

        public bool IsLightStyle;

        public RelayCommand SuggestionCommand { get; }
        public RelayCommand ModeCommand { get; }
        public RelayCommand CreateCommand { get; }

        public RelayCommand SerbianCommand { get; }
        public RelayCommand EnglishCommand { get; }
        public RelayCommand ResCommand { get; }
        public GuideHomeViewModel()
        {

            Messenger.Default.Register<ChangeModeMessage>(this, OnModeChangeMessageReceived);
            _userController = new UserController();
            LogoutCommand = new RelayCommand(Button_Click_Logout, CanExecute);
            OneCommand = new RelayCommand(Button_Click_1, CanExecute);
            TwoCommand = new RelayCommand(Button_Click_2, CanExecute);
            ThreeCommand = new RelayCommand(Button_Click_3, CanExecute);
            FourCommand = new RelayCommand(Button_Click_4, CanExecute);
            FiveCommand = new RelayCommand(Button_Click_5, CanExecute);
            SuggestionCommand = new RelayCommand(Button_Click_S, CanExecute);
            CreateCommand = new RelayCommand(Button_Click_N, CanExecute);
            ModeCommand = new RelayCommand(Button_Click_M, CanExecute);
            SerbianCommand = new RelayCommand(Button_Click_X, CanExecute);
            EnglishCommand = new RelayCommand(Button_Click_Y, CanExecute);
            ResCommand = new RelayCommand(Button_Click_R, CanExecute);
            GuideRating = 5.5;
            GuideName = _userController.GetLoggedUser().Name;

            this.app = System.Windows.Application.Current as App;
            this.app = (App)System.Windows.Application.Current;
            this.CurrentLanguage = "en-US";
            // lang = SRB;
        }
        private bool _isDarkMode;
        public bool IsDarkMode
        {
            get { return _isDarkMode; }
            set
            {
                if (_isDarkMode != value)
                {
                    _isDarkMode = value;
                    OnPropertyChanged(nameof(IsDarkMode));

                    if (_isDarkMode)
                    {
                        StyleManager.ApplyDarkStyle();
                    }
                    else
                    {
                        StyleManager.ApplyLightStyle();
                    }

                    // Send a message to other windows to update their styles
                    Messenger.Default.Send(new ChangeModeMessage(_isDarkMode));
                }
            }
        }

        private void OnModeChangeMessageReceived(ChangeModeMessage message)
        {
            // Update the IsDarkMode property based on the received message
            IsDarkMode = message.IsDarkMode;
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        private string currentLanguage;
        public string CurrentLanguage
        {
            get { return currentLanguage; }
            set
            {
                currentLanguage = value;
            }
        }
        private void Button_Click_4(object param)
        {
            AllTourRequestsView allTourRequestsView = new AllTourRequestsView();
            allTourRequestsView.Show();
            CloseWindow();
        }

        private void Button_Click_5(object param)
        {
            RequestStatisticsView view = new RequestStatisticsView();
            view.Show();
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
            TourCreationWindow view = new TourCreationWindow(false, false);
            view.Show();
            CloseWindow();
        }
        private void Button_Click_M(object param)
        {
            StyleManager.ApplyDarkStyle();
            IsDarkMode = !IsDarkMode;
            App.MessagingService.PublishModeChange(IsDarkMode);

        }
        private void Button_Click_X(object param)
        {

            CurrentLanguage = "sr-LATN";
            app.ChangeLanguage(CurrentLanguage);


        }

        private void Button_Click_Y(object param)
        {

            CurrentLanguage = "en-US";
            app.ChangeLanguage(CurrentLanguage);
          
        }

        private void Button_Click_R(object param)
        {

            TourResignationWindow window = new TourResignationWindow();
            window.Show();
            CloseWindow();
        }   

    }
}

