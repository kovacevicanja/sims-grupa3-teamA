using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Domain.Enums;
using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Razor.Parser.SyntaxTree;
using System.Windows.Controls;

namespace BookingProject.Repositories.Implementations
{
    public class ComplexTourRequestRepository : IComplexTourRequestRepository
    {
        private const string FilePath = "../../Resources/Data/complexTourRequests.csv";
        private Serializer<ComplexTourRequest> _serializer;
        public List<ComplexTourRequest> _complexTourRequests;
        public ITourRequestRepository _tourRequestRepository { get; set; }

        public ComplexTourRequestRepository()
        {
            _serializer = new Serializer<ComplexTourRequest>();
            _complexTourRequests = Load();
        }
        public void Initialize()
        {
            _tourRequestRepository = Injector.CreateInstance<ITourRequestRepository>();
            TourRequestsListBind();
        }
        public List<ComplexTourRequest> Load()
        {
            return _serializer.FromCSV(FilePath);
        }
        public void Save()
        {
            _serializer.ToCSV(FilePath, _complexTourRequests);
        }
        public int GenerateId()
        {
            int maxId = 0;
            foreach (ComplexTourRequest complexTourRequest in _complexTourRequests)
            {
                if (complexTourRequest.Id > maxId)
                {
                    maxId = complexTourRequest.Id;
                }
            }
            return maxId + 1;
        }

        public void Create(ComplexTourRequest complexTourRequest)
        {
            complexTourRequest.Id = GenerateId();
            _complexTourRequests.Add(complexTourRequest);
            Save();
        }

        public List<ComplexTourRequest> GetAll()
        {
            return _complexTourRequests.ToList();
        }

        public ComplexTourRequest GetById(int id)
        { 
            return _complexTourRequests.Find(complexTourRequest => complexTourRequest.Id == id);
        }

        private void TourRequestsListBind()
        {
            ITourRequestRepository tourRequestRepository = Injector.CreateInstance<ITourRequestRepository>();
            foreach (TourRequest tourRequest in tourRequestRepository.GetAll())
            {
                if (tourRequest.ComplexTourRequestId == -1)
                {
                    continue;
                }
                else
                {
                    ComplexTourRequest complexTourRequest = GetById(tourRequest.ComplexTourRequestId);
                    complexTourRequest.TourRequestsList.Add(tourRequest);
                }
            }
        }

        public List<ComplexTourRequest> GetGuestComplexRequests(int guestId)
        {
            List<ComplexTourRequest> guestComplexRequests = new List<ComplexTourRequest>();
            foreach (ComplexTourRequest complexTourRequest in GetAll())
            {
                if (complexTourRequest.Guest.Id == guestId)
                {
                    guestComplexRequests.Add(complexTourRequest);
                }
            }
            return guestComplexRequests;    
        }

        public void ChnageStatus(ComplexTourRequest complexTourRequest)
        {
            _complexTourRequests.Remove(complexTourRequest);
            ComplexTourRequest changedStatusComplexTourRequest = new ComplexTourRequest(complexTourRequest.Id,
                    complexTourRequest.TourRequestsList, TourRequestStatus.ACCEPTED, complexTourRequest.Guest);
            _complexTourRequests.Add(changedStatusComplexTourRequest);
            Save();
        }
        public void ChnageStatusToInvalid(ComplexTourRequest complexTourRequest)
        {
            _complexTourRequests.Remove(complexTourRequest);
            ComplexTourRequest invalidComplexTourRequest = new ComplexTourRequest(complexTourRequest.Id,
                complexTourRequest.TourRequestsList, TourRequestStatus.INVALID, complexTourRequest.Guest);
            invalidComplexTourRequest.TourRequestsList = new List<TourRequest>(_tourRequestRepository.SetTourRequestsToInvalid(complexTourRequest.TourRequestsList));
            _complexTourRequests.Add(invalidComplexTourRequest);
            Save();
        }
    }
}