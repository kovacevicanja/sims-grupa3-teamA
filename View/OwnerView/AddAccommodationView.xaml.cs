using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.Model.Images;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting;
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
    /// Interaction logic for AddAccommodationView.xaml
    /// </summary>
    public partial class AddAccommodationView : Window
    {
        public AddAccommodationView()
        {
            InitializeComponent();
            this.DataContext = new AddAccommodationViewModel();
        }
    }
}