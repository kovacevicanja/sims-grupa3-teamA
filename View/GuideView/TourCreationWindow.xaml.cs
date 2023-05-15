using BookingProject.Controller;
using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Domain.Enums;
using BookingProject.Domain.Images;
using BookingProject.Services;
using BookingProject.Services.Interfaces;
using BookingProject.View.GuideView;
using BookingProject.View.GuideViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingProject.View.GuideView
{
    /// <summary>
    /// Interaction logic for TourCreationView.xaml
    /// </summary>
    public partial class TourCreationWindow : Window
    {
        public TourCreationWindow(bool isLanguagePicked, bool isLocationPicked )
        {
            InitializeComponent();
            this.DataContext = new TourCreationViewModel(isLanguagePicked, isLocationPicked);

        }
    }
}