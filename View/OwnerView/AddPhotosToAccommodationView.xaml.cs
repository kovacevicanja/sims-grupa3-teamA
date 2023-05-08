using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.View.OwnerViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
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
    /// Interaction logic for AddPhotosToAccommodationView.xaml
    /// </summary>
    public partial class AddPhotosToAccommodationView : Window
    {
        public AddPhotosToAccommodationView()
        {
            InitializeComponent();
            this.DataContext = new AddPhotosToAccommodationViewModel();
        }
    }
}
