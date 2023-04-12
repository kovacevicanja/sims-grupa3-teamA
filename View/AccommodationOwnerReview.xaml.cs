using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain.Images;
using BookingProject.Model;
using BookingProject.Model.Images;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for AccommodationOwnerReview.xaml
    /// </summary>
    public partial class AccommodationOwnerReview : Window
    {
        public AccommodationOwnerGradeController AccommodationOwnerGradeController { get; set; }
        public ObservableCollection<AccommodationOwnerGrade> Grades { get; set; }
        private AccommodationReservation _selectedReservation;
        public AccommodationImageController AccommodationImageController { get; set; }
        public AccommodationGuestImageController AccommodationGuestImageController { get; set; }
        public UserController UserController { get; set; }
        public AccommodationOwnerGrade grade;

        public ObservableCollection<int> CleanlinessOption { get; set; }
        public int chosenCleanliness { get; set; }
        public ObservableCollection<int> CorectnessOption { get; set; }
        public int chosenCorectness { get; set; }


        public AccommodationOwnerReview(AccommodationReservation selectedReservation)
        {
            InitializeComponent();
            this.DataContext = this;
            this.DataContext = this;
            AccommodationOwnerGradeController = new AccommodationOwnerGradeController();
            AccommodationImageController = new AccommodationImageController();
            AccommodationGuestImageController = new AccommodationGuestImageController();
            UserController = new UserController();
            grade = new AccommodationOwnerGrade();
            grade.Id = AccommodationOwnerGradeController.GenerateId();
            _selectedReservation = new AccommodationReservation();
            _selectedReservation = selectedReservation;
            CleanlinessOption = new ObservableCollection<int>();
            CorectnessOption = new ObservableCollection<int>();
            fillOptions(CleanlinessOption, CorectnessOption);
        }

        private void fillOptions(ObservableCollection<int> CleanlinessOption, ObservableCollection<int> CorectnessOption)
        {
            for (int i = 1; i < 6; i++)
            {
                CleanlinessOption.Add(i);
                CorectnessOption.Add(i);
            }
        }

        public AccommodationReservation SelectedReservation
        {
            get
            {
                return _selectedReservation;
            }
            set
            {
                _selectedReservation = value;
                OnPropertyChanged();
            }
        }

        private int _cleanliness;
        public int Cleanliness
        {
            get => _cleanliness;
            set
            {
                if (value != _cleanliness)
                {
                    _cleanliness = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _corectness;
        public int Corectness
        {
            get => _corectness;
            set
            {
                if (value != _corectness)
                {
                    _corectness = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _url;
        public string Url
        {
            get => _url;
            set
            {
                if (value != _url)
                {
                    _url = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _reccommendation;
        public string Reccommendation
        {
            get => _reccommendation;
            set
            {
                if (value != _reccommendation)
                {
                    _reccommendation = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click_Review(object sender, RoutedEventArgs e)
        {
           
            grade.Accommodation.Id = _selectedReservation.Accommodation.Id;
            grade.Cleanliness = chosenCleanliness;
            grade.OwnerCorectness = chosenCorectness;
            grade.AdditionalComment = Comment;
            grade.Reccommendation = Reccommendation;

            
            AccommodationOwnerGradeController.Create(grade);
            AccommodationOwnerGradeController.Save();

            this.Close();
        }

        private void Button_Click_AddPicture(object sender, RoutedEventArgs e)
        {
            if (UrlPicture.Text != "")
            {
                AccommodationGuestImage Picture = new AccommodationGuestImage();
                Picture.Id = AccommodationGuestImageController.GenerateId();
                Picture.Url = UrlPicture.Text;
                Picture.Guest.Id = UserController.GetLoggedUser().Id;
                Picture.Grade.Id = grade.Id;
                grade.guestImages.Add(Picture);
                AccommodationGuestImageController.Create(Picture);
                AccommodationGuestImageController.SaveImage();
            }
            else
            {
                MessageBox.Show("Photo url can not be empty");
            }

            UrlPicture.Text = string.Empty;
        }
    }
}
