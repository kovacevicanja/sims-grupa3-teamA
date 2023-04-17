using BookingProject.Model.Enums;
using BookingProject.Model;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.Repositories.Implementations;

namespace BookingProject.Services.Implementations
{
    public class KeyPointService : IKeyPointService
    {
        public List<KeyPoint> _keyPoints {  get; set; }
        public KeyPointRepository keyPointRepository { get; set; }
        public KeyPointService() 
        {
            _keyPoints = new List<KeyPoint>();
            keyPointRepository = new KeyPointRepository();
            Load();

        }
        public void Load()
        {
            _keyPoints = keyPointRepository.GetAll();
        }
        public void CleanUnused()
        {
            _keyPoints.RemoveAll(r => r.TourId == -1);
        }

        public void LinkToTour(int id)
        {

            foreach (KeyPoint keyPoint in _keyPoints)
            {
                if (keyPoint.TourId == -1)
                {
                    keyPoint.TourId = id;
                }

            }
        }

        public KeyPoint GetCurrentKeyPoint()
        {
            foreach (KeyPoint keyPoint in _keyPoints)
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

            foreach (KeyPoint kp in _keyPoints)
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
            foreach (KeyPoint keyPoint in _keyPoints)
            {
                if (keyPoint.State == KeyPointState.PASSED)
                {
                    return keyPoint;
                }
            }
            return null;
        }
    }
}