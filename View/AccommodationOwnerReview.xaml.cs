using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.DependencyInjection;
using BookingProject.Domain.Images;
using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.Repositories.Intefaces;
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
    public partial class AccommodationOwnerReview : Window
    {
        public AccommodationOwnerReview(AccommodationReservation selectedReservation)
        {
            InitializeComponent();
            this.DataContext = new AccommodationOwnerReviewViewModel(selectedReservation);
        }
    }
}