using BookingProject.Commands;
using BookingProject.Controllers;
using BookingProject.Domain;
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

namespace BookingProject.View.Guest1ViewModel
{
	public class ShowAllForumsViewModel : INotifyPropertyChanged
	{
        public ObservableCollection<Forum> Forums { get; set; }
        public ForumController _forumController;
        public Forum SelectedForum { get; set; }
		public RelayCommand HomePageCommand { get; }
		public RelayCommand MyReservationsCommand { get; }
		public RelayCommand LogOutCommand { get; }
		public RelayCommand MyReviewsCommand { get; }
		public RelayCommand MyProfileCommand { get; }
		public RelayCommand TutorialCommand { get; }
		public RelayCommand CreateForumCommand { get; }
		public RelayCommand QuickSearchCommand { get; }
        public RelayCommand CloseForumCommand { get; }
        public RelayCommand ShowCommentsCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ShowAllForumsViewModel()
		{
            _forumController = new ForumController();
            Forums = new ObservableCollection<Forum>(_forumController.GetAll());
			HomePageCommand = new RelayCommand(Button_Click_Homepage, CanExecute);
			MyReservationsCommand = new RelayCommand(Button_Click_MyReservations, CanExecute);
			LogOutCommand = new RelayCommand(Button_Click_Logout, CanExecute);
			MyReviewsCommand = new RelayCommand(Button_Click_MyReviews, CanExecute);
			MyProfileCommand = new RelayCommand(Button_Click_MyProfile, CanExecute);
			TutorialCommand = new RelayCommand(ButtonClick_Tutorial, CanExecute);
			CreateForumCommand = new RelayCommand(Button_Click_CreateForum, CanExecute);
			QuickSearchCommand = new RelayCommand(Button_Click_Quick_Search, CanExecute);
            CloseForumCommand = new RelayCommand(Button_Click_CloseForum, CanExecute);
            ShowCommentsCommand = new RelayCommand(Button_Click_ShowComments, CanExecute);
            SetDisplayUseful(Forums);
		}

        public void SetDisplayUseful(ObservableCollection<Forum> forums)
		{
            foreach(var forum in forums)
			{
				if (forum.IsUseful)
				{
                    forum.DisplayUseful = "YES";
				}
				else
				{
                    forum.DisplayUseful = "NO";
				}
			}
		}

		private bool CanExecute(object param) { return true; }
		private void CloseWindow()
		{
			foreach (Window window in App.Current.Windows)
			{
				if (window.GetType() == typeof(ShowAllForumsView)) { window.Close(); }
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

        private void Button_Click_CloseForum(object param)
		{
            SelectedForum.Status = "CLOSED";
            _forumController.UpdateForum(SelectedForum);
            MessageBox.Show("You have successfully close forum!");
        }



        private void Button_Click_ShowComments(object param)
		{
            var allComments = new ShowAllComentsView(SelectedForum);
            allComments.Show();
            CloseWindow();
		}
    }
}
