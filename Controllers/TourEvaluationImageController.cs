using BookingProject.Controllers;
using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services;
using BookingProject.Services.Interfaces;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controller
{
    public class TourEvaluationImageController
    {
        private readonly ITourEvaluationImageService _tourEvaluationImageService;
        public TourEvaluationImageController()
        {
            _tourEvaluationImageService = Injector.CreateInstance<ITourEvaluationImageService>();
        }
        public void Create(TourEvaluationImage tourEvaluationImage)
        {
            _tourEvaluationImageService.Create(tourEvaluationImage);
        }

        public List<TourEvaluationImage> GetAll()
        {
            return _tourEvaluationImageService.GetAll();
        }

        public TourEvaluationImage GetByID(int id)
        {
            return _tourEvaluationImageService.GetByID(id);
        }
    }
}