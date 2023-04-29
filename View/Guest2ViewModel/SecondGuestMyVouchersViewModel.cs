using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain.Enums;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.View.CustomMessageBoxes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;

namespace BookingProject.View.Guest2ViewModel
{
    public class SecondGuestMyVouchersViewModel : INotifyPropertyChanged
    {
        public VoucherController VoucherController { get; set; }
        public ObservableCollection<Voucher> Vouchers { get; set; }
        public List<Voucher> _vouchersList { get; set; }
        public Voucher ChosenVoucher { get; set; }
        public TourReservationController TourReservationController { get; set; }
        public Tour ChosenTour { get; set; }
        public CustomMessageBox CustomMessageBox { get; set; }
        public RelayCommand UseVoucherCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public SecondGuestMyVouchersViewModel(int guestId, Tour chosenTour)
        {
            VoucherController = new VoucherController();
            Vouchers = new ObservableCollection<Voucher>(VoucherController.GetUserVouhers(guestId));  
            CustomMessageBox = new CustomMessageBox();

            ChosenVoucher = new Voucher();
            ChosenTour = chosenTour;
            TourReservationController = new TourReservationController();


            VoucherController.DeleteExpiredVouchers();

            _vouchersList = new List<Voucher>();

            UseVoucherCommand = new RelayCommand(Button_UseVoucher, CanExecute);
            CancelCommand = new RelayCommand(Button_Cancel, CanExecute);
        }

        private bool CanExecute(object param) { return true; }

        private void Button_UseVoucher(object param)
        {
            if (ChosenVoucher != null)
            {
                _vouchersList = VoucherController.GetAll();
                CustomMessageBox.ShowCustomMessageBox("You have successfully used your voucher to book this tour.");
                ChosenVoucher.State = VoucherState.USED;
                ChosenVoucher.Tour = ChosenTour;
                VoucherController.Save(_vouchersList);

                CloseWindow();
            }
        }
        private void Button_Cancel(object param)
        {
            CloseWindow();
        }

        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(SecondGuestMyVouchersView)) { window.Close(); }
            }
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