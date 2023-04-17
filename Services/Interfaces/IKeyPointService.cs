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
        void CleanUnused();
        void LinkToTour(int id);
        KeyPoint GetCurrentKeyPoint();
        List<KeyPoint> GetToursKeyPoints(int id);
        KeyPoint GetPassedKeyPoint();
    }
}