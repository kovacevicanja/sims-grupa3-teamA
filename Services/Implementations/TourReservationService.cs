using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.FileHandler;
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

namespace BookingProject.Services
{
    public class TourReservationService : ITourReservationService
    {
        public CustomMessageBox CustomMessageBox { get; set; }
        private ITourReservationRepository _tourReservationRepository;
        public TourReservationService() { }
        public void Initialize()
        {
            _tourReservationRepository = Injector.CreateInstance<ITourReservationRepository>();
            CustomMessageBox = new CustomMessageBox();
        }
        public bool BookingSuccess(Tour chosenTour, string numberOfGuests, DateTime selectedDate, User guest)
        {
            if (_tourReservationRepository.GetAll().Count() == 0)
            {
                if (TryReservation(chosenTour, numberOfGuests, selectedDate, guest)) { return true; }
                else { return false; }
            }
            else
            {
                if (GoThroughReservations(chosenTour, numberOfGuests, selectedDate, guest)) { return true; }
                else { return false; }
            }
        }
        public bool GoThroughReservations(Tour chosenTour, string numberOfGuests, DateTime selectedDate, User guest)
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
                            FullyBookedTours(chosenTour, selectedDate, guest);
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
        public void TryToBook(Tour chosenTour, string numberOfGuests, DateTime selectedDate, User guest)
        {
            if (int.Parse(numberOfGuests) <= 0)
            {
                CustomMessageBox.ShowCustomMessageBox("If you want to make a reservation, you must enter the reasonable number of people.");
            }
            else
            {
                if (BookingSuccess(chosenTour, numberOfGuests, selectedDate, guest))
                {
                    SuccessfulReservationMessage(numberOfGuests, guest, chosenTour);
                }
            }
        }
        public void FullyBookedTours(Tour choosenTour, DateTime selectedDate, User guest)
        {
            CustomMessageBox.ShowCustomMessageBox("The tour is fully booked. The system will offer you tours at the same location.");
            ReservationTourOtherOffersView reservationTourOtherOffersView = new ReservationTourOtherOffersView(choosenTour, selectedDate, guest.Id);
            reservationTourOtherOffersView.Show();
        }
        public void SuccessfulReservationMessage(string numberOfGuests, User guest, Tour chosenTour)
        {
            guest.Vouchers = Injector.CreateInstance<IVoucherService>().GetUserVouhers(guest.Id);

            if (int.Parse(numberOfGuests) == 1)
            {
                CustomMessageBox.ShowCustomMessageBox("You have successfully booked a tour for " + numberOfGuests + " person");
                if (guest.Vouchers.Count != 0)
                {
                    CustomMessageBox.ShowCustomMessageBox("The system has detected that you have an unused voucher. You can use them now.");
                    SecondGuestMyVouchersView secondGuestMyVouchers = new SecondGuestMyVouchersView(guest.Id, chosenTour);
                    secondGuestMyVouchers.ShowDialog();
                }
            }
            else
            {
                CustomMessageBox.ShowCustomMessageBox("You have successfully booked a tour for " + numberOfGuests + " people");
                if (guest.Vouchers.Count != 0)
                {
                    CustomMessageBox.ShowCustomMessageBox("The system has detected that you have an unused voucher. You can use them now.");
                    SecondGuestMyVouchersView secondGuestMyVouchers = new SecondGuestMyVouchersView(guest.Id, chosenTour);
                    secondGuestMyVouchers.ShowDialog();
                }
            }
        }
        public void FreePlaceMessage(int maxGuests)
        {
            if (maxGuests == 1)
            {
                CustomMessageBox.ShowCustomMessageBox("There is space for " + maxGuests + " more person");
            }
            else
            {
                CustomMessageBox.ShowCustomMessageBox("There is space for " + maxGuests + " more people");
            }
        }
        public List<Tour> GetFilteredTours(Location location, DateTime selectedDate)
        {
            List<Tour> filteredTours = FilterToursByDate(selectedDate);
            filteredTours = FilterToursByLocation(filteredTours, location, selectedDate);

            if (filteredTours.Count == 0)
            {
                CustomMessageBox.ShowCustomMessageBox("Unfortunately, it is not possible to make a reservation. All tours at that location are booked.");
            }

            return filteredTours;
        }

        private List<Tour> FilterToursByDate(DateTime selectedDate) //ovo prebaciti u tourservice
        {
            List<Tour> _tours = Injector.CreateInstance<ITourService>().GetAll();
            List<Tour> filteredTours = new List<Tour>();

            foreach (Tour tour in _tours)
            {
                GoThroughTourDates(tour, selectedDate);
            }

            return filteredTours;
        }
        public void GoThroughTourDates(Tour tour, DateTime selectedDate)
        {
            List<TourDateTime> startingTimeCopy = tour.StartingTime.ToList();

            foreach (TourDateTime tdt in startingTimeCopy)
            {
                if (tdt.StartingDateTime == selectedDate)
                {
                    tour.StartingTime.Remove(tdt);
                }
                else
                {
                    GoThroughBookedToursDates(tour, selectedDate, tdt);
                }
            }
        }
        public void GoThroughBookedToursDates(Tour tour, DateTime selectedDate, TourDateTime tdt)
        {
            foreach (TourReservation tourReservation in _tourReservationRepository.GetAll())
            {
                if (tourReservation.GuestsNumberPerReservation == 0 && tourReservation.ReservationStartingTime == tdt.StartingDateTime)
                {
                    tour.StartingTime.Remove(tdt);
                }
            }
        }
        private List<Tour> FilterToursByLocation(List<Tour> filteredTours, Location location, DateTime selectedDate)
        {
            List<Tour> filteredToursCopy = new List<Tour>(filteredTours);
            List<Tour> _tours = Injector.CreateInstance<ITourService>().GetAll();

            foreach (Tour tour in _tours)
            {
                if (tour.Location.City == location.City && tour.Location.Country == location.Country && tour.StartingTime.Count != 0)
                {
                    filteredTours.Add(tour);
                }
            }
            return filteredTours;
        }

        public List<TourReservation> GetAll()
        {
            return _tourReservationRepository.GetAll();
        }

        public TourReservation GetByID(int id)
        {
            return _tourReservationRepository.GetByID(id);  
        }
        public List<TourReservation> GetUserReservations(int guestId)
        {
            return _tourReservationRepository.GetUserReservations(guestId);
        }
    }
}