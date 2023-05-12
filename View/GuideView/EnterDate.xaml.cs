using BookingProject.Controller;
using BookingProject.Domain;
using BookingProject.ConversionHelp;
using System;
using System.Collections.Generic;
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
using System.Text.RegularExpressions;
using BookingProject.Model;
using BookingProject.View.GuideViewModel;

namespace BookingProject.View.GuideView
{
    /// <summary>
    /// Interaction logic for EnterDate.xaml
    /// </summary>
    public partial class EnterDate : Window
    {

        public EnterDate()
        {
            InitializeComponent();
            this.DataContext = new EnterDateViewModel();
        }

    }
}
