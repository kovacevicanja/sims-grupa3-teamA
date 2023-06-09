using BookingProject.DependencyInjection;
using BookingProject.Model.Images;
using BookingProject.Model;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.Domain;
using BookingProject.Domain.Enums;
using System.Windows.Media.Animation;
using System.Security.Policy;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BookingProject.Repositories.Implementations
{
    public class TourRequestRepository : ITourRequestRepository
    {
        private const string FilePath = "../../Resources/Data/tourRequests.csv";

        private Serializer<TourRequest> _serializer;

        public List<TourRequest> _tourRequests;

        public TourRequestRepository()
        {
            _serializer = new Serializer<TourRequest>();
            _tourRequests = Load();
        }
        public void Initialize()
        {
            TourRequestLocationBind();
        }
        private List<TourRequest> Load()
        {
            return _serializer.FromCSV(FilePath);
        }
        public void Save()
        {
            _serializer.ToCSV(FilePath, _tourRequests);
        }
        public void SaveTourRequest(List<TourRequest> tourRequests)
        {
            _serializer.ToCSV(FilePath, tourRequests);
        }
        private int GenerateId()
        {
            int maxId = 0;
            foreach (TourRequest tourRequest in _tourRequests)
            {
                if (tourRequest.Id > maxId)
                {
                    maxId = tourRequest.Id;
                }
            }
            return maxId + 1;
        }
        public TourRequest GetById(int id)
        {
            return _tourRequests.Find(tourRequest => tourRequest.Id == id);
        }
        public List<TourRequest> GetAll()
        {
            return _tourRequests;
        }
        public void Create(TourRequest tourRequest)
        {
            tourRequest.Id = GenerateId();
            _tourRequests.Add(tourRequest);
            SaveTourRequest(_tourRequests);
        }
        private void TourRequestLocationBind()
        {
            foreach (TourRequest tourRequest in _tourRequests)
            {
                Location location = Injector.CreateInstance<ITourLocationRepository>().GetById(tourRequest.Location.Id);
                tourRequest.Location = location;
            }
        }
        public void CheckRequestStatus()
        {
            List<TourRequest> requestsCopy = new List<TourRequest>(GetAll());
            foreach (TourRequest tourRequest in requestsCopy)
            {
                if (DateTime.Now >= tourRequest.EndDate.AddHours(-48) && tourRequest.Status == TourRequestStatus.PENDING)
                {
                    TourRequest newRequestStatus = new TourRequest(tourRequest.Id, -1, TourRequestStatus.INVALID, 
                        tourRequest.Location, tourRequest.Description, tourRequest.Language, 
                        tourRequest.GuestsNumber, tourRequest.StartDate, tourRequest.EndDate, tourRequest.Guest);
                    newRequestStatus.ComplexTourRequestId = tourRequest.ComplexTourRequestId;
                    _tourRequests.Remove(tourRequest);
                    _tourRequests.Add(newRequestStatus);
                }
            }
            SaveTourRequest(_tourRequests);
        }
        public List<TourRequest> GetGuestRequests(int guestId, string enteredYear = "")
        {
            CheckRequestStatus();

            List<TourRequest> guestRequests = new List<TourRequest>();

            foreach (TourRequest request in _tourRequests)
            {
                if (request.Guest.Id == guestId && string.IsNullOrEmpty(enteredYear) && request.ComplexTourRequestId == -1 ||
                         (request.StartDate.Year.ToString().Equals(enteredYear) && request.EndDate.Year.ToString().Equals(enteredYear)))
                { guestRequests.Add(request); }         
            }
            return guestRequests;
        }

        public void ChangeStatus(TourRequest request, int guestId)
        {
            if (request.Guest.Id == guestId)
            {
                _tourRequests.RemoveAll(r => r.Id == request.Id);
                TourRequest changedStatusRequest = new TourRequest(request.Id, -1, TourRequestStatus.ACCEPTED, request.Location,
                    request.Description, request.Language, request.GuestsNumber, request.StartDate, request.EndDate, request.Guest);
                changedStatusRequest.ComplexTourRequestId = request.ComplexTourRequestId;
                _tourRequests.Add(changedStatusRequest);
                Save();
            }
        }
        public List<TourRequest> SetTourRequestsToInvalid (List<TourRequest> tourRequests)
        {
            List<TourRequest> newTourRequests = new List<TourRequest>();
            foreach (TourRequest request in tourRequests)
            {
                _tourRequests.Remove(request);
                TourRequest newRequest = new TourRequest(request.Id, -1, TourRequestStatus.INVALID, request.Location,
                    request.Description, request.Language, request.GuestsNumber, request.StartDate, request.EndDate, request.Guest);
                newRequest.ComplexTourRequestId = request.ComplexTourRequestId;
                newRequest.SetDate = request.SetDate;
                _tourRequests.Add(newRequest);
                newTourRequests.Add(newRequest);
                Save();
            }

            return newTourRequests;
        }
    }
}