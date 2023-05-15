using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.View.Guest1ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    public partial class Guest1View : Window
    {
        public Guest1View()
        {
            InitializeComponent();
            this.DataContext = new Guest1ViewViewModel();
        }
    }
}