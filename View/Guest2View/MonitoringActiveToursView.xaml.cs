﻿using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.Model.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for MonitoringActiveToursView.xaml
    /// </summary>
    public partial class MonitoringActiveToursView : Window
    {
        public Tour Tour { get; set; }
        public KeyPointController KeyPointController { get; set; }
        public ObservableCollection<KeyPoint> KeyPoints { get; set; }
        public MonitoringActiveToursView(Tour tour)
        {
            InitializeComponent();
            this.DataContext = this;

            Tour = tour;
            KeyPointController = new KeyPointController();

            KeyPoints = new ObservableCollection<KeyPoint>(KeyPointController.GetToursKeyPoints(Tour.Id));

            KeyPointDataGrid.ItemsSource = KeyPoints;
        }

        public void Button_Click_Cancel (object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}