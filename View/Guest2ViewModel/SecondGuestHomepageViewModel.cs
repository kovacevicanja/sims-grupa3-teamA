using BookingProject.Commands;
using BookingProject.ViewModelBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.View.Guest2ViewModel
{
    public class SecondGuestHomepageViewModel : ViewModelBase
    {
        /*
        public int GuestId { get; set; }
        public ViewModelBase _searchAndReservationToursViewModel;
        public RelayCommand MyAttendedToursCommand { get; }

        public SecondGuestHomepageViewModel(int guestId)
        {
            GuestId = guestId;
            _searchAndReservationToursViewModel = new ViewModelBase();
            MyAttendedToursCommand = new RelayCommand(Button_Click_MyAttendedTours, CanExecute);
        }

        private object _guest2Homepage;
        public object Guest2Homepage
        {
            get { return _guest2Homepage; }
            set
            {
                if (_guest2Homepage != value)
                {
                    _guest2Homepage = value;
                    OnPropertyChanged(nameof(Guest2Homepage));
                }
            }
        }

        private void Button_Click_MyAttendedTours(object param)
        {
            Guest2Homepage = new SecondGuestMyAttendedToursView(GuestId);

        }

        private bool CanExecute(object param) { return true; }
        */
    }
}
