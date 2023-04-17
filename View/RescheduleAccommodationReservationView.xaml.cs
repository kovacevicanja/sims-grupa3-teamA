using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for RescheduleAccommodationReservationView.xaml
    /// </summary>
    public partial class RescheduleAccommodationReservationView : Window
    {
        public RequestAccommodationReservationController RequestAccommodationReservationController  { get; set;}
        public AccommodationReservation SelectedReservation;
        public RescheduleAccommodationReservationView(AccommodationReservation selectedReservation)
        {
            InitializeComponent();
            this.DataContext = this;
            RequestAccommodationReservationController = new RequestAccommodationReservationController();
            SelectedReservation = new AccommodationReservation();
            SelectedReservation = selectedReservation;
            NewInitialDate = DateTime.Now;
            NewEndDate = DateTime.Now;
        }

        public DateTime _newInitialDate;
        public DateTime NewInitialDate
        {
            get => _newInitialDate;
            set
            {
                if (_newInitialDate != value)
                {
                    _newInitialDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime _newEndDate;
        public DateTime NewEndDate
        {
            get => _newEndDate;
            set
            {
                if (_newEndDate != value)
                {
                    _newEndDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public String _comment;
        public String Comment
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click_Send_Request(object sender, RoutedEventArgs e)
        {
            RequestAccommodationReservation request = new RequestAccommodationReservation();
            request.AccommodationReservation = SelectedReservation;
            request.Comment = Comment;
            request.NewArrivalDay = NewInitialDate;
            request.NewDeparuteDay = NewEndDate;
            request.Status = Domain.Enums.RequestStatus.PENDING;

            RequestAccommodationReservationController.Create(request);
            RequestAccommodationReservationController.SaveRequest();
            this.Close();
        }
    }
}
