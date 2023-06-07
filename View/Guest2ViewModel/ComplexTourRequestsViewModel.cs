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
        public ObservableCollection<TourRequest> TourRequests { get; set; }
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
            _tourRequestController = new TourRequestController();

            _tourRequestController = new TourRequestController();
            TourRequests = new ObservableCollection<TourRequest>(_tourRequestController.GetGuestRequests(guestId, ""));

            ComplexTourRequests = new ObservableCollection<ComplexTourRequest>(_complexTourRequestController.GetGuestComplexRequests(guestId));

            CancelCommand = new RelayCommand(Button_Cancel, CanExecute);

            NavigationService = navigationService;

            TourRequestsList = new List<TourRequest>();

            /*
            foreach (TourRequest tourRequest in _tourRequestController.GetGuestRequests(guestId, ""))
            {
                if (tourRequest.ComplexTourRequest.Id == -1)
                {
                    continue;
                }
                else
                {
                    foreach (ComplexTourRequest complexTourRequest in ComplexTourRequests.ToList())
                    {
                        if (tourRequest.ComplexTourRequest.Id == complexTourRequest.Id)
                        {
                            TourRequestsList.Add(tourRequest);
                        }
                    }
                }
            }
            */

            foreach (ComplexTourRequest complexRequest in ComplexTourRequests.ToList())
            {
                foreach (TourRequest tourRequest in _tourRequestController.GetGuestRequests(guestId, ""))
                {
                    if (tourRequest.ComplexTourRequest.Id == complexRequest.Id)
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