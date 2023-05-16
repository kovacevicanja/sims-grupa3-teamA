using BookingProject.View.Guest2ViewModel;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookingProject.View.Guest2View
{
    /// <summary>
    /// Interaction logic for NewlyCreatedToursOfNeverAcceptedRequests.xaml
    /// </summary>
    public partial class NewlyCreatedToursOfNeverAcceptedRequestsView : Page
    {
        public NewlyCreatedToursOfNeverAcceptedRequestsView(int guestId, NavigationService navigationService)
        {
            InitializeComponent();
            this.DataContext = new NewlyCreatedToursOfNeverAcceptedRequestsViewModel(guestId, navigationService);
        }
    }
}
