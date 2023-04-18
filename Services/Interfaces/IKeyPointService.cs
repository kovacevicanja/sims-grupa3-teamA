using BookingProject.Model.Enums;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface IKeyPointService
    {
        void Create(KeyPoint keyPoint);
        List<KeyPoint> GetAll();
        KeyPoint GetByID(int id);
        void CleanUnused();
        void LinkToTour(int id);
        KeyPoint GetCurrentKeyPoint();
        List<KeyPoint> GetToursKeyPoints(int id);
        KeyPoint GetPassedKeyPoint();
        void Initialize();
        void Save(List<KeyPoint> keyPoints);
    }
}