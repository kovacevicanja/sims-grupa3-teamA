using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.View.OwnersViewModel;
using BookingProject.View.OwnerViewModel;
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

namespace BookingProject.View.OwnersView
{
    /// <summary>
    /// Interaction logic for OwnersForumsView.xaml
    /// </summary>
    public partial class OwnersForumsView : Page
    {
        public NavigationService NavigationService { get; set; }
        public OwnersForumsView(NavigationService navigationService)
        {
            InitializeComponent();
            NavigationService = navigationService;
            this.DataContext = new OwnersForumsViewModel(navigationService);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var forum = ((Button)sender).DataContext as Forum;
            NavigationService.Navigate(new OneForumView(forum, NavigationService));
        }
    }
}
