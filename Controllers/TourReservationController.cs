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
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

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
        public VoucherController VoucherController { get; set; }
        public Guest2 Guest { get; set; }

        public TourTimeInstanceController TourTimeInstanceController { get; set; } //
        public TourReservationController()
        {
            _reservationHandler = new TourReservationHandler();

            _toursGuestsHandler = new ToursGuestsHandler();

            _reservations = new List<TourReservation>();

            tourReservation = new TourReservation();

            _tourController = new TourController();
            _tours = new List<Tour>(_tourController.GetAll());

            _guest2Controller = new Guest2Controller();//
            _guests = new List<Guest2>(_guest2Controller.GetAll());//

            VoucherController = new VoucherController();

            TourTimeInstanceController = new TourTimeInstanceController(); //

            observers = new List<IObserver>();

            Load();
        }

        public void Load()
        {
            _reservations = _reservationHandler.Load();
            TourReservationBind();
            //GuestReservationBind();
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

        public void ReservationGuestBind(int id)
        {
            _tourController.Load();
            foreach (TourReservation reservation in _reservations)
            {
                if (reservation.Guest.Id == -1)
                {
                    reservation.Guest.Id = id;
                }
            }
            NotifyObservers();
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

        public List<TourReservation> GetUserReservations(int guestId)
        {
            List<TourReservation> guestsTours = new List<TourReservation>();
            foreach (TourReservation tr in _reservations)
            {
                if (tr.Guest.Id == guestId)
                {
                    guestsTours.Add(tr);
                }
            }
            return guestsTours;
        }

        public void SaveReservationToFile(Tour choosenTour, string numberOfGuests, DateTime selectedDate, User guest)
        {
            //_guests.Add(guest);

            TourReservation reservation = new TourReservation(GenerateId(), choosenTour, choosenTour.MaxGuests - int.Parse(numberOfGuests), selectedDate, guest);

            guest.MyTours = GetUserReservations(guest.Id);
            
            ReservationGuestBind(guest.Id); //ovde bindujem
            guest.MyTours.Add(reservation);
            _reservations.Add(reservation);
            _reservationHandler.Save(_reservations);
        }

        public void SaveSameReservationToFile(Tour choosenTour, TourReservation tourReservation, string numberOfGuests, DateTime selectedDate, User guest)
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
        }

        public bool BookingSuccess(Tour choosenTour, string numberOfGuests, DateTime selectedDate, User guest)
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
        public bool GoThroughReservations(Tour choosenTour, string numberOfGuests, DateTime selectedDate, User guest)
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
        public bool TryReservation(Tour choosenTour, string numberOfGuests, DateTime selectedDate, User guest)
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
        public void ShowCustomMessageBox(string messageText)
        {
            Window customMessageBox = new Window
            {
                Title = "Message",
                FontWeight = FontWeights.Bold,
                Height = 200,
                Width = 300,
                WindowStyle = WindowStyle.ThreeDBorderWindow,
                ResizeMode = ResizeMode.NoResize,
                Background = Brushes.LightBlue,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            TextBlock message = new TextBlock
            {
                Text = messageText,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                TextAlignment = TextAlignment.Center,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(20, 20, 20, 0)
            };

            Button okButton = new Button
            {
                Content = "OK",
                Width = 80,
                Height = 30,
                Margin = new Thickness(0, 10, 0, 0),
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            okButton.Click += (o, args) =>
            {
                customMessageBox.Close();
            };
            StackPanel stackPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            stackPanel.Children.Add(message);
            stackPanel.Children.Add(okButton);

            customMessageBox.Content = stackPanel;
            customMessageBox.ShowDialog();
        }

        public void TryToBook(Tour choosenTour, string numberOfGuests, DateTime selectedDate, User guest)
        {
            if (int.Parse(numberOfGuests) <= 0)
            {
                ShowCustomMessageBox("If you want to make a reservation, you must enter the reasonable number of people.");
            }
            else
            {
                if (BookingSuccess(choosenTour, numberOfGuests, selectedDate, guest))
                {
                    SuccessfulReservationMessage(numberOfGuests, guest);
                }
            }
        }

        public void FullyBookedTours(Tour choosenTour, DateTime selectedDate, User guest)
        {
            ShowCustomMessageBox("The tour is fully booked. The system will offer you tours at the same location.");
            ReservationTourOtherOffersView reservationTourOtherOffersView = new ReservationTourOtherOffersView(choosenTour, selectedDate, guest.Id);
            reservationTourOtherOffersView.Show();
        }

        public void SuccessfulReservationMessage(string numberOfGuests, User guest) //ovde smanjujem vaucer
        {
            /*List<Voucher> vouchers = new List<Voucher>();
            vouchers = VoucherController.GetAll();

            foreach (Voucher voucher in vouchers)
            {
                if (guest.Id == voucher.Guest.Id)
                {
                    guest.MyVouchers.Add(voucher);
                }
            }*/

            guest.Vouchers = VoucherController.GetUserVouhers(guest.Id);

            if (int.Parse(numberOfGuests) == 1)
            {
                ShowCustomMessageBox("You have successfully booked a tour for " + numberOfGuests + " person");
                //VoucherController.Load(); //
                //_guest2Controller.Load(); //


                if (guest.Vouchers.Count != 0)
                {
                    ShowCustomMessageBox("The system has detected that you have an unused voucher. You can use them now.");
                    SecondGuestMyVouchersView secondGuestMyVouchers = new SecondGuestMyVouchersView(guest.Id);
                    secondGuestMyVouchers.ShowDialog();
                }
            }
            else
            {
                ShowCustomMessageBox("You have successfully booked a tour for " + numberOfGuests + " people");
                if (guest.Vouchers.Count != 0)
                {
                    ShowCustomMessageBox("The system has detected that you have an unused voucher. You can use them now.");
                    SecondGuestMyVouchersView secondGuestMyVouchers = new SecondGuestMyVouchersView(guest.Id);
                    secondGuestMyVouchers.ShowDialog();
                }
            }
        }

        public void FreePlaceMessage(int maxGuests)
        {
            if (maxGuests == 1)
            {
                ShowCustomMessageBox("There is space for " + maxGuests + " more person");
            }
            else
            {
                ShowCustomMessageBox("There is space for " + maxGuests + " more people");
            }
        }

        public List<Tour> GetFilteredTours(Location location, DateTime selectedDate)
        {
            List<Tour> filteredTours = FilterToursByDate(selectedDate);
            filteredTours = FilterToursByLocation(filteredTours, location, selectedDate);

            if (filteredTours.Count == 0)
            {
                ShowCustomMessageBox("Unfortunately, it is not possible to make a reservation. All tours at that location are booked.");
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
            foreach (TourReservation tourReservation in _reservations)
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
