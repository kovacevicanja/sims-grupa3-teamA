using BookingProject.Commands;
using BookingProject.Model;
using BookingProject.Services.Implementations;
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

namespace BookingProject.View.OwnersViewModel
{
    public class EnterAccommodationRenovationDatesViewModel
    {
        public AccommodationRenovationService _renovationService;
        public RelayCommand ShowCommand { get; set; }
        public Accommodation SelectedAccommodation { get; set; }

        public EnterAccommodationRenovationDatesViewModel(Accommodation selectedAccommodation)
        {
            SelectedAccommodation=selectedAccommodation;
            _renovationService = new AccommodationRenovationService();
            ShowCommand = new RelayCommand(Button_Click_Show, CanExecute);
        }
        private bool CanExecute(object param) { return true; }

        private void Button_Click_Show(object param)
        {
            var view = new AccommodationRenovationsView();
            view.Show();
            CloseWindow();
        }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(EnterAccommodationRenovationDatesView)) { window.Close(); }
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

        private int _renovationDuration;
        public int RenovationDuration
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
