using BookingProject.Controller;
using BookingProject.FileHandler;
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
        public TourController TourController { get; set; }
        public LocationController LocationController { get; set; }
        public App()
        {

            TourController = new TourController();
            LocationController = new LocationController();
        }



    }
}
