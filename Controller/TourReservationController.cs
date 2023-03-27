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
using BookingProject.ConversionHelp;


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

        public void SaveReservationToFile(Tour choosenTour, string numberOfGuests, DateTime selectedDate)
        {
            TourReservation reservation = new TourReservation(GenerateId(), choosenTour.Id, choosenTour.MaxGuests - int.Parse(numberOfGuests), selectedDate);
            _reservations.Add(reservation);
            _reservationHandler.Save(_reservations);
        }

        public void SaveSameReservationToFile(Tour choosenTour, TourReservation tourReservation, string numberOfGuests, DateTime selectedDate)
        {
            _reservations.Remove(tourReservation);
            TourReservation newReservation = new TourReservation(tourReservation.Id, choosenTour.Id, tourReservation.GuestsNumberPerReservation - int.Parse(numberOfGuests), selectedDate);
            _reservations.Add(newReservation);
            _reservationHandler.Save(_reservations);
        }

        public bool BookingSuccess(Tour choosenTour, string numberOfGuests, DateTime selectedDate)
        {
            if (_reservations.Count() == 0)
            {
                if (TryReservation(choosenTour, numberOfGuests, selectedDate)) { return true; }
                else { return false; }
            }
            else 
            {
                if (GoThroughReservations(choosenTour, numberOfGuests, selectedDate)) { return true; }
                else { return false; }
            }
       }

        public bool GoThroughReservations(Tour choosenTour, string numberOfGuests, DateTime selectedDate)
        {
            foreach (TourReservation tourReservation in _reservations)
            {
                if (tourReservation.TourId == choosenTour.Id && tourReservation.ReservationStartingTime == selectedDate)
                {
                    if (int.Parse(numberOfGuests) <= tourReservation.GuestsNumberPerReservation)
                    {
                        SaveSameReservationToFile(choosenTour, tourReservation, numberOfGuests, selectedDate);
                        return true;

                    }
                    else
                    {
                        if (tourReservation.GuestsNumberPerReservation == 0)
                        {
                            FullyBookedTours(choosenTour, selectedDate);
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
            if (TryReservation(choosenTour, numberOfGuests, selectedDate)) { return true; }
            else { return false; }
           
        }

        public bool TryReservation (Tour choosenTour, string numberOfGuests, DateTime selectedDate)
        {
            if (int.Parse(numberOfGuests) <= choosenTour.MaxGuests)
            {
                SaveReservationToFile(choosenTour, numberOfGuests, selectedDate);
                return true;

            }
            else
            {
                FreePlaceMessage(choosenTour.MaxGuests);
                return false;
            }
        }


        public void TryToBook(Tour choosenTour, string numberOfGuests, DateTime selectedDate)
        {
            if (int.Parse(numberOfGuests) <= 0)
            {
                MessageBox.Show("If you want to make a reservation, you must enter the reasonable number of people.");
            }
            else
            {
                if (BookingSuccess(choosenTour, numberOfGuests, selectedDate))
                {
                    SuccessfulReservationMessage(numberOfGuests);
                }
            }
        }

        public void FullyBookedTours(Tour choosenTour, DateTime selectedDate)
        {
            MessageBox.Show("The tour is fully booked. The system will offer you tours at the same location.");
            ReservationTourOtherOffersView reservationTourOtherOffersView = new ReservationTourOtherOffersView(choosenTour, selectedDate);
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

        public List<Tour> GetFilteredTours(Location location, DateTime selectedDate)
        {
            List<Tour> filteredTours = FilterToursByDate(selectedDate);
            filteredTours = FilterToursByLocation(filteredTours, location, selectedDate);

            if (filteredTours.Count == 0)
            {
                MessageBox.Show("Unfortunately, it is not possible to make a reservation. All tours at that location are booked.");
            }

            return filteredTours;
        }

        private List<Tour> FilterToursByDate(DateTime selectedDate)
        {
            List<Tour> filteredTours = new List<Tour>();

            foreach (Tour tour in _tours)
            {
                GoThroughTourDates(tour, selectedDate);
            }

            return filteredTours;
        }

        public void GoThroughTourDates (Tour tour, DateTime selectedDate)
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
            foreach (TourReservation tourReservation in _reservations)
            {
                if (tourReservation.GuestsNumberPerReservation == 0 && tourReservation.ReservationStartingTime == tdt.StartingDateTime)
                {
                    tour.StartingTime.Remove(tdt);
                }
            }
        }
        private List<Tour> FilterToursByLocation(List <Tour> filteredTours, Location location, DateTime selectedDate) 
        {
            List<Tour> filteredToursCopy = new List<Tour>(filteredTours);
         
            foreach (Tour tour in _tours)
            {
                if (tour.Location.City == location.City && tour.Location.Country == location.Country && tour.StartingTime.Count != 0) 
                {
                    filteredTours.Add(tour);
                }
            }

            return filteredTours;
        }

    }
}
