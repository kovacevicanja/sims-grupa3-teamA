using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.DependencyInjection;
using BookingProject.Localization;
using BookingProject.Styles;
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
        public static MessagingService MessagingService { get; } = new MessagingService();

        public void ChangeLanguage(string lang)
        {
            TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo(lang);
        }
        public App()
        {
            Injector.Initialize();
            TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        }
    }
}