using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controllers
{
    public class TourRequestController
    {
        private readonly ITourRequestService _tourRequestService;
        public TourRequestController()
        {
            _tourRequestService = Injector.CreateInstance<ITourRequestService>();
        }
        public void Create(TourRequest tourRequest)
        {
            _tourRequestService.Create(tourRequest);
        }
        public List<TourRequest> GetAll()
        {
            return _tourRequestService.GetAll();
        }
        public TourRequest GetByID(int id)
        {
            return _tourRequestService.GetByID(id);
        }
        public void Save(List<TourRequest> _tourRequests)
        {
            _tourRequestService.Save(_tourRequests);
        }
        public void SaveTourRequest()
        {
            _tourRequestService.SaveTourRequest();
        }
        public List<TourRequest> GetGuestRequests (int guestId)
        {
            return _tourRequestService.GetGuestRequests(guestId);
        }
    }
}