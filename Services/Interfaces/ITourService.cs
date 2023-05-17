using BookingProject.DependencyInjection;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingProject.Services.Interfaces
{
    public interface ITourService
    {
        void Create(Tour tour);
        List<Tour> GetAll();
        Tour GetById(int id);
        Tour GetLastTour();
        void FullBind();
        void BindLastTour();
        void Initialize();
        List<Tour> LoadAgain();
    }
}