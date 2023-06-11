using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OisisiProjekat.Observer;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using BookingProject.View.Guest1View;
using BookingProject.View.Guest1View.Tutorials;

namespace BookingProject.View.Guest1ViewModel
{
	public class QuickSearchViewModel : INotifyPropertyChanged
    {
        private AccommodationController _accommodationController;
        private AccommodationDateController _accommodationDateController;
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation selectedAccommodation { get; set; }
        public ObservableCollection<Accommodation> FilteredAccommodations { get; set; }

        public RelayCommand SearchCommand { get; }
        public RelayCommand BookCommand { get; }
        public RelayCommand CancelSearchCommand { get; }
        public RelayCommand HomePageCommand { get; }
        public RelayCommand MyReservationsCommand { get; }
        public RelayCommand LogOutCommand { get; }
        public RelayCommand MyReviewsCommand { get; }
        public RelayCommand MyProfileCommand { get; }
        public RelayCommand ImagesMoreCommand { get; }
        public RelayCommand ViewTutorialCommand { get; }
        public RelayCommand CreateForumCommand { get; }
        public RelayCommand QuickSearchCommand { get; }
        public QuickSearchViewModel()
        {
            _accommodationController = new AccommodationController();
            _accommodationDateController = new AccommodationDateController();
            FilteredAccommodations = new ObservableCollection<Accommodation>();
            List<Accommodation> accommodations = new List<Accommodation>(_accommodationController.GetAll());
            List<Accommodation> sortedAccommodations = accommodations.OrderByDescending(a => a.Owner.IsSuper).ToList();
            Accommodations = new ObservableCollection<Accommodation>(sortedAccommodations);

            SearchCommand = new RelayCommand(Button_Click_Search, CanExecute);
            BookCommand = new RelayCommand(Button_Click_Book, CanIfSelected);
            CancelSearchCommand = new RelayCommand(Button_Click_Cancel_Search, CanExecute);
            HomePageCommand = new RelayCommand(Button_Click_Homepage, CanExecute);
            MyReservationsCommand = new RelayCommand(Button_Click_MyReservations, CanExecute);
            LogOutCommand = new RelayCommand(Button_Click_Logout, CanExecute);
            MyReviewsCommand = new RelayCommand(Button_Click_MyReviews, CanExecute);
            MyProfileCommand = new RelayCommand(Button_Click_MyProfile, CanExecute);
            ImagesMoreCommand = new RelayCommand(Button_Click_ImagesAndMore, CanIfSelected);
            ViewTutorialCommand = new RelayCommand(Button_Click_ViewTutorial, CanExecute);
            CreateForumCommand = new RelayCommand(Button_Click_CreateForum, CanExecute);
            QuickSearchCommand = new RelayCommand(Button_Click_Quick_Search, CanExecute);
        }

        public DateTime _initialDate;
        public DateTime InitialDate
        {
            get => _initialDate;
            set
            {
                if (_initialDate != value)
                {
                    _initialDate = value;
                    OnPropertyChanged();
                }
            }
        }


        public DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public string _numberOfGuests;
        public string NumberOfGuests
        {
            get => _numberOfGuests;
            set
            {
                if (_numberOfGuests != value)
                {
                    _numberOfGuests = value;
                    OnPropertyChanged();
                }
            }

        }

        public string _daysToStay;
        public string DaysToStay
        {
            get => _daysToStay;
            set
            {
                if (_daysToStay != value)
                {
                    _daysToStay = value;
                    OnPropertyChanged();
                }
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        private bool CanExecute(object param) { return true; }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(QuickSearchView)) { window.Close(); }
            }
        }

        private bool CanIfSelected(object param)
        {
            if (selectedAccommodation == null) { return false; }
            else { return true; }
        }

        private void Button_Click_MyReservations(object param)
        {
            var res = new Guest1Reservations();
            res.Show();
            CloseWindow();
        }

        private void Button_Click_Search(object param)
        {
            List<AccommodationDTO> accommodationDTOs = new List<AccommodationDTO>();
            if(InitialDate == null && EndDate == null)
			{
                foreach(Accommodation accommodation in Accommodations)
				{
                    if(_accommodationController.CheckGuestsNumber(accommodation, int.Parse(NumberOfGuests)) && _accommodationController.AccommodationIsAvailable(accommodation, int.Parse(DaysToStay))){
                        AccommodationDTO dto = new AccommodationDTO();
                        dto.accommodation = accommodation;
                        List<(DateTime, DateTime)> ranges = _accommodationDateController.FindAvailableDatesQuick(accommodation, int.Parse(DaysToStay));
                        List<DatesDTO> datesList = new List<DatesDTO>();
                        foreach (var range in ranges)
                        {
                            DatesDTO dates = new DatesDTO();
                            dates.InitialDate = range.Item1;
                            dates.EndDate = range.Item2;
                            datesList.Add(dates);
                        }
                        dto.dates = datesList;
                        accommodationDTOs.Add(dto);
                    }
				}
                var suggestions = new QuickSearchSuggestionsView(accommodationDTOs);
			}
			else
			{
                foreach (Accommodation accommodation in Accommodations)
                {
                    if (_accommodationController.CheckGuestsNumber(accommodation, int.Parse(NumberOfGuests)) && _accommodationController.AccommodationIsAvailable(accommodation, int.Parse(DaysToStay)))
                    {
                        AccommodationDTO dto = new AccommodationDTO();
                        dto.accommodation = accommodation;
                        List<(DateTime, DateTime)> ranges = _accommodationDateController.FindAvailableDatesQuickRanges(accommodation, int.Parse(DaysToStay), InitialDate, EndDate);
                        List<DatesDTO> datesList = new List<DatesDTO>();
                        foreach (var range in ranges)
                        {
                            DatesDTO dates = new DatesDTO();
                            dates.InitialDate = range.Item1;
                            dates.EndDate = range.Item2;
                            datesList.Add(dates);
                        }
                        dto.dates = datesList;
                        accommodationDTOs.Add(dto);
                    }
                }
                var suggestions = new QuickSearchSuggestionsView(accommodationDTOs);
            }
        }

        public class AccommodationDTO
		{
            public Accommodation accommodation { get; set; }
            public List<DatesDTO> dates { get; set; }
        }

        public class DatesDTO
		{
            public DateTime InitialDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        private void Button_Click_Book(object param)
        {
            ReservationAccommodationView reservationAccommodationView = new ReservationAccommodationView(selectedAccommodation);
            reservationAccommodationView.Show();
            CloseWindow();
        }

        private void Button_Click_Cancel_Search(object param)
        {
            Accommodations.Clear();
            foreach (Accommodation a in _accommodationController.GetAll())
            {
                Accommodations.Add(a);
            }
        }

        private void Button_Click_Homepage(object param)
        {
            var ghp = new Guest1HomepageView();
            ghp.Show();
            CloseWindow();
        }

        private void Button_Click_Logout(object param)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            CloseWindow();
        }

        private void Button_Click_MyReviews(object param)
        {
            var reviews = new Guest1ReviewsView();
            reviews.Show();
            CloseWindow();
        }
        private void Button_Click_MyProfile(object param)
        {
            var profile = new Guest1ProfileView();
            profile.Show();
            CloseWindow();
        }
        private void Button_Click_ImagesAndMore(object param)
        {
            var imagesAndMore = new ImagesAndMoreGuest1View(selectedAccommodation);
            imagesAndMore.Show();
            CloseWindow();
        }
        private void Button_Click_ViewTutorial(object param)
        {
            var tutorial = new QuickSearchTutorialView();
            tutorial.Show();
            CloseWindow();
        }
        private void Button_Click_CreateForum(object param)
        {
            var forum = new OpenForumView();
            forum.Show();
            CloseWindow();
        }

        private void Button_Click_Quick_Search(object param)
        {
            var quickS = new QuickSearchView();
            quickS.Show();
            CloseWindow();
        }
    }
}
