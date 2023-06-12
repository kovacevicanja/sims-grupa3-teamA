using BookingProject.Commands;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.Model;
using Org.BouncyCastle.Asn1.X509;
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
        public string DisplayText { get; set; }
        public ComplexTourRequestsViewModel(int guestId, NavigationService navigationService)
        {
            GuestId = guestId;

            _complexTourRequestController = new ComplexTourRequestController();
            _tourRequestController = new TourRequestController();

            _complexTourRequestController.ChnageStatusComplexTourRequest(guestId);

            DateTime comparisonDate = new DateTime(2001, 1, 1, 0, 0, 0);

            foreach (ComplexTourRequest ctr in _complexTourRequestController.GetGuestComplexRequests(guestId))
            {
                foreach (TourRequest tr in ctr.TourRequestsList)
                {
                    if (tr.SetDate == comparisonDate)
                    {
                        if (tr.Status == Domain.Enums.TourRequestStatus.PENDING)
                        {
                            tr.DisplaySetDate = "Guide not accepted the tour request yet.";
                        }
                        else if (tr.Status == Domain.Enums.TourRequestStatus.INVALID)
                        {
                            tr.DisplaySetDate = "The tour request is not accepted.";
                        }
                    }
                    else
                    {
                        tr.DisplaySetDate = "The guide set the date: " + tr.SetDate.ToString();
                    }
                }
            }

            ComplexTourRequests = new ObservableCollection<ComplexTourRequest>(_complexTourRequestController.GetGuestComplexRequests(guestId));

            CancelCommand = new RelayCommand(Button_Cancel, CanExecute);

            NavigationService = navigationService;
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