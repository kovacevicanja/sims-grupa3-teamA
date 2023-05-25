using BookingProject.Controller;
using BookingProject.Domain;
using BookingProject.Domain.Enums;
using Microsoft.Win32;
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
using System.Windows.Shapes;
using BookingProject.Controllers;
using BookingProject.View.OwnerViewModel;
using System.Windows.Navigation;

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for OwnersApprovingDenyingRequestView.xaml
    /// </summary>
    public partial class OwnersApprovingDenyingRequestView : Page
    {
        public OwnersApprovingDenyingRequestView(RequestAccommodationReservation request, NavigationService navigationService)
        {
            InitializeComponent();
            this.DataContext = new OwnersApprovingDenyingRequestViewModel(request, navigationService);
        }
        private void Comment_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Comment.Text.Length > 0)
            {
                Button_Comment.IsEnabled = false;
            }
            else
            {
                Button_Comment.IsEnabled = true;
            }
        }
    }
}
