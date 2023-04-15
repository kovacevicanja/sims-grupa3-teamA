using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.FileHandler;
using BookingProject.Model.Enums;
using BookingProject.Model;
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
using BookingProject.Domain.Enums;
using BookingProject.ConversionHelp;

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for SecondGuestMyVouchersView.xaml
    /// </summary>
    public partial class SecondGuestMyVouchersView : Window, INotifyPropertyChanged
    {
        public VoucherController VoucherController { get; set; }
        private VoucherHandler _voucherHandler { get; set; }
        public ObservableCollection<Voucher> _vouchers { get; set; }
        public List<Voucher> _vouchersList { get; set; }
        public Voucher ChosenVoucher { get; set; }
        public TourReservationController TourReservationController { get; set; }
        public TourReservationHandler TourReservationHandler { get; set; }
        public Tour ChosenTour { get; set; }
        public int ReservationId { get; set; }  

        public SecondGuestMyVouchersView(int guestId, Tour chosenTour)
        {
            InitializeComponent();
            this.DataContext = this;

            ChosenVoucher = new Voucher();
            ChosenTour = chosenTour;
            TourReservationController = new TourReservationController();
            TourReservationHandler = new TourReservationHandler();
            //ReservationId = reservationId;
            _voucherHandler = new VoucherHandler();
            VoucherController = new VoucherController();


            //Guest = guest;

            //VoucherController.GuestVoucherBind(guestId);

            VoucherController.DeleteExpiredVouchers();

            _vouchers = new ObservableCollection<Voucher>(VoucherController.GetUserVouhers(guestId));
            _vouchersList = new List<Voucher>();

            MyVouchersDataGrid.ItemsSource = _vouchers;
        }

        private void Button_UseVoucher(object sender, RoutedEventArgs e)
        {
            if (ChosenVoucher != null)
            {
                _vouchersList = VoucherController.GetAll();
                TourReservationController.ShowCustomMessageBox("You have successfully used your voucher to book this tour.");
                //_vouchersList.Remove(ChosenVoucher);
                ChosenVoucher.State = VoucherState.USED;
                ChosenVoucher.Tour = ChosenTour; //
                _voucherHandler.Save(_vouchersList);

                /*
                List<TourReservation> tourReservations = new List<TourReservation>();
                tourReservations = TourReservationHandler.Load();

                foreach (TourReservation tr in tourReservations)
                {
                    if (tr.Id == ReservationId)
                    {
                        tr.UsedVoucher = true;
                    }
                }

                TourReservationHandler.Save(tourReservations);
                */

                this.Close();
            }
        }
        private void Button_Cancel (object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (value != _endDate)
                {
                    _endDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private VoucherState _voucherState;
        public VoucherState VoucherState
        {
            get => _voucherState;
            set
            {
                if (value != _voucherState)
                {
                    _voucherState = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
