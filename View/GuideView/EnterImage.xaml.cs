using BookingProject.Controller;
using BookingProject.Domain.Enums;
using BookingProject.Domain;
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
using BookingProject.Domain.Images;
using System.Text.RegularExpressions;
using BookingProject.Model.Images;
using BookingProject.View.GuideViewModel;

namespace BookingProject.View.GuideView
{
    /// <summary>
    /// Interaction logic for EnterImage.xaml
    /// </summary>
    public partial class EnterImage : Window
    {

        public EnterImage()
        {
            InitializeComponent();
            this.DataContext = new EnterImageViewModel();
        }
    }
}