using BookingProject.Commands;
using BookingProject.Model;
using BookingProject.Model.Images;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace BookingProject.View.Guest2ViewModel
{
    public class ShowGalleryViewModel : INotifyPropertyChanged
    {
        public RelayCommand CancelCommand { get; set; }
        public Tour ChosenTour { get; set;  } 
        public NavigationService NavigationService { get; set; }
        public ShowGalleryViewModel(Tour chosenTour, NavigationService navigationService)
        {
            ChosenTour = chosenTour;
            CancelCommand = new RelayCommand(Button_Click_Cancel, CanExecute);
            NavigationService = navigationService;
        }

        private bool CanExecute(object param) { return true; }

        private void Button_Click_Cancel(object param)
        {
            NavigationService.GoBack();
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

        public TourImage CurrentImage => ChosenTour.Images[CurrentImageIndex];

        public bool CanMoveToPreviousImage => CurrentImageIndex > 0;

        public bool CanMoveToNextImage => CurrentImageIndex < ChosenTour.Images.Count - 1;

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
