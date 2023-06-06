﻿using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.Model.Enums;
using BookingProject.Model;
using BookingProject.View.GuideView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingProject.Commands;

namespace BookingProject.View.GuideViewModel
{


    public class TourResignationViewModel
    {
        private TourTimeInstanceController _tourTimeInstanceController;
        private VoucherController _voucherController;
        private TourReservationController _tourReservationController;
        public TourTimeInstance ChosenTour;
        public RelayCommand YesCommand { get; }
        public RelayCommand NoCommand { get; }
        public TourResignationViewModel()
        {
            _tourTimeInstanceController = new TourTimeInstanceController();
            _voucherController = new VoucherController();
            _tourReservationController = new TourReservationController();   
            YesCommand = new RelayCommand(Yes_Click, CanExecute);
            NoCommand = new RelayCommand(No_Click, CanExecute);
        }
        public void SendVouchers()
        {
            foreach (TourReservation reservation in _tourReservationController.GetAll())
            {
                if (reservation.Tour.Id == ChosenTour.TourId)
                {
                    Voucher voucher = new Voucher();
                    voucher.Guest = reservation.Guest;
                    voucher.StartDate = DateTime.Now;
                    voucher.EndDate = DateTime.Now.AddDays(7);
                    _voucherController.Create(voucher);
                }
            }
        }

        private bool CanExecute(object param) { return true; }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(TourResignationWindow)) { window.Close(); }
            }
        }

        private void No_Click(object param)
        {
            GuideHomeWindow guideHomeWindow= new GuideHomeWindow();
            guideHomeWindow.Show();
            CloseWindow();
        }
        private void Yes_Click(object param)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            CloseWindow();
        }

    }
}
