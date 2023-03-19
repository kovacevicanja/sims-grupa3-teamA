using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.Model.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
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
        public object SelectedObject { get; set; }
        private AccommodationReservation _selectedReservation;


        public ObservableCollection<int> cleanliness_option { get; set; }
        public int chosenCleanliness { get; set; }
        public ObservableCollection<int> communication_option { get; set; }
        public int chosenCommunication { get; set; }
        public ObservableCollection<int> observance_option { get; set; }
        public int chosenObservance { get; set; }
        public ObservableCollection<int> decency_option { get; set; }
        public int chosenDecency { get; set; }
        public ObservableCollection<int> noisiness_option { get; set; }
        public int chosenNoisiness { get; set; }




        public GuestRateView(AccommodationReservation selectedReservation)
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            GradeController = app.GuestGradeController;
            _selectedReservation = new AccommodationReservation();
            _selectedReservation = selectedReservation;
            cleanliness_option = new ObservableCollection<int>();
            cleanliness_option.Add(1);
            cleanliness_option.Add(2);
            cleanliness_option.Add(3);
            cleanliness_option.Add(4);
            cleanliness_option.Add(5);

            communication_option = new ObservableCollection<int>();
            communication_option.Add(1);
            communication_option.Add(2);
            communication_option.Add(3);
            communication_option.Add(4);
            communication_option.Add(5);

            observance_option = new ObservableCollection<int>();
            observance_option.Add(1);
            observance_option.Add(2);
            observance_option.Add(3);
            observance_option.Add(4);
            observance_option.Add(5);

            decency_option = new ObservableCollection<int>();
            decency_option.Add(1);
            decency_option.Add(2);
            decency_option.Add(3);
            decency_option.Add(4);
            decency_option.Add(5);

            noisiness_option = new ObservableCollection<int>();
            noisiness_option.Add(1);
            noisiness_option.Add(2);
            noisiness_option.Add(3);
            noisiness_option.Add(4);
            noisiness_option.Add(5);
        }


        //private AccommodationReservation _selectedReservation;
        public AccommodationReservation SelectedReservation
        {
            get { return _selectedReservation; }
            set { _selectedReservation = value; OnPropertyChanged(); }
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
            grade.Cleanliness = chosenCleanliness;
            grade.Communication = chosenCommunication;
            grade.ObservanceOfRules = chosenObservance;
            grade.Decency = chosenDecency;
            grade.Noisiness = chosenNoisiness;
            grade.Comment = Comment;
            grade.Id = GradeController.GenerateId();
            //grade.Id = GradeController.GenerateId();
            grade.AccommodationReservation.Id = _selectedReservation.Id;

            GradeController.Create(grade);
            GradeController.SaveGrade();



            
            this.Close();
            
        }
       
    }
}
