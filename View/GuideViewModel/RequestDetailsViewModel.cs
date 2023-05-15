using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.View.GuideView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.View.GuideViewModel
{
    public class RequestDetailsViewModel
    {
        public TourRequest ChosenRequest { get; set; }
        public RelayCommand CancelCommand { get; }
        public RequestDetailsViewModel(TourRequest chosenRequest)
        {
            ChosenRequest = chosenRequest;
            CancelCommand = new RelayCommand(Button_Click_Close, CanExecute);
        }
        private bool CanExecute(object param) { return true; }

        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(RequestDetailsView)) { window.Close(); }
            }
        }
        private void Button_Click_Close(object param)
        {
            //RequestedTourCreation view = new RequestedTourCreation(ChosenRequest);
            //view.Show();
            CloseWindow();
        }

    }
}

