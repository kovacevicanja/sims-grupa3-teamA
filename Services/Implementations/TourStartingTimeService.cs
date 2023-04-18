using BookingProject.DependencyInjection;
using BookingProject.Model;
using BookingProject.Repositories.Implementations;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using BookingProject.Services.Interfaces;
using System.Collections.Generic;

namespace BookingProject.Services.Implementations
{
    public class TourStartingTimeService : ITourStartingTimeService
    {
        public ITourStartingTimeRepository _tourStartingTimeRepository;

        public TourStartingTimeService() { }
        public void Initialize()
        {
            _tourStartingTimeRepository = Injector.CreateInstance<ITourStartingTimeRepository>();
        }
        public void LinkToTour(int id)
        {
            foreach (TourDateTime startingDate in _tourStartingTimeRepository.GetAll())
            {
                if (startingDate.TourId == -1)
                {
                    startingDate.TourId = id;
                }
            }
        }
        public void CleanUnused()
        {
            _tourStartingTimeRepository.GetAll().RemoveAll(d => d.TourId == -1);
        }
        public void Create(TourDateTime date)
        {
            _tourStartingTimeRepository.Create(date);
        }
        public List<TourDateTime> GetAll()
        {
            return _tourStartingTimeRepository.GetAll();
        }
        public TourDateTime GetByID(int id)
        {
            return _tourStartingTimeRepository.GetByID(id);
        }
        public void Save()
        {
            _tourStartingTimeRepository.Save();
        }
    }
}