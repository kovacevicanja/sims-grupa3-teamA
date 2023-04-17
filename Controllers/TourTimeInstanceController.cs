using BookingProject.FileHandler;
using BookingProject.Model.Images;
using BookingProject.Model;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controller
{
    public class TourTimeInstanceController
    {
        private readonly List<IObserver> observers;

        private readonly TourTimeInstanceHandler _tourInstanceHandler;

        private List<TourTimeInstance> _tourInstances;

        private TourController _tourController;

        private TourStartingTimeController _startingDateController;

        public TourTimeInstanceController()
        {
            _tourInstanceHandler = new TourTimeInstanceHandler();
            _tourInstances = new List<TourTimeInstance>();
            _tourController= new TourController();
            _startingDateController = new TourStartingTimeController();
            observers = new List<IObserver>();
            Load();
        }

        public void Load()
        {
            _tourInstances = _tourInstanceHandler.Load();
            InstanceTourBind();
            InstanceDateBind();
        }

        private int GenerateId()
        {
            int maxId = 0;
            foreach (TourTimeInstance instance in _tourInstances)
            {
                if (instance.Id > maxId)
                {
                    maxId = instance.Id;
                }
            }
            return maxId + 1;
        }

        public void Create(TourTimeInstance instance)
        {
            instance.Id = GenerateId();
            _tourInstances.Add(instance);
            NotifyObservers();
        }

        public void Save()
        {
            _tourInstanceHandler.Save(_tourInstances);
            NotifyObservers();
        }


        public List<TourTimeInstance> GetAll()
        {
            return _tourInstances;
        }

        public TourTimeInstance GetByID(int id)
        {
            return _tourInstances.Find(instance => instance.Id == id);
        }

        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update();
            }
        }

        public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void InstanceTourBind()
        {
            //_tourController.Load();
            foreach (TourTimeInstance tourTimeInstance in _tourInstances)
            {
                Tour tour = _tourController.GetByID(tourTimeInstance.TourId);
                tourTimeInstance.Tour = tour;
            }
        }

        public void InstanceDateBind()
        {
            _startingDateController.Load();
            foreach (TourTimeInstance tourTimeInstance in _tourInstances)
            {
                TourDateTime tourTime = _startingDateController.GetByID(tourTimeInstance.DateId);
                tourTimeInstance.TourTime = tourTime;
            }
        }

    }
}
