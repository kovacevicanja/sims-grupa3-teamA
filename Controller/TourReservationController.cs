using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingProject.FileHandler;
using BookingProject.Model;
using BookingProject.View;
using OisisiProjekat.Observer;

namespace BookingProject.Controller
{
    internal class TourReservationController : ISubject
    {
        private readonly List<IObserver> observers;

        private readonly TourReservationHandler _reservationHandler;

        private List<TourReservation> _reservations;
        private TourReservation tourReservation { get; set; }

        private List<Tour> _tours { get; set; }
        private TourController _tourController { get; set; }


        public TourReservationController()
        {
            _reservationHandler = new TourReservationHandler();
            _reservations = new List<TourReservation>();
            tourReservation = new TourReservation();

            _tourController = new TourController();
            _tours = new List<Tour>(_tourController.GetAll());

            Load();
        }

        public void Load()
        {
            _reservations = _reservationHandler.Load();
        }

        public List<TourReservation> GetAll()
        {
            return _reservations;
        }

        public TourReservation GetByID(int id)
        {
            return _reservations.Find(date => date.Id == id);
        }

        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update();
            }
        }

        public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            observers.Remove(observer);
        }

        public int GenerateId()
        {
            if (_reservations.Count == 0) return 0;
            return _reservations.Max(s => s.Id) + 1;

        }

        public void SaveReservationToFile(Tour choosenTour, string numberOfGuests)
        {
            TourReservation reservation = new TourReservation(GenerateId(), choosenTour.Id, choosenTour.MaxGuests - int.Parse(numberOfGuests));
            _reservations.Add(reservation);
            _reservationHandler.Save(_reservations);
        }

        public void SaveSameReservationToFile(Tour choosenTour, TourReservation tourReservation, string numberOfGuests)
        {
            _reservations.Remove(tourReservation);
            TourReservation newReservation = new TourReservation(tourReservation.Id, choosenTour.Id, tourReservation.GuestsNumberPerReservation - int.Parse(numberOfGuests));
            _reservations.Add(newReservation);
            _reservationHandler.Save(_reservations);
        }
        
        public bool BookingSuccess(Tour choosenTour, string numberOfGuests)
        {
            if (_reservations.Count() == 0)
            {
                if (int.Parse(numberOfGuests) <= choosenTour.MaxGuests)
                {
                    SaveReservationToFile(choosenTour, numberOfGuests);
                    return true;

                }
                else
                {
                    FreePlaceMessage(choosenTour.MaxGuests);
                    return false;
                }
            }
            else
            {
                foreach (TourReservation tourReservation in _reservations)
                {
                    if (tourReservation.TourId == choosenTour.Id)
                    {
                        if (int.Parse(numberOfGuests) <= tourReservation.GuestsNumberPerReservation)
                        {
                            SaveSameReservationToFile(choosenTour, tourReservation, numberOfGuests);
                            return true;

                        }
                        else
                        {
                            if (tourReservation.GuestsNumberPerReservation == 0)
                            {
                                FullyBookedTours(choosenTour);
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
                if (int.Parse(numberOfGuests) <= choosenTour.MaxGuests)
                {
                    SaveReservationToFile(choosenTour, numberOfGuests);
                    return true;
                }
                else
                {
                    FreePlaceMessage(choosenTour.MaxGuests);
                    return false;
                }
            }
        }

        /*
        public bool BookingSuccess(Tour chosenTour, string numberOfGuests)
        {
            int maxGuests = chosenTour.MaxGuests;
            int guestsPerReservation = GetGuestsPerReservation(chosenTour);

            if (int.Parse(numberOfGuests) <= guestsPerReservation || int.Parse(numberOfGuests) <= maxGuests)
            {
                SaveReservationToFile(chosenTour, numberOfGuests);
                return true;
            }
            else
            {
                FreePlaceMessage(guestsPerReservation);
                return false;
            }
        }

        private int GetGuestsPerReservation(Tour chosenTour)
        {
            foreach (TourReservation tourReservation in _reservations)
            {
                if (tourReservation.TourId == chosenTour.Id)
                {
                    if (tourReservation.GuestsNumberPerReservation == 0)
                    {
                        FullyBookedTours(chosenTour);
                        return -1;
                    }
                    else
                    {
                        return tourReservation.GuestsNumberPerReservation;
                    }
                }
            }

            return chosenTour.MaxGuests;
        }
        */

        public void TryToBook(Tour choosenTour, string numberOfGuests)
        {
            if (int.Parse(numberOfGuests) <= 0)
            {
                MessageBox.Show("If you want to make a reservation, you must enter the reasonable number of people.");
            }
            else
            {
                if (BookingSuccess(choosenTour, numberOfGuests))
                {
                    SuccessfulReservationMessage(numberOfGuests);
                }
            }
        }

        public void FullyBookedTours(Tour choosenTour)
        {
            MessageBox.Show("The tour is fully booked. The system will offer you tours at the same location.");
            ReservationTourOtherOffersView reservationTourOtherOffersView = new ReservationTourOtherOffersView(choosenTour);
            reservationTourOtherOffersView.Show();
        }

        public void SuccessfulReservationMessage(string numberOfGuests)
        {
            if (int.Parse(numberOfGuests) == 1)
            {
                MessageBox.Show("You have successfully booked a tour for " + numberOfGuests + " person");
            }
            else
            {
                MessageBox.Show("You have successfully booked a tour for " + numberOfGuests + " people");
            }
        }

        public void FreePlaceMessage(int maxGuests)
        {
            if (maxGuests == 1)
            {
                MessageBox.Show("There is space for " + maxGuests + " more person");
            }
            else
            {
                MessageBox.Show("There is space for " + maxGuests + " more people");
            }
        }

        public List<Tour> GetFilteredTours(Location location, int chosenTourId)
        {
            List<Tour> filteredTours = FilterToursByLocation(location, chosenTourId);
            filteredTours = RemoveFullyBookedTours(filteredTours);

            if (filteredTours.Count == 0)
            {
                MessageBox.Show("Unfortunately, it is not possible to make a reservation. All tours at that location are booked.");
            }

            return filteredTours;
        }

        private List<Tour> FilterToursByLocation(Location location, int chosenTourId)
        {
            List<Tour> filteredTours = new List<Tour>();

            foreach (Tour tour in _tours)
            {
                if (tour.Location.City == location.City && tour.Location.Country == location.Country && chosenTourId != tour.Id)
                {
                    filteredTours.Add(tour);
                }
            }

            return filteredTours;
        }

        private List<Tour> RemoveFullyBookedTours(List<Tour> filteredTours)
        {
            List<Tour> filteredToursCopy = new List<Tour>(filteredTours);

            foreach (TourReservation tourReservation in _reservations)
            {
                foreach (Tour tour in filteredToursCopy)
                {
                    if (tourReservation.TourId == tour.Id && tourReservation.GuestsNumberPerReservation == 0)
                    {
                        filteredTours.Remove(tour);
                    }
                }
            }

            return filteredTours;
        }
    }
}
