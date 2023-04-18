using BookingProject.DependencyInjection;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.Services.Interfaces;
using BookingProject.View;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controller
{
    public class KeyPointController
    {
        private readonly IKeyPointService _tourKeyPointService;
        public KeyPointController()
        {
            _tourKeyPointService = Injector.CreateInstance<IKeyPointService>();
        }
        public void Create(KeyPoint keyPoint)
        {
            _tourKeyPointService.Create(keyPoint);
        }
        public List<KeyPoint> GetAll()
        {
            return _tourKeyPointService.GetAll();
        }
        public KeyPoint GetByID(int id)
        {
            return _tourKeyPointService.GetByID(id);
        }
        public void CleanUnused()
        {
            _tourKeyPointService.CleanUnused();
        }
        public void LinkToTour(int id)
        {
            _tourKeyPointService.LinkToTour(id);
        }
        public KeyPoint GetCurrentKeyPoint()
        {
            return _tourKeyPointService.GetCurrentKeyPoint();
        }
        public List<KeyPoint> GetToursKeyPoints(int id)
        {
            return _tourKeyPointService.GetToursKeyPoints(id);
        }
        public KeyPoint GetPassedKeyPoint()
        {
            return _tourKeyPointService.GetPassedKeyPoint();
        }
        public void Save()
        {
            _tourKeyPointService.Save();
        }
    }
}