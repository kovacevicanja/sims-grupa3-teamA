using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingProject.FileHandler;
using BookingProject.Model;
using OisisiProjekat.Observer;

namespace BookingProject.Controller
{
    internal class TourReservationController : ISubject
    {
        private readonly List<IObserver> observers;

        private readonly TourReservationHandler _reservationHandler;

        private List<TourReservation> _reservations;

        private TourReservation tourReservation { get; set;  }

        public TourReservationController()
        {
            _reservationHandler = new TourReservationHandler();
            _reservations = new List<TourReservation>();
            tourReservation = new TourReservation();
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
        
        /*
        public int GenerateId()
        {
            int maxId = 0;
            if (_reservations.Count() == 0)
            {
                return maxId;
            }
            else
            {
                foreach (TourReservation tourReservation in _reservations)
                {
                    if (tourReservation.Id > maxId)
                    {
                        maxId = tourReservation.Id;
                    }
                }

                return maxId + 1;
            }
        }
        */

        public void SaveTourReservation (TourReservation tourReservation)
        {
            Load();
            foreach (TourReservation tr in _reservations)
            {
                if (tr.TourId == tourReservation.TourId)
                {
                    _reservations.Remove(tourReservation);
                    _reservationHandler.Save(_reservations);
                    _reservations.Add(tourReservation);
                    _reservationHandler.Save(_reservations);
                } 
                else
                {
                    //tourReservation.Id = GenerateId();
                    _reservations.Add(tourReservation);
                    _reservationHandler.Save(_reservations);
                }
            }
        }

        /*public void SaveTourReservation(TourReservation tourReservation)
        {
            foreach(TourReservation tr in _reservations)
            {
                if(tourReservation.Id == tr.TourId)
                {
                    _reservations.Remove(tourReservation);
                    _reservationHandler.Save(_reservations);
                    _reservations.Add(tourReservation);
                    _reservationHandler.Save(_reservations);
                }
                else
                {
                    _reservations.Add(tourReservation);
                    _reservationHandler.Save(_reservations);
                }
            }
        }*/

       public void SaveSameTour (TourReservation tourReservation)
        {
            _reservationHandler.Save(_reservations);
        }



        /*public bool TryToBook(Tour choosenTour, string numberOfGuest)
        {
            if (_reservations.Count() == 0)
            {
                if (int.Parse(numberOfGuest) <= choosenTour.MaxGuests)
                {
                    //choosenTour.MaxGuests = choosenTour.MaxGuests - int.Parse(numberOfGuest);
                    tourReservation.TourId = choosenTour.Id;
                    tourReservation.GuestsNumberPerReservation = choosenTour.MaxGuests - int.Parse(numberOfGuest);
                    tourReservation.Id = GenerateId();
                    //tourReservationObservable.Add(tourReservation);
                    //_reservations.Add(tourReservation);
                    SaveTourReservation(tourReservation);
                    return true;
                }
                else
                {
                    //MessageBox.Show("You have exceeded the maximum number of guests per tour!");
                    return false;
                }
            }
            else
            {
                foreach (TourReservation tourReservation in _reservations)
                {

                    if (choosenTour.Id == tourReservation.TourId)
                    {
                        if (int.Parse(numberOfGuest) <= tourReservation.GuestsNumberPerReservation)
                        {
                            tourReservation.GuestsNumberPerReservation = tourReservation.GuestsNumberPerReservation - int.Parse(numberOfGuest);
                            //tourReservation.Id = GenerateId();
                            SaveTourReservation(tourReservation);

                            //tourReservationObservable.Add(tourReservation);
                            //SaveTourReservation(tourReservation);
                            return true;
                        }
                        else
                        {
                            //MessageBox.Show("You have exceeded the maximum number of guests per tour!");
                            return false;
                        }
                    }
                    else
                    {
                        if (int.Parse(numberOfGuest) <= choosenTour.MaxGuests)
                        {
                            //choosenTour.MaxGuests = choosenTour.MaxGuests - int.Parse(numberOfGuest);
                            tourReservation.TourId = choosenTour.Id;
                            tourReservation.GuestsNumberPerReservation = choosenTour.MaxGuests - int.Parse(numberOfGuest);
                            tourReservation.Id = GenerateId();
                            //tourReservationObservable.Add(tourReservation);
                            SaveTourReservation(tourReservation);
                            return true;
                        }
                        else
                        {
                            return false;
                            //MessageBox.Show("You have exceeded the maximum number of guests per tour!");
                        }
                    }
                }
                return false;
            }

           
        } 

        /*public ObservableCollection<TourReservation> TryToBook(ObservableCollection<TourReservation> tourReservationObservable, Tour choosenTour, string numberOfGuest)
        {
            if (tourReservationObservable.Count() == 0)
            {
                if (int.Parse(numberOfGuest) <= choosenTour.MaxGuests)
                {
                    //choosenTour.MaxGuests = choosenTour.MaxGuests - int.Parse(numberOfGuest);
                    tourReservation.TourId = choosenTour.Id;
                    tourReservation.GuestsNumberPerReservation = choosenTour.MaxGuests - int.Parse(numberOfGuest);
                    tourReservation.Id = GenerateId();
                    //tourReservationObservable.Add(tourReservation);
                    //_reservations.Add(tourReservation);
                    SaveTourReservation(tourReservation);
                }
                else
                {
                    MessageBox.Show("You have exceeded the maximum number of guests per tour!");
                }
            }
            else
            {
                foreach (TourReservation tourReservation in _reservations)
                {

                    if (choosenTour.Id == tourReservation.TourId)
                    {
                        if (int.Parse(numberOfGuest) <= tourReservation.GuestsNumberPerReservation)
                        {
                            tourReservation.GuestsNumberPerReservation = tourReservation.GuestsNumberPerReservation - int.Parse(numberOfGuest);
                            tourReservation.Id = GenerateId();
                            SaveTourReservation(tourReservation);

                             //tourReservationObservable.Add(tourReservation);
                            //SaveTourReservation(tourReservation);
                        }
                        else
                        {
                            MessageBox.Show("You have exceeded the maximum number of guests per tour!");
                        }
                    }
                    else
                    {
                        if (int.Parse(numberOfGuest) <= choosenTour.MaxGuests)
                        {
                            //choosenTour.MaxGuests = choosenTour.MaxGuests - int.Parse(numberOfGuest);
                            tourReservation.TourId = choosenTour.Id;
                            tourReservation.GuestsNumberPerReservation = choosenTour.MaxGuests - int.Parse(numberOfGuest);
                            tourReservation.Id = GenerateId();
                            //tourReservationObservable.Add(tourReservation);
                            SaveTourReservation(tourReservation);
                        }
                        else
                        {
                            MessageBox.Show("You have exceeded the maximum number of guests per tour!");
                        }
                    }
                 }
            }

            return tourReservationObservable;
        }*/

        public bool BookingSuccess(Tour choosenTour, string numberOfGuests)
        {
            if (_reservations.Count() == 0)
            {
                if (int.Parse(numberOfGuests) <= choosenTour.MaxGuests)
                {
                    TourReservation reservation = new TourReservation(GenerateId(), choosenTour.Id, choosenTour.MaxGuests - int.Parse(numberOfGuests));
                    _reservations.Add(reservation);
                    _reservationHandler.Save(_reservations);
                    return true;

                }
                else
                {
                    MessageBox.Show("Ima jos mesta za " + choosenTour.MaxGuests);
                    return false;                    
                    //ovde je slucaj kada ima mesta, ali smo promasili broj
                }

            }
            else
            {
                foreach(TourReservation tr in _reservations)
                {
                    if(tr.TourId == choosenTour.Id)
                    {
                        if (int.Parse(numberOfGuests) <= tr.GuestsNumberPerReservation)
                        {
                            int idR = tr.Id;
                            int gnpr = tr.GuestsNumberPerReservation;
                            _reservations.Remove(tr);
                            TourReservation newReservation = new TourReservation(idR, choosenTour.Id, gnpr - int.Parse(numberOfGuests));
                            _reservations.Add(newReservation);
                            _reservationHandler.Save(_reservations);
                            return true;

                        }
                        else
                        {
                            if (tr.GuestsNumberPerReservation == 0)
                            {
                                MessageBox.Show("Tura je skroz popunjena");
                                return false;
                                //solucije za druge ture na istom mestu

                            }
                            else
                            {
                                MessageBox.Show("Ima jos mesta za " + tr.GuestsNumberPerReservation);
                                return false;
                                //promena broja ljudi pa da proba da rezervise
                                //Gost može promeniti broj ljudi ili odustati od ture (odustati od kreiranje rezervacije).
                            }
                        }
                    }
                }
                if (int.Parse(numberOfGuests) <= choosenTour.MaxGuests)
                {
                     TourReservation reservation = new TourReservation(GenerateId(), choosenTour.Id, choosenTour.MaxGuests - int.Parse(numberOfGuests));
                    _reservations.Add(reservation);
                    _reservationHandler.Save(_reservations);
                    return true;

                }
                else
                {
                    MessageBox.Show("Ima jos mesta za " + choosenTour.MaxGuests);
                    return false;
                    //ovde je slucaj kada ima mesta, ali smo promasili broj
                }

            }
        }

        public void TryToBook(Tour choosenTour, string numberOfGuests)
        {
            if (int.Parse(numberOfGuests) == 0)
            {
                MessageBox.Show("If you want to make a reservation, you must enter the number of people.");
            }
            else
            {
                if (BookingSuccess(choosenTour, numberOfGuests))
                {
                    if (int.Parse(numberOfGuests) == 1)
                    {
                        MessageBox.Show("You have successfully booked a tour for " + numberOfGuests + " people");
                    }
                    else
                    {
                        MessageBox.Show("You have successfully booked a tour for " + numberOfGuests + " people");
                    }
                }
                /*
                else
                {
                    MessageBox.Show("Unfortunately, it is not possible to make a reservation. The number of vacancies has been filled.");
                    //ovde treba ispitati da li je tura SKROZ POPUNJENA, ili ima mesta za jos neki odredjen broj ljudi 
                    //ponuditi druge ture na istom mestu
                }
                */
            }
        }
    }
}
