using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.FileHandler;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using BookingProject.Domain.Enums;
using BookingProject.View.CustomMessageBoxes;

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
        public Tour ChosenTour { get; set; }
        public CustomMessageBox CustomMessageBox { get; set; }  
        public SecondGuestMyVouchersView(int guestId, Tour chosenTour)
        {
            InitializeComponent();
            this.DataContext = this;

            CustomMessageBox = new CustomMessageBox();

            ChosenVoucher = new Voucher();
            ChosenTour = chosenTour;
            TourReservationController = new TourReservationController();

            _voucherHandler = new VoucherHandler();
            VoucherController = new VoucherController();

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
                CustomMessageBox.ShowCustomMessageBox("You have successfully used your voucher to book this tour.");
                ChosenVoucher.State = VoucherState.USED;
                ChosenVoucher.Tour = ChosenTour; 
                _voucherHandler.Save(_vouchersList);

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