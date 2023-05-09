using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.View.OwnerViewModel
{
    public class NotGradedViewModel
    {
        private AccommodationReservationController _accommodationController;
        public ObservableCollection<AccommodationReservation> Reservations { get; set; }

        public AccommodationReservation SelectedReservation { get; set; }
        public RelayCommand GradeCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand MenuCommand { get; }
        public NotGradedViewModel()
        {
            

            //var app = Application.Current as App;
            //_accommodationController = app.AccommodationReservationController;
            _accommodationController = new AccommodationReservationController();


            Reservations = new ObservableCollection<AccommodationReservation>(_accommodationController.GetAllNotGradedReservations(SignInForm.LoggedInUser.Id));
            GradeCommand = new RelayCommand(Button_Grade, CanExecute);
            CancelCommand = new RelayCommand(Button_Cancel, CanExecute);
            MenuCommand = new RelayCommand(Button_Click_Menu, CanExecute);
        }
        //private void selectedIndexChanged(object sender, EventArgs e)
        //{
        //    var selectedItem = SelectedReservation;
        //    var window2 = new GuestRateViewModel(selectedItem);
        //    window2.SelectedObject = selectedItem;
        //    window2.Show();
        //}
        private void Button_Click_Menu(object param)
        {
            MenuView view = new MenuView();
            view.Show();
            CloseWindow();
        }
        private bool CanExecute(object param) { return true; }
        private void Button_Grade(object param)
        {
            if (SelectedReservation != null)
            {
                GuestRateView view = new GuestRateView(SelectedReservation);
                view.Show();
                CloseWindow();
            }
        }
        private void Button_Cancel(object param)
        {
            var view = new OwnerView();
            view.Show();
            CloseWindow();
        }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(NotGradedView)) { window.Close(); }
            }
        }
        public int RowNum()
        {
            return Reservations.Count;
        }
    }
}
