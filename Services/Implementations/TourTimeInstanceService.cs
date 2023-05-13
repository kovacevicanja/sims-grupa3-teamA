using BookingProject.DependencyInjection;
using BookingProject.Model;
using BookingProject.Repositories;
using BookingProject.Repositories.Implementations;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Implementations
{
    public class TourTimeInstanceService : ITourTimeInstanceService
    {
        private ITourTimeInstanceRepository _tourTimeInstanceRepository;

        public TourTimeInstanceService() { }
        public void Initialize()
        {
            _tourTimeInstanceRepository = Injector.CreateInstance<ITourTimeInstanceRepository>();
        }
        public void Create(TourTimeInstance instance)
        {
            _tourTimeInstanceRepository.Create(instance);
        }
        public List<TourTimeInstance> GetAll()
        {
            return _tourTimeInstanceRepository.GetAll();
        }
        public TourTimeInstance GetById(int id)
        {
            return _tourTimeInstanceRepository.GetById(id);
        }
        public void Save()
        {
            _tourTimeInstanceRepository.Save();
        }

        public TourTimeInstance GetLastTour()
        {
            return _tourTimeInstanceRepository.GetAll().Last();
        }

        public void BindLastTour()
        {
            _tourTimeInstanceRepository.BindLastTour();
        }
    }
}