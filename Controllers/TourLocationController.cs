using BookingProject.DependencyInjection;
using BookingProject.Model;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using BookingProject.Services.Interfaces;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Annotations;

namespace BookingProject.Controller
{
    public class TourLocationController
    {
        private ITourLocationService _tourLocationService { get; set; }
        public TourLocationController()
        {
            _tourLocationService = Injector.CreateInstance<ITourLocationService>();
        }
        /*
        public void Initialize()
        {
            _tourLocationRepository = Injector.CreateInstance<ITourLocationRepository>();
        }
        */
        public void Create(Location location)
        {
            _tourLocationService.Create(location);
        }

        public List<Location> GetAll()
        {
            return _tourLocationService.GetAll();
        }

        public Location GetByID(int id)
        {
            return _tourLocationService.GetByID(id);
        }
        public void Save()
        {
            _tourLocationService.Save();
        }
    }
}