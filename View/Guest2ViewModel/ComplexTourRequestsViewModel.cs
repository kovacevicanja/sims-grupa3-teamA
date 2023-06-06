using BookingProject.Commands;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingProject.View.Guest2ViewModel
{
    public class ComplexTourRequestsViewModel : INotifyPropertyChanged
    {
        public int GuestId { get; set; }
        public ComplexTourRequestController _complexTourRequestController { get; set; }
        public TourRequestController _tourRequestController { get; set; }   
        public ObservableCollection<ComplexTourRequest> ComplexTourRequests { get; set; }
        public RelayCommand CancelCommand { get; }
        public NavigationService NavigationService { get; set; }
        public string ComplexTourRequestName { get; set; }

        private List<TourRequest> _tourRequestsList;
        public List<TourRequest> TourRequestsList
        {
            get { return _tourRequestsList; }
            set
            {
                _tourRequestsList = value;
                OnPropertyChanged();
            }
        }
        public ComplexTourRequestsViewModel(int guestId, NavigationService navigationService)
        {
            GuestId = guestId;

            _complexTourRequestController = new ComplexTourRequestController();

            ComplexTourRequests = new ObservableCollection<ComplexTourRequest>(_complexTourRequestController.GetGuestComplexRequests(guestId));

            CancelCommand = new RelayCommand(Button_Cancel, CanExecute);

            NavigationService = navigationService;

            _tourRequestController = new TourRequestController();

            TourRequestsList = new List<TourRequest>();

            foreach (ComplexTourRequest complexRequest in ComplexTourRequests.ToList())
            {
                foreach (TourRequest tourRequest in _tourRequestController.GetGuestRequests(guestId, ""))
                {
                    if (tourRequest.ComplexTour.Id ==  complexRequest.Id)
                    {
                        TourRequestsList.Add(tourRequest);
                    }
                }
            }
        }

        private bool CanExecute(object param) { return true; }

        private void Button_Cancel(object param)
        {
            NavigationService.GoBack();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}