using BookingProject.Controller;
using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Implementations
{
    public class TourTimeInstanceRepository : ITourTimeInstanceRepository
    {
        private const string FilePath = "../../Resources/Data/tourTimeInstances.csv";
        private Serializer<TourTimeInstance> _serializer;
        public List<TourTimeInstance> _instances;

        public TourTimeInstanceRepository() 
        {
            _serializer = new Serializer<TourTimeInstance>();
            _instances = Load();
        }
        public void Initialize()
        {
            InstanceDateBind();
            InstanceTourBind();
        }
        public List<TourTimeInstance> Load()
        {
            return _serializer.FromCSV(FilePath);
        }
        public void Save()
        {
            _serializer.ToCSV(FilePath, _instances);
        }
        private int GenerateId()
        {
            int maxId = 0;
            foreach (TourTimeInstance instances in _instances)
            {
                if (instances.Id > maxId)
                {
                    maxId = instances.Id;
                }
            }
            return maxId + 1;
        }
        public void Create(TourTimeInstance instances)
        {
            instances.Id = GenerateId();
            _instances.Add(instances);
            Save();
        }
        public List<TourTimeInstance> GetAll()
        {
            return _instances;
        }
        public TourTimeInstance GetById(int id)
        {
            return _instances.Find(presence => presence.Id == id);
        }
        public void InstanceTourBind()
        {
            foreach (TourTimeInstance tourTimeInstance in _instances)
            {
                Tour tour = Injector.CreateInstance<ITourRepository>().GetById(tourTimeInstance.TourId);
                tourTimeInstance.Tour = tour;
            }
        }

        public void BindLastTour()
        {

            if (_instances.Count == 0)
            {
                return;
            }
            else
            {
                TourTimeInstance tourInstance = _instances.Last();
                List<Tour> tours = Injector.CreateInstance<ITourRepository>().GetAll();
                foreach (Tour tour in tours)
                {
                    if (tourInstance.TourId == tour.Id)
                    {
                        tourInstance.Tour=tour;
                    }
                }
                List<TourDateTime> dates = Injector.CreateInstance<ITourStartingTimeRepository>().GetAll();
                foreach (TourDateTime date in dates)
                {
                    if (tourInstance.DateId == date.Id)
                    {
                        tourInstance.TourTime = date;
                    }
                }


            }
        }

        public void InstanceDateBind()
        {
            foreach (TourTimeInstance tourTimeInstance in _instances)
            {
                TourDateTime tourTime = Injector.CreateInstance<ITourStartingTimeRepository>().GetById(tourTimeInstance.DateId);
                tourTimeInstance.TourTime = tourTime;
            }
        }
    }
}
