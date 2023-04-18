using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.DependencyInjection;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public TourController TourController{ get; set; }
        public TourLocationController LocationController { get; set; }
        public TourImageController ImageController { get; set; }
        public KeyPointController KeyPointController { get; set; }
        public TourStartingTimeController StartingDateController { get; set; }

        public TourGuestController TourGuestController { get; set; }
        public TourTimeInstanceController TourTimeInstanceController { get; set; }
        public AccommodationController AccommodationController { get; set; }
        public AccommodationLocationController AccommodationLocationController { get; set; }
        public AccommodationImageController AccommodationImageController { get; set; }
        public GuestGradeController GuestGradeController { get; set; }

        public AccommodationReservationController AccommodationReservationController { get; set; }
        public VoucherController VoucherController { get; set; }

        public TourReservationController TourReservationController { get; set; }
        //
        public TourEvaluationController TourEvaluationController { get; set; }

        //
        public UserController UserController { get; set; }

        public TourPresenceController TourPresenceController { get; set; }

        public App()
        {
            /*
            //TourController = new TourController(); //
            //LocationController = new TourLocationController();
            ImageController = new TourImageController();
            //KeyPointController = new KeyPointController();  
            StartingDateController = new TourStartingTimeController();  
            AccommodationImageController= new AccommodationImageController();
            AccommodationLocationController = new AccommodationLocationController();
            AccommodationController = new AccommodationController();
            TourTimeInstanceController = new TourTimeInstanceController();
            TourGuestController = new TourGuestController();
            AccommodationReservationController = new AccommodationReservationController();
            GuestGradeController = new GuestGradeController();
            //TourEvaluationController = new TourEvaluationController();
            TourPresenceController= new TourPresenceController();
            //VoucherController = new VoucherController();

            GuestGradeController._accommodationController = AccommodationReservationController;
            AccommodationReservationController._guestGradeController= GuestGradeController;
            AccommodationReservationController._accommodationController = AccommodationController;

            //
            //TourReservationController = new TourReservationController();

            //

            AccommodationReservationController.Load();
            GuestGradeController.Load();

            //TourEvaluationController.Load();
            //TourReservationController.Load(); //
                                              //
            //UserController = new UserController();

            */
            Injector.Initialize();  
        }



    }
}
