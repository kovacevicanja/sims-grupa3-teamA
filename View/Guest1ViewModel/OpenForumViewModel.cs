using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.View.Guest1View;
using BookingProject.View.Guest1View.Tutorials;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.View.Guest1ViewModel
{
	public class OpenForumViewModel : INotifyPropertyChanged
	{
        public AccommodationLocationController _accommodationLocationController;
        public UserController _userController;
        public ForumCommentController _forumCommentController;
        public ForumController _forumController;
        public AccommodationReservationController accommodationReservationController;
        public ObservableCollection<string> CityCollection { get; set; }
		public ObservableCollection<string> CountryComboBox { get; set; }
        public RelayCommand FillCityCommand { get; }
        public RelayCommand HomePageCommand { get; }
        public RelayCommand MyReservationsCommand { get; }
        public RelayCommand LogoutCommand { get; }
        public RelayCommand MyReviewsCommand { get; }
        public RelayCommand MyProfileCommand { get; }
        public RelayCommand OpenForumCommand { get; }
        public RelayCommand AllForumsCommand { get; }
        public RelayCommand CreateForumCommand { get; }
        public RelayCommand QuickSearchCommand { get; }
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public OpenForumViewModel()
		{
            _forumController = new ForumController();
            _forumCommentController = new ForumCommentController();
            _accommodationLocationController = new AccommodationLocationController();
            _userController = new UserController();
            accommodationReservationController = new AccommodationReservationController();
            CityCollection = new ObservableCollection<string>();
			CountryComboBox = new ObservableCollection<string>();
            FillCityCommand = new RelayCommand(FindCities, CanExecute);
            HomePageCommand = new RelayCommand(Button_Click_Homepage, CanExecute);
            MyReservationsCommand = new RelayCommand(Button_Click_MyReservations, CanExecute);
            LogoutCommand = new RelayCommand(Button_Click_Logout, CanExecute);
            MyReviewsCommand = new RelayCommand(Button_Click_MyReviews, CanExecute);
            MyProfileCommand = new RelayCommand(Button_Click_MyProfile, CanExecute);
            OpenForumCommand = new RelayCommand(Button_Click_OpenForum, CanExecute);
            AllForumsCommand = new RelayCommand(Button_Click_AllForums, CanExecute);
            CreateForumCommand = new RelayCommand(Button_Click_CreateForum, CanExecute);
            QuickSearchCommand = new RelayCommand(Button_Click_Quick_Search, CanExecute);
            FindAllStates();
		}
        public string _comment;
        public string Comment
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
        private bool _cityComboBoxEnabled;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool CityComboboxEnabled
        {
            get => _cityComboBoxEnabled;
            set
            {
                if (_cityComboBoxEnabled != value)
                {
                    _cityComboBoxEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool CanExecute(object param) { return true; }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(OpenForumView)) { window.Close(); }
            }
        }
        public void FindAllStates()
        {
            {
                List<string> items = new List<string>();

                using (StreamReader reader = new StreamReader("../../Resources/Data/accommodationLocations.csv"))
                {
                    while (!reader.EndOfStream)
                    {

                        string[] fields = reader.ReadLine().Split(',');
                        foreach (var field in fields)
                        {
                            string[] Countries = field.Split('|');
                            items.Add(Countries[2]);
                        }
                    }
                }
                var distinctItems = items.Distinct().ToList();

                UpdateCountryComboBox(distinctItems);
                if (State == null)
                {
                    CityComboboxEnabled = false;
                }

            }
        }
        public void UpdateCountryComboBox(List<string> coutries)
        {
            CountryComboBox.Clear();
            foreach (string s in coutries)
            {
                CountryComboBox.Add(s);
            }
        }
        private void FindCities(object param)
        {

            CityCollection.Clear();

            var locations = _accommodationLocationController.GetAll().Where(l => l.Country.Equals(State));

            foreach (Location location in locations)
            {
                CityCollection.Add(location.City);
            }

            CityComboboxEnabled = true;

        }

        private void Button_Click_MyReservations(object param)
        {
            var res = new Guest1Reservations();
            res.Show();
            CloseWindow();
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
        private void Button_Click_OpenForum(object param)
		{
            Forum newForum = new Forum();
            newForum.Location.Country = State;
            newForum.Location.City = City;
            newForum.Location.Id = _accommodationLocationController.GetIdByCountryAndCity(State, City);
            newForum.User.Id = _userController.GetLoggedUser().Id;
            newForum.Name = "FORUM";
            ForumComment newComment = new ForumComment();
            newComment.Text = Comment;
            newComment.IsOwners = false;
            newComment.IsGuests = true;
            newComment.User.Id = _userController.GetLoggedUser().Id;
            newComment.NumberOfReports = 0;
            newComment.Forum = newForum;
            newComment.IsInvalid = accommodationReservationController.IsLocationVisited(newForum.Location);
            newForum.Comments.Add(newComment);
            newForum.Status = "OPENED";
            newForum.IsUseful = false;
            _forumController.Create(newForum);
            _forumCommentController.Create(newComment);
        }
        private void Button_Click_AllForums(object param)
		{
            var allForums = new ShowAllForumsView();
            allForums.Show();
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
