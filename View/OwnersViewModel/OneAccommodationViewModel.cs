using BookingProject.Commands;
using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.View.OwnersView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace BookingProject.View.OwnersViewModel
{
    public class OneAccommodationViewModel : INotifyPropertyChanged
    { 
        public Accommodation SelectedAccommodation { get; set; }
        public NavigationService NavigationService { get; set; }
        public RelayCommand BackCommand { get; set; }
        public RelayCommand BookingCommand { get; set; }

        public OneAccommodationViewModel(Accommodation selectedAccommodation, NavigationService navigationService)
        {
            SelectedAccommodation = selectedAccommodation;
            NavigationService = navigationService;
            BackCommand = new RelayCommand(Button_Click_Back, CanExecute);
            BookingCommand = new RelayCommand(Button_Click_Calendar, CanExecute);
        }

        private bool CanExecute(object param) { return true; }

        private void Button_Click_Back(object param)
        {
            NavigationService.GoBack();
        }
        private void Button_Click_Calendar(object param)
        {
            NavigationService.Navigate(new BookingCalendarView(SelectedAccommodation, NavigationService));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
    }
}
