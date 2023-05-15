using BookingProject.Controller;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.View.GuideViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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
    /// Interaction logic for LiveTourView.xaml
    /// </summary>
    /// 
    public partial class LiveTourView : Window
    {
        public LiveTourView(TourTimeInstance chosenTour)
        {
            InitializeComponent();
            LiveTourViewModel ViewModel = new LiveTourViewModel(chosenTour);
            this.DataContext = ViewModel;
            KeyPointDataGrid.ItemsSource = ViewModel._keyPoints;

        }
    }
}