using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Model.Enums;
using System;
using System.Collections.Generic;
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

namespace BookingProject.View.GuideView
{
    /// <summary>
    /// Interaction logic for TourCancellationWindow.xaml
    /// </summary>
    public partial class TourCancellationWindow : Window
    {
        private TourTimeInstanceController _tourTimeInstanceController;
        private VoucherController _voucherController;
        private TourReservationController _tourReservationController;
        public TourTimeInstance ChosenTour;
        public TourCancellationWindow(TourTimeInstance chosenTour)
        {
            InitializeComponent();
            _tourTimeInstanceController = new TourTimeInstanceController();
            _voucherController = new VoucherController();
            _tourReservationController= new TourReservationController();
            ChosenTour = chosenTour;
        }
        
        public void SendVouchers()
        {
            foreach(TourReservation reservation in _tourReservationController.GetAll())
            {
                if (reservation.Tour.Id == ChosenTour.Id)
                {
                    Voucher voucher = new Voucher();
                    voucher.Guest.Id = reservation.Guest.Id;
                    voucher.StartDate = DateTime.Now;
                    voucher.EndDate=DateTime.Now.AddDays(7);
                    _voucherController.Create(voucher);
                }
            }
            _voucherController.Save();
        }
        

        private void No_Click(object sender, RoutedEventArgs e)
        {
            MyToursWindow myToursWindow = new MyToursWindow();
            myToursWindow.Show();
            Close();

        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            _tourTimeInstanceController.GetByID(ChosenTour.Id).State = TourState.CANCELLED;
            _tourTimeInstanceController.Save();
            SendVouchers();
            MyToursWindow myToursWindow = new MyToursWindow();
            myToursWindow.Show();
            Close();
        }

    }
}
