using BookingProject.Controller;
using BookingProject.Model.Enums;
using BookingProject.Model;
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
using BookingProject.View.GuideViewModel;

namespace BookingProject.View.GuideView
{
    /// <summary>
    /// Interaction logic for MyToursWindow.xaml
    /// </summary>
    public partial class MyToursWindow : Window
    {


        public MyToursWindow()
        {
            InitializeComponent();
            MyToursViewModel ViewModel= new MyToursViewModel();
            this.DataContext = ViewModel;
            TourDataGrid.ItemsSource = ViewModel._instances;
        }

    }
}
