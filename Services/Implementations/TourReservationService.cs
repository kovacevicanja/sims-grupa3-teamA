using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Model;
using BookingProject.View;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using BookingProject.Repositories.Intefaces;
using BookingProject.Repositories.Implementations;
using BookingProject.Repositories;
using BookingProject.Services.Interfaces;
using BookingProject.View.CustomMessageBoxes;
using BookingProject.DependencyInjection;
using System.Windows.Navigation;

namespace BookingProject.Services
{
    public class TourReservationService : ITourReservationService
    {
        public CustomMessageBox CustomMessageBox { get; set; }
        private ITourReservationRepository _tourReservationRepository;
        private ITourService _tourService;

        public TourReservationService() { }
        public void Initialize()
        {
            _tourReservationRepository = Injector.CreateInstance<ITourReservationRepository>();
            _tourService = Injector.CreateInstance<ITourService>(); 
            CustomMessageBox = new CustomMessageBox();
        }

        public bool BookingSuccess(Tour chosenTour, string numberOfGuests, DateTime selectedDate, User guest, NavigationService navigationService)
        {
            if (_tourReservationRepository.GetAll().Count() == 0)
            {
                if (TryReservation(chosenTour, numberOfGuests, selectedDate, guest)) { return true; }
                else { return false; }
            }
            else
            {
                if (GoThroughReservations(chosenTour, numberOfGuests, selectedDate, guest, navigationService)) { return true; }
                else { return false; }
            }
        }
        public bool GoThroughReservations(Tour chosenTour, string numberOfGuests, DateTime selectedDate, User guest, NavigationService navigationService)
        {
            foreach (TourReservation tourReservation in _tourReservationRepository.GetAll())
            {
                if (tourReservation.Tour.Id == chosenTour.Id && tourReservation.ReservationStartingTime == selectedDate)
                {
                    if (int.Parse(numberOfGuests) <= tourReservation.GuestsNumberPerReservation)
                    {
                        _tourReservationRepository.SaveSameReservationToFile(chosenTour, tourReservation, numberOfGuests, selectedDate, guest);
                        return true;

                    }
                    else
                    {
                        if (tourReservation.GuestsNumberPerReservation == 0)
                        {
                            FullyBookedTours(chosenTour, selectedDate, guest, navigationService);
                            return false;
                        }
                        else
                        {
                            FreePlaceMessage(tourReservation.GuestsNumberPerReservation);
                            return false;
                        }
                    }
                }
            }
            if (TryReservation(chosenTour, numberOfGuests, selectedDate, guest)) { return true; }
            else { return false; }
        }
        public bool TryReservation(Tour chosenTour, string numberOfGuests, DateTime selectedDate, User guest)
        {
            if (int.Parse(numberOfGuests) <= chosenTour.MaxGuests)
            {
                _tourReservationRepository.SaveReservationToFile(chosenTour, numberOfGuests, selectedDate, guest);
                return true;
            }
            else
            {
                FreePlaceMessage(chosenTour.MaxGuests);
                return false;
            }
        }
        public void TryToBook(Tour chosenTour, string numberOfGuests, DateTime selectedDate, User guest, NavigationService navigationService)
        {
            if (BookingSuccess(chosenTour, numberOfGuests, selectedDate, guest, navigationService)) SuccessfulReservationMessage(numberOfGuests, guest, chosenTour, navigationService);
        }
        public void FullyBookedTours(Tour chosenTour, DateTime selectedDate, User guest, NavigationService navigationService)
        {
            CustomMessageBox.ShowCustomMessageBox("The tour is fully booked. The system will offer you tours at the same location.");
            //ReservationTourOtherOffersView reservationTourOtherOffersView = new ReservationTourOtherOffersView(chosenTour, selectedDate, guest.Id);
            //reservationTourOtherOffersView.Show();
            navigationService.Navigate(new ReservationTourOtherOffersView(chosenTour, selectedDate, guest.Id, navigationService));
        }
        public void SuccessfulReservationMessage(string numberOfGuests, User guest, Tour chosenTour, NavigationService navigationService)
        { 
            if (int.Parse(numberOfGuests) == 1)
            {
                CustomMessageBox.ShowCustomMessageBox("You have successfully booked a tour for " + numberOfGuests + " person");
            }
            else
            {
                CustomMessageBox.ShowCustomMessageBox("You have successfully booked a tour for " + numberOfGuests + " people");
            }
            UnusedVouchers(guest, chosenTour, navigationService);
        }
        private void UnusedVouchers(User guest, Tour chosenTour, NavigationService navigationService)
        {
            guest.Vouchers = Injector.CreateInstance<IVoucherService>().GetUserVouhers(guest.Id);

            if (guest.Vouchers.Count != 0)
            {
                CustomMessageBox.ShowCustomMessageBox("The system has detected that you have an unused voucher. You can use them now.");
                //SecondGuestMyVouchersView secondGuestMyVouchers = new SecondGuestMyVouchersView(guest.Id, chosenTour.Id, NavigationService);
                //secondGuestMyVouchers.Show();
                navigationService.Navigate(new SecondGuestMyVouchersView(guest.Id, chosenTour.Id, navigationService));
            }
        }
        public void FreePlaceMessage (int maxGuests)
        {
            if (maxGuests == 1) { CustomMessageBox.ShowCustomMessageBox("There is space for " + maxGuests + " more person"); }
            else { CustomMessageBox.ShowCustomMessageBox("There is space for " + maxGuests + " more people"); }
        }
        public List<TourReservation> GetAll()
        {
            return _tourReservationRepository.GetAll();
        }
        public TourReservation GetById(int id)
        {
            return _tourReservationRepository.GetById(id);  
        }
        public List<TourReservation> GetUserReservations(int guestId)
        {
            return _tourReservationRepository.GetUserReservations(guestId);
        }
    }
}