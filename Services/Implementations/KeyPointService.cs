using BookingProject.Model.Enums;
using BookingProject.Model;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.Repositories.Implementations;
using BookingProject.Serializer;
using BookingProject.DependencyInjection;
using BookingProject.Repositories.Intefaces;
using BookingProject.Repositories;

namespace BookingProject.Services.Implementations
{
    public class KeyPointService : IKeyPointService
    {
        public IKeyPointRepository _keyPointRepository { get; set; }
        public KeyPointService() { }
        public void Initialize()
        {
            _keyPointRepository = Injector.CreateInstance<IKeyPointRepository>();
        }
       
        public void CleanUnused()
        {
            _keyPointRepository.GetAll().RemoveAll(r => r.TourId == -1);
        }

        public void LinkToTour(int id)
        {
            foreach (KeyPoint keyPoint in _keyPointRepository.GetAll())
            {
                if (keyPoint.TourId == -1)
                {
                    keyPoint.TourId = id;
                }
            }
        }

        public KeyPoint GetCurrentKeyPoint()
        {
            foreach (KeyPoint keyPoint in _keyPointRepository.GetAll())
            {
                if (keyPoint.State == KeyPointState.CURRENT)
                {
                    return keyPoint;
                }
            }
            return GetPassedKeyPoint();
        }
        public List<KeyPoint> GetToursKeyPoints(int id)
        {
            List<KeyPoint> tourKeyPoints = new List<KeyPoint>();

            foreach (KeyPoint kp in _keyPointRepository.GetAll())
            {
                if (kp.TourId == id)
                {
                    tourKeyPoints.Add(kp);
                }
            }
            return tourKeyPoints;
        }
        public KeyPoint GetPassedKeyPoint()
        {
            foreach (KeyPoint keyPoint in _keyPointRepository.GetAll())
            {
                if (keyPoint.State == KeyPointState.PASSED)
                {
                    return keyPoint;
                }
            }
            return null;
        }

        public void Create(KeyPoint keyPoint)
        {
            _keyPointRepository.Create(keyPoint);
        }

        public List<KeyPoint> GetAll()
        {
            return _keyPointRepository.GetAll();
        }

        public KeyPoint GetByID(int id)
        {
            return _keyPointRepository.GetByID(id); 
        }

        public void Save()
        {
            _keyPointRepository.Save();
        }
    }
}