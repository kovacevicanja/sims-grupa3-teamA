using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Model;
using BookingProject.Services.Implementations;
using BookingProject.View.CustomMessageBoxes;
using BookingProject.View.OwnersView;
using BookingProject.View.OwnerView;
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

namespace BookingProject.View.OwnersViewModel
{
    public class EnterAccommodationRenovationDatesViewModel
    {
        public AccommodationRenovationController _renovationController { get; set; }
        public AccommodationReservationController _reservationController { get; set; }
        public RelayCommand ShowCommand { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public NavigationService NavigationService { get; set; }
        public OwnerNotificationCustomBox box { get; set; }
        public RelayCommand BackCommand
        {
            get; set;
        }

        public EnterAccommodationRenovationDatesViewModel(Accommodation selectedAccommodation, NavigationService navigationService)
        {
            SelectedAccommodation=selectedAccommodation;
            AvailableDatesPair = new ObservableCollection<Tuple<DateTime, DateTime>>();
            _renovationController = new AccommodationRenovationController();
            _reservationController = new AccommodationReservationController();
            ShowCommand = new RelayCommand(Button_Click_Show, CanExecute);
            box = new OwnerNotificationCustomBox();
            BackCommand = new RelayCommand(Button_Click_Back, CanExecute);
            NavigationService = navigationService;
        }
        private void Button_Click_Back(object param)
        {
            NavigationService.GoBack();
        }
        private bool CanExecute(object param) { return true; }
        int renovationDuration;
        private void Button_Click_Show(object param)
        {
            if (StartDate == DateTime.MinValue)
            {
                box.ShowCustomMessageBox("You must enter start date!");
                return;
            }
            if (EndDate == DateTime.MinValue)
            {
                box.ShowCustomMessageBox("You must enter end date!");
                return;
            }
            if (!int.TryParse(RenovationDuration.ToString(), out renovationDuration))
            {
                box.ShowCustomMessageBox("Renovation duration has to be a integer!");
                return;
            }
            if (RenovationDuration == null)
            {
                box.ShowCustomMessageBox("You must enter expected duration!");
                return;
            }
            if (StartDate>EndDate)
            {
                box.ShowCustomMessageBox("End date must be after start date!");
                return;
            }
            if(RenovationDuration <= 0)
            {
                box.ShowCustomMessageBox("Renovation duration has to be a positive number!");
                return;
            }
            if(!int.TryParse(RenovationDuration.ToString(), out renovationDuration))
            {
                box.ShowCustomMessageBox("Renovation duration has to be a integer!");
                return;
            }
            //AvailableDatesPair = new ObservableCollection<Tuple<DateTime, DateTime>>(_renovationService.FindAvailableDates(StartDate, EndDate, RenovationDuration, SelectedAccommodation));
            AvailableDatesPair = new ObservableCollection<Tuple<DateTime, DateTime>>(_reservationController.FindAvailableDates(StartDate, EndDate, (int)RenovationDuration, SelectedAccommodation));
            //var view = new AvailableDatesForAccommodationRenovationView(SelectedAccommodation, AvailableDatesPair);
            //view.Show();
            //CloseWindow();
            NavigationService.Navigate(new AvailableDatesForAccommodationRenovationView(SelectedAccommodation, AvailableDatesPair, NavigationService));
        }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(EnterAccommodationRenovationDatesView)) { window.Close(); }
            }
        }
        private ObservableCollection<Tuple<DateTime, DateTime>> _availableDatesPair;
        public ObservableCollection<Tuple<DateTime, DateTime>> AvailableDatesPair
        {
            get => _availableDatesPair;
            set
            {
                if (value != _availableDatesPair)
                {
                    _availableDatesPair = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (value != _endDate)
                {
                    _endDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private int? _renovationDuration;
        public int? RenovationDuration
        {
            get => _renovationDuration;
            set
            {
                if (value != _renovationDuration)
                {
                    _renovationDuration = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
