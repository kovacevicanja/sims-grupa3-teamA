using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Domain.Images;
using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.Repositories.Implementations;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services;
using BookingProject.View.CustomMessageBoxes;
using BookingProject.View.Guest2ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Xml.Linq;

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for ToursAndGuidesEvaluationView.xaml
    /// </summary>
    public partial class ToursAndGuidesEvaluationView : Window
    {
        public ToursAndGuidesEvaluationView(Tour chosenTour, int guestId)
        {
            InitializeComponent();
            this.DataContext = new ToursAndGuidesEvaluationViewModel(chosenTour, guestId);
        }
    }
}