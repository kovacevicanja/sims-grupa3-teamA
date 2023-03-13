using BookingProject.Controller;
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
        public LocationController LocationController { get;  set; }
        public AccommodationController AccommodationController { get;  set; }

        public App()
        {

            //TourController = new TourController();
            AccommodationController= new AccommodationController();
            LocationController = new LocationController();
            AccommodationController._locationController = LocationController;
        }
    }
}
