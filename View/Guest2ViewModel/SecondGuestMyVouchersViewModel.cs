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
using System.Windows.Navigation;

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
        public RelayCommand UseVoucherCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand ProfileCommand { get; }
        public int GuestId { get; set; }
        public NavigationService NavigationService { get; set; }
        public string NumberOfDays { get; set; }
        public SecondGuestMyVouchersViewModel(int guestId, int chosenTourId, NavigationService navigationService)
        {
            VoucherController = new VoucherController();
            Vouchers = new ObservableCollection<Voucher>(VoucherController.GetUserVouhers(guestId));  
            CustomMessageBox = new CustomMessageBox();

            NavigationService = navigationService; 

            ChosenTour = new Tour();
            ChosenTour.Id = chosenTourId;
            TourReservationController = new TourReservationController();

            VoucherController.DeleteExpiredVouchers();

            _vouchersList = new List<Voucher>();

            UseVoucherCommand = new RelayCommand(Button_UseVoucher, CanUse);
            CancelCommand = new RelayCommand(Button_Cancel, CanExecute);
            ProfileCommand = new RelayCommand(Button_BackToProfile, CanExecute);

            GuestId = guestId;

            NumberOfDays = "5";

            /*foreach (Voucher voucher in _vouchersList)
            {
                NumberofDays = (int)(DateTime.Now.Date - voucher.EndDate.Date).TotalDays;
            }*/
        }

        private bool CanExecute(object param) { return true; }
        
        private bool CanUse(object param)
        {
            if (ChosenVoucher != null) { return true; }
            else { return false;  }
        }

        private void Button_BackToProfile(object param)
        {
            NavigationService.GoBack();
        }

        private void Button_UseVoucher(object param)
        {
            _vouchersList = VoucherController.GetAll();
            if (ChosenTour.Id == -1)
            {
                CustomMessageBox.ShowCustomMessageBox("You cannot use the voucher, you have not selected any tour for booking.");
            }
            else
            {
                CustomMessageBox.ShowCustomMessageBox("You have successfully used your voucher to book this tour.");
                ChosenVoucher.State = VoucherState.USED;
                ChosenVoucher.Tour = ChosenTour;
                VoucherController.Save(_vouchersList);

                NavigationService.Navigate(new SerachAndReservationToursView(GuestId, NavigationService));
            }
        }
        private void Button_Cancel(object param)
        {
            NavigationService.GoBack();
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