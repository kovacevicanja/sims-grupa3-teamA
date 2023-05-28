using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.View.CustomMessageBoxes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace BookingProject.View.OwnerViewModel
{
    public class GuestRateViewModel
    {
        public GuestGradeController GradeController { get; set; }
        public ObservableCollection<GuestGrade> Grades { get; set; }
        public object SelectedObject { get; set; }
        private AccommodationReservation _selectedReservation;
        public ObservableCollection<int> CleanlinessOption { get; set; }
        public int ChosenCleanliness { get; set; }
        public ObservableCollection<int> CommunicationOption { get; set; }
        public int ChosenCommunication { get; set; }
        public ObservableCollection<int> ObservanceOption { get; set; }
        public int ChosenObservance { get; set; }
        public ObservableCollection<int> DecencyOption { get; set; }
        public int ChosenDecency { get; set; }
        public ObservableCollection<int> NoisinessOption { get; set; }
        public int ChosenNoisiness { get; set; }
        public RelayCommand RateCommand { get; }
        public RelayCommand MenuCommand { get; }
        public RelayCommand BackCommand { get; }
        public NavigationService NavigationService { get; set; }
        public OwnerNotificationCustomBox box { get; set; }
        public GuestRateViewModel(AccommodationReservation selectedReservation, NavigationService navigationService)
        {
            GradeController = new GuestGradeController();
            _selectedReservation = new AccommodationReservation();
            _selectedReservation = selectedReservation;
            box = new OwnerNotificationCustomBox();
            CleanlinessOption = new ObservableCollection<int>();
            CommunicationOption = new ObservableCollection<int>();
            ObservanceOption = new ObservableCollection<int>();
            DecencyOption = new ObservableCollection<int>();
            NoisinessOption = new ObservableCollection<int>();
            for (int i = 1; i <= 5; i++)
            {
                CleanlinessOption.Add(i);
                CommunicationOption.Add(i);
                ObservanceOption.Add(i);
                DecencyOption.Add(i);
                NoisinessOption.Add(i);
            }
            RateCommand = new RelayCommand(Button_Click_Rate, CanExecute);
            MenuCommand = new RelayCommand(Button_Click_Menu, CanExecute);
            BackCommand = new RelayCommand(Button_Click_Back, CanExecute);
            NavigationService = navigationService;
        }
        private bool CanExecute(object param) { return true; }

        public AccommodationReservation SelectedReservation
        {
            get { return _selectedReservation; }
            set { _selectedReservation = value; OnPropertyChanged(); }
        }
        private void Button_Click_Menu(object param)
        {
            MenuView view = new MenuView();
            view.Show();
            CloseWindow();
        }
        private void Button_Click_Back(object param)
        {
            //var view = new NotGradedView();
            //view.Show();
            //CloseWindow();
            NavigationService.GoBack();
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

        private int _communication;

        public int Communication
        {
            get => _communication;
            set
            {
                if (value != _communication)
                {
                    _communication = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _observance;

        public int ObservanceOfRules
        {
            get => _observance;
            set
            {
                if (value != _observance)
                {
                    _observance = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _decency;
        public int Decency
        {
            get => _decency;
            set
            {
                if (value != _decency)
                {
                    _decency = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _noisiness;
        public int Noisiness
        {
            get => _noisiness;
            set
            {
                if (value != _noisiness)
                {
                    _noisiness = value;
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
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void Button_Click_Rate(object param)
        {
            if(ChosenCleanliness ==0 || ChosenCommunication==0 || ChosenDecency==0 || ChosenNoisiness==0 || ChosenObservance==0)
            {
                box.ShowCustomMessageBox("You must select one of dropdown options for grade!");
                return;
            }
            GuestGrade grade = new GuestGrade();
            grade.Cleanliness = ChosenCleanliness;
            grade.Communication = ChosenCommunication;
            grade.ObservanceOfRules = ChosenObservance;
            grade.Decency = ChosenDecency;
            grade.Noisiness = ChosenNoisiness;
            grade.Comment = Comment;
            grade.AccommodationReservation.Id = _selectedReservation.Id;

            GradeController.Create(grade);
            box.ShowCustomMessageBox("You have rated a guest!");
            //var view = new NotGradedView();
            //view.Show();



            //CloseWindow();
            NavigationService.Navigate(new NotGradedView(NavigationService));

        }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(GuestRateView)) { window.Close(); }
            }
        }
    }
}
