using BookingProject.Commands;
using BookingProject.Model;
using BookingProject.View.Guest1View;
using BookingProject.View.Guest1View.Tutorials;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static BookingProject.View.Guest1ViewModel.QuickSearchViewModel;

namespace BookingProject.View.Guest1ViewModel
{
	public class QuickSearchSuggestionsViewModel : INotifyPropertyChanged
	{
		public ObservableCollection<AccommodationDTO> DTOs { get; set; }
        public AccommodationDTO SelectedDTO { get; set; }
		public RelayCommand HomePageCommand { get; }
		public RelayCommand MyReservationsCommand { get; }
		public RelayCommand LogOutCommand { get; }
		public RelayCommand MyReviewsCommand { get; }
		public RelayCommand MyProfileCommand { get; }
		public RelayCommand TutorialCommand { get; }
		public RelayCommand CreateForumCommand { get; }
		public RelayCommand QuickSearchCommand { get; }
        public RelayCommand ViewAvailableDatesCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
		public QuickSearchSuggestionsViewModel(List<AccommodationDTO> dtos)
		{
			DTOs = new ObservableCollection<AccommodationDTO>(dtos);
			HomePageCommand = new RelayCommand(Button_Click_Homepage, CanExecute);
			MyReservationsCommand = new RelayCommand(Button_Click_MyReservations, CanExecute);
			LogOutCommand = new RelayCommand(Button_Click_Logout, CanExecute);
			MyReviewsCommand = new RelayCommand(Button_Click_MyReviews, CanExecute);
			MyProfileCommand = new RelayCommand(Button_Click_MyProfile, CanExecute);
			TutorialCommand = new RelayCommand(ButtonClick_Tutorial, CanExecute);
			CreateForumCommand = new RelayCommand(Button_Click_CreateForum, CanExecute);
			QuickSearchCommand = new RelayCommand(Button_Click_Quick_Search, CanExecute);
            ViewAvailableDatesCommand = new RelayCommand(Button_Click_ViewAvailableDates, CanExecute);

        }


		private bool CanExecute(object param) { return true; }
		private void CloseWindow()
		{
			foreach (Window window in App.Current.Windows)
			{
				if (window.GetType() == typeof(QuickSearchSuggestionsView)) { window.Close(); }
			}
		}
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

        private void Button_Click_Homepage(object param)
        {
            var Guest1Homepage = new Guest1HomepageView();
            Guest1Homepage.Show();
            CloseWindow();
        }

        private void Button_Click_MyReservations(object param)
        {
            var Guest1Reservations = new Guest1Reservations();
            Guest1Reservations.Show();
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

        private void ButtonClick_Tutorial(object param)
        {
            var tutorial = new MyProfileTutorialView();
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

        private void Button_Click_ViewAvailableDates(object param)
		{
            var availableDates = new ShowSuggestionsDatesView(SelectedDTO);
            availableDates.Show();
            CloseWindow();
		}
    }
}
