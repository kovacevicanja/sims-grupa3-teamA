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
using BookingProject.Controllers;
using BookingProject.Domain;


namespace BookingProject.Controller
{
    public class TourReservationController : ISubject
    {
        private readonly List<IObserver> observers;

        private readonly TourReservationHandler _reservationHandler;
        private readonly ToursGuestsHandler _toursGuestsHandler;

        private List<TourReservation> _reservations;
        private List<ToursGuests> _toursGuests;
        private TourReservation tourReservation { get; set; }

        private List<Tour> _tours { get; set; }
        private TourController _tourController { get; set; }

        private ToursGuestsController _toursGuestsController { get; set; }

        private List<Guest2> _guests { get; set; }
        private Guest2Controller _guest2Controller { get; set; }


        public TourReservationController()
        {
            _reservationHandler = new TourReservationHandler();

            _toursGuestsHandler = new ToursGuestsHandler();

            _reservations = new List<TourReservation>();

            //_toursGuests = new List<ToursGuests>(); //

           // _toursGuestsController = new ToursGuestsController(); //

            //_toursGuests = new List<ToursGuests>(_toursGuestsController.GetAll()); //
           
            tourReservation = new TourReservation();

            _tourController = new TourController();
            _tours = new List<Tour>(_tourController.GetAll());

            _guest2Controller = new Guest2Controller();//
            _guests = new List<Guest2>(_guest2Controller.GetAll());//

            //_toursGuestsController = new ToursGuestsController();
            // _guests = new List<Guest2>(_toursGuestsController.GetAll());

            observers = new List<IObserver>();


            Load();
        }

        public void Load()
        {
            _reservations = _reservationHandler.Load();
            TourReservationBind();
            GuestReservationBind();
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

        
        public void TourReservationBind()
        {
            _tourController.Load();
            foreach (TourReservation reservation in _reservations)
            {
                Tour tour = _tourController.GetByID(reservation.Tour.Id);
                reservation.Tour = tour;
            }
        }

        public void GuestReservationBind()
        {
            _tourController.Load();
            foreach (TourReservation reservation in _reservations)
            {
                Guest2 guest = _guest2Controller.GetByID(reservation.Guest.Id);
                reservation.Guest = guest;
            }
        }


        public void SaveReservationToFile(Tour choosenTour, string numberOfGuests, DateTime selectedDate, Guest2 guest)
        {
            //List<Guest2> guests = new List<Guest2>();
            //guests.Add(guest);
            _guests.Add(guest);

            TourReservation reservation = new TourReservation(GenerateId(), choosenTour, choosenTour.MaxGuests - int.Parse(numberOfGuests), selectedDate, guest);
            //reservation.Guests.Add(guest); //
            guest.MyTours.Add(reservation); //
            _reservations.Add(reservation);
            _reservationHandler.Save(_reservations);

            //ToursGuests tg = new ToursGuests(GenerateId(), reservation, guest);
            //_toursGuests.Add(tg);
            //_toursGuestsHandler.Save(_toursGuests);

        }

        public void SaveSameReservationToFile(Tour choosenTour, TourReservation tourReservation, string numberOfGuests, DateTime selectedDate, Guest2 guest)
        {
            foreach (TourReservation tr in _reservations)
            {
                if (tr.Id == tourReservation.Id)
                {
                    tr.GuestsNumberPerReservation -= int.Parse(numberOfGuests);
                    _reservationHandler.Save(_reservations);
                }
            }
            List<TourReservation> reservationsCopy = new List<TourReservation>(_reservations);
            List<TourReservation> reservationsToRemove = new List<TourReservation>(); 

            foreach (TourReservation tr in reservationsCopy)
            {
                if (tr.Guest.Id == guest.Id && tr.Id == tourReservation.Id && selectedDate == tr.ReservationStartingTime)
                {
                    reservationsToRemove.Add(tr); 
                }
            }

            foreach (TourReservation tr in reservationsToRemove)
            {
                reservationsCopy.Remove(tr); 
            }

            TourReservation newReservation = new TourReservation(tourReservation.Id, choosenTour, tourReservation.GuestsNumberPerReservation, selectedDate, guest);
            reservationsCopy.Add(newReservation);
            _reservationHandler.Save(reservationsCopy);

            /*

            List<TourReservation> reservationsCopy = new List<TourReservation>(_reservations);
            List<TourReservation> reservationsToRemove = new List<TourReservation>();


            foreach (TourReservation tr in _reservations)
            {
                if (tr.Id == tourReservation.Id)
                {
                    tr.GuestsNumberPerReservation -= int.Parse(numberOfGuests);
                    _reservationHandler.Save(_reservations);
                }
            }

            foreach (TourReservation tr in reservationsCopy)
            {
                if (tr.Guest.Id == guest.Id && tr.Id == tourReservation.Id && selectedDate == tr.ReservationStartingTime)
                {
                    reservationsCopy.Remove(tourReservation);
                } 
            }
                    //TourReservation newReservation2 = new TourReservation(tourReservation.Id, choosenTour, tourReservation.GuestsNumberPerReservation, selectedDate, guest);
                    //newReservation.Guests.Add(guest); //
                    //guest.MyTours.Add(newReservation); //
                    //_reservations.Add(newReservation2);
                    //_reservationHandler.Save(_reservations);
          
             TourReservation newReservation = new TourReservation(tourReservation.Id, choosenTour, tourReservation.GuestsNumberPerReservation, selectedDate, guest);
             //newReservation.Guests.Add(guest); //
             //guest.MyTours.Add(newReservation); //
              reservationsCopy.Add(newReservation);
              _reservationHandler.Save(reservationsCopy);
                

            /*if (tourReservation.Guest.Id == guest.Id)
            {
                _reservations.Remove(tourReservation);
                TourReservation newReservation = new TourReservation(tourReservation.Id, choosenTour, tourReservation.GuestsNumberPerReservation - int.Parse(numberOfGuests), selectedDate, guest);
                //newReservation.Guests.Add(guest); //
                guest.MyTours.Add(newReservation); //
                _reservations.Add(newReservation);
                _reservationHandler.Save(_reservations);
            }*/

            //ToursGuests tg = new ToursGuests(GenerateId(), newReservation, guest);
            //_toursGuests.Add(tg);
            //_toursGuestsHandler.Save(_toursGuests);

            //ako rez vec postoji, znaci da ju je neki gost s nekim idjem zauzeo
            //znaci da moram da uzmem sve info koje mi trebaju
            //ako je id isti kao sto je u rez, onda odradi remove, ako nije onda ostavi !!!!!

        }

        public bool BookingSuccess(Tour choosenTour, string numberOfGuests, DateTime selectedDate, Guest2 guest)
        {
            if (_reservations.Count() == 0)
            {
                if (TryReservation(choosenTour, numberOfGuests, selectedDate, guest)) { return true; }
                else { return false; }
            }
            else 
            {
                if (GoThroughReservations(choosenTour, numberOfGuests, selectedDate, guest)) { return true; }
                else { return false; }
            }
       }

        public bool GoThroughReservations(Tour choosenTour, string numberOfGuests, DateTime selectedDate, Guest2 guest)
        {
            foreach (TourReservation tourReservation in _reservations)
            {
                if (tourReservation.Tour.Id == choosenTour.Id && tourReservation.ReservationStartingTime == selectedDate)
                {
                    if (int.Parse(numberOfGuests) <= tourReservation.GuestsNumberPerReservation)
                    {
                        SaveSameReservationToFile(choosenTour, tourReservation, numberOfGuests, selectedDate, guest);
                        return true;

                    }
                    else
                    {
                        if (tourReservation.GuestsNumberPerReservation == 0)
                        {
                            FullyBookedTours(choosenTour, selectedDate, guest);
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
            if (TryReservation(choosenTour, numberOfGuests, selectedDate, guest)) { return true; }
            else { return false; }
           
        }

        public bool TryReservation (Tour choosenTour, string numberOfGuests, DateTime selectedDate, Guest2 guest)
        {
            if (int.Parse(numberOfGuests) <= choosenTour.MaxGuests)
            {
                SaveReservationToFile(choosenTour, numberOfGuests, selectedDate, guest);
                return true;

            }
            else
            {
                FreePlaceMessage(choosenTour.MaxGuests);
                return false;
            }
        }


        public void TryToBook(Tour choosenTour, string numberOfGuests, DateTime selectedDate, Guest2 guest)
        {
            if (int.Parse(numberOfGuests) <= 0)
            {
                MessageBox.Show("If you want to make a reservation, you must enter the reasonable number of people.");
            }
            else
            {
                if (BookingSuccess(choosenTour, numberOfGuests, selectedDate, guest))
                {
                    SuccessfulReservationMessage(numberOfGuests);
                }
            }
        }

        public void FullyBookedTours(Tour choosenTour, DateTime selectedDate, Guest2 guest)
        {
            MessageBox.Show("The tour is fully booked. The system will offer you tours at the same location.");
            ReservationTourOtherOffersView reservationTourOtherOffersView = new ReservationTourOtherOffersView(choosenTour, selectedDate, guest);
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
