using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.View.Guest2ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for ActiveToursView.xaml
    /// </summary>
    public partial class ActiveToursView : Page
    {
        public ActiveToursView(List<int> activeToursIds, int guestId, NavigationService navigationService)
        {
            InitializeComponent();
            this.DataContext = new ActiveToursViewModel(activeToursIds, guestId, navigationService);
        }
    }
}