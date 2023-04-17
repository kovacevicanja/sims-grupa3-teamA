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
        public ObservableCollection<int> CleanlinessOption { get; set; }
        public int ChosenCleanliness { get; set; }
        public ObservableCollection<int> CommunicationOption { get; set; }
        public int ChosenCommunication { get; set; }
        public ObservableCollection<int> ObservanceOption { get; set; }
        public int ChosenObservance { get; set; }
        public ObservableCollection<int> DecencyOption { get; set; }
        public int ChosenDecency { get; set; }
        public ObservableCollection<int> NoisinessOption { get; set; }
        public int ChosenNoisiness { get; set; }
        public GuestRateView(AccommodationReservation selectedReservation)
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            GradeController = app.GuestGradeController;
            _selectedReservation = new AccommodationReservation();
            _selectedReservation = selectedReservation;
            CleanlinessOption = new ObservableCollection<int>();
            CleanlinessOption.Add(1);
            CleanlinessOption.Add(2);
            CleanlinessOption.Add(3);
            CleanlinessOption.Add(4);
            CleanlinessOption.Add(5);

            CommunicationOption = new ObservableCollection<int>();
            CommunicationOption.Add(1);
            CommunicationOption.Add(2);
            CommunicationOption.Add(3);
            CommunicationOption.Add(4);
            CommunicationOption.Add(5);

            ObservanceOption = new ObservableCollection<int>();
            ObservanceOption.Add(1);
            ObservanceOption.Add(2);
            ObservanceOption.Add(3);
            ObservanceOption.Add(4);
            ObservanceOption.Add(5);

            DecencyOption = new ObservableCollection<int>();
            DecencyOption.Add(1);
            DecencyOption.Add(2);
            DecencyOption.Add(3);
            DecencyOption.Add(4);
            DecencyOption.Add(5);

            NoisinessOption = new ObservableCollection<int>();
            NoisinessOption.Add(1);
            NoisinessOption.Add(2);
            NoisinessOption.Add(3);
            NoisinessOption.Add(4);
            NoisinessOption.Add(5);
        }


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
            grade.Cleanliness = ChosenCleanliness;
            grade.Communication = ChosenCommunication;
            grade.ObservanceOfRules = ChosenObservance;
            grade.Decency = ChosenDecency;
            grade.Noisiness = ChosenNoisiness;
            grade.Comment = Comment;
            grade.Id = GradeController.GenerateId();
            grade.AccommodationReservation.Id = _selectedReservation.Id;

            GradeController.Create(grade);
            GradeController.SaveGrade();



            
            this.Close();
            
        }
       
    }
}
