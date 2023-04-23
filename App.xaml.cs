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
        public App()
        {
            Injector.Initialize();  
        }
    }
}