using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.View.Guest1ViewModel;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public partial class Guest1Reservations
    {
        public Guest1Reservations()
        {
            InitializeComponent();
            this.DataContext = new Guest1ReservationsViewModel(this);
        }

    }
}