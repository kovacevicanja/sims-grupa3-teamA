using BookingProject.Controller;
using BookingProject.Model.Images;
using BookingProject.Model;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using BookingProject.Repositories;
using BookingProject.Services.Interfaces;
using BookingProject.DependencyInjection;
using BookingProject.Repositories.Intefaces;
using BookingProject.Repositories.Implementations;
using BookingProject.View.CustomMessageBoxes;
using BookingProject.Domain;
using System.Windows.Navigation;

namespace BookingProject.Services
{
    public class TourService : ITourService
    {
        private ITourRepository _tourRepository;
        private ITourReservationRepository _tourReservationRepository;
        private ITourRequestService _tourRequestService;
        public CustomMessageBox CustomMessageBox { get; set; }

        public TourService()
        {
            CustomMessageBox = new CustomMessageBox();
        }
        public void Initialize()
        {
            _tourRepository = Injector.CreateInstance<ITourRepository>();
            _tourReservationRepository = Injector.CreateInstance<ITourReservationRepository>();
            _tourRequestService = Injector.CreateInstance<ITourRequestService>();
        }
        public void Create(Tour tour)
        {
            _tourRepository.Create(tour);
        }
        public List<Tour> GetAll()
        {
            return _tourRepository.GetAll();
        }

        public void FullBind()
        {
            _tourRepository.TourLocationBind();
            _tourRepository.BindTourImage();
            _tourRepository.TourKeyPointBind();
            _tourRepository.TourDateBind();
        }
        public Tour GetById(int id)
        {
            return _tourRepository.GetById(id);
        }
        public Tour GetLastTour()
        {
            return _tourRepository.GetAll().Last();
        }

        public void BindLastTour()
        {
            _tourRepository.BindLastTour();
        }

        public List<Tour> LoadAgain()
        {
            return _tourRepository.LoadAgain();
        }
    }
}