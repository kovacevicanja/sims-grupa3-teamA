﻿using BookingProject.View.Guest2ViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingProject.View.Guest2View
{
    /// <summary>
    /// Interaction logic for CreateComplexTourRequestView.xaml
    /// </summary>
    public partial class CreateComplexTourRequestView : Page
    {
        public CreateComplexTourRequestView(int guestId, NavigationService navigationService)
        {
            InitializeComponent();
            this.DataContext = new CreateComplexTourRequestViewModel(guestId, navigationService);
        }
    }
}