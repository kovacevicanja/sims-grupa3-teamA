using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Implementations;
using BookingProject.View.OwnersView;
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
    public class AccommodationRenovationsViewModel
    {
        public AccommodationController _accommodationController { get; set; }

        public AccommodationRenovationController _renovationController { get; set; }

        public Action CloseAction { get; set; }
        public RelayCommand CancelRenovationCommand { get; set; }
        public RelayCommand AddRenovationCommand { get; set; }

        public AccommodationRenovation SelectedRenovation { get; set; }
        public static ObservableCollection<Accommodation> Accommodations { get; set; }
        public static List<Location> Locations { get; set; }
        public NavigationService NavigationService { get; set; }

        private ObservableCollection<AccommodationRenovation> _lastRenovations;
        public ObservableCollection<AccommodationRenovation> LastRenovations
        {
            get => _lastRenovations;
            set
            {
                if (value != _lastRenovations)
                {
                    _lastRenovations = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<AccommodationRenovation> _futureRenovations;
        public ObservableCollection<AccommodationRenovation> FutureRenovations
        {
            get => _futureRenovations;
            set
            {
                if (value != _futureRenovations)
                {
                    _futureRenovations = value;
                    OnPropertyChanged();
                }
            }
        }

        public AccommodationRenovationsViewModel(NavigationService navigationService)
        {
            _accommodationController = new AccommodationController();
            _renovationController = new AccommodationRenovationController();

            List<AccommodationRenovation> lastRenovations = _renovationController.GetRenovationsInPast();
            LastRenovations = new ObservableCollection<AccommodationRenovation>(_accommodationController.GetAccommodationData(lastRenovations));
            List<AccommodationRenovation> futureRenovations = _renovationController.GetRenovationsInFuture();
            FutureRenovations = new ObservableCollection<AccommodationRenovation>(_accommodationController.GetAccommodationData(futureRenovations));
            CancelRenovationCommand = new RelayCommand(Button_Click_Cancel_Renovation, CanExecute);
            AddRenovationCommand = new RelayCommand(Button_Click_Add, CanExecute);
            NavigationService = navigationService;
        }

        private void Button_Click_Add(object param)
        {
            //NavigationService.Navigate(new EnterAccommodationRenovationDatesView(NavigationService));
        }

        private void Button_Click_Cancel_Renovation(object param)
        {
            if (SelectedRenovation != null)
            {
                TimeSpan dayDifference = SelectedRenovation.StartDate - DateTime.Today;
                if (dayDifference.Days > 5)
                {
                    _renovationController.Delete(SelectedRenovation);

                    this.FutureRenovations.Remove(SelectedRenovation);
                }
                else
                {
                    MessageBox.Show("Otkazivanje nije moguce!\n Do pocetka renoviranja ima manje od 5 dana.");
                }
            }
            else
            {
                MessageBox.Show("Morate izabrati renoviranje koje zelite da otkazete!");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool CanExecute(object param)
        {
            return true;
        }
    }
}
