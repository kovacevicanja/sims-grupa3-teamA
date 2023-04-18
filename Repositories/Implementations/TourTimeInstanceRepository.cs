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
            return _instances.ToList();
        }
        public TourTimeInstance GetByID(int id)
        {
            return _instances.Find(presence => presence.Id == id);
        }

        public void InstanceTourBind()
        {
            //_tourController.Load();
            foreach (TourTimeInstance tourTimeInstance in _instances)
            {
                Tour tour = Injector.CreateInstance<ITourRepository>().GetByID(tourTimeInstance.TourId);
                tourTimeInstance.Tour = tour;
            }
        }

        public void InstanceDateBind()
        {
            foreach (TourTimeInstance tourTimeInstance in _instances)
            {
                TourDateTime tourTime = Injector.CreateInstance<ITourStartingTimeRepository>().GetByID(tourTimeInstance.DateId);
                tourTimeInstance.TourTime = tourTime;
            }
        }
    }
}
