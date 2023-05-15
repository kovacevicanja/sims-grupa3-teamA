using BookingProject.Commands;
using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.View.Guest2View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup.Localizer;
using System.Windows.Navigation;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace BookingProject.View.Guest2ViewModel
{
    public class SeeMoreAboutTourViewModel : INotifyPropertyChanged
    {
        public Tour ChosenTour { get; set; }
        public int GuestId { get; set; }
        public User User { get; set; }
        public RelayCommand NextCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand BookTourCommand { get; set; }
        public RelayCommand ViewGalleryCommand { get; set; }
        public string DurationDisplay { get; set; }
        public string LanguageDisplay { get; set; }
        public string PreviousWindow { get; set; }
        public NavigationService NavigationService { get; set; }

        public SeeMoreAboutTourViewModel(Tour chosenTour, int guestId, NavigationService navigationService)
        {
            ChosenTour = chosenTour;
            GuestId = guestId;
            User = new User();

            CancelCommand = new RelayCommand(Button_Click_Cancel, CanExecute);
            BookTourCommand = new RelayCommand(Button_Click_BookTour, CanExecute);
            ViewGalleryCommand = new RelayCommand(Button_Click_ViewGallery, CanExecute);

            DurationDisplay = ChosenTour.DurationInHours.ToString() + "h";
            string language = ChosenTour.Language.ToString();
            LanguageDisplay = char.ToUpper(language[0]) + language.Substring(1).ToLower();

            NavigationService = navigationService;
        }

        private void Button_Click_BookTour(object param)
        {
            NavigationService.Navigate(new ReservationTourView(ChosenTour, GuestId, NavigationService));
        }

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

        private bool CanExecute(object param) { return true; }

        private void Button_Click_ViewGallery(object param)
        {
            NavigationService.Navigate(new ShowGalleryView(ChosenTour, NavigationService));
        }
    }
}