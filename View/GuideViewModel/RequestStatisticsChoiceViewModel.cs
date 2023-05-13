using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.View.GuideView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.View.GuideViewModel
{
    public class RequestStatisticsChoiceViewModel
    {
        private TourTimeInstanceController _tourTimeInstanceController;
        private VoucherController _voucherController;
        private TourReservationController _tourReservationController;
        public bool IsLocation = false;
        public RelayCommand YesCommand { get; }
        public RelayCommand NoCommand { get; }
        public RequestStatisticsChoiceViewModel()
        {
            _tourTimeInstanceController = new TourTimeInstanceController();
            _voucherController = new VoucherController();
            _tourReservationController = new TourReservationController();
            YesCommand = new RelayCommand(No_Click, CanExecute);
            NoCommand = new RelayCommand(Yes_Click, CanExecute);
        }

        private bool CanExecute(object param) { return true; }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(RequestStatisticsChoiceView)) { window.Close(); }
            }
        }

        private void No_Click(object param)
        {
            IsLocation = false;
            GuideHomeWindow guideHomeWindow = new GuideHomeWindow();
            guideHomeWindow.Show();
            CloseWindow();
        }
        private void Yes_Click(object param)
        {
            IsLocation = false;
            GuideHomeWindow guideHomeWindow = new GuideHomeWindow();
            guideHomeWindow.Show();
            CloseWindow();
        }
    }
}
