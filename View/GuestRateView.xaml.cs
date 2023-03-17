using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.Model.Enums;
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
    /// Interaction logic for GuestRateView.xaml
    /// </summary>
    public partial class GuestRateView : Window
    {
        public GuestGradeController GradeController { get; set; }
        public ObservableCollection<GuestGrade> Grades { get; set; }
        public GuestRateView()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            GradeController = app.GuestGradeController;
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

        private int _communication;

        public int Communication
        {
            get => _communication;
            set
            {
                if (value != _communication)
                {
                    _communication = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _observance;

        public int ObservanceOfRules
        {
            get => _observance;
            set
            {
                if (value != _observance)
                {
                    _observance = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _decency;
        public int Decency
        {
            get => _decency;
            set
            {
                if (value != _decency)
                {
                    _decency = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _noisiness;
        public int Noisiness
        {
            get => _noisiness;
            set
            {
                if (value != _noisiness)
                {
                    _noisiness = value;
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
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void Button_Click_Rate(object sender, RoutedEventArgs e)
        {
            GuestGrade grade = new GuestGrade();
            grade.Cleanliness = Cleanliness;
            grade.Communication = Communication;
            grade.ObservanceOfRules = ObservanceOfRules;
            grade.Decency = Decency;
            grade.Noisiness = Noisiness;
            grade.Comment = Comment;

            //Location location = new Location();
            //location.City = City;
            //location.Country = Country;
            ////location.Id = LocationController.GenerateId();

            GradeController.Create(grade);
            GradeController.SaveGrade();
            //accommodation.IdLocation = location.Id;

            //accommodation.Location = location;
            //accommodation.IdLocation = location.Id;
            //AccommodationController.Create(accommodation);
            //AccommodationController.SaveAccommodation();

            //ImageController.LinkToAccommodation(accommodation.Id);
            //ImageController.SaveImage();

            //AccommodationImage accommodationImage= new AccommodationImage();
            //accommodationImage.Url= Url;
            //accommodationImage.Id = ImageController.GenerateId();



            
                GradeController.Create(grade);
            
        }
       
    }
}
