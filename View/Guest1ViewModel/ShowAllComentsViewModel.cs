using BookingProject.Commands;
using BookingProject.Controller;
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
	public class ShowAllComentsViewModel : INotifyPropertyChanged
	{
        public UserController _userController;
		public RelayCommand HomePageCommand { get; }
		public RelayCommand MyReservationsCommand { get; }
		public RelayCommand LogOutCommand { get; }
		public RelayCommand MyReviewsCommand { get; }
		public RelayCommand MyProfileCommand { get; }
		public RelayCommand TutorialCommand { get; }
		public RelayCommand CreateForumCommand { get; }
		public RelayCommand QuickSearchCommand { get; }
        public RelayCommand LeaveCommentCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;
        public Forum SelectedForum;
        public ObservableCollection<ForumComment> Comments { get; set; }
        public ForumController _forumController;
        public ForumCommentController _forumCommentController;
        public AccommodationReservationController accommodationReservationController;
        public ShowAllComentsViewModel(Forum selectedForum)
		{
            _userController = new UserController();
            SelectedForum = selectedForum;
			HomePageCommand = new RelayCommand(Button_Click_Homepage, CanExecute);
			MyReservationsCommand = new RelayCommand(Button_Click_MyReservations, CanExecute);
			LogOutCommand = new RelayCommand(Button_Click_Logout, CanExecute);
			MyReviewsCommand = new RelayCommand(Button_Click_MyReviews, CanExecute);
			MyProfileCommand = new RelayCommand(Button_Click_MyProfile, CanExecute);
			TutorialCommand = new RelayCommand(ButtonClick_Tutorial, CanExecute);
			CreateForumCommand = new RelayCommand(Button_Click_CreateForum, CanExecute);
			QuickSearchCommand = new RelayCommand(Button_Click_Quick_Search, CanExecute);
            LeaveCommentCommand = new RelayCommand(Button_Click_LeaveComment, CanExecute);
            _forumController = new ForumController();
            _forumCommentController = new ForumCommentController();
            accommodationReservationController = new AccommodationReservationController();
            Comments = new ObservableCollection<ForumComment>(_forumController.GetCommentsForForum(SelectedForum));
		}
        public string _comment;
        public string NewComment
        {
            get => _comment;
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool CanExecute(object param) { return true; }
		private void CloseWindow()
		{
			foreach (Window window in App.Current.Windows)
			{
				if (window.GetType() == typeof(ShowAllComentsView)) { window.Close(); }
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

        private void Button_Click_LeaveComment(object param)
		{
            ForumComment newForumComment = new ForumComment();
            newForumComment.Text = NewComment;
            newForumComment.Forum = SelectedForum;
            newForumComment.User = _userController.GetLoggedUser();
            newForumComment.NumberOfReports = 0;
            newForumComment.IsOwners = false;
            newForumComment.IsGuests = true;
            newForumComment.IsInvalid = accommodationReservationController.IsLocationVisited(SelectedForum.Location);
            _forumCommentController.Create(newForumComment);
            _forumController.SetVeryHelpful(SelectedForum);
        }

    }
}
