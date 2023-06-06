using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controllers
{
    public class ComplexTourRequestController
    {
        private readonly IComplexTourRequestService _complexTourRequestService;
        public ComplexTourRequestController()
        {
            _complexTourRequestService = Injector.CreateInstance<IComplexTourRequestService>();
        }
        public void Create(ComplexTourRequest complexTourRequest)
        {
            _complexTourRequestService.Create(complexTourRequest);
        }
        public List<ComplexTourRequest> GetAll()
        {
            return _complexTourRequestService.GetAll();
        }
        public void Save()
        {
            _complexTourRequestService.Save();
        }
        public ComplexTourRequest GetById(int id)
        {
            return _complexTourRequestService.GetById(id);
        }
        public void DeleteRequestIfComplexRequestNotCreated(int complexTourRequestId)
        {
            _complexTourRequestService.DeleteRequestIfComplexRequestNotCreated(complexTourRequestId);
        }
    }
}