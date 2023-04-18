using BookingProject.FileHandler;
using BookingProject.Model.Images;
using BookingProject.Model;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.Services.Interfaces;
using BookingProject.DependencyInjection;

namespace BookingProject.Controller
{
    public class TourTimeInstanceController
    {

        private readonly ITourTimeInstanceService _tourTimeInstanceService;
        public TourTimeInstanceController()
        {
            _tourTimeInstanceService = Injector.CreateInstance<ITourTimeInstanceService>();
        }



        public void Create(TourTimeInstance instance)
        {
            _tourTimeInstanceService.Create(instance);
        }

        public List<TourTimeInstance> GetAll()
        {
            return _tourTimeInstanceService.GetAll();
        }

        public TourTimeInstance GetByID(int id)
        {
            return _tourTimeInstanceService.GetByID(id);
        }



    }
}
